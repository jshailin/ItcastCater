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
    public partial class TableInfoDal
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public List<TableInfo> GetList(Dictionary<string ,string > dic )
        {
            //构造sql语句
            string sql = "SELECT ti.TId, ti.TTitle, ti.THallId, ti.TIsFree, ti.TIsDelete, hi.HTitle AS HallTitle " +
                        "FROM TableInfo AS ti " +
                        "INNER JOIN HallInfo AS hi " +
                        "ON ti.THallId=hi.HId " +
                        "WHERE ti.TIsDelete=0 AND hi.HIsDelete=0";
            //拼接筛选条件
            List<MySqlParameter> listP=new List<MySqlParameter>();
            if (dic.Count>0)
            {
                foreach (var pair in dic)
                {
                    sql += " AND " + pair.Key + "=@" + pair.Key;
                    listP.Add(new MySqlParameter("@" + pair.Key, pair.Value));
                }
            }
            //查询排序
            sql += " ORDER BY ti.TId";
            //执行,得到表格
            DataTable dt = MysqlHelper.GetDataTable(sql,listP.ToArray());
            //定义列表,遍历表格,填充列表并返回
            List<TableInfo> list=new List<TableInfo>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new TableInfo()
                {
                    TId=Convert.ToInt32(row["TId"]),
                    TTitle=row["TTitle"].ToString(),
                    HallTitle=row["HallTitle"].ToString(),
                    TIsFree=Convert.ToInt32(row["TIsFree"])
                });
            }

            return list;
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="ti"></param>
        /// <returns></returns>
        public int Insert(TableInfo ti)
        {
            //构造sql语句及参数
            string sql = "INSERT INTO TableInfo (TTitle, THallId, TIsFree, TIsDelete ) VALUES "+
                "(@TTitle, @THallId, @TIsFree, 0)";
            MySqlParameter[] ps=
            {
                new MySqlParameter("@TTitle",ti.TTitle), 
                new MySqlParameter("@THallId",ti.THallId), 
                new MySqlParameter("TIsFree",ti.TIsFree), 
            };
            //执行并返回
            return MysqlHelper.ExecuteNonQuery(sql, ps);
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="ti"></param>
        /// <returns></returns>
        public int Update(TableInfo ti)
        {
            //构造sql语句及参数
            string sql = "UPDATE TableInfo SET TTitle = @TTitle, THallId = @THallId, TIsFree = @TIsFree WHERE TId = @TId ";
            MySqlParameter[] ps =
            {
                new MySqlParameter("@TId",ti.TId), 
                new MySqlParameter("@TTitle",ti.TTitle), 
                new MySqlParameter("@THallId",ti.THallId), 
                new MySqlParameter("TIsFree",ti.TIsFree), 
            };
            //执行并返回
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
            string sql = "UPDATE TableInfo SET TIsDelete = 1 WHERE TId = @TId ";
            MySqlParameter p =new MySqlParameter("@TId",id);
            //执行并返回
            return MysqlHelper.ExecuteNonQuery(sql, p);
        }

        /// <summary>
        /// 设置餐桌状态
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="isFree"></param>
        /// <returns></returns>
        //public int SetState(int tableId, bool isFree)
        //{
        //    string sql = "UPDATE TableInfo SET TIsFree = @TIsFree WHERE TId = @TId ";
        //    MySqlParameter[] ps =
        //    {
        //        new MySqlParameter("@TId",tableId), 
        //        new MySqlParameter("@TIsFree",isFree?1:0), 
        //    };
        //    return MysqlHelper.ExecuteNonQuery(sql, ps);
        //}
    }
}
