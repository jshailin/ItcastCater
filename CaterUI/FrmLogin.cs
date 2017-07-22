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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //获取用户输入的信息
            string name = txtName.Text;
            string pwd = txtPwd.Text;
            //调用代码
            int type;
            ManagerInfoBll miBll = new ManagerInfoBll();
            LoginState loginState = miBll.Login(name, pwd,out type);
            switch (loginState)
            {
                case LoginState.Ok:
                    FrmMain main=new FrmMain();
                    main.Tag = type;    //将登陆者类型传递给主窗口
                    main.Show();
                    //将登陆窗口隐藏
                    this.Hide();    
                    break;
                case LoginState.NameError:
                    MessageBox.Show("用户名错误"); 
                    break;
                case LoginState.PwdError:
                    MessageBox.Show("密码错误");
                    break;
            }
        }
    }
}
