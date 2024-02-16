namespace main.src.Dtos
{
    public class SendErrorDto
    {
        public bool Success { get { return false; } }
        public int Statuscode { get; set; }
        public string Errcode { get; set; }
        public string Error {  get; set; }
    }
}
