using BillsOfMaterial_App.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace BillsOfMaterial_App.Utilities
{
    public class Util
    {
        public static string[] GetConfig()
        {
            string[] vet = new string[2];
            string path = AppDomain.CurrentDomain.BaseDirectory + @"Config.xml";
            string dataSource = string.Empty;
            string initialCatalog = string.Empty;
            string userId = string.Empty;
            string password = string.Empty;
            string integred = string.Empty;
            string MultipleARS = string.Empty;
            string app = string.Empty;
            string id = string.Empty;
            try
            {
                if (File.Exists(path))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(path);

                    XmlNode node1 = doc.DocumentElement.SelectSingleNode("/Params/connectionString");
                    XmlNode node2 = doc.DocumentElement.SelectSingleNode("/Params/custQuotaId");
                    id = node2.InnerText;

                    string ss = node1.InnerText;
                    string[] txtSplit = ss.Split(';');
                    dataSource = txtSplit[0].ToString();
                    initialCatalog = txtSplit[1].ToString();
                    userId = txtSplit[2].ToString();
                    password = txtSplit[3].ToString();
                    MultipleARS = "MultipleActiveResultSets=True";
                    app = "App=EntityFramework\" providerName=\"System.Data.SqlClient";
                }

                if(ChooseDataBaseView.DataBaseString != string.Empty)
                {
                    initialCatalog = $"Initial Catalog='{ ChooseDataBaseView.DataBaseString }'";
                }

                string conStr = dataSource + ";" + initialCatalog.Replace("'", "") + ";" + userId.Replace("'", "") + ";" + password.Replace("'", "") + ";" + MultipleARS + ";" + app;
                vet[0] = conStr;
                vet[1] = id;

                return vet;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao ler arquivo de configuração!" + "\n" + ex.Message, "Erro",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
    }
}
