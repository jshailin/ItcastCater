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
    public partial class FrmOrderPay : Form
    {
        public FrmOrderPay()
        {
            InitializeComponent();
            oiBll = new OrderInfoBll();
        }
        private OrderInfoBll oiBll;
        //订单编号
        private int orderId;
        public event Action RefreshList;

        private void FrmOrderPay_Load(object sender, EventArgs e)
        {
            gbMember.Enabled = false;
            orderId = oiBll.GetOidByTid(Convert.ToInt32(this.Tag));
            this.Text = orderId.ToString() + "号单" + this.Tag.ToString() + "号桌结账";

            GetMoneyByOrderId();
        }

        private void GetMoneyByOrderId()
        {
            lblPayMoney.Text = lblPayMoneyDiscount.Text = oiBll.GetToltalMoneyByOrderId(orderId).ToString();
        }

        private void cbkMember_CheckedChanged(object sender, EventArgs e)
        {
            gbMember.Enabled = cbkMember.Checked;
            if (!cbkMember.Checked)
            {
                lblDiscount.Text = "1";
                lblPayMoneyDiscount.Text = lblPayMoney.Text;
            }
        }

        private void LoadMember()
        {
            //拼接查询条件
            Dictionary<string ,string > dic=new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(txtId.Text))
            {
                dic.Add("MId",txtId.Text.Trim());
            }
            if (!string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                dic.Add("MPhone",txtPhone.Text.Trim());
            }

            //调用查询方法,得到列表
            MemberInfoBll miBll=new MemberInfoBll();
            List<MemberInfo> list = miBll.GetList(dic);
            //控件赋值
            if (list.Count>0)
            {
                //查到会员信息,显示信息
                MemberInfo mi = list[0];
                lblMoney.Text = mi.MMoney.ToString();
                lblTypeTitle.Text = mi.MTypeTitle;
                lblDiscount.Text = mi.MDiscount.ToString();

                lblPayMoneyDiscount.Text=(decimal.Parse(lblPayMoney.Text)*decimal.Parse(lblDiscount.Text)).ToString();
            }
            else
            {
                MessageBox.Show("信息有误,未查到此会员");
            }
        }


        private void txtId_Leave(object sender, EventArgs e)
        {
            LoadMember();
        }


        private void txtPhone_Leave(object sender, EventArgs e)
        {
            LoadMember();
        }

        private void btnOrderPay_Click(object sender, EventArgs e)
        {
            //1、根据是否使用余额决定扣款方式
            //2、将订单状态为IsPage=1
            //3、将餐桌状态IsFree=1
            //未完成项:不是会员结账
            if (cbkMember.Checked)
            {
                if (oiBll.Pay(cbkMoney.Checked, int.Parse(txtId.Text), Convert.ToDecimal(lblPayMoneyDiscount.Text),
                    orderId,
                    Convert.ToDecimal(lblDiscount.Text)))
                {
                    //MessageBox.Show("结账成功");
                    RefreshList();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("结账失败");
                }
            }
            else
            {
                if (oiBll.Pay(false, 0, Convert.ToDecimal(lblPayMoneyDiscount.Text),
                    orderId,
                    Convert.ToDecimal(lblDiscount.Text)))
                {
                    //MessageBox.Show("结账成功");
                    RefreshList();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("结账失败");
                }
            }
        }

        private void cbkMoney_CheckedChanged(object sender, EventArgs e)
        {
            if (decimal.Parse(lblPayMoneyDiscount.Text)>decimal.Parse(lblMoney.Text))
            {
                MessageBox.Show("余额不足,请充值或现金结账");
                cbkMoney.Checked = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
