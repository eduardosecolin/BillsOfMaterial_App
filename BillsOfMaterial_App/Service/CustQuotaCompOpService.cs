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
        SqlConnection conn;

        public CustQuotaCompOpService()
        {
            _context = new DBContext();
            conn = new SqlConnection(_context.Database.Connection.ConnectionString);
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

        public int GetMaxLineComp()
        {
            try
            {
                conn.Open();
                string sql = @"SELECT ISNULL(MAX(Line), 0) + 1 FROM CS_CustQuotasComponent";
                SqlCommand cmd = new SqlCommand(sql, conn);
                var result = cmd.ExecuteScalar();
                int line = 0;
                if(result != null)
                {
                    line = Convert.ToInt32(result.ToString());
                }
                else
                {
                    line = 0 + 1;
                }
                conn.Close();
                return line;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetMaxLineOp()
        {
            try
            {
                conn.Open();
                string sql = @"SELECT ISNULL(MAX(Line), 0) + 1 FROM CS_CustQuotasOperation";
                SqlCommand cmd = new SqlCommand(sql, conn);
                var result = cmd.ExecuteScalar();
                int line = 0;
                if (result != null)
                {
                    line = Convert.ToInt32(result.ToString());
                }
                else
                {
                    line = 0 + 1;
                }
                conn.Close();
                return line;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<CS_CustQuotasComponent> GetSimulationComponents(int id, string item)
        {

            return _context.CS_CustQuotasComponent.Where(x => x.Id == id && x.Item == item).ToList(); ;

        }

        public List<CS_CustQuotasOperation> GetSimulationOperations(int id, string item)
        {
            return _context.CS_CustQuotasOperation.Where(x => x.Id == id && x.Item == item).ToList();
        }

        public void Delete(int id, string item)
        {
            List<CS_CustQuotasComponent> listComp = GetSimulationComponents(id, item);
            List<CS_CustQuotasOperation> listOp = GetSimulationOperations(id, item);

            foreach (var comp in listComp)
            {
                _context.CS_CustQuotasComponent.Remove(comp);
            }

            foreach (var op in listOp)
            {
                _context.CS_CustQuotasOperation.Remove(op);
            }
        }


    }
}
