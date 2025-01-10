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
        /// 0bXX_A_UW_UR_P_S_C (XXX are unused).
        /// <list type="bullet">
        /// <item>A: ability to create new administrators</item>
        /// <item>UW: ability to create new users</item>
        /// <item>UR: ability to read all users</item>
        /// <item>P: ability to write products (create, update and delete)</item>
        /// <item>S: ability to write sales (create, update and delete)</item>
        /// <item>C: ability to write categories (create, update and delete)</item>
        /// </list>
        /// </summary>
        public byte Permissions { get; set; }
    }
}
