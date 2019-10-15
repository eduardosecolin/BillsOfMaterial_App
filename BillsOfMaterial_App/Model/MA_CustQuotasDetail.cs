namespace BillsOfMaterial_App.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MA_CustQuotasDetail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Line { get; set; }

        public int? LineType { get; set; }

        [StringLength(128)]
        public string Description { get; set; }

        [StringLength(1)]
        public string NoPrint { get; set; }

        [StringLength(1)]
        public string NoCopyOnOrder { get; set; }

        [StringLength(21)]
        public string Item { get; set; }

        [StringLength(21)]
        public string Variant { get; set; }

        [StringLength(21)]
        public string Drawing { get; set; }

        [StringLength(8)]
        public string Department { get; set; }

        [StringLength(8)]
        public string UoM { get; set; }

        public double? Qty { get; set; }

        public double? AdditionalQty1 { get; set; }

        public double? AdditionalQty2 { get; set; }

        public double? AdditionalQty3 { get; set; }

        public double? AdditionalQty { get; set; }

        public double? UnitValue { get; set; }

        public double? TaxableAmount { get; set; }

        [StringLength(4)]
        public string TaxCode { get; set; }

        public double? TotalAmount { get; set; }

        public double? Discount1 { get; set; }

        public double? Discount2 { get; set; }

        [StringLength(16)]
        public string DiscountFormula { get; set; }

        public double? DiscountAmount { get; set; }

        public int? SaleType { get; set; }

        public short? NoOfPacks { get; set; }

        [StringLength(8)]
        public string PacksUoM { get; set; }

        public double? GrossWeight { get; set; }

        public double? NetWeight { get; set; }

        public double? GrossVolume { get; set; }

        public DateTime? QuotationDate { get; set; }

        [StringLength(12)]
        public string Customer { get; set; }

        [StringLength(12)]
        public string Contact { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustQuotaId { get; set; }

        public short? KitNo { get; set; }

        public double? KitQty { get; set; }

        public short? DaysForDelivery { get; set; }

        public double? BookedQty { get; set; }

        public DateTime? ExpectedDeliveryDate { get; set; }

        public short? Position { get; set; }

        public int? SubId { get; set; }

        public short? ExternalLineReference { get; set; }

        [StringLength(10)]
        public string Job { get; set; }

        [StringLength(8)]
        public string CostCenter { get; set; }

        [StringLength(8)]
        public string ProductLine { get; set; }

        public int? ReferenceDocId { get; set; }

        [StringLength(10)]
        public string ReferenceDocNo { get; set; }

        [StringLength(32)]
        public string Notes { get; set; }

        [StringLength(8)]
        public string PriceList { get; set; }

        public int? CRRefType { get; set; }

        public int? CRRefID { get; set; }

        public int? CRRefSubID { get; set; }

        public double? NetPrice { get; set; }

        [StringLength(1)]
        public string NetPriceIsAuto { get; set; }

        [StringLength(8)]
        public string AccountingTypePhase1 { get; set; }

        [StringLength(8)]
        public string AccountingTypePhase2 { get; set; }

        public double? DistributedShipCharges { get; set; }

        public double? DistributedAdditionalCharges { get; set; }

        public double? DistributedPackingCharges { get; set; }

        [StringLength(16)]
        public string Offset { get; set; }

        //public double? UnitCostValue { get; set; }

        //public double? SaleMargin { get; set; }

        //public double? FixedExpenses { get; set; }

        //public double? VariableExpenses { get; set; }

        //public double? IPI { get; set; }

        //public double? ICMS { get; set; }

        [StringLength(1)]
        public string InEI { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }

        public virtual MA_CustQuotas MA_CustQuotas { get; set; }
    }
}
