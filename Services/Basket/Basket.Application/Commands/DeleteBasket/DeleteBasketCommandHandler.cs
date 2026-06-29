using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Commands.DeleteBasket;

public class DeleteBasketCommand : IRequest<bool>
{
    public DeleteBasketCommand(string userName)
    {
        UserName = userName;
    }

    public string UserName { get; set; }
}
public class DeleteBasketCommandHandler(IBasketRepository basketRepository) : IRequestHandler<DeleteBasketCommand, bool>
{
    public async Task<bool> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        await basketRepository.DeleteBasket(request.UserName);
        return true;
    }
}
