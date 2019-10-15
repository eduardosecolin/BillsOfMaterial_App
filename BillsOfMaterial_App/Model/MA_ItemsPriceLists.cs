namespace BillsOfMaterial_App.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MA_ItemsPriceLists
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(21)]
        public string Item { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string PriceList { get; set; }

        [StringLength(8)]
        public string PriceListUoM { get; set; }

        public DateTime? LastModificationDate { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime ValidityStartingDate { get; set; }

        public DateTime? ValidityEndingDate { get; set; }

        [Key]
        [Column(Order = 3)]
        public double Qty { get; set; }

        public double? Price { get; set; }

        public double? Discount1 { get; set; }

        public double? Discount2 { get; set; }

        [StringLength(16)]
        public string DiscountFormula { get; set; }

        [StringLength(1)]
        public string WithTax { get; set; }

        [StringLength(1)]
        public string Discounted { get; set; }

        [StringLength(1)]
        public string Disabled { get; set; }

        [StringLength(1)]
        public string AlwaysShow { get; set; }

        [StringLength(32)]
        public string Notes { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }

        public Guid TBGuid { get; set; }

        public virtual MA_Items MA_Items { get; set; }
    }
}
