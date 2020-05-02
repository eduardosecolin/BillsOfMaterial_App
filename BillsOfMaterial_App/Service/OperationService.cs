using BillsOfMaterial_App.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfMaterial_App.Service
{
    public class OperationService
    {
        private readonly DBContext _context;
        SqlConnection conn;

        public OperationService()
        {
            _context = new DBContext();
            conn = new SqlConnection(_context.Database.Connection.ConnectionString);
        }

        public List<MA_Operations> GetAll()
        {
            return _context.MA_Operations.ToList();
        }

        public List<MA_Operations> GetAll(string op)
        {
            return _context.MA_Operations.Where(x => x.Operation.Contains(op)).ToList();
        }

        public string GetOperationNotes(string op)
        {
            var sql = _context.MA_Operations.Where(x => x.Operation == op).FirstOrDefault();
            return (string.IsNullOrEmpty(sql.Notes)) ? string.Empty : sql.Notes;
        }

        public string GetDescriptionOp(string op)
        {
            var sql = _context.MA_Operations.Where(x => x.Operation == op).FirstOrDefault();
            return sql.Description;
        }

        public int? GetWorkTimeOperation(string op)
        {
            var sql = _context.MA_OperationsLabour.Where(x => x.Operation == op).FirstOrDefault();
            if(sql != null)
            {
                return sql.WorkingTime;
            }
            else
            {
                return 0;
            }
        }

        public List<MA_OperationsLabour> GetWorkersOperation(string op)
        {
            return _context.MA_OperationsLabour.Where(x => x.Operation == op).ToList();
 
        }

        public double? GetOpTotalCost(string op)
        {
            var sql = _context.MA_Operations.Where(x => x.Operation == op).FirstOrDefault();
            if(sql != null)
            {
                return sql.AdditionalCost;
            }
            else
            {
                return 0;
            }
        }

        public bool IsCdt(string op)
        {
            try
            {
                string sql = $"SELECT IsWC FROM MA_Operations WHERE Operation = '{ op }'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                bool valor = false;
                var result = cmd.ExecuteScalar();
                if(result != null)
                {
                    if(Convert.ToInt32(result.ToString()) == 1)
                    {
                        valor = true;
                    }
                }
                conn.Close();
                return valor;

            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        public bool isTercExteranlCDT(string WC)
        {
            try
            {
                string sql = $"SELECT Outsourced FROM MA_WorkCenters WHERE WC = '{ WC }'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                bool valor = false;
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    if (Convert.ToInt32(result.ToString()) == 1)
                    {
                        valor = true;
                    }
                }
                conn.Close();
                return valor;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        public bool isTercExteranlFamily(string WC)
        {
            try
            {
                string sql = @"SELECT TOP 1
	                                W.Outsourced
                                FROM 
	                                MA_Operations O
	                                LEFT OUTER JOIN MA_WCFamilies F ON F.WCFamily = O.WC
	                                LEFT OUTER JOIN MA_WCFamiliesDetails D ON D.WCFamily = F.WCFamily
	                                LEFT OUTER JOIN MA_WorkCenters W ON W.WC = D.WC
                                WHERE
	                                W.Outsourced = 1
	                                AND F.WCFamily = '" + WC + "'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                var result = cmd.ExecuteScalar();
                bool valor = false;
                if (result != null)
                {
                    if (Convert.ToInt32(result.ToString()) == 1)
                    {
                        valor = true;
                    }
                }
                conn.Close();
                return valor;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }


    }
}
