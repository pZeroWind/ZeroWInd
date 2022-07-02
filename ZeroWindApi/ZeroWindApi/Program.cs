using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using ZeroWindApi.Models;

var builder = WebApplication.CreateBuilder(args);
//注入Redis
//builder.Services.AddSingleton();
//注入Dapper上下文
builder.Services.AddScoped<DapperContext>();
//配置跨域
builder.Services.AddCors(op =>
{
    op.AddDefaultPolicy(op2 =>
    {
        op2.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod().Build();
    });
});
//配置模型拦截反馈
builder.Services.Configure<ApiBehaviorOptions>(op =>
{
    
    op.SuppressModelStateInvalidFilter = true;
    string? msg = null;
    op.InvalidModelStateResponseFactory = (context) =>
    {
        if(context.ModelState.Keys.Any())
        {
            foreach (var key in context.ModelState.Keys)
            {
                var errors = context.ModelState[key]!.Errors;
                if (errors.Any())
                {
                    msg = errors[0].ToString();
                }
            }
        }
        return new JsonResult(new Result<object>()
        {
            Code = 400,
            Msg = msg,
        });
    };
});
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
