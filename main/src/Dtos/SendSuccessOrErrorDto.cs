namespace main.src.Dtos
{
    public class SendSuccessOrErrorDto<T>
    {
        public bool Success { get; set; }
        public string Message {get; set;} = string.Empty;
        public List<T> data { get; set;}
}
}
