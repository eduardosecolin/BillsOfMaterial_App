using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfMaterial_App.Model
{
    public partial class MA_WorkCenters
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MA_WorkCenters()
        {

        }

        [Key]
        [StringLength(8)]
        public string WC { get; set; }

        [StringLength(32)]
        public string Description { get; set; }

        [StringLength(12)]
        public string Supplier { get; set; }

        public int? ManagerID { get; set; }

        [StringLength(1)]
        public string Outsourced { get; set; }

        public DateTime? PlacedInServiceDate { get; set; }

        [StringLength(16)]
        public string Template { get; set; }

        public DateTime? PurchaseDate { get; set; }

        [StringLength(16)]
        public string Make { get; set; }

        public int? AverageCapacity { get; set; }

        public short? ResourceNo { get; set; }

        [StringLength(64)]
        public string Notes { get; set; }

        public int? WorkType { get; set; }

        public double? HourlyCost { get; set; }

        public double? UnitCost { get; set; }

        public double? AdditionalCost { get; set; }

        [StringLength(8)]
        public string Calendar { get; set; }

        public int? ConfirmMOBy { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }

        public Guid TBGuid { get; set; }
    }
}
