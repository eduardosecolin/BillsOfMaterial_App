namespace BillsOfMaterial_App.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MA_ItemsBRTaxes
    {
        [Key]
        [StringLength(21)]
        public string Item { get; set; }

        [StringLength(8)]
        public string NCM { get; set; }

        [StringLength(6)]
        public string NVE { get; set; }

        [StringLength(7)]
        public string CEST { get; set; }

        [StringLength(9)]
        public string ANP { get; set; }

        [StringLength(8)]
        public string ServiceTypeCode { get; set; }

        public int? ItemType { get; set; }

        public double? ApproxTaxesImportPerc { get; set; }

        public double? ApproxTaxesDomesticPerc { get; set; }

        public double? StateApproxTaxesImportPerc { get; set; }

        public double? StateApproxTaxesDomesticPerc { get; set; }

        public double? MunApproxTaxesImportPerc { get; set; }

        public double? MunApproxTaxesDomesticPerc { get; set; }

        [StringLength(36)]
        public string FCI { get; set; }

        [StringLength(10)]
        public string FiscalBenefCode { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }
    }
}
