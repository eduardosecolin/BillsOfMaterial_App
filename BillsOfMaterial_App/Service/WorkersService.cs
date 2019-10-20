using BillsOfMaterial_App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfMaterial_App.Service
{
    public class WorkersService
    {
        private readonly DBContext _context;

        public WorkersService()
        {
            _context = new DBContext();
        }

        public double? GetHorlyCostWorker(int? worker)
        {
            var sql = _context.RM_Workers.Where(x => x.WorkerID == worker).FirstOrDefault();
            if (sql != null)
            {
                return sql.HourlyCost;
            }
            else
            {
                return 0;
            }
        }
    }
}
