namespace BillsOfMaterial_App.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RM_WorkersDetails
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WorkerId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(1)]
        public string IsWorker { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(8)]
        public string ChildResourceType { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(8)]
        public string ChildResourceCode { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ChildWorkerId { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }

        public virtual RM_Workers RM_Workers { get; set; }
    }
}
