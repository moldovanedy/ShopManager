using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopManager.Controller.DataModels
{
    [Table("Users")]
    public class User
    {
        [Required]
        [Key]
        public long ID { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "The username can't have more than 255 characters")]
        public string Username { get; set; } = "";

        [Required]
        public string Password { get; set; } = "";

        /// <summary>
        /// For now 1 is admin, 0 is normal user.
        /// </summary>
        public byte Permissions { get; set; }
    }
}
