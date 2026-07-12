using FCG.Contracts.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PurchaseController : ControllerBase
{
    private readonly IPublishEndpoint _publishEndpoint;

    public PurchaseController(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PurchaseRequest request)
    {
        await _publishEndpoint.Publish(new OrderPlacedEvent(
            request.OrderId,
            request.UserId,
            request.GameId,
            request.Price
        ));

        return Accepted(new { message = "Compra processada com sucesso." });
    }
}

public record PurchaseRequest(Guid OrderId, Guid UserId, Guid GameId, decimal Price);