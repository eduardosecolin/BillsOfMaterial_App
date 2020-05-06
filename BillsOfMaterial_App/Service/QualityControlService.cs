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
    public class QualityControlService
    {
        private readonly DBContext _context;
        SqlConnection conn;

        public QualityControlService()
        {
            _context = new DBContext();
            conn = new SqlConnection(_context.Database.Connection.ConnectionString);
        }

        private bool HasRowsQualityTB()
        {
            try
            {
                conn.Open();
                string sql = "SELECT * FROM CS_ItemsAnalysisParameters";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
                
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        public List<CS_ItemsAnalysisParameters> GetAll(int idsimulation, string item)
        {
            try
            {
                bool hasRow = HasRowsQualityTB();
                if (hasRow)
                {
                    List<CS_ItemsAnalysisParameters> list = new List<CS_ItemsAnalysisParameters>();
                    conn.Open();
                    string sql = $"SELECT * FROM CS_ItemsAnalysisParameters WHERE IdSimulation = { idsimulation } AND Item = '{ item }'";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            CS_ItemsAnalysisParameters iap = new CS_ItemsAnalysisParameters();
                            iap.Item = reader["Item"].ToString().Trim();
                            iap.Line = (short)Convert.ToInt32(reader["Line"]);
                            iap.Parameter = reader["Parameter"].ToString().Trim();
                            iap.UoM = reader["UoM"].ToString().Trim();
                            iap.ExpectedNumResult = Convert.ToDouble(reader["ExpectedNumResult"]);
                            iap.AnalysisMethod = reader["AnalysisMethod"].ToString().Trim();
                            iap.AnalysisArea = Convert.ToInt32(reader["AnalysisArea"]);
                            iap.LowerBound = reader["LowerBound"].ToString().Trim();
                            iap.UpperBound = reader["UpperBound"].ToString().Trim();
                            iap.DetectableBound = reader["DetectableBound"].ToString().Trim();
                            iap.Revision = Convert.ToDouble(reader["Revision"]);
                            iap.IdSimulation = Convert.ToInt32(reader["IdSimulation"]);
                            iap.InsertionDate = Convert.ToDateTime(reader["InsertionDate"]);

                            list.Add(iap);
                        }
                        conn.Close();
                        return list;
                    }
                    else
                    {
                        return null;
                    }
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

        public List<string> GetParametersQA()
        {
            try
            {
                conn.Open();
                List<string> list = new List<string>();
                string sql = "SELECT Parameter FROM MA_QltCtrlParameters";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(reader["Parameter"].ToString().Trim());
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

        public List<string> GetAnalysisQA()
        {
            try
            {
                conn.Open();
                List<string> list = new List<string>();
                string sql = "SELECT AnalysisMet FROM MA_QltCtrlAnalMet";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(reader["AnalysisMet"].ToString().Trim());
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

        public string GetUoM(string parameter)
        {
            try
            {
                conn.Open();
                string sql = $"SELECT UoM FROM MA_QltCtrlParameters WHERE Parameter = '{ parameter }'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                var result = cmd.ExecuteScalar();
                if(result != null)
                {
                    conn.Close();
                    return result.ToString();
                }
                conn.Close();
                return string.Empty;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        public string GetAnalysisDescription(string analysis)
        {
            try
            {
                conn.Open();
                string sql = $"SELECT Description FROM MA_QltCtrlAnalMet WHERE AnalysisMet = '{ analysis }'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    conn.Close();
                    return result.ToString();
                }
                conn.Close();
                return string.Empty;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        public int GetMaxLineQA()
        {
            try
            {
                conn.Open();
                string sql = @"SELECT ISNULL(MAX(Line), 0) + 1 FROM CS_ItemsAnalysisParameters";
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

        private int GetCount(int idsimulation, string item)
        {
            try
            {
                conn.Open();
                string sql = $"SELECT COUNT(*) FROM CS_ItemsAnalysisParameters WHERE IdSimulation = { idsimulation } AND Item = '{ item.Trim() }'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                var result = cmd.ExecuteScalar();
                if(result != null)
                {
                    conn.Close();
                    return Convert.ToInt32(result.ToString());
                }
                else
                {
                    conn.Close();
                    return 0;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        private void InsertData(CS_ItemsAnalysisParameters IAP)
        {
            try
            {
                int line = GetMaxLineQA();
                conn.Open();
                string sql = @"INSERT INTO CS_ItemsAnalysisParameters
                                (Item
                                ,Line
                                ,UoM
                                ,Parameter
                                ,AnalysisMethod
                                ,AnalysisArea
                                ,UpperBound
                                ,LowerBound
                                ,DetectableBound
                                ,ExpectedNumResult
                                ,ExpectedResult
                                ,ToBePrinted
                                ,Revision
                                ,DisabledDate
                                ,InsertionDate
                                ,Customer
                                ,Notes
                                ,Disabled
                                ,IdSimulation
                                ,TBCreated
                                ,TBModified
                                ,TBCreatedID
                                ,TBModifiedID
                                ,TBGuid) " +  
                             $"VALUES ('{ IAP.Item }'," +
                             $"{ line }," +
                             $"'{ IAP.UoM }'," +
                             $"'{ IAP.Parameter }'," +
                             $"'{ IAP.AnalysisMethod }'," +
                             $"{ IAP.AnalysisArea }," +
                             $"'{ IAP.UpperBound }'," +
                             $"'{ IAP.LowerBound }'," +
                             $"'{ IAP.DetectableBound }'," +
                             $"{ IAP.ExpectedNumResult.ToString().Replace(",", ".") }," +
                             $"'{ IAP.ExpectedResult }'," +
                             $"'{ IAP.ToBePrinted }'," +
                             $"{ IAP.Revision.ToString().Replace(",", ".") }," +
                             $"'{ IAP.DisabledDate }'," +
                             $"'{ IAP.InsertionDate }'," +
                             $"'{ IAP.Customer }'," +
                             $"'{ IAP.Notes }'," +
                             $"'{ IAP.Disabled }'," +
                             $"{ IAP.IdSimulation }," +
                             $"'{ DateTime.Now }'," +
                             $"'{ DateTime.Now }'," +
                             $"{ 1 }," +
                             $"{ 1 }," +
                             $"'{ Guid.NewGuid() }')";

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

        private void UpdatetData(CS_ItemsAnalysisParameters IAP)
        {
            try
            {
                conn.Open();
                string sql = @"UPDATE CS_ItemsAnalysisParameters SET " + 
                                $" UoM = '{ IAP.UoM }' " +
                                $",Parameter = '{ IAP.Parameter }' " +
                                $",AnalysisMethod = '{ IAP.AnalysisMethod }' " +
                                $",AnalysisArea = { IAP.AnalysisArea } " +
                                $",UpperBound = '{ IAP.UpperBound }' " +
                                $",LowerBound = '{ IAP.LowerBound }' " +
                                $",DetectableBound = '{ IAP.DetectableBound }' " +
                                $",ExpectedNumResult = { IAP.ExpectedNumResult } " +
                                $",ExpectedResult = '{ IAP.ExpectedResult }' " +
                                $",Revision = { IAP.Revision } " +
                                $",InsertionDate = '{ IAP.InsertionDate }' " +
                                $",TBCreated = '{ DateTime.Now }' " +
                                $",TBModified = '{ DateTime.Now }' WHERE IdSimulation = { IAP.IdSimulation } AND Item = '{ IAP.Item }' AND Line = { IAP.Line }";

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

        public void InsertAllDataWithList()
        {
            try
            {
                List<List<CS_ItemsAnalysisParameters>> list = ItemsView.itemsAnalysisParametersSuperList;
                if(list.Count > 0)
                {
                    foreach(var superlist in list)
                    {
                        int cont = 1;
                        foreach(var item in superlist)
                        {
                            int getCount = GetCount((int)item.IdSimulation, item.Item);
                            if (getCount == 0)
                            {
                                InsertData(item);
                            }else if(getCount > 0 && cont <= getCount)
                            {
                                UpdatetData(item);
                            }
                            else
                            {
                                InsertData(item);
                            }
                            cont++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
