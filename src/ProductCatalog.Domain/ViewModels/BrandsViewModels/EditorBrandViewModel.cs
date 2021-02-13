using Flunt.Notifications;
using Flunt.Validations;

namespace ProductCatalog.ViewModels.BrandsViewModels
{
    public class EditorBrandViewModel : Notifiable, IValidatable
    {
        public string Name { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                .HasMaxLen(Name, 120, "Name", "O nome da marca deve conter até 120 caracteres")
                .HasMinLen(Name, 2, "Name", "O nome da marca deve conter pelo menos 2 caracteres")
            );
        }
    }
}