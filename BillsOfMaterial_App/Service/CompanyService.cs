using BillsOfMaterial_App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfMaterial_App.Service
{
    public class CompanyService
    {
        private readonly DBContext _context;

        public CompanyService()
        {
            _context = new DBContext();
        }

        public double GetFixedExpenses()
        {
            var sql = _context.MA_Company.FirstOrDefault();
            if(sql != null)
            {
                return Convert.ToDouble(sql.FixedExpenses);
            }
            else
            {
                return 0;
            }
        }

        public double GetVariableExpenses()
        {
            var sql = _context.MA_Company.FirstOrDefault();
            if (sql != null)
            {
                return Convert.ToDouble(sql.VariableExpenses);
            }
            else
            {
                return 0;
            }
        }

    }
}
