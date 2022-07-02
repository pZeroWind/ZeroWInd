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
    public class TypeController : ControllerBase
    {
        private readonly MySqlConnection _db;

        public TypeController(DapperContext dapper)
        {
            _db = dapper.GetConnection();
        }

        /// <summary>
        /// 获取分区
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result<List<Types>>> Get()
        {
            //实例化返回模型
            Result<List<Types>> result = new Result<List<Types>>();
            try
            {
                //查询分区列表
                result.Data = (await _db.QueryAsync<Types>("select * from types")).ToList();
            }
            catch (Exception ex)
            {
                //如果报错返回错误信息
                result.Code = 500;
                result.Msg = ex.Message;
            }
            return result;
        }
    }
}
