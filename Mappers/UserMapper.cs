using RandomUserImporter.Dto;
using RandomUserImporter.Models;
using static RandomUserImporter.Dto.RandomUserResponse;

namespace RandomUserImporter.Mappers;

public static class UserMapper
{
    public static User ToUser(RandomUserResponse.Result dto)
    {
        return new User
        {
            Gender = dto.gender,
            Title = dto.name?.title ?? string.Empty,
            FirstName = dto.name?.first ?? string.Empty,
            LastName = dto.name?.last ?? string.Empty,

            StreetNumber = dto.location?.street?.number,
            StreetName = dto.location?.street?.name ?? string.Empty,
            City = dto.location?.city ?? string.Empty,
            State = dto.location?.state ?? string.Empty,
            Country = dto.location?.country ?? string.Empty,
            Postcode = dto.location?.postcode?.ToString() ?? string.Empty,

            Email = dto.email ?? string.Empty,
            Username = dto.login?.username ?? string.Empty,

            Dob = dto.dob?.date,
            Age = dto.dob?.age,

            Phone = dto.phone ?? string.Empty,
            Cell = dto.cell ?? string.Empty,

            PictureLarge = dto.picture?.large ?? string.Empty,
            Nat = dto.nat ?? string.Empty
        };
    }

    public static List<User> ToUsers(List<RandomUserResponse.Result> results)
    {
        return results.Select(ToUser).ToList();
    }
}
