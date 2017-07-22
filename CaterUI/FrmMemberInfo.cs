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
    public partial class FrmMemberInfo : Form
    {
        private FrmMemberInfo()
        {
            InitializeComponent();
        }
        MemberInfoBll miBll=new MemberInfoBll();

        #region 实现窗体的单例
        private static FrmMemberInfo _frm;

        public static FrmMemberInfo Creat()
        {
            if (_frm == null) _frm = new FrmMemberInfo();
            return _frm;
        }

        private void FrmMemberInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            //与单例保持一致
            //出现这种代码的原因：Form的close()会释放当前窗体对象
            _frm = null;
        } 
        #endregion

        private void FrmMemberInfo_Load(object sender, EventArgs e)
        {
            //加载会员信息
            LoadList();

            //加载会员分类信息
            LoadTypeList();
        }
        /// <summary>
        /// 加载会员分类信息
        /// </summary>
        private void LoadTypeList()
        {
            MemberTypeInfoBll mtiBll=new MemberTypeInfoBll();
            List<MemberTypeInfo> list = mtiBll.GetList();

            ddlType.DataSource = list;
            //设置显示值
            ddlType.DisplayMember = "MTitle";
            //设置value值
            ddlType.ValueMember = "MId";
        }

        /// <summary>
        /// 加载会员信息
        /// </summary>
        private void LoadList()
        {
            //定义键值对，存放查询条件
            Dictionary<string ,string > dic=new Dictionary<string, string>();

            if (txtNameSearch.Text!="")
            {
                dic.Add("MName",txtNameSearch.Text);
            }
            if (txtPhoneSearch.Text != "")
            {
                dic.Add("MPhone", txtPhoneSearch.Text);
            }

            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = miBll.GetList(dic);

        }

        private void txtNameSearch_Leave(object sender, EventArgs e)
        {
            LoadList();
        }

        private void txtPhoneSearch_Leave(object sender, EventArgs e)
        {
            LoadList();
        }

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            txtNameSearch.Text = "";
            txtPhoneSearch.Text = "";
            LoadList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //判断输入有效性
            if (string.IsNullOrWhiteSpace(txtNameAdd.Text))
            {
                MessageBox.Show("请输入姓名");
                txtNameAdd.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPhoneAdd.Text))
            {
                MessageBox.Show("请输入手机号");
                txtPhoneAdd.Focus();
                return;
            } if (string.IsNullOrWhiteSpace(txtMoney.Text))
            {
                MessageBox.Show("请输入余额");
                txtMoney.Focus();
                return;
            }
            //接收用户输入数据
            MemberInfo mi=new MemberInfo()
            {
                MName=txtNameAdd.Text,
                MPhone=txtPhoneAdd.Text,
                MMoney=Convert.ToDecimal(txtMoney.Text),
                MTypeId=Convert.ToInt32(ddlType.SelectedValue)

            };
            if (txtId.Text.Equals("添加时无编号"))
            {
                //添加操作
                if (miBll.Add(mi))
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
                mi.MId = int.Parse(txtId.Text);
                if (miBll.Edit(mi))
                {
                    LoadList();
                }
                else
                {
                    MessageBox.Show("修改失败，请稍候重试");
                }
            }

            //恢复控件的值
            txtId.Text = "添加时无编号";
            txtNameAdd.Text = "";
            txtPhoneAdd.Text = "";
            txtMoney.Text = "";
            ddlType.SelectedIndex = 0;
            btnSave.Text = "添加";

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //恢复控件的值
            txtId.Text = "添加时无编号";
            txtNameAdd.Text = "";
            txtPhoneAdd.Text = "";
            txtMoney.Text = "";
            ddlType.SelectedIndex = 0;
            btnSave.Text = "添加";
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //获取双击的行数据
            var row = dgvList.Rows[e.RowIndex];
            //将行中的数据显示到控件中
            txtId.Text = row.Cells[0].Value.ToString();
            txtNameAdd.Text = row.Cells[1].Value.ToString();
            ddlType.Text = row.Cells[2].Value.ToString();
            txtPhoneAdd.Text = row.Cells[3].Value.ToString();
            txtMoney.Text = row.Cells[4].Value.ToString();
            btnSave.Text = "修改";

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            //获取选中项的编号
            int id = Convert.ToInt32(dgvList.SelectedRows[0].Cells[0].Value);
            //提示确认删除
            DialogResult result = MessageBox.Show("确定要删除吗？", "提示", MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            if (miBll.Remove(id))
            {
                LoadList();
            }
            else
            {
                MessageBox.Show("删除失败，请稍候重试");
            }
        }

        private void btnAddType_Click(object sender, EventArgs e)
        {
            FrmMemberTypeInfo frmMti=new FrmMemberTypeInfo();
            //以模态窗口打开分类管理窗口
            DialogResult result= frmMti.ShowDialog();
            //根据返回值，判断是否更新下拉列表
            if (result==DialogResult.OK)
            {
                LoadTypeList();
                LoadList();
            }
        }

        

    }
}
