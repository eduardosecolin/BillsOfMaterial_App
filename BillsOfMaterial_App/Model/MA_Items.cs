namespace BillsOfMaterial_App.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MA_Items
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MA_Items()
        {
            MA_ItemsBalances = new HashSet<MA_ItemsBalances>();
            MA_ItemsFiscalYearData = new HashSet<MA_ItemsFiscalYearData>();
            MA_ItemsPriceLists = new HashSet<MA_ItemsPriceLists>();
        }

        [Key]
        [StringLength(21)]
        public string Item { get; set; }

        [StringLength(21)]
        public string SaleBarCode { get; set; }

        [StringLength(128)]
        public string Description { get; set; }

        [StringLength(1)]
        public string IsGood { get; set; }

        [StringLength(4)]
        public string TaxCode { get; set; }

        [StringLength(8)]
        public string BaseUoM { get; set; }

        public double? BasePrice { get; set; }

        public double? Discount1 { get; set; }

        public double? Discount2 { get; set; }

        [StringLength(16)]
        public string DiscountFormula { get; set; }

        public double? Markup { get; set; }

        [StringLength(8)]
        public string ItemType { get; set; }

        [StringLength(8)]
        public string CommodityCtg { get; set; }

        [StringLength(8)]
        public string HomogeneousCtg { get; set; }

        [StringLength(8)]
        public string CommissionCtg { get; set; }

        [StringLength(16)]
        public string SaleOffset { get; set; }

        [StringLength(16)]
        public string PurchaseOffset { get; set; }

        public DateTime? AvailabilityDate { get; set; }

        public int? SaleType { get; set; }

        [StringLength(1)]
        public string HasCustomers { get; set; }

        [StringLength(1)]
        public string HasSuppliers { get; set; }

        [StringLength(128)]
        public string InternalNote { get; set; }

        [StringLength(128)]
        public string PublicNote { get; set; }

        [StringLength(16)]
        public string Producer { get; set; }

        [StringLength(1)]
        public string UseSerialNo { get; set; }

        [StringLength(21)]
        public string OldItem { get; set; }

        [StringLength(1)]
        public string Disabled { get; set; }

        [StringLength(1)]
        public string IsSpecificItem { get; set; }

        [StringLength(8)]
        public string ProductCtg { get; set; }

        [StringLength(8)]
        public string ProductSubCtg { get; set; }

        [StringLength(1)]
        public string KitExpansion { get; set; }

        [StringLength(1)]
        public string PostKitComponents { get; set; }

        [StringLength(128)]
        public string Picture { get; set; }

        public DateTime? StandardCostDate { get; set; }

        public int? Nature { get; set; }

        [StringLength(8)]
        public string SecondRateUoM { get; set; }

        [StringLength(21)]
        public string SecondRate { get; set; }

        public int? PurchaseType { get; set; }

        [StringLength(16)]
        public string ConsuptionOffset { get; set; }

        [StringLength(1)]
        public string NotPostable { get; set; }

        public double? SalespersonComm { get; set; }

        [StringLength(8)]
        public string CostCenter { get; set; }

        [StringLength(1)]
        public string NoUoMSearch { get; set; }

        [StringLength(10)]
        public string Job { get; set; }

        [StringLength(64)]
        public string DescriptionText { get; set; }

        [StringLength(1)]
        public string CanBeDisabled { get; set; }

        [StringLength(8)]
        public string ProductLine { get; set; }

        [StringLength(40)]
        public string ShortDescription { get; set; }

        [StringLength(1)]
        public string BasePriceWithTax { get; set; }

        [StringLength(1)]
        public string SubjectToWithholdingTax { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? ModificationDate { get; set; }

        [StringLength(1)]
        public string NoAddDiscountInSaleDoc { get; set; }

        [StringLength(21)]
        public string BarcodeSegment { get; set; }

        [StringLength(1)]
        public string ReverseCharge { get; set; }

        [StringLength(4)]
        public string RCTaxCode { get; set; }

        [StringLength(1)]
        public string Draft { get; set; }

        [StringLength(1)]
        public string Valorize { get; set; }

        [StringLength(8)]
        public string FiscalUoM { get; set; }

        [StringLength(8)]
        public string AccountingType { get; set; }

        [StringLength(8)]
        public string AccountingRuleCode { get; set; }

        [StringLength(1)]
        public string NoPaymDiscountInDoc { get; set; }

        [StringLength(1)]
        public string ToBeDiscontinued { get; set; }

        //public int? LastSubIdNotes { get; set; }

        [StringLength(35)]
        public string EITypeCode { get; set; }

        [StringLength(35)]
        public string EIValueCode { get; set; }

        [StringLength(2)]
        public string TSChargeType { get; set; }

        [StringLength(1)]
        public string TSChargeTypeFlag { get; set; }

        [StringLength(15)]
        public string ISBN { get; set; }

        [StringLength(8)]
        public string AuthorCode { get; set; }

        public double? CoverPrice { get; set; }

        [StringLength(16)]
        public string ItemCodes { get; set; }

        public double? SpecificWeight { get; set; }

        [StringLength(1)]
        public string AdditionalCharge { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }

        public Guid TBGuid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MA_ItemsBalances> MA_ItemsBalances { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MA_ItemsFiscalYearData> MA_ItemsFiscalYearData { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MA_ItemsPriceLists> MA_ItemsPriceLists { get; set; }
    }
}
