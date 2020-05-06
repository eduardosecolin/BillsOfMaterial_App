using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfMaterial_App.Model
{
    public class MA_QltCtrlParameters
    {
        [Key]
        [StringLength(32)]
        public string Parameter { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        [StringLength(8)]
        public string UoM { get; set; }

        [StringLength(32)]
        public string AnalysisMethod { get; set; }

        public int? AnalysisArea { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }

        public Guid TBGuid { get; set; }
    }
}
