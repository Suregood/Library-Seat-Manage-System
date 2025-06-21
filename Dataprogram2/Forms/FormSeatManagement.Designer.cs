using System.Windows.Forms;

namespace Dataprogram2.Forms
{
    partial class FormSeatManagement
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabLibrary = new System.Windows.Forms.TabPage();
            this.tabFloor = new System.Windows.Forms.TabPage();
            this.tabSeat = new System.Windows.Forms.TabPage();

            // 图书馆管理控件
            this.dgvLibraries = new DataGridView();
            this.txtLibName = new TextBox();
            this.txtLibLocation = new TextBox();
            this.btnAddLibrary = new Button();
            this.btnDeleteLibrary = new Button();

            // 楼层管理控件
            this.dgvFloors = new DataGridView();
            this.cmbLibraryForFloor = new ComboBox();
            this.txtFloorNumber = new TextBox();
            this.txtFloorCapacity = new TextBox();
            this.btnAddFloor = new Button();
            this.btnDeleteFloor = new Button();

            // 座位管理控件
            this.dgvSeats = new DataGridView();
            this.cmbFloorForSeat = new ComboBox();
            this.txtSeatNumber = new TextBox();
            this.btnAddSeat = new Button();
            this.btnDeleteSeat = new Button();

            // ====================
            // 各控件位置布局略（可视化中拖拽即可），下面是Tab绑定：
            // ====================

            // Tab页加入控件（每页只添加关键控件）
            this.tabLibrary.Controls.AddRange(new Control[] {
                dgvLibraries, txtLibName, txtLibLocation, btnAddLibrary, btnDeleteLibrary
            });

            this.tabFloor.Controls.AddRange(new Control[] {
                dgvFloors, cmbLibraryForFloor, txtFloorNumber, txtFloorCapacity, btnAddFloor, btnDeleteFloor
            });

            this.tabSeat.Controls.AddRange(new Control[] {
                dgvSeats, cmbFloorForSeat, txtSeatNumber, btnAddSeat, btnDeleteSeat
            });

            // TabControl 绑定 Tab
            this.tabControl.Controls.Add(this.tabLibrary);
            this.tabControl.Controls.Add(this.tabFloor);
            this.tabControl.Controls.Add(this.tabSeat);
            this.tabControl.Dock = DockStyle.Fill;

            // 主窗体
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.tabControl);
            this.Text = "图书馆与座位管理";
        }

        // 控件字段定义
        private TabControl tabControl;
        private TabPage tabLibrary, tabFloor, tabSeat;

        private DataGridView dgvLibraries, dgvFloors, dgvSeats;
        private TextBox txtLibName, txtLibLocation;
        private Button btnAddLibrary, btnDeleteLibrary;

        private ComboBox cmbLibraryForFloor;
        private TextBox txtFloorNumber, txtFloorCapacity;
        private Button btnAddFloor, btnDeleteFloor;

        private ComboBox cmbFloorForSeat;
        private TextBox txtSeatNumber;
        private Button btnAddSeat, btnDeleteSeat;
    }
}
