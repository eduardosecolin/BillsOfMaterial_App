using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfMaterial_App.Model
{
    public class MA_Company
    {
        [Key]
        public int CompanyId { get; set; }

        public double? FixedExpenses { get; set; }

        public double? VariableExpenses { get; set; }
    }
}
