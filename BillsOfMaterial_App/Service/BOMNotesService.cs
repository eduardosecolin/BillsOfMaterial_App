using BillsOfMaterial_App.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfMaterial_App.Service
{
    public partial class BOMNotesService
    {
        private readonly DBContext _context;
        private readonly SqlConnection conn;

        public BOMNotesService()
        {
            _context = new DBContext();
            conn = new SqlConnection(_context.Database.Connection.ConnectionString);
        }

        public List<MA_BillOfMaterialsNotes> GetAll()
        {
            try
            {
                List<MA_BillOfMaterialsNotes> list = new List<MA_BillOfMaterialsNotes>();
                conn.Open();
                string sql = "SELECT NoteCode, Description FROM MA_BillOfMaterialsNotes";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    MA_BillOfMaterialsNotes bomNotes = new MA_BillOfMaterialsNotes();
                    bomNotes.NoteCode = reader["NoteCode"].ToString();
                    bomNotes.Description = reader["Description"].ToString();
                    list.Add(bomNotes);
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

        public List<MA_BillOfMaterialsNotes> Filter(string notecode)
        {
            try
            {
                List<MA_BillOfMaterialsNotes> list = new List<MA_BillOfMaterialsNotes>();
                string sql = $"SELECT NoteCode, Description FROM MA_BillOfMaterialsNotes WHERE NoteCode LIKE '%{ notecode }%'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    MA_BillOfMaterialsNotes bomNotes = new MA_BillOfMaterialsNotes();
                    bomNotes.NoteCode = reader["NoteCode"].ToString();
                    bomNotes.Description = reader["Description"].ToString();
                    list.Add(bomNotes);
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
