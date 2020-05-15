using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfMaterial_App.Model
{
    public class CS_TimeSheetPROD
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Line { get; set; }

        public int? MOId { get; set; }

        [StringLength(21)]
        public string Phase { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? StatusODP { get; set; }

        [StringLength(1)]
        public string IsFinalized { get; set; }

        [StringLength(1)]
        public string IsInitialized { get; set; }

        public int? RtgStep { get; set; }

        public int? UserIdOP { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }

        public Guid TBGuid { get; set; }

        public string MONo { get; set; }

        public string Status { get; set; }
    }
}
