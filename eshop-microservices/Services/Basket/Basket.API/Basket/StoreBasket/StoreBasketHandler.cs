namespace Basket.API.Basket.StoreBasket;
public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidation : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidation()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("cart can't be null");
        RuleFor(x => x.Cart.UserName).NotNull().WithMessage("username is required");
    }
}

public class StoreBasketCommandHandler(IBasketRepository repository) :
    ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        ShoppingCart cart = command.Cart;
        //TODO : store basket in database (use martin upsert - if existing data = update -
        //If not this will be insert any record and after that we have to update the cache in Redis distributed)

        await repository.StoreBasket(command.Cart, cancellationToken);
        return new StoreBasketResult(command.Cart.UserName );
    }
}
