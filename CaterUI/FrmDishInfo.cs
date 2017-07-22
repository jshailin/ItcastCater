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
using CaterCommon;
using CaterModel;

namespace CaterUI
{
    public partial class FrmDishInfo : Form
    {
        private FrmDishInfo()
        {
            InitializeComponent();
        }

        private DishInfoBll diBll=new DishInfoBll();

        #region 单例窗口

        private static FrmDishInfo _frm;

        public static FrmDishInfo Creat()
        {
            if (_frm==null)
            {
                _frm=new FrmDishInfo();
            }
            return _frm;
        }

        private void FrmDishInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            _frm = null;
        }

        #endregion

        private void FrmDishInfo_Load(object sender, EventArgs e)
        {
            LoadTypeList();
            LoadList();
        }

        private void LoadTypeList()
        {
            DishTypeInfoBll dtiBll=new DishTypeInfoBll();
            
            ddlTypeAdd.DataSource = dtiBll.GetList();
            ddlTypeAdd.DisplayMember = "DTitle";
            ddlTypeAdd.ValueMember = "DId";

            List<DishTypeInfo> list = dtiBll.GetList();
            //增加一个全部的选项
            list.Insert(0,new DishTypeInfo()
            {
                DId=0,
                DTitle="全部"
            });
            ddlTypeSearch.DataSource = list;
            ddlTypeSearch.DisplayMember = "DTitle";
            ddlTypeSearch.ValueMember = "DId";
        }

        private void LoadList()
        {
            //设置筛选字典
            Dictionary<string ,string > dic=new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(txtTitleSearch.Text))
            {
                dic.Add("di.DTitle",txtTitleSearch.Text);
            }
            if (ddlTypeSearch.Text!="全部")
            {
                dic.Add("di.DTypeId",ddlTypeSearch.SelectedValue.ToString());
            }

            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = diBll.GetList(dic);
        }

        private void txtTitleSearch_Leave(object sender, EventArgs e)
        {
            LoadList();
        }

        private void ddlTypeSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadList();
        }

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            txtTitleSearch.Text = "";
            ddlTypeSearch.Text = "全部";
            LoadList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            #region 判断是否有输入数据
            if (string.IsNullOrWhiteSpace(txtTitleSave.Text))
            {
                MessageBox.Show("请输入菜品名称");
                txtTitleSave.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("请输入价格");
                txtPrice.Focus();
                return;
            } 
            #endregion
            //获取用户输入数据，构建对象
            DishInfo di=new DishInfo()
            {
                DTitle=txtTitleSave.Text,
                DPrice=decimal.Parse(txtPrice.Text),
                DChar=txtChar.Text,
                DTypeId=Convert.ToInt32(ddlTypeAdd.SelectedValue)
            };
            if (txtId.Text.Equals("添加时无编号"))
            {
                //添加
                if (diBll.Add(di))
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
                //修改
                di.DId = int.Parse(txtId.Text);
                if (diBll.Edit(di))
                {
                    LoadList();
                }
                else
                {
                    MessageBox.Show("修改失败，请稍候重试");
                }
            }

            //恢复控件
            txtId.Text = "添加时无编号";
            txtTitleSave.Text = "";
            txtPrice.Text = "";
            txtChar.Text = "";
            ddlTypeAdd.SelectedIndex = 0;
            btnSave.Text = "添加";

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //恢复控件
            txtId.Text = "添加时无编号";
            txtTitleSave.Text = "";
            txtPrice.Text = "";
            txtChar.Text = "";
            ddlTypeAdd.SelectedIndex = 0;
            btnSave.Text = "添加";
        }

        private void txtTitleSave_Leave(object sender, EventArgs e)
        {
            txtChar.Text = PinyinHelper.GetPinyin(txtTitleSave.Text);
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //获取用户点击的行数据
            DataGridViewRow row= dgvList.Rows[e.RowIndex];
            //将选择的行数据填充到控件，并改按钮为“修改”
            txtId.Text = row.Cells[0].Value.ToString();
            txtTitleSave.Text = row.Cells[1].Value.ToString();
            ddlTypeAdd.Text = row.Cells[2].Value.ToString();
            txtPrice.Text = row.Cells[3].Value.ToString();
            txtChar.Text = row.Cells[4].Value.ToString();

            btnSave.Text = "修改";
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            //提示删除，用户确认
            DialogResult result= MessageBox.Show("确定要删除吗？","提示",MessageBoxButtons.OKCancel);
            if (result==DialogResult.Cancel)
            {
                return;
            }
            //获取用户选择的编号
            int id = Convert.ToInt32(dgvList.SelectedRows[0].Cells[0].Value);
            //调用方法，根据编号进行软删除
            if (diBll.Remove(id))
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
            FrmDishTypeInfo frmDishTypeInfo=new FrmDishTypeInfo();
            frmDishTypeInfo.RefreshTypeList += LoadTypeList;
            frmDishTypeInfo.RefreshTypeList += LoadList;
            frmDishTypeInfo.ShowDialog();
            //if (result==DialogResult.OK)
            //{
            //    LoadTypeList();
            //    LoadList();
            //}
        }

        
    }
}
