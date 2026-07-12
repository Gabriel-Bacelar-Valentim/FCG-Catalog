using FCG.Domain.Entities.GameEntity;
using FCG.Domain.Repositories;
using MediatR;

namespace FCG.Application.Commands
{
    public class CreateGameCommand : IRequest<Guid>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }

    public class CreateGameCommandHandler(IGameRepository repository) : IRequestHandler<CreateGameCommand, Guid>
    {
        public async Task<Guid> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            var game = new Game
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                Price = request.Price
            };

            await repository.AddAsync(game);
            await repository.SaveChangesAsync();

            return game.Id;
        }
    }
}
