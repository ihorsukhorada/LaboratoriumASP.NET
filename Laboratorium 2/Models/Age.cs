namespace Laboratorium_2.Models;

public class Age
{
    public string? Name { get; set; }
    public DateTime? BirthDate { get; set; }

    public int Value { get; set; }

    
    public bool IsValid()
    {
        if (Name is null) return false;
        
        return !(BirthDate > DateTime.Now) && BirthDate is not null;
    }
    
    public int Birth()
    {
        var today = DateTime.Today;
        
        var age = today.Year - BirthDate.Value.Year;

        if (BirthDate.Value.Date > today.AddYears(-age)) age--;

        Value = age;

        return age;
    }
 
}