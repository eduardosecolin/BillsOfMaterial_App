namespace BillsOfMaterial_App.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MA_BillOfMaterialsQnA
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(21)]
        public string BOM { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SubId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Line { get; set; }

        public short? AnswerNo { get; set; }

        [StringLength(12)]
        public string QuestionNo { get; set; }

        [StringLength(21)]
        public string Component { get; set; }

        public int? ComponentType { get; set; }

        [StringLength(128)]
        public string Description { get; set; }

        [StringLength(21)]
        public string Variant { get; set; }

        [StringLength(8)]
        public string UoM { get; set; }

        public double? Qty { get; set; }

        public double? ScrapQty { get; set; }

        [StringLength(8)]
        public string ScrapUM { get; set; }

        [StringLength(64)]
        public string Notes { get; set; }

        public DateTime? ValidityStartingDate { get; set; }

        public DateTime? ValidityEndingDate { get; set; }

        [StringLength(1)]
        public string ToExplode { get; set; }

        [StringLength(1)]
        public string IsADefault { get; set; }

        public double? WastePerc { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }
    }
}
