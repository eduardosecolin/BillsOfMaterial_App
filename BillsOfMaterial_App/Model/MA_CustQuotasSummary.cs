namespace BillsOfMaterial_App.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MA_CustQuotasSummary
    {
        public double? TaxableAmount { get; set; }

        public double? TaxAmount { get; set; }

        public double? TotalAmount { get; set; }

        public double? TotalAmountDocCurr { get; set; }

        public double? GoodsAmount { get; set; }

        public double? ServiceAmounts { get; set; }

        public double? DiscountOnGoods { get; set; }

        public double? DiscountOnServices { get; set; }

        public double? PayableAmount { get; set; }

        public double? FreeSamples { get; set; }

        public double? PayableAmountInBaseCurr { get; set; }

        public double? Discount1 { get; set; }

        public double? Discount2 { get; set; }

        [StringLength(16)]
        public string DiscountFormula { get; set; }

        public double? Discounts { get; set; }

        public double? Allowances { get; set; }

        public double? Advance { get; set; }

        public double? ReturnedMaterial { get; set; }

        public double? PackagingCharges { get; set; }

        public double? ShippingCharges { get; set; }

        public double? StampsCharges { get; set; }

        public double? CollectionCharges { get; set; }

        public double? AdditionalCharges { get; set; }

        public double? CashOnDeliveryPercentage { get; set; }

        public double? CashOnDeliveryCharges { get; set; }

        public double? Margin { get; set; }

        public double? MarginPerc { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustQuotaId { get; set; }

        public double? TaxableAmountDocCurr { get; set; }

        public double? TaxAmountDocCurr { get; set; }

        [StringLength(1)]
        public string PackagingChargesIsAuto { get; set; }

        [StringLength(1)]
        public string ShippingChargesIsAuto { get; set; }

        [StringLength(1)]
        public string StampsChargesIsAuto { get; set; }

        [StringLength(1)]
        public string CollectionChargesIsAuto { get; set; }

        [StringLength(1)]
        public string AdditionalChargesIsAuto { get; set; }

        [StringLength(1)]
        public string CashOnDeliveryChargesIsAuto { get; set; }

        public double? FreeSamplesDocCurr { get; set; }

        [StringLength(1)]
        public string DiscountsIsAuto { get; set; }

        public double? InsuranceCharges { get; set; }

        [StringLength(1)]
        public string InsuranceChargesIsAuto { get; set; }

        public double? AdditionalTaxAmount { get; set; }

        public double? AdditionalTaxAmountDocCurr { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }

        public virtual MA_CustQuotas MA_CustQuotas { get; set; }
    }
}
