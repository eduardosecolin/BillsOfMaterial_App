using BillsOfMaterial_App.AuxView;
using BillsOfMaterial_App.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfMaterial_App.Service
{
    public class TimeSheetService
    {
        private readonly DBContext _context;
        SqlConnection conn;

        public TimeSheetService()
        {
            _context = new DBContext();
            conn = new SqlConnection(_context.Database.Connection.ConnectionString);
        }

        public List<GenericObject> getOPS()
        {
            try
            {
                conn.Open();
                List<GenericObject> list = new List<GenericObject>();
                string sql = @"SELECT DISTINCT 
	                                MA_MO.MONo 
                                FROM 
	                                MA_MO
	                                LEFT OUTER JOIN MA_MOSteps ON MA_MOSteps.MOId = MA_MO.MOId
                                WHERE 
	                                MA_MOSteps.MOStatus = 20578307";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    GenericObject obj = new GenericObject();
                    obj.MONo = reader["MONo"].ToString();
                    list.Add(obj);
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

        public List<GenericObjectDetails> GetOPSDetails()
        {
            try
            {
                conn.Open();
                List<GenericObjectDetails> list = new List<GenericObjectDetails>();
                string sql2 = @"SELECT TOP 1
	                                Id
                                FROM
	                                CS_TimeSheetPROD
                                WHERE
                                    IsFinalized = 0
	                                AND StatusODP = 20578305";
                SqlCommand cmd2 = new SqlCommand(sql2, conn);
                var result = cmd2.ExecuteScalar();
                string sql = "";
                if (result != null)
                {
                    conn.Close();
                    sql = @"SELECT 
	                                M.MOId,
	                                M.MONo,
	                                S.Operation,
	                                S.RtgStep,
	                                S.MOStatus,
	                                O.Description
                                FROM 
	                                MA_MO M
	                                LEFT OUTER JOIN MA_MOSteps S ON S.MOId = M.MOId
	                                LEFT OUTER JOIN MA_Operations O ON O.Operation = S.Operation
                                WHERE
	                                S.MOStatus IN (20578305)
                                    AND M.MOId = " + Convert.ToInt32(result.ToString());
                    TimeSheetView.blockListView = true;
                }
                else
                {
                    conn.Close();
                     sql = @"SELECT 
	                                M.MOId,
	                                M.MONo,
	                                S.Operation,
	                                S.RtgStep,
	                                S.MOStatus,
	                                O.Description
                                FROM 
	                                MA_MO M
	                                LEFT OUTER JOIN MA_MOSteps S ON S.MOId = M.MOId
	                                LEFT OUTER JOIN MA_Operations O ON O.Operation = S.Operation
                                WHERE
	                                S.MOStatus IN (20578307)";
                    TimeSheetView.blockListView = false;
                }
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    GenericObjectDetails obj = new GenericObjectDetails();
                    obj.MONo = reader["MONo"].ToString();
                    obj.MOId = Convert.ToInt32(reader["MOId"]);
                    obj.Operation = reader["Operation"].ToString();
                    obj.RtgStep = Convert.ToInt32(reader["RtgStep"]);
                    obj.MOStatus = Convert.ToInt32(reader["MOStatus"]);
                    obj.Description = reader["Description"].ToString();
                    obj.Status = obj.MOStatus == 20578307 ? "Confirmada" : "Em processamento";
                    list.Add(obj);
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

        public List<CS_TimeSheetPROD> getTimeSheetOpen(int userid)
        {
            try
            {
                List<CS_TimeSheetPROD> list = new List<CS_TimeSheetPROD>();
                string sql = @"SELECT
	                                *
                                FROM
	                                CS_TimeSheetPROD
                                WHERE
	                                IsFinalized = '0'
	                                AND StartDate IS NOT NULL
                                     AND UserIdOP = " + userid;
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CS_TimeSheetPROD tsp = new CS_TimeSheetPROD();
                    tsp.Id = (int)reader["Id"];
                    tsp.Line = (int)reader["Line"];
                    tsp.MOId = (int)reader["MOId"];
                    tsp.Phase = reader["Phase"].ToString();
                    tsp.RtgStep = (int)reader["RtgStep"];
                    if (!string.IsNullOrEmpty(reader["StartDate"].ToString()))
                    {
                        tsp.StartDate = Convert.ToDateTime(reader["StartDate"]);
                    }
                    if (!string.IsNullOrEmpty(reader["EndDate"].ToString()))
                    {
                        tsp.EndDate = Convert.ToDateTime(reader["EndDate"]);
                    }
                    tsp.IsFinalized = reader["IsFinalized"].ToString();
                    tsp.StatusODP = (int)reader["StatusODP"];
                    tsp.UserIdOP = (int)reader["UserIdOP"];
                    if(Convert.ToDateTime(reader["EndDate"]).ToShortDateString() == "31/12/1799")
                    {
                        tsp.EndDate = Convert.ToDateTime("01/01/0001");
                    }
                    tsp.Status = (tsp.StatusODP == 20578305) ? "Em processamento" : "Terminada";

                    list.Add(tsp);
                }
                conn.Close();
                reader.Close();

                if(list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        item.MONo = getMONo((short)item.MOId);
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        public List<CS_TimeSheetPROD> getTimeSheetRecent(int userid)
        {
            try
            {
                List<CS_TimeSheetPROD> list = new List<CS_TimeSheetPROD>();
                string sql = @"SELECT
	                                *
                                FROM
	                                CS_TimeSheetPROD
                                WHERE
	                                StartDate >= DATEADD(day,-7, GETDATE())
                                     AND UserIdOP = " + userid;
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CS_TimeSheetPROD tsp = new CS_TimeSheetPROD();
                    tsp.Id = (int)reader["Id"];
                    tsp.Line = (int)reader["Line"];
                    tsp.MOId = (int)reader["MOId"];
                    tsp.Phase = reader["Phase"].ToString();
                    tsp.RtgStep = (int)reader["RtgStep"];
                    if (!string.IsNullOrEmpty(reader["StartDate"].ToString()))
                    {
                        tsp.StartDate = Convert.ToDateTime(reader["StartDate"]);
                    }
                    if (!string.IsNullOrEmpty(reader["EndDate"].ToString()))
                    {
                        tsp.EndDate = Convert.ToDateTime(reader["EndDate"]);
                    }
                    tsp.IsFinalized = reader["IsFinalized"].ToString();
                    tsp.StatusODP = (int)reader["StatusODP"];
                    tsp.UserIdOP = (int)reader["UserIdOP"];

                    list.Add(tsp);
                }
                conn.Close();
                reader.Close();

                if (list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        item.MONo = getMONo((short)item.MOId);
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        public List<CS_TimeSheetPROD> getTimeSheetAll(int userid)
        {
            try
            {
                List<CS_TimeSheetPROD> list = new List<CS_TimeSheetPROD>();
                string sql = @"SELECT
	                                *
                                FROM
	                                CS_TimeSheetPROD
                                WHERE
                                    UserIdOP = " + userid;
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CS_TimeSheetPROD tsp = new CS_TimeSheetPROD();
                    tsp.Id = (int)reader["Id"];
                    tsp.Line = (int)reader["Line"];
                    tsp.MOId = (int)reader["MOId"];
                    tsp.Phase = reader["Phase"].ToString();
                    tsp.RtgStep = (int)reader["RtgStep"];
                    if (!string.IsNullOrEmpty(reader["StartDate"].ToString()))
                    {
                        tsp.StartDate = Convert.ToDateTime(reader["StartDate"]);
                    }
                    if (!string.IsNullOrEmpty(reader["EndDate"].ToString()))
                    {
                        tsp.EndDate = Convert.ToDateTime(reader["EndDate"]);
                    }
                    tsp.IsFinalized = reader["IsFinalized"].ToString();
                    tsp.StatusODP = (int)reader["StatusODP"];
                    tsp.UserIdOP = (int)reader["UserIdOP"];

                    list.Add(tsp);
                }
                conn.Close();
                reader.Close();

                if (list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        item.MONo = getMONo((short)item.MOId);
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        public string getMONo(int id)
        {
            try
            {
                string sql = $"SELECT MONo FROM MA_MO WHERE MOId = { id }";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                var result = cmd.ExecuteScalar();
                if(result != null)
                {
                    conn.Close();
                    return result.ToString();
                }
                else
                {
                    conn.Close();
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        public List<string> GetPhase(int id)
        {
            try
            {
                List<string> list = new List<string>();
                string sql = $"SELECT Operation FROM MA_MOSteps WHERE MOId = { id } AND MOStatus = 20578307";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(reader["Operation"].ToString());
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

        public int GetMOId(string mono)
        {
            try
            {
                string sql = $"SELECT MOId FROM MA_MO WHERE MONo = '{ mono }'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                var result = cmd.ExecuteScalar();
                if (result != null)
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

        public int GetMaxLine()
        {
            try
            {
                string sql = "SELECT ISNULL(MAX(Line), 0) + 1 FROM CS_TimeSheetPROD";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                var result = cmd.ExecuteScalar();
                if(result != null)
                {
                    conn.Close();
                    return Convert.ToInt32(result.ToString());
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        public void Insert(CS_TimeSheetPROD obj)
        {
            try
            {
                string sql = @"INSERT INTO CS_TimeSheetPROD
                                       (Id
                                       ,Line
                                       ,MOId
                                       ,Phase
                                       ,StartDate
                                       ,StatusODP
                                       ,RtgStep
                                       ,UserIdOP
                                       ,TBCreated
                                       ,TBModified
                                       ,TBCreatedID
                                       ,TBModifiedID
                                       ,TBGuid) VALUES (" +
                                   $"{ obj.Id }," +
                                   $"{ obj.Line }," +
                                   $"{ obj.MOId }," +
                                   $"'{ obj.Phase }'," +
                                   $"'{ obj.StartDate }'," +
                                   $"{ obj.StatusODP }," +
                                   $"{ obj.RtgStep }," +
                                   $"{ obj.UserIdOP }," +
                                   $"'{ DateTime.Now }'," +
                                   $"'{ DateTime.Now }'," +
                                   $"{ 1 }," +
                                   $"{ 1 }," +
                                   $"'{ Guid.NewGuid() }')";

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

        public bool ExistRow(int status, int moid, string op, int steps)
        {
            try
            {
                string sql = $"SELECT * FROM CS_TimeSheetPROD WHERE StatusODP = { status } AND MOId = { moid } AND Phase = '{ op }' AND RtgStep = { steps }";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
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

        public void UpdateODPStausProcessing(int moid, int steps, string op)
        {
            try
            {
                string sql = $"UPDATE MA_MOSteps SET MOStatus = 20578305 WHERE MOId = { moid } AND Operation = '{ op }' AND RtgStep = { steps }";
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

        public void UpdateODPStausFinalized(int moid, int steps, string op)
        {
            try
            {
                string sql = $"UPDATE MA_MOSteps SET MOStatus = 20578306 WHERE MOId = { moid } AND Operation = '{ op }' AND RtgStep = { steps }";
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

        public void UpdateTMFinalDate(int id, int line, DateTime datafim)
        {
            try
            {
                string sql = $"UPDATE CS_TimeSheetPROD SET EndDate = '{ datafim }' WHERE Id = { id } AND Line = { line }";
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

        public void UpdateTMFinalized(int id, string isfinalized, int status)
        {
            try
            {
                string sql = $"UPDATE CS_TimeSheetPROD SET IsFinalized = '{ isfinalized }', StatusODP = { status } WHERE Id = { id }";
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

    }

    public class GenericObject
    {
        public string MONo { get; set; }
    }

    public class GenericObjectDetails
    {
        public string MONo { get; set; }
        public int MOId { get; set; }
        public string Operation { get; set; }
        public int RtgStep { get; set; }
        public int MOStatus { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
