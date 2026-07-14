using FCG.Contracts.Events;
using MassTransit;

namespace FCG.Application.Consumers
{
    public class PaymentProcessedConsumer : IConsumer<OrderPlacedEvent>
    {
        public async Task Consume(ConsumeContext<OrderPlacedEvent> context)
        {
            var result = context.Message;

            Console.WriteLine($"--- NOVA COMPRA RECEBIDA ---");
            Console.WriteLine($"Pedido ID: {result.OrderId}");
            Console.WriteLine($"Usuário ID: {result.UserId}");
            Console.WriteLine($"Jogo ID: {result.GameId}");
            Console.WriteLine($"Preço: {result.Price}");
            Console.WriteLine($"-----------------------------");
        }
    }
}
