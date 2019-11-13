using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfMaterial_App.Model
{
    public class CS_DBDefaultOBS
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(2000)]
        public string Observation { get; set; }
    }
}
