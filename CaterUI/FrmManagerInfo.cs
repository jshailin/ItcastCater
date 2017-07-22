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
    public partial class FrmManagerInfo : Form
    {
        
        private FrmManagerInfo()
        {
            InitializeComponent();
        }

        //实现窗体的单例
        private static FrmManagerInfo _frm;
        public static FrmManagerInfo Create()
        {
            if (_frm==null)
            {
                _frm=new FrmManagerInfo();
            }
            return _frm;
        }

        //创建业务逻辑层对象
        ManagerInfoBll miBll = new ManagerInfoBll();
        private void FrmManagerInfo_Load(object sender, EventArgs e)
        {
            //加载列表
            LoadList();
        }

        private void LoadList()
        {
            //禁用列表的自动生成
            dgvList.AutoGenerateColumns = false;
            //调用方法获取数据，绑定到列表的数据源上
            dgvList.DataSource = miBll.GetList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //判断输入有效性
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("请输入用户名");
                txtName.Focus();
                return;
            }
           if (string.IsNullOrWhiteSpace(txtPwd.Text))
            {
                MessageBox.Show("请输入密码");
                txtPwd.Focus();
                return;
            }
            //接收用户输入
            ManagerInfo mi = new ManagerInfo()
            {
                MName = txtName.Text,
                MPwd = txtPwd.Text,
                MType = rb1.Checked ? 1 : 0   //经理值为1，店员值为0
            };
            if (txtId.Text.Equals("添加时无编号"))
            {
                #region 添加
                //调用bll的Add方法
                if (miBll.Add(mi))
                {
                    //如果添加成功，则重新加载数据
                    LoadList();
                    
                }
                else
                {
                    MessageBox.Show("添加失败，请稍候重试");
                }
                #endregion

            }
            else
            {
                #region 修改

                mi.MId = int.Parse(txtId.Text);
                if (miBll.Edit(mi))
                {
                    LoadList();
                }

                #endregion
            }
            //清除文本框
            txtName.Text = "";
            txtPwd.Text = "";
            txtId.Text = "添加时无编号";
            rb2.Checked = true;
            btnSave.Text = "添加";

        }

        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //对类型列进行格式化处理
            if (e.ColumnIndex==2)
            {
                e.Value = Convert.ToInt32(e.Value) == 1 ? "经理" : "店员";
            }
        }

    
        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //根据当前点击的单元格，找到行与列，进行赋值
            //根据索引找到行
            DataGridViewRow row = dgvList.Rows[e.RowIndex];
            //找到对应的列
            txtId.Text = row.Cells[0].Value.ToString();
            txtName.Text = row.Cells[1].Value.ToString();
            if (row.Cells[2].Value.ToString().Equals("1"))
            {
                rb1.Checked = true; //1，经理；0，店员
            }
            else
            {
                rb2.Checked = true;
            }
            //指定密码的值
            txtPwd.Text = "这是原来的密码吗？";

            btnSave.Text = "修改";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //清除文本框
            txtName.Text = "";
            txtPwd.Text = "";
            txtId.Text = "添加时无编号";
            rb2.Checked = true;
            btnSave.Text = "添加";
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            //获取选中的行
            var rows = dgvList.SelectedRows;
            if (rows.Count>0)
            {
                DialogResult result = MessageBox.Show("确定要删除吗？", "提示", MessageBoxButtons.OKCancel);
                if (result==DialogResult.Cancel)
                {
                    //用户取消删除
                    return;
                }
                //获取要删除的id
                int id=int.Parse( rows[0].Cells[0].Value.ToString());
                //调用删除操作
                if (miBll.Remove(id))
                {
                    LoadList();
                }
            }
            else
            {
                MessageBox.Show("请先选择要删除的行");
            }
        }

        private void FrmManagerInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            //与单例保持一致
            //出现这种代码的原因：Form的close()会释放当前窗体对象
            _frm = null;
        }
    }
}
