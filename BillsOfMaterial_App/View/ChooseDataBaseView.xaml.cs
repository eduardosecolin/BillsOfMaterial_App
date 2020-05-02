using BillsOfMaterial_App.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BillsOfMaterial_App.View
{
    /// <summary>
    /// Lógica interna para ChooseDataBaseView.xaml
    /// </summary>
    public partial class ChooseDataBaseView : Window
    {

        private readonly DBContext _context;
        private readonly SqlConnection conn;
        public static string DataBaseString = string.Empty;

        public ChooseDataBaseView()
        {
            InitializeComponent();
            _context = new DBContext();
            conn = new SqlConnection(_context.Database.Connection.ConnectionString);
            FillComboBoxDataBase();
        }

        private List<string> GetDataBase()
        {
            try
            {
                List<string> dataBases = new List<string>();
                conn.Open();
                string sql = @"SELECT name, database_id, create_date FROM sys.databases ;";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    dataBases.Add(reader["name"].ToString());
                }

                if (dataBases.Count > 0)
                {
                    conn.Close();
                    return dataBases;
                }

                conn.Close();
                return dataBases;
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("Erro: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        private void FillComboBoxDataBase()
        {
            cbDatabase.ItemsSource = null;
            cbDatabase.ItemsSource = GetDataBase();

        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                DataBaseString = cbDatabase.Text;
                Menu window = new Menu();
                window.Show();
                this.Close();
            }
        }

        private bool Validate()
        {
            if (cbDatabase.Text == string.Empty)
            {
                MessageBox.Show("Selecione uma base de dados!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }
    }
}
