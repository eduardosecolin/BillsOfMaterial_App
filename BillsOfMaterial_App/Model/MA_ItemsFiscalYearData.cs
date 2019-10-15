namespace BillsOfMaterial_App.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MA_ItemsFiscalYearData
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short FiscalYear { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(21)]
        public string Item { get; set; }

        public int? InventoryValuationCriteria { get; set; }

        [StringLength(1)]
        public string NoABCValuation { get; set; }

        public double? StandardCost { get; set; }

        public int? ValuationType { get; set; }

        [StringLength(1)]
        public string EvaluateByLot { get; set; }

        public int? LastLotNo { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }

        public virtual MA_Items MA_Items { get; set; }
    }
}
