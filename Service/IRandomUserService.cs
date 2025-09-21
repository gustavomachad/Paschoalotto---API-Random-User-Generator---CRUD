using RandomUserImporter.Dto;
using RandomUserImporter.Models;

namespace RandomUserImporter.Service
{
    public interface IRandomUserService
    {
        Task<UserControllerResponse> GetRandomUserAsync(int quantity);
    }
}
