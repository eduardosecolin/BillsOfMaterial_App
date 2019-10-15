namespace BillsOfMaterial_App.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MA_BillOfMaterialsComp
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Line { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(21)]
        public string BOM { get; set; }

        [StringLength(21)]
        public string Component { get; set; }

        public int? ComponentType { get; set; }

        [StringLength(128)]
        public string Description { get; set; }

        [StringLength(8)]
        public string UoM { get; set; }

        public double? Qty { get; set; }

        public double? PercQty { get; set; }

        [StringLength(1)]
        public string FixedComponent { get; set; }

        public double? ScrapQty { get; set; }

        [StringLength(8)]
        public string ScrapUM { get; set; }

        [StringLength(64)]
        public string Notes { get; set; }

        [StringLength(21)]
        public string Variant { get; set; }

        public DateTime? ValidityStartingDate { get; set; }

        public DateTime? ValidityEndingDate { get; set; }

        public int? SubId { get; set; }

        [StringLength(8)]
        public string TechnicalData { get; set; }

        public short? ExternalLineReference { get; set; }

        [StringLength(8)]
        public string ItemType { get; set; }

        [StringLength(8)]
        public string StructureCode { get; set; }

        [StringLength(1)]
        public string ToExplode { get; set; }

        [StringLength(1)]
        public string Configurable { get; set; }

        [StringLength(12)]
        public string QuestionNo { get; set; }

        public double? WastePerc { get; set; }

        public short? DNRtgStep { get; set; }

        [StringLength(1)]
        public string NotPostable { get; set; }

        [StringLength(21)]
        public string Waste { get; set; }

        public int? OperationSubId { get; set; }

        [StringLength(21)]
        public string Drawing { get; set; }

        [StringLength(1)]
        public string IsKanban { get; set; }

        [StringLength(1)]
        public string FixedQty { get; set; }

        public short? EndUseRtgStep { get; set; }

        [StringLength(1)]
        public string Valorize { get; set; }

        [StringLength(1)]
        public string SetFixedQtyOnMO { get; set; }

        [StringLength(8)]
        public string PickingStorage { get; set; }

        public int? PickingSpecificatorType { get; set; }

        [StringLength(12)]
        public string PickingSpecificator { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }

        public virtual MA_BillOfMaterials MA_BillOfMaterials { get; set; }
    }
}
