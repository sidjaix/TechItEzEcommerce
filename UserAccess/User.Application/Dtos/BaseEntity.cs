using System.ComponentModel.DataAnnotations.Schema;

namespace UserAccess.Application.Dtos;

public class BaseEntity
{
    public DateTime CreatedOn { get; set; }
    public int CreatedBy { get; set; }
    public DateTime LastModifiedOn { get; set; }
    public int LastModifiedBy { get; set; }
}
