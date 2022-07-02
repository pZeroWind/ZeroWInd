using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ZeroWindApi.Models;
using Dapper;

namespace ZeroWindApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly MySqlConnection _db;

        public FileController(DapperContext dapper)
        {
            _db = dapper.GetConnection();
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        [HttpPost("Upload")]
        public async Task<Result<IEnumerable<string>>> Upload()
        {
            try
            {
                List<string> files = new List<string>();
                //获取文件列表
                HttpContext.Request.Form.Files.ToList().ForEach(f =>
                {
                    //判断文件夹路径是否存在
                    if (!Directory.Exists("./Upload"))
                    {
                        Directory.CreateDirectory("./Upload");
                    }
                    //获取文件后缀名
                    string ext = Path.GetExtension(f.FileName);
                    //拼接文件名
                    string fileName = "/" + Guid.NewGuid().ToString() + ext;
                    //保存文件
                    using (FileStream fs = new FileStream("./Upload" + fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        f.CopyTo(fs);
                    }
                    files.Add(fileName);
                });
                return await Task.FromResult(new Result<IEnumerable<string>>()
                {
                    Data = files
                });
            }
            catch (Exception ex)
            {
                //返回错误信息
                return new Result<IEnumerable<string>>()
                {
                    Code = 500,
                    Msg = ex.Message,
                };
            }
        }

        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        [HttpGet("{FileName}")]
        public async Task<IActionResult> Download(string FileName)
        {
            FileStream file = new FileStream("./Upload/" + FileName, FileMode.Open, FileAccess.Read);
            return await Task.FromResult(File(file, "application/octet-stream"));
        }
    }
}
