namespace BillsOfMaterial_App.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MA_OperationsTools
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(21)]
        public string Operation { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Line { get; set; }

        [StringLength(1)]
        public string IsFamily { get; set; }

        [StringLength(10)]
        public string Tool { get; set; }

        public int? ProcessingType { get; set; }

        [StringLength(1)]
        public string Fixed { get; set; }

        public double? UsageQuantity { get; set; }

        public int? UsageTime { get; set; }

        [StringLength(1)]
        public string ExclusiveUse { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }

        public virtual MA_Operations MA_Operations { get; set; }
    }
}
