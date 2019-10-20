using BillsOfMaterial_App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfMaterial_App.Service
{
    public class OPFamilyService
    {
        private readonly DBContext _context;

        public OPFamilyService()
        {
            _context = new DBContext();
        }

        public string GetOperationFamily(string op)
        {
            var sql = _context.MA_Operations.Where(x => x.Operation == op).FirstOrDefault();
            if(sql != null)
            {
                return sql.WC;
            }
            else
            {
                return string.Empty;
            }
        }

        public double? GetHourlyCostOpFamily(string WC)
        {
            try
            {
                double? result = 0;
                int cont = 0;
                var sql = _context.MA_WCFamiliesDetails.Where(x => x.WC == WC).ToList();
                if (sql.Count > 0)
                {
                    foreach (var item in sql)
                    {
                        var sql2 = _context.MA_WorkCenters.Where(x => x.WC == item.WC).FirstOrDefault();
                        if (sql2 != null)
                        {
                            result += sql2.HourlyCost;
                            cont++;
                        }
                    }
                }

                return result = (result / cont);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
