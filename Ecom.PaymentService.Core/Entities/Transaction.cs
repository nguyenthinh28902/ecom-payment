using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ecom.PaymentService.Core.Entities;

public partial class Transaction
{
    [Key]
    public int Id { get; set; }

    public int OrderId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string OrderCode { get; set; } = null!;

    public int PaymentMethodId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Amount { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? Currency { get; set; }

    [StringLength(255)]
    public string? ExternalTransactionId { get; set; }

    public string? PaymentMetadata { get; set; }

    public byte? Status { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FinishedAt { get; set; }

    [ForeignKey("PaymentMethodId")]
    [InverseProperty("Transactions")]
    public virtual PaymentMethod PaymentMethod { get; set; } = null!;

    [InverseProperty("Transaction")]
    public virtual ICollection<TransactionLog> TransactionLogs { get; set; } = new List<TransactionLog>();
}
