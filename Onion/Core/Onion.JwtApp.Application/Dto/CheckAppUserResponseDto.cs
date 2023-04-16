namespace Onion.JwtApp.Application.Dto
{
    public class CheckAppUserResponseDto
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public bool IsExist { get; set; }
    }
}
