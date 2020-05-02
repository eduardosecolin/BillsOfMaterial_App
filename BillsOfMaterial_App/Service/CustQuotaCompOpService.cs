using BillsOfMaterial_App.Model;
using BillsOfMaterial_App.View;
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

        public void SaveComp2(CS_CustQuotasComponent comp)
        {
            try
            {
                comp.R1Costvalue = string.IsNullOrEmpty(comp.R1Costvalue.ToString()) ? 0 : comp.R1Costvalue;
                string sql = @"INSERT INTO CS_CustQuotasComponent (
                    Id, Line, BOM, Item, Component, Description, UoM, Qty, Obs, Drawing, 
                    R1CostValue, PathFile1, PathFile2, PathFile3, TBCreated, TBModified, 
                    TBCreatedID, TecConclusion2, DrawingComponent, CostValue
                )" + $" VALUES ( {comp.Id}, {comp.Line}, '{comp.BOM}', '{comp.Item}', '{comp.Component}'," +
                $"'{comp.Description}', '{comp.UoM}', {comp.Qty.ToString().Replace(",", ".")}, '{comp.Obs}'," +
                $"'{comp.Drawing}', {comp.R1Costvalue.ToString().Replace(",", ".")}, '{comp.PathFile1}'," +
                $"'{comp.PathFile2}', '{comp.PathFile3}', '{comp.TBCreated}', '{comp.TBModified}', {comp.TBCreatedID}," +
                $"'{comp.TecConclusion2}', '{comp.DrawingComponent}', {comp.Costvalue.ToString().Replace(",", ".")})";

                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        public void SaveOp(CS_CustQuotasOperation op)
        {
            _context.CS_CustQuotasOperation.Add(op);
            _context.SaveChanges();
        }

        public void SaveOp2(CS_CustQuotasOperation op)
        {
            try
            {
                op.CostOperation = string.IsNullOrEmpty(op.CostOperation.ToString()) ? 0 : op.CostOperation;
                string sql = @"INSERT INTO CS_CustQuotasOperation (
                    Id, Line, Operation, DescriptionOperation, TimeProcessStr, Item, BOM, Obs, 
                    TBCreated, TBModified, TBCreatedID, CostOperation, UoM, Qty
                )" + $" VALUES ( {op.Id}, {op.Line}, '{op.Operation}', '{op.DescriptionOperation}', '{op.TimeProcessStr}'," +
                $"'{op.Item}', '{op.BOM}', '{op.Obs}', '{op.TBCreated}'," +
                $"'{op.TBModified}', {op.TBCreatedID}, {op.CostOperation.ToString().Replace(",", ".")}, '{ op.UoM }', { op.Qty })"; 

                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
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
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
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
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        public List<CS_CustQuotasComponent> GetSimulationComponents(int id, string item)
        {

            //return _context.CS_CustQuotasComponent.Where(x => x.Id == id && x.Item == item).ToList();
            List<CS_CustQuotasComponent> list = new List<CS_CustQuotasComponent>();
            try
            {
                conn.Open();
                string sql = $"SELECT * FROM CS_CustQuotasComponent WHERE Id = { id } AND Item = '{ item }'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CS_CustQuotasComponent comp = new CS_CustQuotasComponent();
                    comp.Id = Convert.ToInt32(reader["Id"]);
                    comp.Line = Convert.ToInt32(reader["Line"]);
                    comp.Item = reader["Item"].ToString();
                    comp.Obs = reader["Obs"].ToString();
                    comp.Component = reader["Component"].ToString();
                    comp.Costvalue = string.IsNullOrEmpty(reader["Costvalue"].ToString()) ? 0 : Convert.ToDouble(reader["Costvalue"]);
                    comp.BOM = reader["BOM"].ToString();
                    comp.Description = reader["Description"].ToString();
                    comp.Drawing = reader["Drawing"].ToString();
                    comp.DrawingComponent = reader["DrawingComponent"].ToString();
                    comp.PathFile1 = reader["PathFile1"].ToString();
                    comp.PathFile2 = reader["PathFile2"].ToString();
                    comp.PathFile3 = reader["PathFile2"].ToString();
                    comp.Qty = string.IsNullOrEmpty(reader["Qty"].ToString()) ? 0 : Convert.ToDouble(reader["Qty"]);
                    comp.R1Costvalue = string.IsNullOrEmpty(reader["R1Costvalue"].ToString()) ? 0 : Convert.ToDouble(reader["R1Costvalue"]);
                    comp.TecConclusion = reader["TecConclusion"].ToString();
                    comp.TecConclusion2 = reader["TecConclusion2"].ToString();
                    comp.UoM = reader["UoM"].ToString();

                    list.Add(comp);
                }

                conn.Close();
                reader.Close();
                return list;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }

        }

        public List<CS_CustQuotasOperation> GetSimulationOperations(int id, string item)
        {
            //return _context.CS_CustQuotasOperation.Where(x => x.Id == id && x.Item == item).ToList();
            List<CS_CustQuotasOperation> list = new List<CS_CustQuotasOperation>();
            try
            {
                conn.Open();
                string sql = $"SELECT * FROM CS_CustQuotasOperation WHERE Id = { id } AND Item = '{ item }'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CS_CustQuotasOperation op = new CS_CustQuotasOperation();
                    op.Id = Convert.ToInt32(reader["Id"]);
                    op.Line = Convert.ToInt32(reader["Line"]);
                    op.Item = reader["Item"].ToString();
                    op.Obs = reader["Obs"].ToString();
                    op.Operation = reader["Operation"].ToString();
                    op.TimeProcess = string.IsNullOrEmpty(reader["TimeProcess"].ToString()) ? Convert.ToDateTime(DateTime.Now.ToShortDateString()) : Convert.ToDateTime(reader["TimeProcess"]);
                    op.BOM = reader["BOM"].ToString();
                    op.CostOperation = string.IsNullOrEmpty(reader["CostOperation"].ToString()) ? 0 : Convert.ToDouble(reader["CostOperation"]);

                    list.Add(op);
                }

                conn.Close();
                reader.Close();
                return list;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        public List<CS_CustQuotasComponent> GetSimulationComponents2(int id, string item)
        {

            //return _context.CS_CustQuotasComponent.Where(x => x.Id == id && x.BOM == item).ToList(); ;
            List<CS_CustQuotasComponent> list = new List<CS_CustQuotasComponent>();
            try
            {
                conn.Open();
                string sql = $"SELECT * FROM CS_CustQuotasComponent WHERE Id = { id } AND BOM = '{ item }'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CS_CustQuotasComponent comp = new CS_CustQuotasComponent();
                    comp.Id = Convert.ToInt32(reader["Id"]);
                    comp.Line = Convert.ToInt32(reader["Line"]);
                    comp.Item = reader["Item"].ToString();
                    comp.Obs = reader["Obs"].ToString();
                    comp.Component = reader["Component"].ToString();
                    comp.Costvalue = string.IsNullOrEmpty(reader["Costvalue"].ToString()) ? 0 : Convert.ToDouble(reader["Costvalue"]);
                    comp.BOM = reader["BOM"].ToString();
                    comp.Description = reader["Description"].ToString();
                    comp.Drawing = reader["Drawing"].ToString();
                    comp.DrawingComponent = reader["DrawingComponent"].ToString();
                    comp.PathFile1 = reader["PathFile1"].ToString();
                    comp.PathFile2 = reader["PathFile2"].ToString();
                    comp.PathFile3 = reader["PathFile2"].ToString();
                    comp.Qty = string.IsNullOrEmpty(reader["Qty"].ToString()) ? 0 : Convert.ToDouble(reader["Qty"]);
                    comp.R1Costvalue = string.IsNullOrEmpty(reader["R1Costvalue"].ToString()) ? 0 : Convert.ToDouble(reader["R1Costvalue"]);
                    comp.TecConclusion = reader["TecConclusion"].ToString();
                    comp.TecConclusion2 = reader["TecConclusion2"].ToString();
                    comp.UoM = reader["UoM"].ToString();

                    list.Add(comp);
                }

                conn.Close();
                reader.Close();
                return list;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        public List<CS_CustQuotasComponent> GetSimulationComponents2_2(int id, string item)
        {

            //return _context.CS_CustQuotasComponent.Where(x => x.Id == id && x.BOM == item).ToList(); ;
            List<CS_CustQuotasComponent> list = new List<CS_CustQuotasComponent>();
            try
            {
                conn.Open();
                string sql = $"SELECT *, ISNULL((SELECT ISNULL(StandardCost, 0) FROM MA_ItemsFiscalYearData WHERE Item = CQ.Component AND CQ.Component NOT LIKE '%OUTROS%' AND FiscalYear = DATEPART(YEAR, GETDATE())) * CQ.Qty, 0) AS Cost FROM CS_CustQuotasComponent CQ WHERE Id = { id } AND BOM = '{ item }'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CS_CustQuotasComponent comp = new CS_CustQuotasComponent();
                    comp.Id = Convert.ToInt32(reader["Id"]);
                    comp.Line = Convert.ToInt32(reader["Line"]);
                    comp.Item = reader["Item"].ToString();
                    comp.Obs = reader["Obs"].ToString();
                    comp.Component = reader["Component"].ToString();
                    if(reader["Component"].ToString().Equals("OUTROS", StringComparison.OrdinalIgnoreCase))
                    {
                        comp.Costvalue = string.IsNullOrEmpty(reader["CostValue"].ToString()) ? 0 : Convert.ToDouble(reader["CostValue"]);
                    }
                    else
                    {
                        comp.Costvalue = string.IsNullOrEmpty(reader["Cost"].ToString()) ? 0 : Convert.ToDouble(reader["Cost"]);
                    }
                    comp.BOM = reader["BOM"].ToString();
                    comp.Description = reader["Description"].ToString();
                    comp.Drawing = reader["Drawing"].ToString();
                    comp.DrawingComponent = reader["DrawingComponent"].ToString();
                    comp.PathFile1 = reader["PathFile1"].ToString();
                    comp.PathFile2 = reader["PathFile2"].ToString();
                    comp.PathFile3 = reader["PathFile2"].ToString();
                    comp.Qty = string.IsNullOrEmpty(reader["Qty"].ToString()) ? 0 : Convert.ToDouble(reader["Qty"]);
                    comp.R1Costvalue = string.IsNullOrEmpty(reader["R1Costvalue"].ToString()) ? 0 : Convert.ToDouble(reader["R1Costvalue"]);
                    comp.TecConclusion = reader["TecConclusion"].ToString();
                    comp.TecConclusion2 = reader["TecConclusion2"].ToString();
                    comp.UoM = reader["UoM"].ToString();

                    list.Add(comp);
                }

                conn.Close();
                reader.Close();
                return list;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
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
                    op.CostOperation = string.IsNullOrEmpty(reader["CostOperation"].ToString()) ? 0 : Convert.ToDouble(reader["CostOperation"]);
                    op.TimeProcessStr = reader["TimeProcessStr"].ToString();
                    op.UoM = reader["UoM"].ToString().Trim();
                    op.Qty = string.IsNullOrEmpty(reader["Qty"].ToString()) ? 0 : Convert.ToDouble(reader["Qty"]);
                    listOp.Add(op);
                }
                conn.Close();
                return listOp;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        public List<CS_CustQuotasOperation> GetSimulationOperations2_2(int id, string item)
        {
            //return _context.CS_CustQuotasOperation.Where(x => x.Id == id && x.BOM == item).ToList();
            try
            {
                List<CS_CustQuotasOperation> listOp = new List<CS_CustQuotasOperation>();
                conn.Open();
                string sql = $"SELECT *, (SELECT ISNULL(AdditionalCost, 0) FROM MA_Operations WHERE Operation = CQ.Operation) AS Cost FROM CS_CustQuotasOperation CQ WHERE id = { id } AND BOM = '{ item }'";
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
                    op.TimeProcessStr = reader["TimeProcessStr"].ToString();
                    double valor = OperationView.CalculateCostOperation(reader["TimeProcessStr"].ToString(), Convert.ToDouble(reader["Cost"]));
                    op.CostOperation = Math.Round(valor, 2);
                    op.UoM = reader["UoM"].ToString().Trim();
                    op.Qty = string.IsNullOrEmpty(reader["Qty"].ToString()) ? 0 : Convert.ToDouble(reader["Qty"]);
                    listOp.Add(op);
                }
                conn.Close();
                return listOp;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        public int? GetIdFromSimulationCompBOM(string item)
        {
            try
            {
                conn.Open();
                string sql = $"SELECT ISNULL(Id, 0) AS Id FROM CS_CustQuotasComponent WHERE Item = '{ item }'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                var result = cmd.ExecuteScalar();
                int line = 0;
                if (result != null)
                {
                    line = Convert.ToInt32(result.ToString());
                }
                else
                {
                    line = 0;
                }
                conn.Close();
                return line;

            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        public int? GetIdFromSimulationOpBOM(string item)
        {
            try
            {
                conn.Open();
                string sql = $"SELECT ISNULL(Id, 0) AS Id FROM CS_CustQuotasOperation WHERE Item = '{ item }'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                var result = cmd.ExecuteScalar();
                int line = 0;
                if (result != null)
                {
                    line = Convert.ToInt32(result.ToString());
                }
                else
                {
                    line = 0;
                }
                conn.Close();
                return line;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        public DateTime? GetTBCreatedFromSimulationCompBOM(string item)
        {
            try{
                conn.Open();
                string sql = $"SELECT TBCreated FROM CS_CustQuotasComponent WHERE Item = '{ item }'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    conn.Close();
                    return Convert.ToDateTime(result.ToString());
                }
                else
                {
                    conn.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        public DateTime? GetTBCreatedFromSimulationOpBOM(string item)
        {
            try
            {
                conn.Open();
                string sql = $"SELECT TBCreated FROM CS_CustQuotasOperation WHERE Item = '{ item }'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    conn.Close();
                    return Convert.ToDateTime(result.ToString());
                }
                else
                {
                    conn.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        public DateTime? GetTBModifiedFromSimulationCompBOM(string item)
        {
            try
            {
                conn.Open();
                string sql = $"SELECT TBModified FROM CS_CustQuotasComponent WHERE Item = '{ item }'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                var result = cmd.ExecuteScalar();
                if(result != null)
                {
                    conn.Close();
                    return Convert.ToDateTime(result.ToString());
                }
                else
                {
                    conn.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        public DateTime? GetTBModifiedFromSimulationOpBOM(string item)
        {
            try
            {
                conn.Open();
                string sql = $"SELECT TBModified FROM CS_CustQuotasOperation WHERE Item = '{ item }'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    conn.Close();
                    return Convert.ToDateTime(result.ToString());
                }
                else
                {
                    conn.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        public List<string> GetDistinctItemBOM()
        {
            List<string> list = new List<string>();
            try
            {
                conn.Open();
                string sql = $"SELECT DISTINCT Item FROM CS_CustQuotasComponent";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(reader["Item"].ToString());
                }
                conn.Close();
                reader.Close();

                return list;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        public void Delete(int id, string item)
        {
            try
            {
                conn.Open();
                string sql = $"DELETE FROM CS_CustQuotasComponent WHERE Id = { id } AND Item = '{ item }'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                sql = $"DELETE FROM CS_CustQuotasOperation WHERE Id = { id } AND Item = '{ item }'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
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

        public double GetTotalComponents(int id, string item, string bom)
        {
            try
            {
                double result2 = 0;
                conn.Open();
                string sql = $"SELECT ISNULL(SUM(costvalue), 0) FROM CS_CustQuotasComponent WHERE Id = { id } AND Item = '{ item }' AND BOM = '{ bom }'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                var result = cmd.ExecuteScalar();
                if(result != null)
                {
                    result2 = Convert.ToDouble(result.ToString());
                }
                conn.Close();
                return result2;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        public double GetNCMPartyRate(int id, string item)
        {
            try
            {
                conn.Open();
                string sql = @"SELECT TOP 1
	                                ISNULL(R.TaxRate, 0)
                                FROM
	                                CS_CustQuotasComponent C
	                                LEFT OUTER JOIN MA_CustQuotas CQ ON CQ.CustQuotaId = C.Id
	                                LEFT OUTER JOIN MA_CustQuotasDetail CQD ON CQD.CustQuotaId = CQ.CustQuotaId
	                                LEFT OUTER JOIN MA_Items I ON I.Item = CQD.Item
	                                LEFT OUTER JOIN MA_BRNCM N ON N.NCM = I.NCMParty
	                                LEFT OUTER JOIN MA_BRTaxRate R ON R.TaxRateCode = N.ICMSTaxRateCode
                                WHERE 
	                                C.BOM = '" + item + "'" +
	                                " AND C.Id = " + id;
                SqlCommand cmd = new SqlCommand(sql, conn);
                var result = cmd.ExecuteScalar();
                double valor = 0;
                if (result != null)
                {
                    valor = Convert.ToDouble(result.ToString());
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

        public double GetTotalComponentsR1(int id, string item, string bom)
        {
            try
            {
                double result2 = 0;
                conn.Open();
                string sql = $"SELECT ISNULL(SUM(R1Costvalue), 0) FROM CS_CustQuotasComponent WHERE Id = { id } AND Item = '{ item }' AND BOM = '{ bom }'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    result2 = Convert.ToDouble(result.ToString());
                }
                conn.Close();
                return result2;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        public double GetTotalOperations(int id, string item, string bom)
        {
            try
            {
                double result2 = 0;
                conn.Open();
                string sql = $"SELECT ISNULL(SUM(CostOperation), 0) FROM CS_CustQuotasOperation WHERE id = { id } AND Item = '{ item }' AND BOM = '{ bom }'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    result2 = Convert.ToDouble(result.ToString());
                }
                conn.Close();
                return Math.Round(result2, 2);
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        public List<string> GetBOMComponents(int id, string item)
        {
            try
            {
                List<string> list = new List<string>();
                conn.Open();
                string sql = $"SELECT DISTINCT BOM FROM CS_CustQuotasComponent WHERE id = { id } AND Item = '{ item }'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(reader["BOM"].ToString());
                }
                conn.Close();
                reader.Close();
                return list;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        public double GetBOMComponentsServices(int id, string item)
        {
            try
            {
                conn.Open();
                string sql = @"SELECT 
	                                ISNULL(SUM(C.CostValue), 0) AS CostValue
                                FROM 
	                                CS_CustQuotasComponent C
	                                LEFT OUTER JOIN MA_Items I ON I.Item = C.Component
                                WHERE 
	                                C.BOM = '" + item + "'" +
	                                @" AND I.Nature = 22413314
                                    AND C.Id = " + id;
                SqlCommand cmd = new SqlCommand(sql, conn);
                var result = cmd.ExecuteScalar();
                double valor = 0;
                if (result != null)
                {
                    valor = Convert.ToDouble(result.ToString());
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

        public List<CS_CustQuotasComponent> GetAllSimulations()
        {

            //return _context.CS_CustQuotasComponent.Where(x => x.Id == id && x.Item == item).ToList();
            List<CS_CustQuotasComponent> list = new List<CS_CustQuotasComponent>();
            try
            {
                conn.Open();
                string sql = $"SELECT * FROM CS_CustQuotasComponent";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CS_CustQuotasComponent comp = new CS_CustQuotasComponent();
                    comp.Id = Convert.ToInt32(reader["Id"]);
                    comp.Line = Convert.ToInt32(reader["Line"]);
                    comp.Item = reader["Item"].ToString();
                    comp.Obs = reader["Obs"].ToString();
                    comp.Component = reader["Component"].ToString();
                    comp.Costvalue = string.IsNullOrEmpty(reader["Costvalue"].ToString()) ? 0 : Convert.ToDouble(reader["Costvalue"]);
                    comp.BOM = reader["BOM"].ToString();
                    comp.Description = reader["Description"].ToString();
                    comp.Drawing = reader["Drawing"].ToString();
                    comp.DrawingComponent = reader["DrawingComponent"].ToString();
                    comp.PathFile1 = reader["PathFile1"].ToString();
                    comp.PathFile2 = reader["PathFile2"].ToString();
                    comp.PathFile3 = reader["PathFile2"].ToString();
                    comp.Qty = string.IsNullOrEmpty(reader["Qty"].ToString()) ? 0 : Convert.ToDouble(reader["Qty"]);
                    comp.R1Costvalue = string.IsNullOrEmpty(reader["R1Costvalue"].ToString()) ? 0 : Convert.ToDouble(reader["R1Costvalue"]);
                    comp.TecConclusion = reader["TecConclusion"].ToString();
                    comp.TecConclusion2 = reader["TecConclusion2"].ToString();
                    comp.UoM = reader["UoM"].ToString();

                    list.Add(comp);
                }

                conn.Close();
                reader.Close();
                return list;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }

        }


    }
}
