using RandomUserImporter.Models;

namespace RandomUserImporter.Dto;

public class UserControllerResponse
{
    public bool Sucess { get; set; }
    public List<User> Users { get; set; }
}
