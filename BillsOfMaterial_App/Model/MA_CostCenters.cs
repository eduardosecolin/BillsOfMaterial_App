namespace BillsOfMaterial_App.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MA_CostCenters
    {
        [Key]
        [StringLength(8)]
        public string CostCenter { get; set; }

        [StringLength(64)]
        public string Description { get; set; }

        [StringLength(8)]
        public string GroupCode { get; set; }

        public int? CodeType { get; set; }

        public int? Nature { get; set; }

        public double? SqMtSurfaceArea { get; set; }

        [StringLength(32)]
        public string CostCenterManager { get; set; }

        public double? DirectEmployeesNo { get; set; }

        public double? IndirectEmployeesNo { get; set; }

        public double? DepreciationPerc { get; set; }

        [StringLength(1)]
        public string DummyCostCenter { get; set; }

        [StringLength(1)]
        public string Disabled { get; set; }

        [StringLength(64)]
        public string Notes { get; set; }

        [StringLength(16)]
        public string Account { get; set; }

        [StringLength(8)]
        public string BarcodeSegment { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }

        public Guid TBGuid { get; set; }
    }
}
