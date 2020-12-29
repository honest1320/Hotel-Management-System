using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotlDB
{
    public partial class Form2 : Form
    {
        string connstring = "Server=localhost;Port=5432;User Id = postgres; Password=1320;Database=ProjectDB;";
        NpgsqlConnection conn;
        string sql;
        NpgsqlCommand cmd;
        DataTable dt;
        NpgsqlDataAdapter da;


        int RowID = 0;
        int ID = 0;
        public Form2()
        {
            InitializeComponent();
            conn = new NpgsqlConnection(connstring);
        }



        private void Form2_Load(object sender, EventArgs e)
        {
            Select();
        }

        private void ClearData()
        {
            textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = ""; textBox4.Text = ""; textBox5.Text = ""; textBox6.Text = ""; textBox7.Text = "";
            RowID = 0;
            ID = 0;
            numericUpDown1.Value = 0; numericUpDown2.Value = 0; numericUpDown3.Value = 0;

        }

        private void Select()
        {
            sql = @"SELECT * FROM hotel_database.employees";
            DataTable dt = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                RowID = (int)dataGridView1.Rows[e.RowIndex].Cells["emp_id"].Value;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            textBox2.Text = ID.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            numericUpDown1.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            numericUpDown2.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            numericUpDown3.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            
            cmd = new NpgsqlCommand("insert into hotel_database.employees (emp_id, emp_first_name, emp_last_name, emp_designation, emp_contact_number, emp_email_address, department_department_id, addresses_address_id, hotel_hotel_id )   values(@empId, @name, @sname, @designat, @cont, @mail, @depId, @adId, @hotelId) ", conn);
            conn.Open();                                    
            cmd.Parameters.AddWithValue("@empId", int.Parse(textBox2.Text));
            cmd.Parameters.AddWithValue("@name", textBox3.Text);
            cmd.Parameters.AddWithValue("@sname", textBox4.Text);
            cmd.Parameters.AddWithValue("@designat", textBox5.Text);
            cmd.Parameters.AddWithValue("@cont", textBox6.Text);
            cmd.Parameters.AddWithValue("@mail", textBox7.Text);
            cmd.Parameters.AddWithValue("@depId", numericUpDown1.Value);
            cmd.Parameters.AddWithValue("@adId", numericUpDown2.Value);
            cmd.Parameters.AddWithValue("@hotelId", numericUpDown3.Value);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Record Added Successfully");
            conn.Close();
            Select();
            ClearData();
            
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {

                using (var cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = string.Format("delete from hotel_database.employees where emp_id={0}", RowID);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                MessageBox.Show(ex.Message);
            }
            Select();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
           
            cmd = new NpgsqlCommand("update hotel_database.employees set emp_first_name=@name, emp_last_name=@sname, emp_designation=@designat, emp_contact_number=@cont,  emp_email_address=@mail, department_department_id=@depId, addresses_address_id=@adId, hotel_hotel_id=@hotelId where emp_id=@empId", conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@empId", ID);

            cmd.Parameters.AddWithValue("@name", textBox3.Text);
            cmd.Parameters.AddWithValue("@sname", textBox4.Text);
            cmd.Parameters.AddWithValue("@designat", textBox5.Text);
            cmd.Parameters.AddWithValue("@cont", textBox6.Text);
            cmd.Parameters.AddWithValue("@mail", textBox7.Text);
            cmd.Parameters.AddWithValue("@depId", numericUpDown1.Value);
            cmd.Parameters.AddWithValue("@adId", numericUpDown2.Value);
            cmd.Parameters.AddWithValue("@hotelId", numericUpDown3.Value);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Booking Updated Successfully");
            conn.Close();
            Select();
            ClearData();
            

        }

        private void btn_home_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Visible = false;
            f1.Dock = DockStyle.Fill;
            f1.Show();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            da = new NpgsqlDataAdapter("select * from hotel_database.bookings where check_in_date like '" + textBox8.Text + "%'", conn);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            da = new NpgsqlDataAdapter("select * from hotel_database.employees where emp_first_name like '" + textBox8.Text + "%'", conn);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }
    }
}
