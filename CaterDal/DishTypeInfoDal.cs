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
    public partial class DishTypeInfoDal
    {
       /// <summary>
       /// 获取列表
       /// </summary>
       /// <returns></returns>
        public List<DishTypeInfo> GetList()
        {
            //构造查询sql语句
            string sql = "SELECT DId, DTitle, DIsDelete FROM DishTypeInfo WHERE DIsDelete=0";
            //执行查询，取得表格
            DataTable dt = MysqlHelper.GetDataTable(sql);
            //定义列表，遍历表格，填充列表并返回
            List<DishTypeInfo> list=new List<DishTypeInfo>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new DishTypeInfo()
                {
                    DId=Convert.ToInt32(row["DId"]),
                    DTitle=row["DTitle"].ToString()
                });
            }
            return list;
        }

        /// <summary>
        /// 添加操作
        /// </summary>
        /// <param name="dti"></param>
        /// <returns></returns>
        public int Insert(DishTypeInfo dti)
        {
            //构造sql语句及参数
            string sql = "INSERT INTO DishTypeInfo (DTitle, DIsDelete) VALUES (@DTitle, 0)";
            MySqlParameter p=new MySqlParameter("@DTitle",dti.DTitle);
            //执行并返回
            return MysqlHelper.ExecuteNonQuery(sql, p);
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="dti"></param>
        /// <returns></returns>
        public int Update(DishTypeInfo dti)
        {
            //构造sql语句及参数
            string sql = "UPDATE DishTypeInfo SET DTitle = @DTitle WHERE DId = @DId";
            MySqlParameter[] ps =
            {
                new MySqlParameter("@DTitle", dti.DTitle),
                new MySqlParameter("@DId",dti.DId), 
            };
            //执行并返回
            return MysqlHelper.ExecuteNonQuery(sql, ps);
        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id) 
        {
            //构造sql语句及参数
            string sql = "UPDATE DishTypeInfo SET DIsDelete = 1 WHERE DId = @DId";
            MySqlParameter p =new MySqlParameter("@DId",id);
            //执行并返回
            return MysqlHelper.ExecuteNonQuery(sql, p);
        }
    }

}
