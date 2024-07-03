namespace Sevriukoff.Gwalt.WebApi.ViewModels;

public class UserRegisterViewModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Gender { get; set; }
    public int Age { get; set; }
    public bool IsDefaultImages { get; set; }
}