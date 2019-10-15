namespace BillsOfMaterial_App.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MA_BillOfMaterialsRouting
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short RtgStep { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string Alternate { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short AltRtgStep { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(21)]
        public string BOM { get; set; }

        [StringLength(21)]
        public string Operation { get; set; }

        public double? ProcessingAttendancePerc { get; set; }

        public double? SetupAttendancePerc { get; set; }

        [StringLength(1024)]
        public string Notes { get; set; }

        [StringLength(8)]
        public string WC { get; set; }

        public int? ProcessingTime { get; set; }

        public int? ProcessingWorkingTime { get; set; }

        public int? SetupTime { get; set; }

        public int? SetupWorkingTime { get; set; }

        public int? LineTypeInDN { get; set; }

        public short? NoOfProcessingWorkers { get; set; }

        public short? NoOfSetupWorkers { get; set; }

        public int? QueueTime { get; set; }

        public int? SubId { get; set; }

        [StringLength(12)]
        public string Supplier { get; set; }

        public double? Qty { get; set; }

        [StringLength(1)]
        public string TotalTime { get; set; }

        [StringLength(1)]
        public string IsWC { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }

        public virtual MA_BillOfMaterials MA_BillOfMaterials { get; set; }
    }
}
