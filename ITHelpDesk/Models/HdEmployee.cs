using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ITHelpDesk.Models;

[Table("HD_Employees")]
[Index("Email", Name = "UQ__HD_Emplo__A9D10534E07FF22C", IsUnique = true)]
public partial class HdEmployee
{
    [Key]
    public int EmployeeId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string EmployeeName { get; set; } = null!;

    [StringLength(150)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    public int DepartmentId { get; set; }

    [ForeignKey("DepartmentId")]
    [InverseProperty("HdEmployees")]
    public virtual HdDepartment Department { get; set; } = null!;

    [InverseProperty("Employee")]
    public virtual ICollection<HdTicket> HdTickets { get; set; } = new List<HdTicket>();
}
