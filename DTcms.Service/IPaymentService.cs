using System.Collections.Generic;

namespace DTcms.Service
{
    public interface IPaymentService
    {
        /// <summary>
        /// 返回支付列表
        /// </summary>
        /// <param name="top">显示条数</param>
        /// <param name="strwhere">查询条件</param>
        /// <returns></returns>
        List<Model.payment> GetPaymentList(int top, string strwhere);
        /// <summary>
        /// 返回支付类型的标题
        /// </summary>
        /// <param name="paymentId">ID</param>
        /// <returns>String</returns>
        string GetPaymentTitle(int paymentId);
        /// <summary>
        /// 返回支付费用金额
        /// </summary>
        /// <param name="paymentId">支付ID</param>
        /// <param name="totalAmount">总金额</param>
        /// <returns>decimal</returns>
        decimal GetPaymentPoundageAmount(int paymentId, decimal totalAmount);
    }
}