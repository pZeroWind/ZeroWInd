namespace ZeroWindApi.Models
{
    public class Result<T>
    {
        public int Code { get; set; } = 200;

        public string? Msg { get; set; } = "操作成功";

        public T? Data { get; set; }
    }
}
