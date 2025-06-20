namespace WebApiProj1.Models
{
    public class GenericRes<T> where T : class
    {
        public T Data { get; set; }
        public string Message { get; set; }
    }
}
