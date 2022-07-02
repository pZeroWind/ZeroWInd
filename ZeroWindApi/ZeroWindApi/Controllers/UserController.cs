using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ZeroWindApi.Models;
using ZeroWindApi.Models.DBModels;
using Dapper;

namespace ZeroWindApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MySqlConnection _db;

        public UserController(DapperContext dapper)
        {
            _db = dapper.GetConnection();
        }

        /// <summary>
        /// 获取个人信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get")]
        public async Task<Result<Users>> Get()
        {
            try
            {
                Users? users = await _db.QueryFirstOrDefaultAsync<Users>("select * from users");
                if (users == null)
                {
                    return new Result<Users>()
                    {
                        Code = 404,
                        Msg = "未找到对应资源"
                    };
                }
                return new Result<Users>() { Data = users };
            }
            catch (Exception ex)
            {
                return new Result<Users>()
                {
                    Code = 500,
                    Msg = ex.Message,
                };
            }
        }
    }
}
