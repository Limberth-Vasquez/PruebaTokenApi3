namespace PruebaTokenApi3.Models
{
	public class LoginRequest
	{
        public string User { get; set; }
        public string Pass { get; set; }
        public string? Rol { get; set; }
    }
}
