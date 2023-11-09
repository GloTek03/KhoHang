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
    public partial class frmMaterial : Form
    {
        DataTable tblMt;
        public frmMaterial()
        {
            InitializeComponent();
        }

        private void frmMaterial_Load(object sender, EventArgs e)
        {
            txtMaterialId.Enabled = false;
            btnSave.Enabled = false;
            btnIgnore.Enabled = false;
            LoadDataGridView();

        }
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT MaChatLieu, TenChatLieu FROM tblChatLieu";
            tblMt = Function.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvMaterial.DataSource = tblMt; //Nguồn dữ liệu            
            dgvMaterial.Columns[0].HeaderText = "Material ID";
            dgvMaterial.Columns[1].HeaderText = "Material name";
            dgvMaterial.Columns[0].Width = 100;
            dgvMaterial.Columns[1].Width = 300;
            dgvMaterial.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            dgvMaterial.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            btnModify.Enabled = false;
            btnIgnore.Enabled = true;
            btnSave.Enabled = true;
            btnAdd.Enabled = false;
            ResetValue();
            txtMaterialId.Enabled = true;
            txtMaterialId.Focus();
        }

        private void ResetValue()
        {
            txtMaterialId.Text = "";
            txtMaterialName.Text = "";
        }

        private void dgvMaterial_Click(object sender, EventArgs e)
        {
            if (btnAdd.Enabled == false)
            {
                MessageBox.Show("Function Add is active", "Notation",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaterialId.Focus();
                return;
            }
            if (tblMt.Rows.Count == 0)
            {
                MessageBox.Show("Data Null", "Notation",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaterialId.Text = dgvMaterial.CurrentRow.Cells[0].Value.ToString();
            txtMaterialName.Text = dgvMaterial.CurrentRow.Cells[1].Value.ToString();
            btnModify.Enabled = true;
            btnDelete.Enabled = true;
            btnIgnore.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string sql; //Lưu lệnh sql
            if (txtMaterialId.Text.Trim().Length == 0) //Nếu chưa nhập mã chất liệu
            {
                MessageBox.Show("Bạn phải nhập mã chất liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaterialId.Focus();
                return;
            }
            if (txtMaterialName.Text.Trim().Length == 0) //Nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn phải nhập tên chất liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaterialName.Focus();
                return;
            }
            sql = "Select MaChatLieu From tblChatLieu where MaChatLieu=N'" + txtMaterialId.Text.Trim() + "'";
            if (Function.CheckKey(sql))
            {
                MessageBox.Show("Mã chất liệu này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaterialId.Focus();
                return;
            }

            sql = "INSERT INTO tblChatLieu VALUES(N'" +
                txtMaterialId.Text + "',N'" + txtMaterialName.Text + "')";
            Function.RunSQL(sql); //Thực hiện câu lệnh sql
            LoadDataGridView(); //Nạp lại DataGridView
            ResetValue();
            btnDelete.Enabled = true;
            btnAdd.Enabled = true;
            btnModify.Enabled = true;
            btnIgnore.Enabled = false;
            btnSave.Enabled = false;
            txtMaterialId.Enabled = false;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            string sql; //Lưu câu lệnh sql
            if (tblMt.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaterialId.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaterialName.Text.Trim().Length == 0) //nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn chưa nhập tên chất liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "UPDATE tblChatLieu SET TenChatLieu=N'" +
                txtMaterialName.Text.ToString() +
                "' WHERE MaChatLieu=N'" + txtMaterialId.Text + "'";
            Function.RunSQL(sql);
            LoadDataGridView();
            ResetValue();

            btnIgnore.Enabled = false;

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblMt.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaterialId.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE tblChatLieu WHERE MaChatLieu=N'" + txtMaterialId.Text + "'";
                Function.RunSQL(sql);
                LoadDataGridView();
                ResetValue();
            }
        }

        private void btnIgnore_Click(object sender, EventArgs e)
        {
            ResetValue();
            btnIgnore.Enabled = false;
            btnAdd.Enabled = true;
            btnDelete.Enabled = true;
            btnModify.Enabled = true;
            btnSave.Enabled = false;
            txtMaterialId.Enabled = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMaterialId_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }
    }
}
