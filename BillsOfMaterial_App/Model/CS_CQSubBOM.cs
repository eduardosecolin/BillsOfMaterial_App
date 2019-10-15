namespace BillsOfMaterial_App.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CS_CQSubBOM
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustQuotaId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Line { get; set; }

        [StringLength(21)]
        public string SemiFinished { get; set; }

        [StringLength(21)]
        public string BOM { get; set; }

        [StringLength(21)]
        public string Component { get; set; }

        [StringLength(128)]
        public string Description { get; set; }

        [StringLength(8)]
        public string UoM { get; set; }

        public double? Qty { get; set; }

        [Column(TypeName = "ntext")]
        public string OBSComp { get; set; }

        [StringLength(21)]
        public string Operation { get; set; }

        [StringLength(96)]
        public string DescriptionOperation { get; set; }

        public DateTime? TimeProcess { get; set; }

        [StringLength(3000)]
        public string OBSOperation { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }

        public Guid TBGuid { get; set; }
    }
}
