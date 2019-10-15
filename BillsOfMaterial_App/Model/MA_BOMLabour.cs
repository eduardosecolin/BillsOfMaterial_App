namespace BillsOfMaterial_App.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MA_BOMLabour
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(21)]
        public string BOM { get; set; }

        public short? RtgStep { get; set; }

        [StringLength(8)]
        public string Alternate { get; set; }

        public short? AltRtgStep { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SubId { get; set; }

        [Key]
        [Column(Order = 2)]
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
    }
}
