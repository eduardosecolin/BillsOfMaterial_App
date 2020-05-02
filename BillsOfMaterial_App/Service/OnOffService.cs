using BillsOfMaterial_App.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfMaterial_App.Service
{
    public class OnOffService
    {
        private readonly DBContext _context;
        static SqlConnection conn;

        public OnOffService()
        {
            _context = new DBContext();
            conn = new SqlConnection(_context.Database.Connection.ConnectionString);
        }

        public CS_OnOffValidate GetData(int idOffer, string offerNo, string item)
        {
            try
            {
                string sql = $"SELECT * FROM CS_OnOffValidate WHERE Id_Offer = {idOffer} AND OfferNo = '{offerNo}' AND Item = '{item}'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    CS_OnOffValidate onoff = new CS_OnOffValidate();
                    onoff.Id = Convert.ToInt32(reader["Id"]);
                    onoff.Id_Offer = Convert.ToInt32(reader["Id_Offer"]);
                    onoff.OfferNo = reader["OfferNo"].ToString();
                    onoff.Item = reader["Item"].ToString();
                    if(reader["On_Off"].ToString() == "1")
                    {
                        onoff.On_Off = true;
                    }
                    else
                    {
                        onoff.On_Off = false;
                    }
                   
                    conn.Close();

                    return onoff;
                }
                else
                {
                    conn.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Insert(CS_OnOffValidate onOff)
        {
            try
            {
                string on_off = onOff.On_Off ? "1" : "0";
                string sql = @"INSERT INTO CS_OnOffValidate (Id, Id_Offer, OfferNo, Item, On_Off,
                                           TBCreated, TBModified, TBCreatedID) VALUES (" +
                                            $"{onOff.Id}, {onOff.Id_Offer}, '{onOff.OfferNo}', '{onOff.Item}', " +
                                            $"'{on_off}', '{onOff.TBCreated}', '{onOff.TBModified}', {onOff.TBCreatedID}" +
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

        public static void Update(int idOffer, string offerNo, string item, bool param)
        {
            string on_off = param ? "1" : "0";
            try
            {
                string sql = $"UPDATE CS_OnOffValidate SET On_Off = '{ on_off }' WHERE Id_Offer = {idOffer} AND OfferNo = '{offerNo}' AND Item = '{item}'";
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
                string sql = @"SELECT ISNULL(MAX(Id), 0) + 1 FROM CS_OnOffValidate";
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

        public List<CS_OnOffValidate> GetDataFiltered()
        {
            try
            {
                string sql = "SELECT * FROM CS_OnOffValidate WHERE  On_Off = 1";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                List<CS_OnOffValidate> list = new List<CS_OnOffValidate>();
                while (reader.Read())
                {
                    CS_OnOffValidate onOff = new CS_OnOffValidate();
                    onOff.Id = Convert.ToInt32(reader["Id"]);
                    onOff.Id_Offer = Convert.ToInt32(reader["Id_Offer"]);
                    onOff.Item = reader["Item"].ToString();
                    onOff.OfferNo = reader["OfferNo"].ToString();
                    if(Convert.ToInt32(reader["On_Off"]) == 1){
                        onOff.On_Off = true;
                    }
                    else
                    {
                        onOff.On_Off = false;
                    }

                    list.Add(onOff);
                }
                reader.Close();
                conn.Close();
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
