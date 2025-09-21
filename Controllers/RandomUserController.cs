using Microsoft.AspNetCore.Mvc;
using RandomUserImporter.Models;
using RandomUserImporter.Repositories;
using RandomUserImporter.Service;

namespace RandomUserImporter.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RandomUserController : ControllerBase
{
    private readonly IRandomUserService _randomUserService;
    private readonly IUserRepository _userRepository;

    public RandomUserController(IRandomUserService randomUserService, IUserRepository userRepository)
    {
        _randomUserService = randomUserService;
        _userRepository = userRepository;
    }

    [HttpGet("{quantity}")]
    public async Task<ActionResult<List<User>>> GetAndSaveUsers(int quantity)
    {
        try
        {
            var users = await _randomUserService.GetRandomUserAsync(quantity);
            return Ok(users);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> GetUsers(int quantityUser)
    {
        try
        {
            var users = await _userRepository.GetUsersAsync(quantityUser);
            return Ok(users);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<User>>> GetAllUsers()
    {
        var users = await _userRepository.GetUsersAsync(1000);
        return Ok(users);
    }

    [HttpGet("byId/{id}")]
    public async Task<ActionResult<User>> GetUserById(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser([FromBody] User user)
    {
        var created = await _userRepository.AddUserAsync(user);
        return CreatedAtAction(nameof(GetUserById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] User? user)
    {
        if (user == null)
            return BadRequest("O corpo da requisição não pode ser nulo.");

        if (id != user.Id)
            return BadRequest("ID da URL e do corpo não coincidem.");

        var updated = await _userRepository.UpdateUserAsync(user);
        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var deleted = await _userRepository.DeleteUserAsync(id);
        if (!deleted) return NotFound();

        return NoContent();
    }
}
