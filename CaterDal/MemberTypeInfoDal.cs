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
   public partial class MemberTypeInfoDal
    {
       /// <summary>
       /// 查询未删除数据
       /// </summary>
       /// <returns></returns>
       public List<MemberTypeInfo> GetList()
       {
           //构造查询sql语句
           string sql = "SELECT MID,MTitle,MDiscount,MIsDelete FROM MemberTypeInfo WHERE MIsDelete=0";
           //执行查询,返回表格
           DataTable dt = MysqlHelper.GetDataTable(sql);
           //遍历表格,填充列表并返回
           List<MemberTypeInfo> list = new List<MemberTypeInfo>();
           foreach (DataRow row in dt.Rows)
           {
               list.Add(new MemberTypeInfo()
               {
                   MId=Convert.ToInt32(row["MId"]),
                   MTitle=row["MTitle"].ToString(),
                   MDiscount=Convert.ToDecimal(row["MDiscount"])
               });
           }
           return list;
       }

       /// <summary>
       /// 添加数据
       /// </summary>
       /// <param name="mti"></param>
       /// <returns></returns>
       public int Insert(MemberTypeInfo mti)
       {
           //构造添加sql语句
           string sql = "INSERT INTO MemberTypeInfo (MTitle,MDiscount,MIsDelete) VALUES(@MTitle,@MDiscount,0)";
           //构造参数
           MySqlParameter[] ps =
           {
               new MySqlParameter("@MTitle", mti.MTitle),
               new MySqlParameter("@MDiscount", mti.MDiscount),
           };
           //调用操作数据方法
           return MysqlHelper.ExecuteNonQuery(sql, ps);
       }

       /// <summary>
       /// 修改数据
       /// </summary>
       /// <param name="mti"></param>
       /// <returns></returns>
       public int Update(MemberTypeInfo mti)
       {
           //构造修改sql语句
           string sql = "UPDATE MemberTypeInfo SET MTitle=@MTitle,MDiscount=@MDiscount WHERE MId =@MId";
           //构造参数
           MySqlParameter[] ps =
           {
               new MySqlParameter("@MTitle", mti.MTitle),
               new MySqlParameter("@MDiscount", mti.MDiscount),
               new MySqlParameter("@MId",mti.MId), 
           };
           //调用操作数据方法
           return MysqlHelper.ExecuteNonQuery(sql, ps);
       }

       /// <summary>
       /// 软删除数据
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public int Delete(int id)
       {
           //构造软删除sql语句
           string sql = "UPDATE MemberTypeInfo SET MIsDelete=1 WHERE MId =@MId";
           //构造参数
           MySqlParameter p = new MySqlParameter("@MId", id);
           //调用操作数据方法
           return MysqlHelper.ExecuteNonQuery(sql, p);
       }
    }
}
