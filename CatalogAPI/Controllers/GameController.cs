using FCG.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGameCommand command)
    {
        var gameId = await mediator.Send(command);
        return Ok(new { Id = gameId, Message = "Jogo criado com sucesso!" });
    }
}