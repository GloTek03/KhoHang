using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KhoHang.Class;

namespace KhoHang
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Class.Function.Connect();
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Class.Function.disconnect();
            Application.Exit();
        }

        private void mnuMaterial_Click(object sender, EventArgs e)
        {
            frmMaterial material = new frmMaterial();
            material.ShowDialog();
        }

        private void mnuItem_Click(object sender, EventArgs e)
        {
            frmListItem item = new frmListItem();
            item.MdiParent = this;
            item.Show();
        }

        private void mnuStaff_Click(object sender, EventArgs e)
        {
            frmStaff staff = new frmStaff();
            staff.MdiParent = this;
            staff.Show();
        }

        private void mnuInvoiceDetail_Click(object sender, EventArgs e)
        {
            frmInvoice invoice = new frmInvoice();
            invoice.MdiParent = this;
            invoice.Show();
        }

        private void mnuSeInvoice_Click(object sender, EventArgs e)
        {
            frmTimHDBan seInvoice = new frmTimHDBan();
            seInvoice.MdiParent = this;
            seInvoice.Show();
        }

        private void mnuCustomer_Click(object sender, EventArgs e)
        {
            frmCustomer frm = new frmCustomer();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
