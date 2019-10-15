using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfMaterial_App.Model
{
    public partial class CS_CustQuotasOperation
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Line { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(21)]
        public string BOM { get; set; }

        [StringLength(21)]
        public string Item { get; set; }

        [StringLength(21)]
        public string Operation { get; set; }

        [StringLength(128)]
        public string DescriptionOperation { get; set; }

        [StringLength(1000)]
        public string Obs { get; set; }

        public DateTime? TimeProcess { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }
    }
}
