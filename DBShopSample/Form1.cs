using DBWrapperLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DBShopSample
{
  public partial class Form1 : Form
  {
    private DBWrapper oDbWrapper;
    
    public Form1()
    {
      InitializeComponent();

  
      oDbWrapper = new DBWrapper(@"VDBS", "Agenstvo","dbuser", "dbuser");
      dataGridView1.AllowUserToAddRows = false;
      dataGridView1.ContextMenuStrip = contextMenuStrip1;

    }

         private void btnReadAll_Click(object sender, EventArgs e)
         {
         
         }

         private void button1_Click(object sender, EventArgs e)
         {
            dataGridView1.DataSource =
             oDbWrapper.FillDataSet(@"SELECT * FROM Client");

         }
         private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource =
             oDbWrapper.FillDataSet(@"SELECT * FROM AD_Object");

        }
        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource =
            oDbWrapper.FillDataSet(@"SELECT * FROM AD_Platform");

        }
        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource =
            oDbWrapper.FillDataSet(@"SELECT * FROM AD_Placing");
        }
        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource =
            oDbWrapper.FillDataSet(@"SELECT * FROM Rent_place");
            

        }
        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource =
            oDbWrapper.FillDataSet(@"SELECT * FROM Income");
         

        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {

            int clientId = int.Parse(ID_Client1.Text);
            string newName = name.Text;
            string newPhone = phone.Text;

         
            bool success = oDbWrapper.UpdateClient(clientId, newName, newPhone);

            if (success)
            {
                MessageBox.Show("Данные успешно обновлены", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Ошибка при обновлении данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public bool UpdateClient(int clientId, string newName, string newPhone)
        {
            try
            {
                // Формируем SQL-запрос для обновления данных
                string sql = $"UPDATE Client SET name = '{newName}', phone = '{newPhone}' WHERE ID_Client = {clientId}";

                // Выполняем запрос с использованием DBWrapper
                oDbWrapper.ExecuteNonQuery(sql);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
  }


