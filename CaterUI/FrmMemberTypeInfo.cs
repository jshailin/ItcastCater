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
    public partial class FrmMemberTypeInfo : Form
    {
        public FrmMemberTypeInfo()
        {
            InitializeComponent();
        }

        private MemberTypeInfoBll mtiBll = new MemberTypeInfoBll();
        private void FrmMemberTypeInfo_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        private DialogResult result = DialogResult.Cancel;
        private void LoadList()
        {
            //禁止自动生成列
            dgvList.AutoGenerateColumns = false;
            //将列表赋值给显示控件
            dgvList.DataSource = mtiBll.GetList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //判断输入有效性
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("请输入标题");
                txtTitle.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtDiscount.Text))
            {
                MessageBox.Show("请输入折扣");
                txtDiscount.Focus();
                return;
            }
            //接收用户输入的值,构造对象
            MemberTypeInfo mti = new MemberTypeInfo()
            {
                MTitle = txtTitle.Text,
                MDiscount = decimal.Parse(txtDiscount.Text)
            };
            if (txtId.Text.Equals("添加时无编号"))
            {
                //添加操作
                if (mtiBll.Add(mti))    //调用添加方法
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
                //修改操作
                mti.MId = int.Parse(txtId.Text);
                if (mtiBll.Edit(mti))
                {
                    LoadList();
                }
                else
                {
                    MessageBox.Show("修改失败，请稍候重试");
                }
            }
            //将控件还原
            txtId.Text = "添加时无编号";
            txtTitle.Text = "";
            txtDiscount.Text = "";
            btnSave.Text = "添加";

            result = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //将控件还原
            txtId.Text = "添加时无编号";
            txtTitle.Text = "";
            txtDiscount.Text = "";
            btnSave.Text = "添加";
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //获取用户点击的行
            var row = dgvList.Rows[e.RowIndex];
            //将行数据赋值给文本框
            txtId.Text = row.Cells[0].Value.ToString();
            txtTitle.Text = row.Cells[1].Value.ToString();
            txtDiscount.Text = row.Cells[2].Value.ToString();
            btnSave.Text = "修改";
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            //获取选中行的编号
            var row = dgvList.SelectedRows[0];
            int id = Convert.ToInt32(row.Cells[0].Value);
            //确认是否删除
            DialogResult result = MessageBox.Show("确定要删除吗？", "提示", MessageBoxButtons.OKCancel);
            if (result==DialogResult.Cancel)
            {
                return;
            }
            //调用删除方法（软删除）
            if (mtiBll.Remove(id))
            {
                LoadList();
            }
            else
            {
                MessageBox.Show("删除失败，请稍候重试");
            }

            result = DialogResult.OK;

        }

        private void FrmMemberTypeInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = result;
        }
    }
}
