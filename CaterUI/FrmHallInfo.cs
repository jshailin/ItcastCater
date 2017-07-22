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
    public partial class FrmHallInfo : Form
    {
        public FrmHallInfo()
        {
            InitializeComponent();
        }

        HallInfoBll hiBll=new HallInfoBll();
        public event Action RefreshList;

        private void FrmHallInfo_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        private void LoadList()
        {
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = hiBll.GetList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //判断用户是否输入数据
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("请输入标题");
                txtTitle.Focus();
                return;
            }
            //获取用户输入数据，构建对象
            HallInfo hi=new HallInfo()
            {
                HTitle=txtTitle.Text
            };
            //判断是添加还是修改,并调用方法
            if (txtId.Text.Equals("添加时无编号"))
            {
                //添加操作
                if (hiBll.Add(hi))
                {
                    LoadList();
                }
                else
                {
                    MessageBox.Show("添加失败,请稍候重试");
                }
            }
            else
            {
                //修改操作
                hi.HId = int.Parse(txtId.Text);
                if (hiBll.Edit(hi))
                {
                    LoadList();
                }
                else
                {
                    MessageBox.Show("修改失败,请稍候重试");
                }
            }
            //刷新事件
            RefreshList();
            //恢复控件
            txtId.Text = "添加时无编号";
            txtTitle.Text = "";
            btnSave.Text = "添加";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //恢复控件
            txtId.Text = "添加时无编号";
            txtTitle.Text = "";
            btnSave.Text = "添加";
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //获取用户点击的行数据
            DataGridViewRow row = dgvList.Rows[e.RowIndex];
            //填充控件
            txtId.Text = row.Cells[0].Value.ToString();
            txtTitle.Text = row.Cells[1].Value.ToString();
            //按钮显示为修改状态
            btnSave.Text = "修改";
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            //提示确认是否删除
            DialogResult result= MessageBox.Show("确定要删除?","提示",MessageBoxButtons.OKCancel);
            if (result==DialogResult.Cancel)
            {
                return;
            }
            //获取用户选中的id
            int id =Convert.ToInt32(dgvList.SelectedRows[0].Cells[0].Value);
            //调用方法软删除
            if (hiBll.Remove(id))
            {
                LoadList();
            }
            else
            {
                MessageBox.Show("删除失败,请稍候重试");
            }
            //刷新事件
            RefreshList();
        }
    }
}
