using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCore.Entities;

public class BaseEntity
{
    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }
    public int CreatedBy { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime LastModifiedOn { get; set; }
    public int LastModifiedBy { get; set; }
}
