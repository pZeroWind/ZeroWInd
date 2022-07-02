namespace ZeroWindApi.Models.RequestModels
{
    public class OptionModel<T>
    {
        public T? Value { get; set; }

        public string? Label { get; set; }
    }
}
