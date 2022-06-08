using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace testechetokuka
{
    public partial class Form1 : Form
    {     
        Bitmap bitmap;
        MySqlConnection sqlConn = new MySqlConnection();
        MySqlCommand sqlCmd = new MySqlCommand();
        DataTable sqlDt = new DataTable();
        String sqlQuery;
        MySqlDataAdapter DtA = new MySqlDataAdapter();
        MySqlDataReader sqlRd;
       

        DataSet DS = new DataSet();
        //database info
        String server = "localhost";
        String username = "root";
        String password = "Cheto16495.pt";
        String database = "kuka";
        

        public string txtSearch;

        public Form1()
        {
            InitializeComponent();
        }
        //database connection
        private void upLoadData()
        {
            sqlConn.ConnectionString = "server=" + server + ";" + "user id=" + username + ";" +
                "password=" + password + ";" + "database=" + database;

            sqlConn.Open();
            sqlCmd.Connection = sqlConn;

            sqlCmd.CommandText = "SELECT * FROM kuka.datakuka";

            sqlRd = sqlCmd.ExecuteReader();
            sqlDt.Load(sqlRd);
            sqlRd.Close();
            sqlConn.Close();
            dataGridView1.DataSource = sqlDt;
        }
        //exit button
        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult iExit;
            try
            {


                iExit = MessageBox.Show("Confirm if you want to exit", "Cheto",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (iExit == DialogResult.Yes)
            {
                Application.Exit();
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //reset button
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                foreach(Control c in panel4.Controls)
                {
                    if (c is TextBox)
                        ((TextBox)c).Clear();
                }
                txtSearch = "";
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //print button
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int height = dataGridView1.Height;
                dataGridView1.Height = dataGridView1.RowCount * dataGridView1.RowTemplate.Height * 2;
                bitmap = new Bitmap(dataGridView1.Width, dataGridView1.Height);
                dataGridView1.DrawToBitmap(bitmap, new Rectangle(0, 0, dataGridView1.Width, dataGridView1.Height));
                printPreviewDialog1.PrintPreviewControl.Zoom = 1;
                printPreviewDialog1.ShowDialog();
                dataGridView1.Height = height;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                e.Graphics.DrawImage(bitmap, 0, 0);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //upload data
        private void Form1_Load(object sender, EventArgs e)
        {
            upLoadData();
        }

        //add new data button
        private void button1_Click(object sender, EventArgs e)
        {
            sqlConn.ConnectionString = "server=" + server + ";" + "user id=" + username + ";" +
                "password=" + password + ";" + "database=" + database;

            try
            {
                sqlConn.Open();
                sqlQuery = "insert into kuka.datakuka (part_id, mold_id, mass, center_mass_x, center_mass_y, center_mass_z, inertia_xx, inertia_yy, inertia_zz, actual_slot, home_slot, image)" + 
                    "values('" + textBox1.Text + "','" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "', '" + textBox6.Text + "', '" + textBox7.Text + "', '" + textBox8.Text + "', '" + textBox9.Text + "', '" + textBox10.Text + "', '" + textBox11.Text + "', '" + textBox12.Text + "')";

                sqlCmd = new MySqlCommand(sqlQuery, sqlConn);
                sqlRd = sqlCmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlConn.Close();
            }
            upLoadData();
        }
        //update button
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                sqlConn.ConnectionString = "server=" + server + ";" + "user id=" + username + ";" +
               "password=" + password + ";" + "database=" + database;
                sqlConn.Open();
            }
            catch (Exception tg)
            {
                MessageBox.Show(tg.Message);
            }
            

            try
            {
                MySqlCommand sqlCmd = new MySqlCommand();
                sqlCmd.Connection = sqlConn;
                sqlCmd.CommandText = "UPDATE kuka.datakuka SET part_id = @part_id, mold_id= @mold_id, mass = @mass, center_mass_x = @center_mass_x, center_mass_y = @center_mass_y, center_mass_z = @center_mass_z, inertia_xx = @inertia_xx, inertia_yy = @inertia_yy, inertia_zz = @inertia_zz, actual_slot = @actual_slot, home_slot = @home_slot, image = @image Where part_id = @part_id";
                
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@part_id", textBox1.Text);
                sqlCmd.Parameters.AddWithValue("@mold_id", textBox2.Text);
                sqlCmd.Parameters.AddWithValue("@mass", textBox3.Text);
                sqlCmd.Parameters.AddWithValue("@center_mass_x", textBox4.Text);
                sqlCmd.Parameters.AddWithValue("@center_mass_y", textBox5.Text);
                sqlCmd.Parameters.AddWithValue("@center_mass_z", textBox6.Text);
                sqlCmd.Parameters.AddWithValue("@inertia_xx", textBox7.Text);
                sqlCmd.Parameters.AddWithValue("@inertia_yy", textBox8.Text);
                sqlCmd.Parameters.AddWithValue("@inertia_zz", textBox9.Text);
                sqlCmd.Parameters.AddWithValue("@actual_slot", textBox10.Text);
                sqlCmd.Parameters.AddWithValue("@home_slot", textBox11.Text);
                sqlCmd.Parameters.AddWithValue("@image", textBox12.Text);

                sqlCmd.ExecuteNonQuery();
                sqlConn.Close();
                upLoadData();             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //select data from grid
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                textBox5.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                textBox6.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                textBox7.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                textBox8.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
                textBox9.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
                textBox10.Text = dataGridView1.SelectedRows[0].Cells[9].Value.ToString();
                textBox11.Text = dataGridView1.SelectedRows[0].Cells[10].Value.ToString();
                textBox12.Text = dataGridView1.SelectedRows[0].Cells[11].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //delete button
        private void button3_Click(object sender, EventArgs e)
        {
            try { 
            sqlConn.ConnectionString = "server=" + server + ";" + "user id=" + username + ";" +
               "password=" + password + ";" + "database=" + database;
            sqlConn.Open();

            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandText = "DELETE FROM kuka.datakuka WHERE part_id = @part_id";        
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@part_id", textBox1.Text);
            sqlCmd.ExecuteNonQuery();
            sqlConn.Close();
                
            foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
            { 
                 dataGridView1.Rows.RemoveAt(item.Index);
            }
            upLoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //search
        private void textBox_KeyPress13(object sender, KeyPressEventArgs e)
        {
            try
            {
                DataView dv = sqlDt.DefaultView;
                dv.RowFilter = string.Format("part_id like'%{0}%'", textBox13.Text);
                dataGridView1.DataSource = dv.ToTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}