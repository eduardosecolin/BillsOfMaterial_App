using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfMaterial_App.Model
{
    public partial class MA_UnitsOfMeasure
    {
        [Key]
        [StringLength(8)]
        public string BaseUoM { get; set; }

        [StringLength(32)]
        public string Description { get; set; }

        [StringLength(8)]
        public string Symbol { get; set; }

        [StringLength(32)]
        public string Notes { get; set; }

        [StringLength(8)]
        public string BarcodeSegment { get; set; }

        [StringLength(1)]
        public string Disabled { get; set; }

        [StringLength(10)]
        public string UoMForEI { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }

        public Guid TBGuid { get; set; }
    }
}
