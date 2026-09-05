using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ITHelpDesk.Models;

[Table("HD_Departments")]
[Index("DepartmentName", Name = "UQ__HD_Depar__D949CC349878053F", IsUnique = true)]
public partial class HdDepartment
{
    [Key]
    public int DepartmentId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string DepartmentName { get; set; } = null!;

    [InverseProperty("Department")]
    public virtual ICollection<HdEmployee> HdEmployees { get; set; } = new List<HdEmployee>();
}
