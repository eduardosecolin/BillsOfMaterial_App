using BillsOfMaterial_App.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfMaterial_App.Service
{
    public class TBCostFormationService
    {
        private readonly DBContext _context;
        SqlConnection conn;

        public TBCostFormationService()
        {
            _context = new DBContext();
            conn = new SqlConnection(_context.Database.Connection.ConnectionString);
        }

        public void Insert(CS_TBCostFormation tcs)
        {
            try
            {
                string sql = @"INSERT INTO CS_TBCostFormation (Id, Id_Offer, OfferNo, SumIpiIcms, PIS,
                                            COFINS, ICMS, Freight, IPI, Comission, DF_Percent, DV_Percent,
                                            Margin, MarginVariable, ISS, IR, CSLL, Markup, TotalMarkup,
                                            TotalSimulation, TBCreated, TBModified, TBCreatedID, Item) VALUES (" +
                                            $"{tcs.Id}, {tcs.Id_Offer}, '{tcs.OfferNo}', '{tcs.SumIpiIcms}', " +
                                            $"{tcs.PIS.ToString().Replace(",", ".")}, {tcs.COFINS.ToString().Replace(",", ".")}, " +
                                            $"{tcs.ICMS.ToString().Replace(",", ".")}, {tcs.Freight.ToString().Replace(",", ".")}, " +
                                            $"{tcs.IPI.ToString().Replace(",", ".")}, {tcs.Comission.ToString().Replace(",", ".")}, " +
                                            $"{tcs.DF_Percent.ToString().Replace(",", ".")}, {tcs.DV_Percent.ToString().Replace(",", ".")}, " +
                                            $"{tcs.Margin.ToString().Replace(",", ".")}, {tcs.MarginVariable.ToString().Replace(",", ".")}, " +
                                            $"{tcs.ISS.ToString().Replace(",", ".")}, {tcs.IR.ToString().Replace(",", ".")}, " +
                                            $"{tcs.CSLL.ToString().Replace(",", ".")}, {tcs.Markup.ToString().Replace(",", ".")}, " +
                                            $"{tcs.TotalMarkup.ToString().Replace(",", ".")}, {tcs.TotalSimulation.ToString().Replace(",", ".")}, " +
                                            $"'{tcs.TBCreated}', '{tcs.TBModified}', {tcs.TBCreatedID}, '{tcs.Item}'" +
                                            ")";

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

        public int GetMaxId()
        {
            try
            {
                conn.Open();
                string sql = @"SELECT ISNULL(MAX(Id), 0) + 1 FROM CS_TBCostFormation";
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

        public CS_TBCostFormation GetAllWithParams(int idOffer, string offerNo, string item)
        {
            try
            {
                string sql = $"SELECT * FROM CS_TBCostFormation WHERE Id_Offer = {idOffer} AND OfferNo = '{offerNo}' AND Item = '{item}'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    CS_TBCostFormation tcs = new CS_TBCostFormation();
                    tcs.Id = Convert.ToInt32(reader["Id"]);
                    tcs.Id_Offer = Convert.ToInt32(reader["Id_Offer"]);
                    tcs.OfferNo = reader["OfferNo"].ToString();
                    tcs.Item = reader["Item"].ToString();
                    tcs.SumIpiIcms = reader["SumIpiIcms"].ToString();
                    tcs.PIS = string.IsNullOrEmpty(reader["PIS"].ToString()) ? 0 : Convert.ToDouble(reader["PIS"]);
                    tcs.COFINS = string.IsNullOrEmpty(reader["COFINS"].ToString()) ? 0 : Convert.ToDouble(reader["COFINS"]);
                    tcs.ICMS = string.IsNullOrEmpty(reader["ICMS"].ToString()) ? 0 : Convert.ToDouble(reader["ICMS"]);
                    tcs.Freight = string.IsNullOrEmpty(reader["Freight"].ToString()) ? 0 : Convert.ToDouble(reader["Freight"]);
                    tcs.Comission = string.IsNullOrEmpty(reader["Comission"].ToString()) ? 0 : Convert.ToDouble(reader["Comission"]);
                    tcs.IPI = string.IsNullOrEmpty(reader["IPI"].ToString()) ? 0 : Convert.ToDouble(reader["IPI"]);
                    tcs.DF_Percent = string.IsNullOrEmpty(reader["DF_Percent"].ToString()) ? 0 : Convert.ToDouble(reader["DF_Percent"]);
                    tcs.DV_Percent = string.IsNullOrEmpty(reader["DV_Percent"].ToString()) ? 0 : Convert.ToDouble(reader["DV_Percent"]);
                    tcs.Margin = string.IsNullOrEmpty(reader["Margin"].ToString()) ? 0 : Convert.ToDouble(reader["Margin"]);
                    tcs.MarginVariable = string.IsNullOrEmpty(reader["MarginVariable"].ToString()) ? 0 : Convert.ToDouble(reader["MarginVariable"]);
                    tcs.ISS = string.IsNullOrEmpty(reader["ISS"].ToString()) ? 0 : Convert.ToDouble(reader["ISS"]);
                    tcs.IR = string.IsNullOrEmpty(reader["IR"].ToString()) ? 0 : Convert.ToDouble(reader["IR"]);
                    tcs.CSLL = string.IsNullOrEmpty(reader["CSLL"].ToString()) ? 0 : Convert.ToDouble(reader["CSLL"]);
                    tcs.Markup = string.IsNullOrEmpty(reader["Markup"].ToString()) ? 0 : Convert.ToDouble(reader["Markup"]);
                    tcs.TotalMarkup = string.IsNullOrEmpty(reader["TotalMarkup"].ToString()) ? 0 : Convert.ToDouble(reader["TotalMarkup"]);
                    tcs.TotalSimulation = string.IsNullOrEmpty(reader["TotalSimulation"].ToString()) ? 0 : Convert.ToDouble(reader["TotalSimulation"]);
                    conn.Close();

                    return tcs;
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

        public void DeleteData(int idOffer, string offerNo, string item)
        {
            try
            {
                string sql = $"DELETE FROM CS_TBCostFormation WHERE Id_Offer = {idOffer} AND OfferNo = '{offerNo}' AND Item = '{item}'";
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
}
