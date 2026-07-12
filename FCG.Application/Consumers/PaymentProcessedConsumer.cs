using FCG.Contracts.Events;
using MassTransit;

namespace FCG.Application.Consumers
{
    public class PaymentProcessedConsumer : IConsumer<PaymentProcessedEvent>
    {
        public Task Consume(ConsumeContext<PaymentProcessedEvent> context)
        {
            var result = context.Message;

            Console.WriteLine($"--- ATUALIZANDO BIBLIOTECA ---");
            Console.WriteLine($"Pedido: {result.OrderId}");

            if (result.Status == "Approved")
            {
                Console.WriteLine($"Status: APROVADO - Jogo adicionado à biblioteca do usuário {result.UserId}!");
            }
            else
            {
                Console.WriteLine($"Status: REJEITADO - O pagamento não foi processado.");
            }
            Console.WriteLine($"------------------------------");

            return Task.CompletedTask;
        }
    }
}
