using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfMaterial_App.Model
{
    public partial class MA_BRNCM
    {
        [Key]
        [StringLength(8)]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string NCM { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime? ValidityStartingDate { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        public DateTime? ValidityEndingDate { get; set; }

        public double? ApproxTaxesImportPerc { get; set; }

        public double? ApproxTaxesDomesticPerc { get; set; }

        [StringLength(8)]
        public string ICMSTaxRateCode { get; set; }

        public int? IPISettlementType { get; set; }

        public double? StateApproxTaxesDomesticPerc { get; set; }

        public double? MunApproxTaxesImportPerc { get; set; }

        public double? MunApproxTaxesDomesticPerc { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }

        public Guid TBGuid { get; set; }
    }
}
