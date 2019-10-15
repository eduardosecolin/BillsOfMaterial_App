namespace BillsOfMaterial_App.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MA_OperationsLabour
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(21)]
        public string Operation { get; set; }

        [Key]
        [Column(Order = 1)]
        public int Line { get; set; }

        [StringLength(8)]
        public string ResourceType { get; set; }

        [StringLength(8)]
        public string ResourceCode { get; set; }

        public int? WorkerID { get; set; }

        public int? LabourType { get; set; }

        public double? AttendancePerc { get; set; }

        public int? WorkingTime { get; set; }

        public DateTime? LabourDate { get; set; }

        public short? NoOfResources { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }

        public virtual MA_Operations MA_Operations { get; set; }
    }
}
