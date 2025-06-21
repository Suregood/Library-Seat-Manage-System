using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dataprogram2.Forms
{
    public partial class FormCheckIn: Form
    {
        private int _userId;
        public FormCheckIn(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }
        private void FormCheckIn_Load(object sender, EventArgs e)
        {
            LoadTodayReservations();
        }

        private void LoadTodayReservations()
        {
            dgvToday.Rows.Clear();
            string connStr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
                    SELECT r.ReservationID, s.SeatNumber, f.FloorNumber, l.Name AS LibraryName,
                           r.StartTime, r.EndTime, c.CheckInStatus, c.CheckInTime, c.CheckOutTime
                    FROM Reservation r
                    JOIN Seat s ON r.SeatID = s.SeatID
                    JOIN Floor f ON s.FloorID = f.FloorID
                    JOIN Library l ON f.LibraryID = l.LibraryID
                    LEFT JOIN CheckIn c ON c.ReservationID = r.ReservationID
                    WHERE r.UserID = @uid AND CAST(r.StartTime AS DATE) = CAST(GETDATE() AS DATE)
                    ORDER BY r.StartTime", conn);

                cmd.Parameters.AddWithValue("@uid", _userId);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    dgvToday.Rows.Add(
                        reader["ReservationID"],
                        reader["LibraryName"],
                        reader["FloorNumber"],
                        reader["SeatNumber"],
                        Convert.ToDateTime(reader["StartTime"]).ToString("HH:mm"),
                        Convert.ToDateTime(reader["EndTime"]).ToString("HH:mm"),
                        reader["CheckInStatus"]?.ToString() ?? "未签到",
                        reader["CheckInTime"]?.ToString() ?? "",
                        reader["CheckOutTime"]?.ToString() ?? ""
                    );
                }
            }
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            if (dgvToday.SelectedRows.Count == 0)
            {
                lblStatus.Text = "请选择一条预约记录。";
                return;
            }

            int reservationId = Convert.ToInt32(dgvToday.SelectedRows[0].Cells[0].Value);
            DateTime now = DateTime.Now;

            string connStr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // 获取预约时间
                SqlCommand getTime = new SqlCommand("SELECT StartTime FROM Reservation WHERE ReservationID=@rid", conn);
                getTime.Parameters.AddWithValue("@rid", reservationId);
                DateTime startTime = Convert.ToDateTime(getTime.ExecuteScalar());

                if (now < startTime.AddMinutes(-10) || now > startTime.AddMinutes(15))
                {
                    lblStatus.Text = "只能在预约时间前10分钟到后15分钟之间签到。";
                    return;
                }

                // 检查是否已签到
                SqlCommand check = new SqlCommand("SELECT COUNT(*) FROM CheckIn WHERE ReservationID=@rid", conn);
                check.Parameters.AddWithValue("@rid", reservationId);
                int exists = (int)check.ExecuteScalar();
                if (exists > 0)
                {
                    lblStatus.Text = "已签到。";
                    return;
                }

                SqlCommand insert = new SqlCommand(@"
                    INSERT INTO CheckIn (ReservationID, CheckInTime, CheckInStatus)
                    VALUES (@rid, @now, '已签到')", conn);
                insert.Parameters.AddWithValue("@rid", reservationId);
                insert.Parameters.AddWithValue("@now", now);
                insert.ExecuteNonQuery();

                lblStatus.Text = "签到成功！";
                LoadTodayReservations();
            }
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            if (dgvToday.SelectedRows.Count == 0)
            {
                lblStatus.Text = "请选择一条预约记录。";
                return;
            }

            int reservationId = Convert.ToInt32(dgvToday.SelectedRows[0].Cells[0].Value);
            DateTime now = DateTime.Now;

            string connStr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // 检查是否已签到
                SqlCommand check = new SqlCommand("SELECT COUNT(*) FROM CheckIn WHERE ReservationID=@rid", conn);
                check.Parameters.AddWithValue("@rid", reservationId);
                int exists = (int)check.ExecuteScalar();
                if (exists == 0)
                {
                    lblStatus.Text = "尚未签到，无法签退。";
                    return;
                }

                // 更新签退时间
                SqlCommand update = new SqlCommand(@"
                    UPDATE CheckIn 
                    SET CheckOutTime = @now 
                    WHERE ReservationID = @rid", conn);
                update.Parameters.AddWithValue("@now", now);
                update.Parameters.AddWithValue("@rid", reservationId);
                update.ExecuteNonQuery();

                lblStatus.Text = "签退成功！";
                LoadTodayReservations();
            }
        }
    }
}
