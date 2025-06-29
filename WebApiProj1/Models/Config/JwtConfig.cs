namespace WebApiProj1.Models.Config
{
    public class JwtConfig
    {
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
        public string SecretKey { get; set; }
    }
}
