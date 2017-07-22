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
    public partial class MemberInfoDal
    {
        /// <summary>
        /// 列表查询
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public List<MemberInfo> GetList(Dictionary<string ,string > dic )
        {
            //构造查询sql语句，连接查询得到会员类型的名称
            string sql = "SELECT mi.MId,mi.MName,mti.MTitle AS MTypeTitle,mi.MPhone,mi.MMoney,mi.MTypeId,mti.MDiscount " +
                        " FROM MemberInfo AS mi " +
                        " INNER JOIN MemberTypeInfo AS mti " +
                        " ON mi.MTypeId=mti.MId "+
                        " WHERE mi.MIsDelete=0";
            //拼接查询条件
            List<MySqlParameter> listP=new List<MySqlParameter>();
            if (dic.Count>0)
            {
                foreach (var pair in dic)
                {
                    //sql+=" AND "+pair.Key+" LIKE '%"+pair.Value+"%'";
                    //写成参数化，防注入
                    sql += " AND mi." + pair.Key + " LIKE @" + pair.Key ;
                    listP.Add(new MySqlParameter("@"+pair.Key,"%"+pair.Value+"%"));
                }
            }
            //查询排序
            sql += " ORDER BY mi.MId";
            //执行查询
            DataTable dt = MysqlHelper.GetDataTable(sql,listP.ToArray());
            //定义list，完成转存
            List<MemberInfo> list=new List<MemberInfo>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new MemberInfo()
                {
                    MId=Convert.ToInt32(row["MId"]),
                    MName=row["MName"].ToString(),
                    MPhone=row["MPhone"].ToString(),
                    MMoney=Convert.ToDecimal(row["MMoney"]),
                    MTypeId = Convert.ToInt32(row["MTypeId"]),
                    MTypeTitle=row["MTypeTitle"].ToString(),
                    MDiscount = Convert.ToDecimal(row["MDiscount"])
                });
            }
            return list;
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="mi"></param>
        /// <returns></returns>
        public int Insert(MemberInfo mi)
        {
            //构造sql语句及参数
            string sql = "INSERT INTO MemberInfo (MTypeId, MName, MPhone, MMoney, MIsDelete) VALUES (@MTypeId, @MName, @MPhone, @MMoney, 0)";
            MySqlParameter[] ps =
            {
                new MySqlParameter("@MTypeId",mi.MTypeId), 
                new MySqlParameter("@MName",mi.MName), 
                new MySqlParameter("@MPhone",mi.MPhone), 
                new MySqlParameter("@MMoney",mi.MMoney), 
            };
            //执行并返回
            return MysqlHelper.ExecuteNonQuery(sql, ps);
        }

       /// <summary>
       /// 修改数据
       /// </summary>
       /// <param name="mi"></param>
       /// <returns></returns>
        public int Update(MemberInfo mi)
        {
            //构造sql语句及参数
            string sql = "UPDATE MemberInfo SET MTypeId = @MTypeId, MName = @MName, MPhone = @MPhone, MMoney = @MMoney WHERE MId= @MId";
            MySqlParameter[] ps =
            {
                new MySqlParameter("@MId",mi.MId), 
                new MySqlParameter("@MTypeId",mi.MTypeId), 
                new MySqlParameter("@MName",mi.MName), 
                new MySqlParameter("@MPhone",mi.MPhone), 
                new MySqlParameter("@MMoney",mi.MMoney), 
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
            string sql = "UPDATE MemberInfo SET MIsDelete=1 WHERE MId= @MId";
            MySqlParameter p =new MySqlParameter("@MId",id);
            //执行并返回
            return MysqlHelper.ExecuteNonQuery(sql, p);
        }
    }
}
