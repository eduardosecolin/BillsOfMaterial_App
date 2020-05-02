using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfMaterial_App.Model
{
    public class CS_TBCostFormation
    {

        [Key]
        public int Id { get; set; }

        public int? Id_Offer { get; set; }

        [StringLength(10)]
        public string OfferNo { get; set; }

        [StringLength(21)]
        public string Item { get; set; }

        [StringLength(1)]
        public string SumIpiIcms { get; set; }

        public double? PIS { get; set; }
        public double? COFINS { get; set; }
        public double? ICMS { get; set; }
        public double? Freight { get; set; }
        public double? IPI { get; set; }
        public double? Comission { get; set; }
        public double? DF_Percent { get; set; }
        public double? DV_Percent { get; set; }
        public double? Margin { get; set; }
        public double? MarginVariable { get; set; }
        public double? ISS { get; set; }
        public double? IR { get; set; }
        public double? CSLL { get; set; }
        public double? Markup { get; set; }
        public double? TotalMarkup { get; set; }
        public double? TotalSimulation { get; set; }
        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }

        public Guid TBGuid { get; set; }
    }
}
