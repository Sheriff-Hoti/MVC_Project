using System.ComponentModel.DataAnnotations;

namespace MVC_Project.Wrappers
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
