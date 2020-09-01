using System.ComponentModel.DataAnnotations;

namespace FlyRemotely.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Wprowadź adres email")]
        [EmailAddress(ErrorMessage = "Błędny format adresu email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Wprowadź hasło")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło:")]
        public string Password { get; set; }

        [Display(Name = "Zapamiętaj dane logowania")]
        public bool Remember { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Wprowadź adres email")]
        [EmailAddress(ErrorMessage = "Błędny format adresu email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Wprowadź hasło")]
        [StringLength(30, ErrorMessage = "Hasło musi mieć od 2 do 30 znaków", MinimumLength = 2)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło:")]
        public string Password { get; set; }

        [Display(Name = "Potwierdź hasło:")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Hasło i potwierdzenie hasła muszą być takie same")]
        public string ConfirmPassword { get; set; }
    }
}