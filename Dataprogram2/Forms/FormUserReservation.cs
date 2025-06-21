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
using System.Configuration;


namespace Dataprogram2.Forms
{
    public partial class FormUserReservation: Form
    {
        private int _userId;
        private int selectedSeatId = -1;
        public FormUserReservation(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }
        private void FormUserReservation_Load(object sender, EventArgs e)
        {
            LoadLibraries();
        }

        private void LoadLibraries()
        {
            cmbLibrary.Items.Clear();
            string connStr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT LibraryID, Name FROM Library", conn);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    cmbLibrary.Items.Add(new ComboBoxItem(rd["Name"].ToString(), rd["LibraryID"].ToString()));
                }
            }
        }

        private void cmbLibrary_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbFloor.Items.Clear();
            ComboBoxItem selectedLib = cmbLibrary.SelectedItem as ComboBoxItem;
            string connStr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT FloorID, FloorNumber FROM Floor WHERE LibraryID=@id", conn);
                cmd.Parameters.AddWithValue("@id", selectedLib.Value);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    cmbFloor.Items.Add(new ComboBoxItem("Floor " + rd["FloorNumber"], rd["FloorID"].ToString()));
                }
            }
        }

        private void cmbFloor_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlSeats.Controls.Clear();
            ComboBoxItem selectedFloor = cmbFloor.SelectedItem as ComboBoxItem;
            string connStr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT SeatID, SeatNumber, SeatStatus FROM Seat WHERE FloorID=@fid", conn);
                cmd.Parameters.AddWithValue("@fid", selectedFloor.Value);
                SqlDataReader rd = cmd.ExecuteReader();

                int x = 10, y = 10;
                while (rd.Read())
                {
                    Button btn = new Button();
                    btn.Text = rd["SeatNumber"].ToString();
                    btn.Tag = rd["SeatID"];
                    btn.Width = 60;
                    btn.Height = 40;
                    btn.Left = x;
                    btn.Top = y;
                    btn.BackColor = rd["SeatStatus"].ToString() == "空闲" ? Color.LightGreen : Color.Gray;
                    btn.Enabled = rd["SeatStatus"].ToString() == "空闲";
                    btn.Click += BtnSeat_Click;

                    pnlSeats.Controls.Add(btn);
                    x += 70;
                    if (x > pnlSeats.Width - 70)
                    {
                        x = 10;
                        y += 50;
                    }
                }
            }
        }

        private void BtnSeat_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in pnlSeats.Controls)
                if (ctrl is Button)
                    ((Button)ctrl).BackColor = Color.LightGreen;

            Button selectedBtn = sender as Button;
            selectedBtn.BackColor = Color.Orange;
            selectedSeatId = Convert.ToInt32(selectedBtn.Tag);
        }

        private void btnReserve_Click(object sender, EventArgs e)
        {
            if (selectedSeatId == -1)
            {
                lblStatus.Text = "请选择一个座位";
                return;
            }

            DateTime start = dtStart.Value;
            DateTime end = dtEnd.Value;

            if (end <= start)
            {
                lblStatus.Text = "结束时间必须晚于开始时间";
                return;
            }

            string connStr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlCommand checkCmd = new SqlCommand(@"
                    SELECT COUNT(*) FROM Reservation
                    WHERE SeatID=@seat AND 
                    ((StartTime <= @end AND EndTime >= @start))
                ", conn);
                checkCmd.Parameters.AddWithValue("@seat", selectedSeatId);
                checkCmd.Parameters.AddWithValue("@start", start);
                checkCmd.Parameters.AddWithValue("@end", end);

                int conflict = (int)checkCmd.ExecuteScalar();
                if (conflict > 0)
                {
                    lblStatus.Text = "时间段冲突，无法预约";
                    return;
                }

                SqlCommand insertCmd = new SqlCommand(@"
                    INSERT INTO Reservation (UserID, SeatID, StartTime, EndTime)
                    VALUES (@uid, @sid, @st, @et)
                ", conn);
                insertCmd.Parameters.AddWithValue("@uid", _userId);
                insertCmd.Parameters.AddWithValue("@sid", selectedSeatId);
                insertCmd.Parameters.AddWithValue("@st", start);
                insertCmd.Parameters.AddWithValue("@et", end);
                insertCmd.ExecuteNonQuery();

                SqlCommand updateSeat = new SqlCommand("UPDATE Seat SET SeatStatus='已预约' WHERE SeatID=@sid", conn);
                updateSeat.Parameters.AddWithValue("@sid", selectedSeatId);
                updateSeat.ExecuteNonQuery();

                lblStatus.Text = "预约成功！";
            }

            cmbFloor_SelectedIndexChanged(null, null); // 刷新座位图
        }
    }

    public class ComboBoxItem
    {
        public string Text { get; set; }
        public string Value { get; set; }

        public ComboBoxItem(string t, string v)
        {
            Text = t;
            Value = v;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
