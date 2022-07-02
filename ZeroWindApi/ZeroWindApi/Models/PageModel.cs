namespace ZeroWindApi.Models
{
    public class PageModel<T>
    {
        public int Page { get; set; }

        public int Count { get; set; }

        public int Size { get; set; }

        public IEnumerable<T>? Data { get; set; }
    }
}
