namespace WebApiProj1.Models
{
    public class GenericRes<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
    }
}
