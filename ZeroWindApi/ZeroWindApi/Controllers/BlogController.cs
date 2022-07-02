using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Dapper;
using ZeroWindApi.Models.DBModels;
using ZeroWindApi.Models;
using ZeroWindApi.Models.RequestModels;
using System.Text;

namespace ZeroWindApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private MySqlConnection _db;

        public BlogController(DapperContext dapper)
        {
            _db = dapper.GetConnection();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="srcModel"></param>
        /// <returns></returns>
        [HttpPost("Get")]
        public async Task<Result<PageModel<BlogsModel>>> GetList(SearchModel srcModel)
        {
            try
            {
                //sql语句拼接
                string sql = "blogs b join tags t on b.id = t.blogId join types ty on ty.id = b.typeId where b.title like CONCAT('%',@search,'%')";
                if (srcModel.Tags.Count > 0)
                {
                    //拼接Tag查询
                    StringBuilder builder = new StringBuilder();
                    foreach (var item in srcModel.Tags)
                    {
                        builder.Append($"t.name = '{item}'");
                    }
                    sql += " and " + builder.ToString();
                }
                //拼接类型查询
                if (srcModel.Type != 0)
                {
                    sql += " and " + "b.typeId = " + srcModel.Type;
                }
                //判断排序类型
                string? order = null;
                switch (srcModel.Order)
                {
                    case 1:
                        order = "b.updateTime";
                        break;
                    case 2:
                        order = "b.createTime desc";
                        break;
                    case 3:
                        order = "b.createTime";
                        break;
                    default:
                        order = "b.updateTime desc";
                        break;
                }
                //分组过滤+排序
                sql += " group by b.id order by " + order;
                //保存部分参数
                var p = new
                {
                    search = "%" + srcModel.search + "%",
                    order = order,
                    page = srcModel.Page-1,
                    size = srcModel.Size
                };
                //获取列表
                IEnumerable<BlogsModel> list = await _db.QueryAsync<Blogs,Types,BlogsModel>($"select b.*,(select GROUP_CONCAT(name) from tags where blogId = b.id GROUP by blogId) tags,ty.* from {sql} limit @page,@size", (blog,types) =>
                {
                    BlogsModel model = new BlogsModel(blog);
                    model.Type = types.Name;
                    return model;
                },param:p);
                //获取总数
                int count = await _db.QueryFirstAsync<int>($"select count(*) from (select b.* from {sql}) a", p);
                return new Result<PageModel<BlogsModel>>()
                {
                    Data = new PageModel<BlogsModel>()
                    {
                        Data = list,
                        Page = srcModel.Page,
                        Count = count,
                        Size = srcModel.Size,
                        
                    }
                };
            }
            catch (Exception ex)
            {
                return new Result<PageModel<BlogsModel>>()
                {
                    Code = 500,
                    Msg = ex.Message,
                };
            }
        }

        /// <summary>
        /// 获取单一博客
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        public async Task<Result<BlogsModel>> GetBlog(string id)
        {
            try
            {
                //按ID查询对应博客信息
                var blog = (await _db.QueryAsync<Blogs,Types,BlogsModel>("select b.*,(select GROUP_CONCAT(name) from tags where blogId = b.id GROUP by blogId) tags,ty.* from blogs b join types ty on b.typeId = ty.id where b.id = @Id", (b, t) =>
                {
                    BlogsModel model = new BlogsModel(b);
                    model.Type = t.Name;
                    return model;
                },new
                {
                    Id = id,
                })).FirstOrDefault();
                //若内容为空
                if (blog == null)
                {
                    //返回404错误
                    return new Result<BlogsModel>()
                    {
                        Code = 404,
                        Msg = "未找到对应资源",
                    };
                }
                //返回查到的对应内容
                return new Result<BlogsModel>()
                {
                    Data = blog
                };
            }
            catch (Exception ex)
            {
                return new Result<BlogsModel>()
                {
                    Code = 500,
                    Msg = ex.Message,
                };
            }
            
        }

        /// <summary>
        /// 添加博客
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result<BlogsModel>> AddBlog(BlogsModel model)
        {
            await _db.OpenAsync();
            //定义返回模型
            Result<BlogsModel> result = new Result<BlogsModel>()
            {
                Data = model
            };
            //开启事务
            MySqlTransaction transaction = await _db.BeginTransactionAsync();
            try
            {
                //获取时间戳
                long time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                //添加ID与发布时间
                model.Id = Guid.NewGuid().ToString("N");
                model.CreateTime = time;
                model.UpdateTime = time;
                //添加博客
                int len = await _db.ExecuteAsync("insert into blogs values(@Id,@Title,@Context,@UpdateTime,@CreateTime,@TypeId)", model);
                if (len > 0)
                {
                    //遍历标签并添加
                    foreach (var item in model.Tags)
                    {
                        int len2 = await _db.ExecuteAsync("insert into tags values(@Id,@Name,@BlogId)", new
                        {
                            Id = Guid.NewGuid().ToString("N"),
                            Name = item,
                            BlogId = model.Id
                        });
                        //如果返回行数小于1，则添加失败抛出异常
                        if (len2 < 1)
                        {
                            throw new Exception("添加标签时失败，请重试");
                        }
                    }
                    await transaction.CommitAsync();
                }
                else
                {
                    //如果返回行数为0，则添加失败抛出异常
                    throw new Exception("添加博客时失败，请重试");
                }
            }
            catch(Exception ex)
            {
                await transaction.RollbackAsync();
                //如果出现错误返回错误信息
                result.Code = 500;
                result.Msg = ex.Message;
            }
            await _db.CloseAsync();
            return result;
        }

        /// <summary>
        /// 更新博客内容
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<Result<BlogsModel>> UpdateBlog(BlogsModel model)
        {
            await _db.OpenAsync();
            //定义返回模型
            Result<BlogsModel> result = new Result<BlogsModel>()
            {
                Data = model
            };
            //开启事务
            MySqlTransaction transaction = await _db.BeginTransactionAsync();
            try
            {
                //获取时间戳
                long time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                //修改更新时间
                model.UpdateTime = time;
                //添加博客
                int len = await _db.ExecuteAsync("update blogs set title = @Title,context = @Context,updateTime = @UpdateTime,typeId = @TypeId where id = @Id", model);
                if (len > 0)
                {
                    //清除所有关于该博客的标签
                    int len2 = await _db.ExecuteAsync("delete from tags where blogId = @Id", model);
                    //如果返回行数小于1，则添加失败抛出异常
                    if (len2 < 1)
                    {
                        throw new Exception("添加标签时失败，请重试");
                    }
                    //遍历标签并添加
                    foreach (var item in model.Tags)
                    {
                        int len3 = await _db.ExecuteAsync("insert into tags values(@Id,@Name,@BlogId)", new
                        {
                            Id = Guid.NewGuid().ToString("N"),
                            Name = item,
                            BlogId = model.Id
                        });
                        //如果返回行数小于1，则添加失败抛出异常
                        if (len3 < 1)
                        {
                            throw new Exception("添加标签时失败，请重试");
                        }
                    }
                    await transaction.CommitAsync();
                }
                else
                {
                    //如果返回行数为0，则添加失败抛出异常
                    throw new Exception("更新博客时失败，请重试");
                }
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                //如果出现错误返回错误信息
                result.Code = 500;
                result.Msg = ex.Message;
            }
            await _db.CloseAsync();
            return result;
        }
    }
}
