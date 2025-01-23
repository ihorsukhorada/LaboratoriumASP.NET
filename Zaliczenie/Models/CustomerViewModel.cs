namespace WebApp.Models;

public class CustomerViewModel
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public int OrderCount { get; set; }
    public string? CountryName { get; set; }
}