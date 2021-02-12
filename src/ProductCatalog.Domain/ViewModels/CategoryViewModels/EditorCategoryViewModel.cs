using Flunt.Notifications;
using Flunt.Validations;

namespace ProductCatalog.ViewModels.CategoryViewModels
{
    public class EditorCategoryViewModel : Notifiable, IValidatable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                .HasMaxLen(Name, 120, "Name", "O nome da categoria deve conter até 120 caracteres")
                .HasMinLen(Name, 3, "Name", "O nome da categoria deve ter pelo menos 3 caracteres")
            );
        }
    }
}