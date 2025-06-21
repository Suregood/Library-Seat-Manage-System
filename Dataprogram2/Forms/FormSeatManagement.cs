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

namespace Dataprogram2.Forms
{
    public partial class FormSeatManagement: Form
    {
        private string connectionString = "Server=.;Database=LibrarySeatDB;Trusted_Connection=True;";
        public FormSeatManagement()
        {
            InitializeComponent();
            LoadLibraries();
            LoadFloors();
            LoadSeats();
        }
        private void LoadLibraries()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT LibraryID, Name, Location FROM Library";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvLibraries.DataSource = dt;

                // 绑定楼层页面的图书馆下拉框
                cmbLibraryForFloor.DataSource = dt.Copy();
                cmbLibraryForFloor.DisplayMember = "Name";
                cmbLibraryForFloor.ValueMember = "LibraryID";
            }
        }
        private void btnAddLibrary_Click(object sender, EventArgs e)
        {
            string name = txtLibName.Text.Trim();
            string location = txtLibLocation.Text.Trim();

            if (name == "" || location == "")
            {
                MessageBox.Show("请填写完整图书馆信息！");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO Library (Name, Location) VALUES (@Name, @Location)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Location", location);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("添加成功！");
            LoadLibraries();
            txtLibName.Clear();
            txtLibLocation.Clear();
        }
        private void btnDeleteLibrary_Click(object sender, EventArgs e)
        {
            if (dgvLibraries.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择要删除的图书馆！");
                return;
            }

            int libId = Convert.ToInt32(dgvLibraries.SelectedRows[0].Cells["LibraryID"].Value);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "DELETE FROM Library WHERE LibraryID = @LibraryID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@LibraryID", libId);
                conn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("删除成功！");
                }
                catch
                {
                    MessageBox.Show("删除失败，可能存在楼层引用！");
                }
            }

            LoadLibraries();
        }
        private void LoadFloors()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT FloorID, LibraryID, FloorNumber, Capacity FROM Floor";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvFloors.DataSource = dt;

                // 更新座位管理的下拉框
                LoadFloorsForSeatCombo();
            }
        }
        private void LoadFloorsForSeatCombo()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT FloorID, FloorNumber FROM Floor";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                cmbFloorForSeat.DataSource = dt;
                cmbFloorForSeat.DisplayMember = "FloorNumber";
                cmbFloorForSeat.ValueMember = "FloorID";
            }
        }
        private void btnAddFloor_Click(object sender, EventArgs e)
        {
            if (cmbLibraryForFloor.SelectedValue == null || txtFloorNumber.Text == "" || txtFloorCapacity.Text == "")
            {
                MessageBox.Show("请完整填写楼层信息！");
                return;
            }

            int libId = Convert.ToInt32(cmbLibraryForFloor.SelectedValue);
            int floorNum = Convert.ToInt32(txtFloorNumber.Text);
            int capacity = Convert.ToInt32(txtFloorCapacity.Text);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO Floor (LibraryID, FloorNumber, Capacity) VALUES (@LibraryID, @FloorNumber, @Capacity)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@LibraryID", libId);
                cmd.Parameters.AddWithValue("@FloorNumber", floorNum);
                cmd.Parameters.AddWithValue("@Capacity", capacity);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("添加成功！");
            LoadFloors();
            txtFloorNumber.Clear();
            txtFloorCapacity.Clear();
        }
        private void btnDeleteFloor_Click(object sender, EventArgs e)
        {
            if (dgvFloors.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择要删除的楼层！");
                return;
            }

            int floorId = Convert.ToInt32(dgvFloors.SelectedRows[0].Cells["FloorID"].Value);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "DELETE FROM Floor WHERE FloorID = @FloorID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@FloorID", floorId);
                conn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("删除成功！");
                }
                catch
                {
                    MessageBox.Show("删除失败，可能存在座位引用！");
                }
            }

            LoadFloors();
        }
        private void LoadSeats()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT SeatID, FloorID, SeatNumber, SeatStatus FROM Seat";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvSeats.DataSource = dt;
            }
        }
        private void btnAddSeat_Click(object sender, EventArgs e)
        {
            if (cmbFloorForSeat.SelectedValue == null || txtSeatNumber.Text == "")
            {
                MessageBox.Show("请输入座位号！");
                return;
            }

            int floorId = Convert.ToInt32(cmbFloorForSeat.SelectedValue);
            string seatNum = txtSeatNumber.Text.Trim();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO Seat (FloorID, SeatNumber, SeatStatus) VALUES (@FloorID, @SeatNumber, '空闲')";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@FloorID", floorId);
                cmd.Parameters.AddWithValue("@SeatNumber", seatNum);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("添加成功！");
            LoadSeats();
            txtSeatNumber.Clear();
        }
        private void btnDeleteSeat_Click(object sender, EventArgs e)
        {
            if (dgvSeats.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择要删除的座位！");
                return;
            }

            int seatId = Convert.ToInt32(dgvSeats.SelectedRows[0].Cells["SeatID"].Value);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "DELETE FROM Seat WHERE SeatID = @SeatID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@SeatID", seatId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("删除成功！");
            LoadSeats();
        }




    }
}
