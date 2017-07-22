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
    public partial class FrmOrderDish : Form
    {
        public FrmOrderDish()
        {
            InitializeComponent();
            oiBll = new OrderInfoBll();
        }
        private OrderInfoBll oiBll; 
         //订单编号
        private int orderId;

        private void FrmOrderDish_Load(object sender, EventArgs e)
        {
            LoadDishTypeInfo();
            LoadDishInfo();

            orderId = oiBll.GetOidByTid(Convert.ToInt32(this.Tag));
            this.Text = this.Tag.ToString() + "号餐桌" + this.Text;
            groupBox2.Text = orderId.ToString() + "号订单" + groupBox2.Text;

            LoadOrderDish();
        }

        private void LoadDishTypeInfo()
        {
            DishTypeInfoBll dtiBll=new DishTypeInfoBll();
            ddlType.ValueMember = "DId";
            ddlType.DisplayMember = "DTitle";
            var list = dtiBll.GetList();
            list.Insert(0,new DishTypeInfo()
            {
                DId=0,
                DTitle="全部"
            });
            ddlType.DataSource =list ;
        }

        private void LoadDishInfo()
        {
            //拼接查询条件
            Dictionary<string ,string > dic=new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                dic.Add("DChar",txtTitle.Text.Trim());
            }
            if (ddlType.SelectedIndex>0)
            {
                dic.Add("DTypeId",ddlType.SelectedValue.ToString());
            }
            //查询菜品,显示到表格中
            DishInfoBll diBll=new DishInfoBll();
            dgvAllDish.AutoGenerateColumns = false;
            dgvAllDish.DataSource = diBll.GetList(dic);
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            LoadDishInfo();
        }

        private void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDishInfo();
        }

        private void dgvAllDish_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //点菜
            //菜品编号
            int dishId = Convert.ToInt32(dgvAllDish.Rows[e.RowIndex].Cells[0].Value);

            //执行点菜
            if(oiBll.DianCai(orderId, dishId))
            {
                //点菜成功
                LoadOrderDish();
            }
        }

        /// <summary>
        /// 加载详单列表
        /// </summary>
        private void LoadOrderDish()
        {
            dgvOrderDetail.AutoGenerateColumns = false;
            dgvOrderDetail.DataSource = oiBll.GetOrderDetailInfo(orderId);

            //计算总金额
            lblMoney.Text= oiBll.GetToltalMoneyByOrderId(orderId).ToString();
        }


        private void dgvOrderDetail_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex==2)
            {
                //获取到修改的行
                var row = dgvOrderDetail.Rows[e.RowIndex];
                //修改菜的数量
                oiBll.EditCountByOid(Convert.ToInt32(row.Cells[0].Value), Convert.ToInt32(row.Cells[2].Value));
                //计算总金额
                lblMoney.Text = oiBll.GetToltalMoneyByOrderId(orderId).ToString();
            }
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            if (oiBll.XiaDan(orderId, decimal.Parse(lblMoney.Text)))
            {
                MessageBox.Show("下单成功");
            }
            else
            {
                MessageBox.Show("下单失败,请稍候重试");  
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            DialogResult result= MessageBox.Show("确定要删除吗?","提示",MessageBoxButtons.OKCancel);
            if (result==DialogResult.Cancel)
            {
                return;
            }

            int oId = Convert.ToInt32(dgvOrderDetail.SelectedRows[0].Cells[0].Value);
            if (oiBll.RemoveOrderDishByOId(oId))
            {
                LoadOrderDish();
            }

        }
    }
}
