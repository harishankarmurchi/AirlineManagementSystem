using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.DBModels
{
    public class UserRole
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
