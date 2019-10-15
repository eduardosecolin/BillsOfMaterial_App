namespace BillsOfMaterial_App.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RM_WorkersAbsences
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WorkerID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string Reason { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime StartingDate { get; set; }

        public DateTime? EndingDate { get; set; }

        public int? Manager { get; set; }

        [StringLength(64)]
        public string Notes { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }

        public virtual RM_Workers RM_Workers { get; set; }
    }
}
