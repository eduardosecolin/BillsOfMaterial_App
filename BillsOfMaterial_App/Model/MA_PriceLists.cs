namespace BillsOfMaterial_App.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MA_PriceLists
    {
        [Key]
        [StringLength(8)]
        public string PriceList { get; set; }

        [StringLength(32)]
        public string Description { get; set; }

        [StringLength(8)]
        public string Currency { get; set; }

        public DateTime? ValidityStartingDate { get; set; }

        public DateTime? ValidityEndingDate { get; set; }

        public DateTime? LastModificationDate { get; set; }

        [StringLength(1)]
        public string Disabled { get; set; }

        [StringLength(1)]
        public string AlwaysShow { get; set; }

        public Guid? TBGuid { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }
    }
}
