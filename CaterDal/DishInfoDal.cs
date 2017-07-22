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
    public partial class DishInfoDal
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public List<DishInfo> GetList(Dictionary<string ,string > dic)
        {
            //构造sql语句
            string sql =
                "SELECT di.DId, di.DTitle, di.DTypeId, di.DPrice, di.DChar,dti.DTitle AS DTypeTitle, di.DIsDelete " +
                " FROM DishInfo AS di " +
                " INNER JOIN DishTypeInfo AS dti " +
                " ON di.DTypeId=dti.DId " +
                " WHERE di.DIsDelete=0 AND dti.DIsDelete=0 ";
            //拼接筛选条件
            List<MySqlParameter> psList=new List<MySqlParameter>();
            if (dic.Count>0)
            {
                foreach (var pair in dic)
                {
                    sql += " AND " + pair.Key + " LIKE @" + pair.Key;
                    psList.Add(new MySqlParameter("@"+pair.Key,"%"+pair.Value+"%"));
                }
            }
            //查询排序
            sql += " ORDER BY di.DId";
            //执行并等到表格
            DataTable dt = MysqlHelper.GetDataTable(sql,psList.ToArray());
            //定义列表，遍历表格，填充列表并返回
            List<DishInfo> list=new List<DishInfo>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new DishInfo()
                {
                    DId=Convert.ToInt32(row["DId"]),
                    DTitle = row["DTitle"].ToString(),
                    DChar=row["DChar"].ToString(),
                    DPrice=Convert.ToDecimal(row["DPrice"]),
                    DTypeId=Convert.ToInt32(row["DTypeId"]),
                    DTypeTitle=row["DTypeTitle"].ToString()
                });
            }
            return list;
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="di"></param>
        /// <returns></returns>
        public int Insert(DishInfo di)
        {
            //构造sql语句及对应参数
            string sql = "INSERT INTO DishInfo (DTitle, DTypeId, DPrice, DChar, DIsDelete) VALUES (@DTitle, @DTypeId, @DPrice, @DChar, 0)";
            MySqlParameter[] ps =
            {
                new MySqlParameter("@DTitle",di.DTitle), 
                new MySqlParameter("@DTypeId",di.DTypeId), 
                new MySqlParameter("@DPrice",di.DPrice), 
                new MySqlParameter("@DChar",di.DChar), 
            };
            //执行，并返回影响条数
            return MysqlHelper.ExecuteNonQuery(sql, ps);
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="di"></param>
        /// <returns></returns>
        public int Update(DishInfo di)
        {
            //构造sql语句及参数
            string sql = "UPDATE DishInfo SET DTitle = @DTitle, DTypeId = @DTypeId, DPrice = @DPrice, DChar = @DChar WHERE DId = @DId";
            MySqlParameter[] ps =
            {
                new MySqlParameter("@DId",di.DId), 
                new MySqlParameter("@DTitle",di.DTitle), 
                new MySqlParameter("@DTypeId",di.DTypeId), 
                new MySqlParameter("@DPrice",di.DPrice), 
                new MySqlParameter("@DChar",di.DChar), 
            };
            //执行，并返回影响条数
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
            string sql = "UPDATE DishInfo SET DIsDelete=1 WHERE DId = @DId";
            MySqlParameter p =new MySqlParameter("@DId",id);
            //执行，并返回影响条数
            return MysqlHelper.ExecuteNonQuery(sql, p);
        }
    }
}
