using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _01_复习
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //查询数据库
            //0、构造接收数据集合
            List<ManagerInfo> list=new List<ManagerInfo>();
            //1、连接字符串
            string connStr = @"data source=E:\360云盘\Source\C#\ItcastCater\ItcastCater.db;version=3;";
            //2、创建链接
            using (SQLiteConnection conn=new SQLiteConnection(connStr))
            {
                //3、创建命令
                SQLiteCommand cmd=new SQLiteCommand("SELECT MId,MName,MPwd,MType FROM ManagerInfo",conn);
                //4、打开链接
                conn.Open();
                //5、执行命令
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    //6、读取并按行写入集合
                    while (reader.Read())
                    {
                        list.Add(new ManagerInfo()
                        {
                            Mid = int.Parse(reader["Mid"].ToString()),
                            Mname = reader["Mname"].ToString(),
                            Mpwd = reader["Mpwd"].ToString(),
                            Mtype = int.Parse(reader["Mtype"].ToString())
                        });
                    } 
                }
              //7、显示数据
            dataGridView1.DataSource = list;
            }
           
        }
    }
}
