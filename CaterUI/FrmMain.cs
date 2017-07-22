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

namespace CaterUI
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void menuQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //判断登陆者的级别，以确定是否显示menuManagerInfo
            int type = Convert.ToInt32(this.Tag);
            if (type == 1)
            {
                //经理
                this.Text = this.Text + "    :管理员帐号";

            }
            else
            {
                //店员,管理员菜单不显示
                menuManagerInfo.Visible = false;
                this.Text = this.Text + "    :店员帐号";
            }
            //加载所有的厅包信息
            LoadHallInfo();
        }

        /// <summary>
        /// 动态加载厅包
        /// </summary>
        private void LoadHallInfo()
        {
            //清空对象
            tcHallInfo.TabPages.Clear();
            //获取所有厅包对象
            HallInfoBll hiBll = new HallInfoBll();
            TableInfoBll tiBll = new TableInfoBll();
            var list = hiBll.GetList();
            //遍历集合,向标签页中添加信息
            foreach (var hallInfo in list)
            {
                //根据厅包对象创建标签页对象
                TabPage tp = new TabPage(hallInfo.HTitle);

                //获取当前厅包的所有餐桌
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("THallId", hallInfo.HId.ToString());
                var listT = tiBll.GetList(dic);
                //动态创建元素添加到标签页上
                ListView lvTableInfo = new ListView();
                lvTableInfo.DoubleClick += lvTableInfo_DoubleClick;
                tp.Controls.Add(lvTableInfo);
                lvTableInfo.ShowItemToolTips = true;
                //控件充满容器
                lvTableInfo.Dock = DockStyle.Fill;
                //让列表使用图片
                lvTableInfo.LargeImageList = imageList1;
                //添加餐桌信息
                foreach (var ti in listT)
                {
                    ListViewItem lvi = new ListViewItem(ti.TTitle, ti.TIsFree == 1 ? 0 : 1);
                    //存一下餐桌编号,后续要用到
                    lvi.Tag = ti.TId;
                    //设置提示信息
                    lvi.ToolTipText = lvi.Tag.ToString() + "号桌 " + (lvi.ImageIndex == 0 ? "空闲" : "使用中");
                    lvTableInfo.Items.Add(lvi);
                }

                //将标签页加入标签容器
                tcHallInfo.TabPages.Add(tp);
            }
        }

        void lvTableInfo_DoubleClick(object sender, EventArgs e)
        {
            //获取餐桌编号
            var lv1 = sender as ListView;
            var lvi = lv1.SelectedItems[0];

            if (lvi.ImageIndex == 0)
            {
                int tableId = Convert.ToInt32(lvi.Tag);
                //MessageBox.Show(tableId.ToString());

                //餐桌空闲
                //开单,向数据库orderinfo表中添加数据
                //并将餐桌状态改为使用中
                OrderInfoBll oiBll = new OrderInfoBll();
                oiBll.KaiDan(tableId);
                //更新项的图标为占用
                lvi.ImageIndex = 1;
                //设置提示信息
                lvi.ToolTipText = lvi.Tag.ToString() + "号桌 " + (lvi.ImageIndex == 0 ? "空闲" : "使用中");
            }

            FrmOrderDish frmOrderDish = new FrmOrderDish();
            frmOrderDish.Tag = lvi.Tag;
            frmOrderDish.ShowDialog();

        }

        private void menuManagerInfo_Click(object sender, EventArgs e)
        {
            FrmManagerInfo frmManagerInfo = FrmManagerInfo.Create();  //单例中不能new FrmManagerInfo()
            frmManagerInfo.Show();
            frmManagerInfo.Focus(); //获得焦点
        }

        private void menuMemberInfo_Click(object sender, EventArgs e)
        {
            FrmMemberInfo frmMemberInfo = FrmMemberInfo.Creat();
            frmMemberInfo.Show();
            frmMemberInfo.Focus();
        }

        private void menuTableInfo_Click(object sender, EventArgs e)
        {
            FrmTableInfo frmTableInfo = FrmTableInfo.Creat();
            frmTableInfo.RefreshMain += LoadHallInfo;
            frmTableInfo.Show();
            frmTableInfo.Focus();
        }

        private void menuDishInfo_Click(object sender, EventArgs e)
        {
            FrmDishInfo frmDishInfo = FrmDishInfo.Creat();
            frmDishInfo.Show();
            frmDishInfo.Focus();
        }

        private void menuOrder_Click(object sender, EventArgs e)
        {
            //获取选择的lvi
            var lv=tcHallInfo.SelectedTab.Controls[0] as ListView;
            var lvTable = lv.SelectedItems[0];
            if (lvTable.ImageIndex==0)
            {
                MessageBox.Show("餐桌还未使用,无法结账");
                return;
            }
            FrmOrderPay frmOrderPay=new FrmOrderPay();
            frmOrderPay.Tag = lvTable.Tag;
            frmOrderPay.RefreshList += LoadHallInfo;
            frmOrderPay.ShowDialog();
        }
    }
}
