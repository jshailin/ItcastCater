using MySql.Data.MySqlClient;
using System.Data;
using System.IO;
using System.Text;

namespace CaterDal
{
    public static class MysqlHelper
    {
        //链接字符串
        public static string connStr =null ;

        static MysqlHelper()
        {
            using (StreamReader sr=new StreamReader(@"d:\severItcast", Encoding.Default))
            {
                connStr = sr.ReadLine();
            }
        }
        
        //params：可变参数，目的是省略了手动构造数组的过程，直接指定对象，编译器会帮助我们构造数组，并将对象加入数组中，传递过来
        /// <summary>
        /// 执行命令的方法：insert,update,delete
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, params MySqlParameter[] ps)
        {
            //创建连接对象
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                //创建命令对象
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                //添加参数
                cmd.Parameters.AddRange(ps);
                //打开连接
                conn.Open();
                //执行命令，并返回受影响的行数
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 获取首行首列值的方法
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="ps"></param>
        /// <returns></returns>

        public static object ExecuteScalar(string sql, params MySqlParameter[] ps)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddRange(ps);

                conn.Open();
                //执行命令，获取查询结果中的首行首列的值，返回
                return cmd.ExecuteScalar();
            }
        }

        /// <summary>
        /// 获取结果集
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="ps"></param>
        /// <returns></returns>

        public static DataTable GetDataTable(string sql, params MySqlParameter[] ps)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                //构造适配器对象
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                //构造数据表，用于接收查询结果
                DataTable dt = new DataTable();
                //添加参数
                adapter.SelectCommand.Parameters.AddRange(ps);
                //执行结果
                adapter.Fill(dt);
                //返回结果集
                return dt;
            }
        }
    }
}
