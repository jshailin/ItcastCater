using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterModel;
using MySql.Data.MySqlClient;

namespace CaterDal
{
    public partial class HallInfoDal
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public List<HallInfo> GetList()
        {
            //构造sql语句
            string sql = "SELECT HId, HTitle, HIsDelete FROM HallInfo WHERE HIsDelete=0";
            //执行,并得到表格
            DataTable dt = MysqlHelper.GetDataTable(sql);
            //定义列表对象,遍历表格,填充列表并返回
            List<HallInfo> list=new List<HallInfo>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new HallInfo()
                {
                    HId=Convert.ToInt32(row["HId"]),
                    HTitle=row["HTitle"].ToString()
                });
            }

            return list;
        }

       /// <summary>
       /// 添加数据
       /// </summary>
       /// <param name="hi"></param>
       /// <returns></returns>
        public int Insert(HallInfo hi)
        {
            //构造sql语句及参数
            string sql = "INSERT INTO HallInfo (HTitle, HIsDelete) VALUES (@HTitle,0) ";
            MySqlParameter p = new MySqlParameter("@HTitle",hi.HTitle);
           //执行并返回影响行数
           return MysqlHelper.ExecuteNonQuery(sql, p);
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="hi"></param>
        /// <returns></returns>
        public int Update(HallInfo hi)
        {
            //构造sql语句及参数
            string sql = "UPDATE HallInfo SET HTitle =@HTitle WHERE HId = @HId";
            MySqlParameter[] ps =
            {
                new MySqlParameter("@HId",hi.HId), 
                new MySqlParameter("@HTitle", hi.HTitle)
            };
            //执行并返回影响行数
            return MysqlHelper.ExecuteNonQuery(sql, ps);
        }

        /// <summary>
        /// 根据id进行软删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            //构造sql语句及参数
            string sql = "UPDATE HallInfo SET HIsDelete=1 WHERE HId = @HId";
            MySqlParameter p = new MySqlParameter("@HId", id);
            //执行并返回影响行数
            return MysqlHelper.ExecuteNonQuery(sql, p);
        }
    }
}
