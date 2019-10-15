namespace BillsOfMaterial_App.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MA_Operations
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MA_Operations()
        {
            MA_OperationsLabour = new HashSet<MA_OperationsLabour>();
            MA_OperationsTools = new HashSet<MA_OperationsTools>();
        }

        [Key]
        [StringLength(21)]
        public string Operation { get; set; }

        [StringLength(96)]
        public string Description { get; set; }

        [StringLength(8)]
        public string WC { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? LastModificationDate { get; set; }

        [StringLength(64)]
        public string OperationDescriptionFile { get; set; }

        [StringLength(64)]
        public string Notes { get; set; }

        [StringLength(21)]
        public string Item { get; set; }

        [StringLength(8)]
        public string ProcessingTeam { get; set; }

        public int? ProcessingTime { get; set; }

        public double? ProcessingAttendancePerc { get; set; }

        public int? ProcessingWorkingTime { get; set; }

        public short? NoOfProcessingWorkers { get; set; }

        [StringLength(8)]
        public string SetupTeam { get; set; }

        public int? SetupTime { get; set; }

        public double? SetupAttendancePerc { get; set; }

        public int? SetupWorkingTime { get; set; }

        public short? NoOfSetupWorkers { get; set; }

        public int? QueueTime { get; set; }

        public double? HourlyCost { get; set; }

        public double? UnitCost { get; set; }

        public double? AdditionalCost { get; set; }

        [StringLength(1)]
        public string CostsOnQty { get; set; }

        [StringLength(1)]
        public string TotalTime { get; set; }

        [StringLength(1)]
        public string IsWC { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }

        public Guid TBGuid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MA_OperationsLabour> MA_OperationsLabour { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MA_OperationsTools> MA_OperationsTools { get; set; }
    }
}
