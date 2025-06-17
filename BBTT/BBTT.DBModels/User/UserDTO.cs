using BBTT.DBModels.Crossword;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTT.DBModels.User;
public class UserDTO
{
    [Required]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Email { get; set; }

    [Required]
    [MaxLength(200)]
    public string Password { get; set; }

    [Required]
    [MaxLength(50)]
    public string Salt { get; set; } = string.Empty;

    // Define the foreign key property
    public int RoleId { get; set; }

    [Required]
    [ForeignKey(nameof(RoleId))]
    public UserRolesDTO Role { get; set; }

    [Required]
    public string CreatedAt { get; set; } = DateTime.UtcNow.ToString();

    [NotMapped]
    public List<UserCrosswordDTO> UserCrosswords { get; set; }
}
