using FCG.Contracts.Events;
using FCG.Domain.Repositories;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PurchaseController : ControllerBase
{
    private readonly IPublishEndpoint _publishEndpoint; 
    private readonly IGameRepository _gameRepository;


    public PurchaseController(IPublishEndpoint publishEndpoint, IGameRepository gameRepository)
    {
        _publishEndpoint = publishEndpoint;
        _gameRepository = gameRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PurchaseRequest request)
    {
        var game = await _gameRepository.GetByIdAsync(request.GameId);

        await _publishEndpoint.Publish(new OrderPlacedEvent(
            request.OrderId,
            request.UserId,
            request.GameId,
            Price: game?.Price ?? 0m
        ));

        return Accepted(new { message = "Compra processada com sucesso." });
    }
}

public record PurchaseRequest(Guid OrderId, Guid UserId, Guid GameId);