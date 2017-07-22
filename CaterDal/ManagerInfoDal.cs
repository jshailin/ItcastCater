using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterCommon;
using CaterModel;
using MySql.Data.MySqlClient;

namespace CaterDal
{
   public partial class ManagerInfoDal
    {
       /// <summary>
       /// 查询获取结果集
       /// </summary>
       /// <returns></returns>
       public List<ManagerInfo> GetList()
       {
           //构造要查询的sql语句
           string sql = "SELECT MID,MName,MPwd,MType FROM ManagerInfo";
           //使用helper进行查询,得到结果
           DataTable dt = MysqlHelper.GetDataTable(sql);
           //将dt中的数据转存到list中
           List<ManagerInfo> list=new List<ManagerInfo>();
           foreach (DataRow row in dt.Rows)
           {
               list.Add(new ManagerInfo()
               {
                   MId = int.Parse(row["MId"].ToString()),
                   MName=row["MName"].ToString(),
                   MPwd =row["MPwd"].ToString(),
                   MType = int.Parse(row["MType"].ToString())
               });
           }
           //将集合list返回
           return list;
       }

       /// <summary>
       /// 插入数据
       /// </summary>
       /// <param name="mi">ManagerInfo类型的对象</param>
       /// <returns></returns>
       public int Insert(ManagerInfo mi)
       {
           //构造insert语句
           string sql = "insert into ManagerInfo(MName,MPwd,MType) values(@MName,@MPwd,@MType)";
           //构造sql语句的参数
           MySqlParameter[] ps = //使用数组初始化器
           {
               new MySqlParameter("@MName", mi.MName),
               new MySqlParameter("@MPwd", Md5Helper.EncryptString(mi.MPwd)),
               new MySqlParameter("@MType", mi.MType)
           };
           //执行插入操作
           return MysqlHelper.ExecuteNonQuery(sql, ps);
       }

       /// <summary>
       /// 修改数据
       /// </summary>
       /// <param name="mi"></param>
       /// <returns></returns>
       public int Update(ManagerInfo mi)
       {
           //定义参数集合，动态添加
           List<MySqlParameter> listPs=new List<MySqlParameter>();
           //构造update的sql语句
           string sql = "update ManagerInfo set MName=@MName";
           listPs.Add(new MySqlParameter("@MName", mi.MName));
           //判断是否修改密码
           if (!mi.MPwd.Equals("这是原来的密码吗？"))
           {
               sql+=",MPwd=@MPwd";
               listPs.Add(new MySqlParameter("@MPwd", Md5Helper.EncryptString(mi.MPwd)));
           }
           sql+=",MType=@MType where MId=@MId";
           listPs.Add(new MySqlParameter("@MType", mi.MType));
           listPs.Add(new MySqlParameter("@MId", mi.MId));
           //构造语句的参数
           //MySqlParameter[] ps =
           //{
           //    new MySqlParameter("@MName", mi.MName),
           //    new MySqlParameter("@MPwd", mi.MPwd),
           //    new MySqlParameter("@MType", mi.MType),
           //    new MySqlParameter("@MId", mi.MId)
           //};
           //执行语句并返回结果
           return MysqlHelper.ExecuteNonQuery(sql, listPs.ToArray());
       }

       /// <summary>
       /// 根据id删除
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public int Delete(int id)
       {
           //构造删除的sql语句
           string sql = "delete from ManagerInfo where MId=@MId";
           //构造sql语句的参数
           MySqlParameter p=new MySqlParameter("@MId",id);
           //执行操作，并返回
           return MysqlHelper.ExecuteNonQuery(sql, p);
       }

       /// <summary>
       /// 根据名字查询用户信息
       /// </summary>
       /// <param name="name"></param>
       /// <returns></returns>
       public ManagerInfo GetByName(string name)
       {
           //定义一个对象
           ManagerInfo mi = null;
           //构造要查询的sql语句
           string sql = "SELECT MID,MName,MPwd,MType FROM ManagerInfo where MName=@MName";
           MySqlParameter p=new MySqlParameter("@MName",name);
           //使用helper进行查询,得到结果
           DataTable dt = MysqlHelper.GetDataTable(sql, p);
           //判断是否查找到了
           if (dt.Rows.Count>0)
           {
               //用户名是存在的
               mi=new ManagerInfo()
               {
                   MId = int.Parse(dt.Rows[0]["MId"].ToString()),
                   //mysql下，不能用   MName=row["MName"].ToString(),
                   MName = name,
                   MPwd = dt.Rows[0]["MPwd"].ToString(),
                   MType = int.Parse(dt.Rows[0]["MType"].ToString())
               };
           }
           return mi;
       }
    }
}
