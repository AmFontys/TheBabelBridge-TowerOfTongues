using BBTT.DBModels.Crossword;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BBTT.DBModels.User;

public class UserCrosswordDTO
{
    [ForeignKey("UserId")]
    public int UserId { get; set; }
    [ForeignKey("CrosswordId")]
    public int CrosswordId { get; set; }

    [NotMapped]
    public UserDTO User { get; set; } = new UserDTO();
    [NotMapped]
    public CrosswordDTO Crossword { get; set; }

}