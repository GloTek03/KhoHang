using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using KhoHang.Class;

namespace KhoHang
{
    public partial class frmStaff : Form
    {
        DataTable tblStaff;
        public frmStaff()
        {
            InitializeComponent();
        }

        private void frmStaff_Load(object sender, EventArgs e)
        {
            txtStaffID.Enabled = false;
            btnSave.Enabled = false;
            btnIgnore.Enabled = false;
            LoadDgvStaff();
        }
        public void LoadDgvStaff()
        {
            string sql;
            sql = "SELECT" +
                " MaNhanVien, TenNhanVien, GioiTinh, DiaChi, DienThoai, NgaySinh " +
                "From tblNhanVien";
            tblStaff = Function.GetDataToTable(sql);
            dgvStaff.DataSource = tblStaff;
            dgvStaff.Columns[0].HeaderText = "Id";
            dgvStaff.Columns[1].HeaderText = "Name";
            dgvStaff.Columns[2].HeaderText = "Gender";
            dgvStaff.Columns[3].HeaderText = "Address";
            dgvStaff.Columns[4].HeaderText = "Phone";
            dgvStaff.Columns[5].HeaderText = "Date Of Birth";
            dgvStaff.Columns[0].Width = 100;
            dgvStaff.Columns[1].Width = 150;
            dgvStaff.Columns[2].Width = 100;
            dgvStaff.Columns[3].Width = 150;
            dgvStaff.Columns[4].Width = 100;
            dgvStaff.Columns[5].Width = 100;
            dgvStaff.AllowUserToAddRows = false;
            dgvStaff.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dgvStaff_Click(object sender, EventArgs e)
        {
            if (btnAdd.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtStaffID.Focus();
                return;
            }
            if (tblStaff.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtStaffID.Text = dgvStaff.CurrentRow.Cells[0].Value.ToString();
            txtStaffName.Text = dgvStaff.CurrentRow.Cells[1].Value.ToString();
            if (dgvStaff.CurrentRow.Cells[2].Value.ToString() == "Nam") rbtnMale.Checked = true;
            else if(dgvStaff.CurrentRow.Cells[2].Value.ToString() == "Nữ") rbtnFemale.Checked = true;
            else rbtnOther.Checked = true;
            txtAddress.Text = dgvStaff.CurrentRow.Cells[3].Value.ToString();
            mtxtPhone.Text = dgvStaff.CurrentRow.Cells[4].Value.ToString();
            txtDob.Value = (DateTime)dgvStaff.CurrentRow.Cells[5].Value;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnIgnore.Enabled = true;
            btnSave.Enabled = true;
            btnAdd.Enabled = false;
            ResetValues();
            txtStaffID.Enabled = true;
            txtStaffID.Focus();
        }

        private void ResetValues()
        {
            txtStaffName.Text = "";
            txtStaffID.Text = "";
            rbtnMale.Checked = false;
            rbtnFemale .Checked = false;    
            rbtnOther .Checked = false;
            txtAddress.Text = "";
            mtxtPhone.Text = "";
            txtDob.Value = DateTime.Now;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string sql, gt;
            if (txtStaffID.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStaffID.Focus();
                return;
            }
            if (txtStaffName.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStaffName.Focus();
                return;
            }
            if (txtAddress.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAddress.Focus();
                return;
            }
            if (mtxtPhone.Text == "(   )     -")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtxtPhone.Focus();
                return;
            }
            if (rbtnMale.Checked == true)
                gt = "Nam";
            else if (rbtnFemale.Checked == true)
                gt = "Nữ";
            else
                gt = "Khác";
            sql = "SELECT MaNhanVien FROM tblNhanVien WHERE MaNhanVien=N'" + txtStaffID.Text.Trim() + "'";
            if (Function.CheckKey(sql))
            {
                MessageBox.Show("Mã nhân viên này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStaffID.Focus();
                txtStaffID.Text = "";
                return;
            }
            sql = "INSERT INTO tblNhanVien(MaNhanVien,TenNhanVien,GioiTinh, DiaChi,DienThoai, NgaySinh) VALUES (N'" + txtStaffID.Text.Trim() + "',N'" + txtStaffName.Text.Trim() + "',N'" + gt + "',N'" + txtAddress.Text.Trim() + "','" + mtxtPhone.Text + "','" + Function.ConvertDateTime(txtDob.Text) + "')";
            Function.RunSQL(sql);
            LoadDgvStaff();
            ResetValues();
            btnDelete.Enabled = true;
            btnAdd.Enabled = true;
            btnUpdate.Enabled = true;
            btnIgnore.Enabled = false;
            btnSave.Enabled = false;
            txtStaffID.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblStaff.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtStaffID.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá bản ghi này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE tblNhanVien WHERE MaNhanVien=N'" + txtStaffID.Text.Trim() + "'";
                Function.RunSQL(sql);
                LoadDgvStaff();
                ResetValues();
            }
        }

        private void btnIgnore_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnIgnore.Enabled = false;
            btnAdd.Enabled = true;
            btnDelete.Enabled = true;
            btnUpdate.Enabled = true;
            btnSave.Enabled = false;
            txtStaffID.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string sql, gt;
            if (tblStaff.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtStaffID.Text.Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStaffID.Focus();
                return;
            }
            if (txtStaffName.Text.Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStaffName.Focus();
                return;
            }
            if (txtAddress.Text.Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAddress.Focus();
                return;
            }
            if (mtxtPhone.Text == "(   )     -")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtxtPhone.Focus();
                return;
            }
            if (rbtnMale.Checked == true)
                gt = "Nam";
            else if (rbtnFemale.Checked == true)
                gt = "Nữ";
            else
                gt = "Khác";
            sql = "UPDATE tblNhanVien SET TenNhanVien=N'" + txtStaffName.Text.ToString() + "',GioiTinh=N'" + gt + "',DiaChi=N'" +
                 txtAddress.Text.ToString() + "',DienThoai='" + mtxtPhone.Text.ToString() +
                "' WHERE MaNhanVien=N'" + txtStaffID.Text + "'";
            Function.RunSQL(sql);
            LoadDgvStaff();
            ResetValues();
            btnIgnore.Enabled = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
