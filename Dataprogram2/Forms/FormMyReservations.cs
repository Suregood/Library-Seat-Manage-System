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
    public partial class FormMyReservations: Form
    {
        private int _userId;
        public FormMyReservations(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }
        private void FormMyReservations_Load(object sender, EventArgs e)
        {
            LoadReservations();
        }

        private void LoadReservations()
        {
            dgvReservations.Rows.Clear();
            string connStr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
                    SELECT r.ReservationID, s.SeatNumber, f.FloorNumber, l.Name AS LibraryName, 
                           r.StartTime, r.EndTime
                    FROM Reservation r
                    JOIN Seat s ON r.SeatID = s.SeatID
                    JOIN Floor f ON s.FloorID = f.FloorID
                    JOIN Library l ON f.LibraryID = l.LibraryID
                    WHERE r.UserID = @uid
                    ORDER BY r.StartTime DESC", conn);

                cmd.Parameters.AddWithValue("@uid", _userId);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dgvReservations.Rows.Add(
                        reader["ReservationID"],
                        reader["LibraryName"],
                        reader["FloorNumber"],
                        reader["SeatNumber"],
                        Convert.ToDateTime(reader["StartTime"]).ToString("yyyy-MM-dd HH:mm"),
                        Convert.ToDateTime(reader["EndTime"]).ToString("yyyy-MM-dd HH:mm")
                    );
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadReservations();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (dgvReservations.SelectedRows.Count == 0)
            {
                lblInfo.Text = "请选择要取消的预约记录。";
                return;
            }

            int reservationId = Convert.ToInt32(dgvReservations.SelectedRows[0].Cells[0].Value);
            string connStr = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // 获取座位ID
                SqlCommand getSeat = new SqlCommand("SELECT SeatID FROM Reservation WHERE ReservationID=@rid", conn);
                getSeat.Parameters.AddWithValue("@rid", reservationId);
                int seatId = Convert.ToInt32(getSeat.ExecuteScalar());

                // 删除预约记录
                SqlCommand delRes = new SqlCommand("DELETE FROM Reservation WHERE ReservationID=@rid", conn);
                delRes.Parameters.AddWithValue("@rid", reservationId);
                delRes.ExecuteNonQuery();

                // 更新座位状态为“空闲”
                SqlCommand updateSeat = new SqlCommand("UPDATE Seat SET SeatStatus='空闲' WHERE SeatID=@sid", conn);
                updateSeat.Parameters.AddWithValue("@sid", seatId);
                updateSeat.ExecuteNonQuery();
            }

            lblInfo.Text = "取消成功。";
            LoadReservations();
        }
    }
}
