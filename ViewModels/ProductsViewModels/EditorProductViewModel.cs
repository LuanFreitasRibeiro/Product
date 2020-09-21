using Flunt.Notifications;
using Flunt.Validations;

namespace ProductCatalog.ViewModels.ProductViewModels
{
    public class EditorProductViewModel: Notifiable, IValidatable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                .HasMaxLen(Name, 120, "Name", "O nome do produto deve conter até 120 caracteres")
                .HasMinLen(Name, 3, "Name", "O nome do produto deve conter pelo menos 3 caracteres")
                .IsGreaterThan(Price, 0, "Price", "O preço deve der maior que zero")
            );
        }
    }
}