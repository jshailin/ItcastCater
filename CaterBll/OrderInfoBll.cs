using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterDal;
using CaterModel;

namespace CaterBll
{
    public partial class OrderInfoBll
    {
        private OrderInfoDal oiDal=new OrderInfoDal();

        /// <summary>
        /// 开单并返回订单号
        /// </summary>
        /// <param name="tableId">餐桌号</param>
        /// <returns></returns>
        public bool  KaiDan(int tableId)
        {
            return oiDal.KaiOrder(tableId)>0;
        }

        public bool DianCai(int orderId, int dishId)
        {
            if (oiDal.HasCai(orderId,dishId))   //是否已有此菜
            {
                //菜的数量加1
                return oiDal.CaiJiaYiFeng(orderId, dishId) > 0;
            }
            else
            {
                //点新的菜
                return oiDal.DianCai(orderId, dishId) > 0;
                
            }
        }

        public bool EditCountByOid(int oId, int count)
        {
            return oiDal.UpdateCountByOid(oId, count) > 0;
        }

        public int GetOidByTid(int tableId)
        {
            return oiDal.GetOrderIdByTableId(tableId);
        }

        public List<OrderDetailInfo> GetOrderDetailInfo(int orderId)
        {
            return oiDal.GetDetailList(orderId);
        }

        public decimal GetToltalMoneyByOrderId(int orderId)
        {
            return oiDal.GetTotalMoneyByOrderId(orderId);
        }

        public bool XiaDan(int orderId,decimal totalMoney)
        {
            return oiDal.SetOrderMoney(orderId, totalMoney) > 0;
        }

        public bool RemoveOrderDishByOId(int oId)
        {
            return oiDal.DeleteOrderDishByOId(oId) > 0;
        }

        public bool Pay(bool isUseMoney, int memberId, decimal payMoney, int orderid, decimal discount)
        {
            return oiDal.Pay(isUseMoney, memberId, payMoney, orderid, discount) > 0;
        }
    }
}
