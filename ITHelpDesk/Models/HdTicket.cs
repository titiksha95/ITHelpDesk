using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ITHelpDesk.Models;

[Table("HD_Tickets")]
public partial class HdTicket
{
    [Key]
    public int TicketId { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string Title { get; set; } = null!;

    [StringLength(1000)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Category { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string Priority { get; set; } = null!;

    [StringLength(30)]
    [Unicode(false)]
    public string Status { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime? ResolvedDate { get; set; }

    public int EmployeeId { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("HdTickets")]
    public virtual HdEmployee Employee { get; set; } = null!;
}
