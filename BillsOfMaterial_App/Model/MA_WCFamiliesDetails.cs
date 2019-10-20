using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfMaterial_App.Model
{
    public partial class MA_WCFamiliesDetails
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string WC { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string WCFamily { get; set; }

        [StringLength(1)]
        public string Preferential { get; set; }

        [StringLength(64)]
        public string Notes { get; set; }

        public int? Priority { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }

        public Guid TBGuid { get; set; }

        public virtual MA_WCFamilies MA_WCFamilies { get; set; }
    }
}
