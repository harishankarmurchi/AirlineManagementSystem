using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.DBModels
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; } 
        public string Password { get; set; }

        [ForeignKey("UserRole")]
        public int RoleId { get; set; }
        public UserRole UserRole { get; set; }
    }
}
