using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ecom.PaymentService.Core.Entities;

[Index("Code", Name = "UQ__PaymentM__A25C5AA74755C7B9", IsUnique = true)]
public partial class PaymentMethod
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    public bool? IsActive { get; set; }

    [InverseProperty("PaymentMethod")]
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
