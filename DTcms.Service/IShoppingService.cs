using System;
using System.Collections.Generic;
using System.Web;
using DTcms.Common;

namespace DTcms.Service
{
    public interface IShoppingService
    {
        #region token=======

        /// <summary>
        /// 返回购物车商品总数
        /// </summary>
        /// <returns>Int</returns>
        int GetCartQuantity(string username);

        /// <summary>
        /// 返回购物车列表
        /// </summary>
        /// <returns>IList</returns>
        List<Model.cart_items> GetCartList(string username);
        #endregion


        #region 获取对应的商品信息===========================
        /// <summary>
        /// 获取对应的商品信息
        /// </summary>
        /// <param name="context"></param>
        Model.article_goods GetArticleGoodsInfo(int articleId, string specIds);
        #endregion

        #region 购物车加入商品===============================
        /// <summary>
        /// 购物车加入商品
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="articleId">文章主键</param>
        /// <param name="goodsId">商品主键</param>
        /// <param name="quantity">数量</param>
        void CartGoodsAdd(string username, int articleId, int goodsId, int quantity);
        #endregion

        #region 加入结账商品=================================
        /// <summary>
        /// 加入结账商品
        /// </summary>
        /// <param name="context"></param>
        void CartGoodsBuy(string username, string jsondata);
        #endregion

        #region 修改购物车商品===============================
        /// <summary>
        /// 修改购物车商品
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="articleId">文章主键</param>
        /// <param name="goodsId">商品主键</param>
        /// <param name="quantity">数量</param>
        void CartGoodsUpdate(string username, int articleId, int goodsId, int quantity);
        #endregion

        #region 删除购物车商品===============================
        /// <summary>
        /// 删除购物车商品
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="articleId">文章主键</param>
        /// <param name="goodsId">商品主键</param>
        /// <param name="clear">是否清空购物车，0否1是</param>
        void CartGoodsDelete(string username, int articleId, int goodsId, int clear);
        #endregion

        #region 保存购物订单=================================
        /// <summary>
        /// 保存购物订单
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="hideGoodsJson">商品json字符串</param>
        /// <param name="bookId">地址主键</param>
        /// <param name="paymentId">支付方式主键</param>
        /// <param name="expressId">配送方式主键</param>
        /// <param name="isInvoice">是否开具发票</param>
        /// <param name="acceptName">收件人</param>
        /// <param name="province">省标识</param>
        /// <param name="city">市标识</param>
        /// <param name="area">区标识</param>
        /// <param name="address">地址</param>
        /// <param name="telephone">电话</param>
        /// <param name="mobile">手机</param>
        /// <param name="email">邮箱</param>
        /// <param name="postCode">邮政编号</param>
        /// <param name="message">信息</param>
        /// <param name="invoiceTitle">发票抬头</param>
        void OrderSave(string username, string hideGoodsJson, int bookId, int paymentId, int expressId,
            int isInvoice, string acceptName, string province, string city, string area, string address,
            string telephone, string mobile, string email, string postCode, string message, string invoiceTitle);
        #endregion

        #region 用户取消订单=================================
        /// <summary>
        /// 用户取消订单
        /// </summary>
        /// <param name="orderNo">订单编号</param>
        void OrderCancel(string username, string orderNo);

        #endregion

        #region 输出购物车总数===============================

        /// <summary>
        /// 输出购物车总数
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        int ViewCartCount(string username);

        #endregion
    }
}