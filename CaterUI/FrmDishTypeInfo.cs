using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CaterBll;
using CaterModel;

namespace CaterUI
{
    public partial class FrmDishTypeInfo : Form
    {
        public FrmDishTypeInfo()
        {
            InitializeComponent();
        }

        private DishTypeInfoBll dtiBll = new DishTypeInfoBll();
 //       private DialogResult result = DialogResult.Cancel;
        public event Action RefreshTypeList;

        private void FrmDishTypeInfo_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        private void LoadList()
        {
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = dtiBll.GetList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //获取用户输入，构造对象
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("请输入标题");
                txtTitle.Focus();
                return;
            }
            DishTypeInfo dti=new DishTypeInfo()
            {
                DTitle=txtTitle.Text
            };
            if (txtId.Text=="添加时无编号")
	        {
		        //调用方法，添加数据
                if (dtiBll.Add(dti))
                {
                    LoadList();
                }
                else
                {
                    MessageBox.Show("添加失败，请稍候重试");
                } 
	        }
            else
            {
                dti.DId = Convert.ToInt32(txtId.Text);
                //调用方法，修改数据
                if (dtiBll.Edit(dti))
                {
                    LoadList();
                }
                else
                {
                    MessageBox.Show("修改失败，请稍候重试");
                }
            }
 //           result = DialogResult.OK;
            RefreshTypeList();
            //恢复控件
            txtId.Text = "添加时无编号";
            txtTitle.Text = "";
            btnSave.Text = "添加";
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //获取用户点击的内容，填充到控件中
            DataGridViewRow row = dgvList.Rows[e.RowIndex];
            txtId.Text = row.Cells[0].Value.ToString();
            txtTitle.Text = row.Cells[1].Value.ToString();

            btnSave.Text = "修改";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //恢复控件
            txtId.Text = "添加时无编号";
            txtTitle.Text = "";
            btnSave.Text = "添加";
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定要删除吗？","提示",MessageBoxButtons.OKCancel);
            if (result==DialogResult.Cancel)
            {
                return;
            }
            //获取用户选择的行数据，得到编号
            DataGridViewRow row=dgvList.SelectedRows[0];
            int id = Convert.ToInt32(row.Cells[0].Value);
            //调用方法，进行软删除
            if (dtiBll.Remove(id))
            {
                LoadList();
            }
            else
            {
                MessageBox.Show("删除失败，请稍候重试");
            }
 //           result = DialogResult.OK;
            RefreshTypeList();
        }

        //private void FrmDishTypeInfo_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    this.DialogResult = result;
        //}
    }
}
