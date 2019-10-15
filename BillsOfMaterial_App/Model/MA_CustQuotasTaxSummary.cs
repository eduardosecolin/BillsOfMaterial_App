namespace BillsOfMaterial_App.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MA_CustQuotasTaxSummary
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(4)]
        public string TaxCode { get; set; }

        public double? TaxableAmount { get; set; }

        public double? TaxAmount { get; set; }

        public double? TotalAmount { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustQuotaId { get; set; }

        public double? TaxableAmountDocCurr { get; set; }

        public double? TaxAmountDocCurr { get; set; }

        public double? TotalAmountDocCurr { get; set; }

        public double? AdditionalTaxAmount { get; set; }

        public double? AdditionalTaxAmountDocCurr { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(16)]
        public string Offset { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }

        public virtual MA_CustQuotas MA_CustQuotas { get; set; }
    }
}
