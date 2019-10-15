using BillsOfMaterial_App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfMaterial_App.Service
{
    public class CustQuotaCompOpService
    {
        private readonly DBContext _context;

        public CustQuotaCompOpService()
        {
            _context = new DBContext();
        }

        public void SaveComp(CS_CustQuotasComponent comp)
        {
            _context.CS_CustQuotasComponent.Add(comp);
            _context.SaveChanges();
        }

        public void SaveOp(CS_CustQuotasOperation op)
        {
            _context.CS_CustQuotasOperation.Add(op);
            _context.SaveChanges();
        }

        public int? GetMaxLineComp()
        {
            try
            {
                Nullable<int> line = _context.Database.SqlQuery<int>(
                    "SELECT ISNULL(MAX(Line), 0) + 1 FROM CS_CustQuotasComponent"
                    ).FirstOrDefault();
                return line;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int? GetMaxLineOp()
        {
            Nullable<int> line = _context.Database.SqlQuery<int>(
                   "SELECT ISNULL(MAX(Line), 0) + 1 FROM CS_CustQuotasOperation"
                   ).FirstOrDefault();
            return line;
        }
    }
}
