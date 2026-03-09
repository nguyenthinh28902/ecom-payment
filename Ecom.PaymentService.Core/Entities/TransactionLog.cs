using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ecom.PaymentService.Core.Entities;

public partial class TransactionLog
{
    [Key]
    public int Id { get; set; }

    public int TransactionId { get; set; }

    public string LogContent { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [ForeignKey("TransactionId")]
    [InverseProperty("TransactionLogs")]
    public virtual Transaction Transaction { get; set; } = null!;
}
