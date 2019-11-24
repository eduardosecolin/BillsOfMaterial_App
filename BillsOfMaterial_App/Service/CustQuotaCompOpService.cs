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

        public List<CS_CustQuotasComponent> GetSimulationComponents2(int id, string item)
        {

            return _context.CS_CustQuotasComponent.Where(x => x.Id == id && x.BOM == item).ToList(); ;

        }

        public List<CS_CustQuotasOperation> GetSimulationOperations2(int id, string item)
        {
            //return _context.CS_CustQuotasOperation.Where(x => x.Id == id && x.BOM == item).ToList();
            try
            {
                List<CS_CustQuotasOperation> listOp = new List<CS_CustQuotasOperation>();
                conn.Open();
                string sql = $"SELECT * FROM CS_CustQuotasOperation WHERE id = { id } AND BOM = '{ item }'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CS_CustQuotasOperation op = new CS_CustQuotasOperation();
                    op.Id = Convert.ToInt32(reader["Id"]);
                    op.Line = Convert.ToInt32(reader["Line"]);
                    op.Operation = reader["Operation"].ToString().Trim();
                    op.DescriptionOperation = reader["DescriptionOperation"].ToString().Trim();
                    op.TimeProcess = Convert.ToDateTime(reader["TimeProcess"]);
                    op.Item = reader["Item"].ToString().Trim();
                    op.BOM = reader["BOM"].ToString().Trim();
                    op.Obs = reader["Obs"].ToString().Trim();
                    listOp.Add(op);
                }
                conn.Close();
                return listOp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int? GetIdFromSimulationCompBOM(string item)
        {
            var sql = _context.CS_CustQuotasComponent.Where(x => x.Item == item).FirstOrDefault();
            if (sql != null)
            {
                return sql.Id;
            }
            else
            {
                return null;
            }
        }

        public int? GetIdFromSimulationOpBOM(string item)
        {
            var sql = _context.CS_CustQuotasOperation.Where(x => x.Item == item).FirstOrDefault();
            if (sql != null)
            {
                return sql.Id;
            }
            else
            {
                return null;
            }
        }

        public DateTime? GetTBCreatedFromSimulationCompBOM(string item)
        {
            var sql = _context.CS_CustQuotasComponent.Where(x => x.Item == item).FirstOrDefault();
            if (sql != null)
            {
                return sql.TBCreated;
            }
            else
            {
                return null;
            }
        }

        public DateTime? GetTBCreatedFromSimulationOpBOM(string item)
        {
            var sql = _context.CS_CustQuotasOperation.Where(x => x.Item == item).FirstOrDefault();
            if (sql != null)
            {
                return sql.TBCreated;
            }
            else
            {
                return null;
            }
        }

        public DateTime? GetTBModifiedFromSimulationCompBOM(string item)
        {
            var sql = _context.CS_CustQuotasComponent.Where(x => x.Item == item).FirstOrDefault();
            if (sql != null)
            {
                return sql.TBModified;
            }
            else
            {
                return null;
            }
        }

        public DateTime? GetTBModifiedFromSimulationOpBOM(string item)
        {
            var sql = _context.CS_CustQuotasOperation.Where(x => x.Item == item).FirstOrDefault();
            if (sql != null)
            {
                return sql.TBModified;
            }
            else
            {
                return null;
            }
        }

        public List<string> GetDistinctItemBOM()
        {
            List<string> list = new List<string>();
            if (_context.CS_CustQuotasComponent.Any())
            {
                var sql = _context.CS_CustQuotasComponent.Select(x => x.Item).Distinct().ToList();
                if (sql != null)
                {
                    foreach (var item in sql)
                    {
                        list.Add(item);
                    }

                    return list;
                }
            }

            return list;
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

        public bool ExistData(int id, string item)
        {
            var sql = _context.CS_CustQuotasComponent.Any(x => x.Id == id && x.Item == item);
            if (sql)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
