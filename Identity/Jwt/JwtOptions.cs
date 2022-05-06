
namespace Identity.Jwt
{
    public class JwtOptions
    {
        public const string Jwt = nameof(Jwt);

        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
        public string Secret { get; set; }
    }
}
