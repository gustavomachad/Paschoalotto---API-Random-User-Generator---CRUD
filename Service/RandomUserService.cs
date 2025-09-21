using RandomUserImporter.Dto;
using RandomUserImporter.Mappers;
using RandomUserImporter.Models;
using RandomUserImporter.Repositories;
using System.Text.Json;

namespace RandomUserImporter.Service;

public class RandomUserService : IRandomUserService
{
    private readonly IHttpClientFactory _http;
    private readonly IUserRepository userRepository;

    public RandomUserService(IHttpClientFactory http, IUserRepository userRepository)
    {
        _http = http;
        this.userRepository = userRepository;
    }

    public async Task<UserControllerResponse> GetRandomUserAsync(int quantity)
    {
        var userResponse = new UserControllerResponse();

        var client = _http.CreateClient("randomuser");

        var resp = await client.GetAsync($"?results={quantity}");
        resp.EnsureSuccessStatusCode();

        var jsonString = await resp.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var root = JsonSerializer.Deserialize<RandomUserResponse.Root>(jsonString, options)!;

        List<RandomUserResponse.Result> users = root.results;

        var mappedUsers = UserMapper.ToUsers(users!);

        var result = await userRepository.AddUsersAsync(mappedUsers);

        if (result)
            return new UserControllerResponse { Sucess = true, Users = mappedUsers };
        else
            return new UserControllerResponse { Sucess = false, Users = mappedUsers };

    }
}
