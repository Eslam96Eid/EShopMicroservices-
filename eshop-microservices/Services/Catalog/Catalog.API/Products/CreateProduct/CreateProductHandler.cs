using FluentValidation;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);
    //why we are directly injecting AI document session in here, and why we don't cover this session in the repository folder or any data folder ?
    //Because AI document session object is already an abstraction of the database operations.So we don't need any additional abstractions or unnecessary code like repository patterns.

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
    internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //create product entity from command object
            //save to database
            //return CreateProductResult result
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };
            //TODO
            //save the database 
            try
            {
                session.Store(product);
                await session.SaveChangesAsync(cancellationToken);
                //return the result 
                return new CreateProductResult(product.Id);
            }
            catch (Exception ex )
            {

                throw;
            }
           
        }
    }
}
