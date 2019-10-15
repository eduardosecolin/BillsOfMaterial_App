namespace BillsOfMaterial_App.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MA_ItemTypes
    {
        [Key]
        [StringLength(8)]
        public string CodeType { get; set; }

        [StringLength(32)]
        public string Description { get; set; }

        public double? Discount1 { get; set; }

        public double? Discount2 { get; set; }

        [StringLength(16)]
        public string DiscountFormula { get; set; }

        [StringLength(64)]
        public string Notes { get; set; }

        [StringLength(1)]
        public string HasCustomers { get; set; }

        [StringLength(1)]
        public string HasSuppliers { get; set; }

        public double? ForfaitOnReturns { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }

        public Guid TBGuid { get; set; }
    }
}
