using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ZeroWindApi.Models;
using ZeroWindApi.Models.DBModels;
using Dapper;
using ZeroWindApi.Models.RequestModels;

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
        public async Task<Result<IEnumerable<OptionModel<int>>>> Get()
        {
            //实例化返回模型
            Result<IEnumerable<OptionModel<int>>> result = new Result<IEnumerable<OptionModel<int>>>();
            try
            {
                //查询分区列表
                result.Data = await _db.QueryAsync<OptionModel<int>>("select id value,name label from types");
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
