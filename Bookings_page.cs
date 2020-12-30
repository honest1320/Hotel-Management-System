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
    public partial class Form1 : Form
    {

         string connstring = "Server=localhost;Port=5432;User Id = postgres; Password=1320;Database=Project;";
         NpgsqlConnection conn;
         string sql;
         NpgsqlCommand cmd;
         NpgsqlDataAdapter da;
         DataTable dt;

        public Form1()
        {
            InitializeComponent();
        }
        int RowID = 0;
        int ID = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new NpgsqlConnection(connstring);

            Select();
        }

        private void Select()
        {
            conn.Open();
            sql = @"SELECT * FROM database_hotel.bookings order by booking_id";
            DataTable dt = new DataTable();
            da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }


        private void ClearData()
        {
            textBox2.Text = ""; textBox3.Text = "";
            RowID = 0;
            ID = 0;
            numericUpDown1.Value = 0; numericUpDown2.Value = 0; numericUpDown3.Value = 0; numericUpDown4.Value = 0; numericUpDown5.Value = 0; numericUpDown6.Value = 0;


        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedCells.Count != 0)
            {
                RowID = (int)dataGridView1.Rows[e.RowIndex].Cells["booking_id"].Value;

            }
        }



        private void btn_delete_Click(object sender, EventArgs e)
        {

            try
            {

                using (var cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = string.Format("delete from database_hotel.bookings where booking_id={0} ", RowID);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Deleted Successfully");
                    ClearData();
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




        private void button5_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            this.Visible = false;
            f2.Dock = DockStyle.Fill;
            f2.Show();
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            RowID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            numericUpDown1.Text = ID.ToString();
            dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            numericUpDown2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            dateTimePicker2.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            dateTimePicker3.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            numericUpDown3.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            numericUpDown4.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            numericUpDown5.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            numericUpDown6.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();


        }

        private void update_btn_Click(object sender, EventArgs e)
        {
   
            cmd = new NpgsqlCommand("update database_hotel.bookings set booking_date=@bkDate, duration_of_stay=@days, check_in_date=@chkIn, check_out_date=@chkOut,  booking_payment_type=@payment, total_rooms_booked=@noRooms, hotel_hotel_id=@hotelID, guests_guest_id=@guestId, employees_emp_id=@empId, total_amount=@amount where booking_id=@bkId", conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@bkId", ID);

            cmd.Parameters.AddWithValue("@bkDate", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@days", numericUpDown2.Value);
            cmd.Parameters.AddWithValue("@chkIn", dateTimePicker2.Value);
            cmd.Parameters.AddWithValue("@chkOut", dateTimePicker3.Value);
            cmd.Parameters.AddWithValue("@payment", textBox2.Text);
            cmd.Parameters.AddWithValue("@noRooms", numericUpDown3.Value);
            cmd.Parameters.AddWithValue("@hotelID", numericUpDown4.Value);
            cmd.Parameters.AddWithValue("@guestId", numericUpDown5.Value);
            cmd.Parameters.AddWithValue("@empId", numericUpDown6.Value);
            cmd.Parameters.AddWithValue("@amount", Convert.ToDouble(textBox3.Text));

            cmd.ExecuteNonQuery();
            MessageBox.Show("Booking Updated Successfully");
            conn.Close();
            Select();
            ClearData();
            

        }

        private void button3_Click(object sender, EventArgs e)
        {

            cmd = new NpgsqlCommand("insert into database_hotel.bookings (booking_id,booking_date,duration_of_stay,check_in_date,check_out_date,booking_payment_type,total_rooms_booked,hotel_hotel_id,guests_guest_id,employees_emp_id,total_amount)  values(@bkId, @bkDate, @days, @chkIn, @chkOut, @payment, @noRooms, @hotelID, @guestId, @empId, @amount) ", conn);
            conn.Open();                    
            cmd.Parameters.AddWithValue("@bkId", numericUpDown1.Value);
            cmd.Parameters.AddWithValue("@bkDate", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@days", numericUpDown2.Value);
            cmd.Parameters.AddWithValue("@chkIn", dateTimePicker2.Value);
            cmd.Parameters.AddWithValue("@chkOut", dateTimePicker3.Value);
            cmd.Parameters.AddWithValue("@payment", textBox2.Text);
            cmd.Parameters.AddWithValue("@noRooms", numericUpDown3.Value);
            cmd.Parameters.AddWithValue("@hotelID", numericUpDown4.Value);
            cmd.Parameters.AddWithValue("@guestId", numericUpDown5.Value);
            cmd.Parameters.AddWithValue("@empId", numericUpDown6.Value);
            cmd.Parameters.AddWithValue("@amount", Convert.ToDouble(textBox3.Text));

            cmd.ExecuteNonQuery();
            MessageBox.Show("Record Added Successfully");
            conn.Close();
            Select();
            ClearData();

        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            da = new NpgsqlDataAdapter("select * from database_hotel.bookings where booking_date = '" + textBox1.Text + "'", conn);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }


        private void btn_clear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void bt_homepage_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            Select();
        }
    }
}

