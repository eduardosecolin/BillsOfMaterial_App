using BillsOfMaterial_App.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public short? GetMaxLineComp()
        {
            try
            {
                Nullable<short> line = _context.Database.SqlQuery<short>(
                    "SELECT ISNULL(MAX(Line), 0) + 1 FROM CS_CustQuotasComponent"
                    ).FirstOrDefault();
                return line;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public short? GetMaxLineOp()
        {
            Nullable<short> line = _context.Database.SqlQuery<short>(
                   "SELECT ISNULL(MAX(Line), 0) + 1 FROM CS_CustQuotasOperation"
                   ).FirstOrDefault();
            return line;
        }

        public List<CS_CustQuotasComponent> GetSimulationComponents(int id, string item)
        {

            return _context.CS_CustQuotasComponent.Where(x => x.Id == id && x.Item == item).ToList(); ;

        }

        public List<CS_CustQuotasOperation> GetSimulationOperations(int id, string item)
        {
            return _context.CS_CustQuotasOperation.Where(x => x.Id == id && x.Item == item).ToList();
        }
    }
}
