using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfMaterial_App.Model
{
    public partial class MA_WCFamilies
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MA_WCFamilies()
        {
            MA_WCFamiliesDetails = new HashSet<MA_WCFamiliesDetails>();
        }

        [Key]
        [StringLength(8)]
        public string WCFamily { get; set; }

        [StringLength(32)]
        public string Description { get; set; }

        [StringLength(64)]
        public string Notes { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }

        public Guid TBGuid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MA_WCFamiliesDetails> MA_WCFamiliesDetails { get; set; }
    }
}
