namespace main.src.Dtos
{
    public class SendSuccessDto<T>
    {
        public bool Success { get { return true; } }
        public string Message {get; set;}
        public int Statuscode {  get; set;}
        public List<Data<T>> data { get; set;}
}
}
