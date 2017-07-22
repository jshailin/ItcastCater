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
    public partial class OrderInfoDal
    {
        /// <summary>
        /// 开单操作,插入订单,更新餐桌状态
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public int KaiOrder(int tableId)
        {
            string sql =
                "INSERT INTO OrderInfo (ODate,IsPay,TableId) VALUES (NOW(),0,@TableId)" + //插入订单
                "; UPDATE TableInfo SET TIsFree =0 WHERE TId = @TableId";                 //更新餐桌状态

            MySqlParameter p = new MySqlParameter("@TableId", tableId);

            return MysqlHelper.ExecuteNonQuery(sql, p);
        }

        /// <summary>
        /// 点菜操作
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="dishId"></param>
        /// <returns></returns>
        public int DianCai(int orderId, int dishId)
        {
            string sql = "INSERT INTO OrderDetailInfo (OrderId, DishId, Count) VALUES (@OrderId, @DishId,1)";

            MySqlParameter[] ps =
            {
                new MySqlParameter("@OrderId",orderId),
                new MySqlParameter("@DishId",dishId), 
            };

            return MysqlHelper.ExecuteNonQuery(sql, ps);
        }

        /// <summary>
        /// 判断订单中这个菜是否已经存在
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="dishId"></param>
        /// <returns></returns>
        public bool HasCai(int orderId, int dishId)
        {
            string sql = "SELECT COUNT(*) FROM OrderDetailInfo WHERE OrderId=@OrderId AND DishId=@DishId";

            MySqlParameter[] ps =
            {
                new MySqlParameter("@OrderId",orderId),
                new MySqlParameter("@DishId",dishId), 
            };

            return Convert.ToInt32(MysqlHelper.ExecuteScalar(sql, ps)) > 0;
        }

        /// <summary>
        /// 菜的数量加一
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="dishId"></param>
        /// <returns></returns>
        public int CaiJiaYiFeng(int orderId, int dishId)
        {
            string sql = "UPDATE OrderDetailInfo SET Count= Count+1 WHERE OrderId=@OrderId AND DishId=@DishId";

            MySqlParameter[] ps =
            {
                new MySqlParameter("@OrderId",orderId),
                new MySqlParameter("@DishId",dishId), 
            };

            return MysqlHelper.ExecuteNonQuery(sql, ps);
        }

        /// <summary>
        /// 根据订单详表编号oId修改数量
        /// </summary>
        /// <param name="oId"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public int UpdateCountByOid(int oId, int count)
        {
            string sql = "UPDATE OrderDetailInfo SET Count= @Count WHERE OId=@OId";

            MySqlParameter[] ps =
            {
                new MySqlParameter("@Count",count),
                new MySqlParameter("@OId",oId), 
            };

            return MysqlHelper.ExecuteNonQuery(sql, ps);
        }

        /// <summary>
        /// 根据订单详表编号oId删除已点的菜
        /// </summary>
        /// <param name="oId"></param>
        /// <returns></returns>
        public int DeleteOrderDishByOId(int oId)
        {
            string sql = "DELETE FROM OrderDetailInfo WHERE OId = @OId";
            MySqlParameter p = new MySqlParameter("@OId", oId);

            return MysqlHelper.ExecuteNonQuery(sql, p);
        }

        /// <summary>
        /// 根据桌号查询出订单号
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public int GetOrderIdByTableId(int tableId)
        {
            string sql = "SELECT OId FROM OrderInfo " +
                " WHERE TableId=@TableId AND IsPay=0 ORDER BY OId DESC LIMIT 1";
            MySqlParameter p = new MySqlParameter("@TableId", tableId);

            return Convert.ToInt32(MysqlHelper.ExecuteScalar(sql, p));
        }

        /// <summary>
        /// 获取订单的菜单列表
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<OrderDetailInfo> GetDetailList(int orderId)
        {
            string sql = "SELECT odi.OId, di.DTitle, di.DPrice, odi.Count FROM OrderDetailInfo AS odi " +
                         " INNER JOIN DishInfo AS di " +
                         " ON odi.DishId=di.DId " +
                         " WHERE odi.OrderId=@OrderId";
            MySqlParameter p = new MySqlParameter("@OrderId", orderId);

            DataTable dt = MysqlHelper.GetDataTable(sql, p);
            List<OrderDetailInfo> list = new List<OrderDetailInfo>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new OrderDetailInfo()
                {
                    OId = Convert.ToInt32(row["OId"]),
                    DTitle = row["DTitle"].ToString(),
                    DPrice = Convert.ToDecimal(row["DPrice"]),
                    Count = Convert.ToInt32(row["Count"])
                });
            }

            return list;
        }

        /// <summary>
        /// 根据订单，计算订单总金额
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public decimal GetTotalMoneyByOrderId(int orderId)
        {
            string sql = "SELECT SUM(di.DPrice*odi.Count) FROM OrderDetailInfo AS odi " +
                         " INNER JOIN DishInfo AS di " +
                         " ON odi.DishId=di.DId " +
                         " WHERE odi.OrderId=@OrderId";
            MySqlParameter p = new MySqlParameter("@OrderId", orderId);
            var result = MysqlHelper.ExecuteScalar(sql, p);
            if (result != DBNull.Value)
            {
                return Convert.ToDecimal(result);
            }
            return 0;
        }

        /// <summary>
        /// 将总金额存入订单信息表
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="totalMoney"></param>
        /// <returns></returns>
        public int SetOrderMoney(int orderId, decimal totalMoney)
        {
            string sql = "UPDATE OrderInfo SET OMoney= @OMoney WHERE OId=@OId";

            MySqlParameter[] ps =
            {
                new MySqlParameter("@OMoney",totalMoney),
                new MySqlParameter("@OId",orderId), 
            };

            return MysqlHelper.ExecuteNonQuery(sql, ps);
        }

        public int Pay(bool isUseMoney, int memberId, decimal payMoney, int orderid, decimal discount)
        {
            //创建数据库的链接对象
            using (MySqlConnection conn = new MySqlConnection(MysqlHelper.connStr))
            {
                conn.Open();
                //由数据库链接对象创建事务
                using (MySqlTransaction tran = conn.BeginTransaction())
                {
                    int result = 0;
                    //创建command对象
                    MySqlCommand cmd = conn.CreateCommand();
                    //将命令对象启用事务
                    cmd.Transaction = tran;
                    //执行各命令
                    string sql = "";
                    MySqlParameter[] ps;
                    try
                    {
                        //1、根据是否使用余额决定扣款方式
                        if (isUseMoney)
                        {
                            //使用余额
                            sql = "UPDATE MemberInfo SET MMoney=MMoney-@payMoney where MId=@mid";
                            ps = new MySqlParameter[]
                        {
                            new MySqlParameter("@payMoney", payMoney),
                            new MySqlParameter("@mid", memberId)
                        };
                            cmd.CommandText = sql;
                            cmd.Parameters.AddRange(ps);
                            result += cmd.ExecuteNonQuery();
                        }

                        //2、将订单状态为IsPage=1
                        sql = "UPDATE OrderInfo SET IsPay=1";
                        if (memberId != 0)
                        {
                            sql += ",MemberId=@mid";
                        }
                        sql += ",Discount=@discount WHERE OId=@oid";
                        ps = new MySqlParameter[]
                    {
                        new MySqlParameter("@mid", memberId),
                        new MySqlParameter("@discount", discount),
                        new MySqlParameter("@oid", orderid)
                    };
                        cmd.CommandText = sql;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddRange(ps);
                        result += cmd.ExecuteNonQuery();

                        //3、将餐桌状态IsFree=1
                        sql = "UPDATE TableInfo SET TIsFree=1 WHERE TId=(SELECT TableId FROM OrderInfo WHERE OId=@oid)";
                        MySqlParameter p = new MySqlParameter("@oid", orderid);
                        cmd.CommandText = sql;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(p);
                        result += cmd.ExecuteNonQuery();
                        //提交事务
                        tran.Commit();
                    }
                    catch
                    {
                        result = 0;
                        //回滚事务
                        tran.Rollback();
                    }
                    return result;
                }
            }
        }

    }
}
