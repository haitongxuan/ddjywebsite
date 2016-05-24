using System.Collections.Generic;

namespace DTcms.Service
{
    public interface IOrderService
    {
        /// <summary>
        /// 统计订单数量
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="strwhere">查询条件</param>
        /// <returns>Int</returns>
        int GetUserOrderCount(string username, string strwhere);
        /// <summary>
        /// 订单列表
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="top">显示条数</param>
        /// <param name="strwhere">查询条件</param>
        /// <returns>List</returns>
        List<Model.orders> GetOrderList(string username, int top, string strwhere);

        /// <summary>
        /// 订单分页列表
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="totalcount">总记录数</param>
        /// <returns>List</returns>
        List<Model.orders> GetOrderList(string username, int pageSize, int pageIndex, string strwhere, out int totalcount);
        /// <summary>
        /// 返回订单商品列表
        /// </summary>
        /// <param name="orderId">订单</param>
        /// <returns>List</returns>
        List<Model.article> GetOrderGoodsList(int orderId);
        /// <summary>
        /// 返回订单状态
        /// </summary>
        /// <param name="id">订单ID</param>
        /// <returns>String</returns>
        string GetOrderStatus(int id);
        /// <summary>
        /// 返回订单是否需要在线支付
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <returns>bool</returns>
        bool GetOrderPaymentStatus(int orderId);
        /// <summary>
        /// 返回税金费用金额
        /// </summary>
        /// <param name="totalAmount">总金额</param>
        /// <returns>decimal</returns>
        decimal GetOrderTaxamount(decimal totalAmount);
    }
}