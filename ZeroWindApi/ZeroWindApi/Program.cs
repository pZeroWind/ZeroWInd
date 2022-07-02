using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using ZeroWindApi.Models;

var builder = WebApplication.CreateBuilder(args);
//ע��Redis
//builder.Services.AddSingleton();
//ע��Dapper������
builder.Services.AddScoped<DapperContext>();
//���ÿ���
builder.Services.AddCors(op =>
{
    op.AddDefaultPolicy(op2 =>
    {
        op2.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod().Build();
    });
});
//����ģ�����ط���
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
