namespace KoiFarmShop.Business.Dto
{
    public class RegisterDto
    {
        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!; // Added ConfirmPassword field

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Phone { get; set; }
        public int Role { get; set; }
    }
}
