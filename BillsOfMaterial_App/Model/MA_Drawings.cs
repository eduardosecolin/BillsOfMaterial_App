using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfMaterial_App.Model
{
    public partial class MA_Drawings
    {
        public MA_Drawings()
        {
            MA_DrawingsDescription = new HashSet<MA_DrawingsDescription>();
        }

        [Key]
        [StringLength(21)]
        public string Drawing { get; set; }

        [StringLength(32)]
        public string Description { get; set; }

        [StringLength(8)]
        public string Revision { get; set; }

        [StringLength(21)]
        public string DerivedFrom { get; set; }

        [StringLength(15)]
        public string BarCode { get; set; }

        [StringLength(64)]
        public string Notes { get; set; }
       
        public DateTime? DateOfSignature { get; set; }

        [StringLength(32)]
        public string ApprovalSignature { get; set; }

        [StringLength(128)]
        public string FilePath { get; set; }

        [StringLength(1)]
        public string PreferredDrawing { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }

        public Guid TBGuid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MA_DrawingsDescription> MA_DrawingsDescription { get; set; }
    }
}
