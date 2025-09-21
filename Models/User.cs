using System.ComponentModel.DataAnnotations;

namespace RandomUserImporter.Models;

public class User
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Gender { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public int? StreetNumber { get; set; }
    public string StreetName { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Postcode { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;

    public DateTime? Dob { get; set; }
    public int? Age { get; set; }

    public string Phone { get; set; } = string.Empty;
    public string Cell { get; set; } = string.Empty;

    public string PictureLarge { get; set; } = string.Empty;
    public string Nat { get; set; } = string.Empty;
}
