using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ZeroWindApi.Models;
using ZeroWindApi.Models.RequestModels;
using Dapper;

namespace ZeroWindApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly MySqlConnection _db;

        public TagController(DapperContext dapper)
        {
            _db = dapper.GetConnection();
        }

        /// <summary>
        /// 获取标签列表
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result<IEnumerable<OptionModel<string>>>> Get(string? val = "")
        {
            try
            {
                //获取标签列表
                IEnumerable<OptionModel<string>> result = await _db.QueryAsync<OptionModel<string>>("select name Value,name Label from tags where name like CONCAT('%',@Val,'%') group by name order by count(name) desc limit 0,5", new
                {
                    Val = val
                });
                return new Result<IEnumerable<OptionModel<string>>> { Data = result };
            }
            catch (Exception ex)
            {
                //捕获异常并返回错误信息
                return new Result<IEnumerable<OptionModel<string>>> { Code = 500, Msg = ex.Message };
            }
            
        }
    }
}
