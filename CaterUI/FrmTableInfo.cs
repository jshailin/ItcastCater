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
    public partial class FrmTableInfo : Form
    {
        private FrmTableInfo()
        {
            InitializeComponent();

            tiBll = new TableInfoBll();
        }

        private TableInfoBll tiBll;
        public event Action RefreshMain;

        #region 实现窗体的单例
        private static FrmTableInfo _frm;

        public static FrmTableInfo Creat()
        {
            if (_frm == null) _frm = new FrmTableInfo();
            return _frm;
        }

        private void FrmTableInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            //与单例保持一致
            //出现这种代码的原因：Form的close()会释放当前窗体对象
            _frm = null;
        }
        #endregion

        private void FrmTableInfo_Load(object sender, EventArgs e)
        {
            #region 空闲筛选列表
            List<DdlModel> listDdl = new List<DdlModel>()
            {
                new DdlModel("-1","全部"),
                new DdlModel("1","空闲"),
                new DdlModel("0","使用中")
            };
            ddlFreeSearch.DataSource = listDdl;
            ddlFreeSearch.DisplayMember = "Title";
            ddlFreeSearch.ValueMember = "Id";
            #endregion

            LoadTypeList();
            LoadList();
        }

        private void LoadTypeList()
        {
            HallInfoBll hiBll = new HallInfoBll();

            List<HallInfo> list = hiBll.GetList();
            list.Insert(0, new HallInfo()
            {
                HId = 0,
                HTitle = "全部"
            });
            ddlHallSearch.DataSource = list;
            ddlHallSearch.DisplayMember = "HTitle";
            ddlHallSearch.ValueMember = "HId";

            ddlHallAdd.DataSource = hiBll.GetList();
            ddlHallAdd.DisplayMember = "HTitle";
            ddlHallAdd.ValueMember = "HId";

            //#region 空闲筛选列表
            //List<DdlModel> listDdl = new List<DdlModel>()
            //{
            //    new DdlModel("-1","全部"),
            //    new DdlModel("1","空闲"),
            //    new DdlModel("0","使用中")
            //};
            //ddlFreeSearch.DataSource = listDdl;
            //ddlFreeSearch.DisplayMember = "Title";
            //ddlFreeSearch.ValueMember = "Id";
            //#endregion
        }

        private void LoadList()
        {
            //筛选条件
            Dictionary<string, string> dic = new Dictionary<string, string>();

            if (ddlHallSearch.SelectedIndex > 0)
            {
                dic.Add("THallId", ddlHallSearch.SelectedValue.ToString());
            }
            if (ddlFreeSearch.SelectedIndex > 0)
            {
                dic.Add("TIsFree", ddlFreeSearch.SelectedValue.ToString());
            }

            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = tiBll.GetList(dic);
        }

        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.Value = Convert.ToBoolean(e.Value) ? "√" : "×";
            }
        }

        private void ddlHallSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadList();
        }

        private void ddlFreeSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadList();
        }

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            ddlFreeSearch.Text = "全部";
            ddlHallSearch.Text = "全部";
            LoadList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //判断用户是否输入
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("请输入名称");
                txtTitle.Focus();
                return;
            }
            //获取用户输入数据,并构造对象
            TableInfo ti = new TableInfo()
            {
                TTitle = txtTitle.Text,
                THallId = Convert.ToInt32(ddlHallAdd.SelectedValue),
                TIsFree = rbFree.Checked ? 1 : 0
            };
            //判断添加还是修改,然后调用方法
            if (txtId.Text.Equals("添加时无编号"))
            {
                //添加
                if (tiBll.Add(ti))
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
                //修改
                ti.TId = int.Parse(txtId.Text);
                if (tiBll.Edit(ti))
                {
                    LoadList();
                }
                else
                {
                    MessageBox.Show("修改失败,请稍候重试");
                }
            }

            //刷新主窗口
            RefreshMain();
            //恢复控件值
            txtId.Text = "添加时无编号";
            txtTitle.Text = "";
            rbFree.Checked = true;
            ddlHallAdd.SelectedIndex = 0;
            btnSave.Text = "添加";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //恢复控件值
            txtId.Text = "添加时无编号";
            txtTitle.Text = "";
            rbFree.Checked = true;
            ddlHallAdd.SelectedIndex = 0;
            btnSave.Text = "添加";
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //获得用户选择的行数据
            var row = dgvList.Rows[e.RowIndex];
            //填充控件,并修改按钮显示为修改状态
            txtId.Text = row.Cells[0].Value.ToString();
            txtTitle.Text = row.Cells[1].Value.ToString();
            ddlHallAdd.Text = row.Cells[2].Value.ToString();
            if(row.Cells[3].Value.ToString().Equals("1"))
            {
                rbFree.Checked = true;
            }
            else
            {
                rbUnFree.Checked = true;
            }

            btnSave.Text = "修改";
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            //真要删除?
            DialogResult result= MessageBox.Show("确定要删除吗?","提示",MessageBoxButtons.OKCancel);
            if (result==DialogResult.Cancel)
            {
                return;
            }
            //获取选中的编号
            int id = Convert.ToInt32(dgvList.SelectedRows[0].Cells[0].Value);
            //调用方法进行软删除
            if (tiBll.Remove(id))
            {
                LoadList();
            }
            else
            {
                MessageBox.Show("删除失败,请稍候重试");
            }

            //刷新主窗口
            RefreshMain();
        }

        private void btnAddHall_Click(object sender, EventArgs e)
        {
            FrmHallInfo frmHi=new FrmHallInfo();
            frmHi.RefreshList += LoadTypeList;
            frmHi.RefreshList += LoadList;
            frmHi.RefreshList += RefreshMain;
            frmHi.ShowDialog();
        }
    }
}
