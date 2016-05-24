using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DTcms.Common;
using DTcms.Model;
using DTcms.OrderApi.Results;

namespace DTcms.OrderApi.Controllers
{
    [RoutePrefix("api/order")]
    public class OrderController : ApiController
    {
        [HttpGet]
        [Route("getbyid/{id}")]
        public IHttpActionResult GetById(int id)
        {
            var bll = new BLL.orders();
            var order = bll.GetModel(id);
            return Ok(order);
        }
        [HttpGet]
        [Route("getbyorderno/{ordernum}")]
        public IHttpActionResult GetByOrderNum(string ordernum)
        {
            var bll = new BLL.orders();
            var order = bll.GetModel(ordernum);
            return Ok(order);
        }

        [HttpPost]
        [Route("add")]
        public IHttpActionResult Add(orders order)
        {
            var bll = new BLL.orders();
            try
            {
                bll.Add(order);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("paysuccess/{ordernum}/{tradeno}")]
        public IHttpActionResult PaySuccess(string ordernum, string tradeno)
        {
            var result = new PayResult();
            if (ordernum.StartsWith("R")) //充值订单
            {
                BLL.user_recharge bll = new BLL.user_recharge();
                Model.user_recharge model = bll.GetModel(ordernum);
                if (model == null)
                {
                    result.msg = "该订单号不存在";
                    result.success = false;
                    result.status = 201;
                }
                else
                {
                    if (model.status == 1)
                    {
                        result.msg = "该订单已经支付，请不要重复支付";
                        result.success = false;
                        result.status = 202;
                    }
                    //订单编号验证通过后执行
                    bool r = bll.Confirm(ordernum);
                    if (r)
                    {
                        result.msg = "充值成功";
                        result.status = 200;
                        result.success = true;
                    }
                    else
                    {
                        result.msg = "充值订单信息更新失败";
                        result.status = 204;
                        result.success = false;
                    }
                }
            }
            else if (ordernum.StartsWith("B"))
            {
                BLL.orders bll = new BLL.orders();
                Model.orders model = bll.GetModel(ordernum);
                if (model == null)
                {
                    result.msg = "该订单号不存在";
                    result.success = false;
                    result.status = 201;
                }
                else
                {
                    if (model.payment_status == 2) //已付款
                    {
                        result.msg = "该订单已经支付，请不要重复支付";
                        result.success = false;
                        result.status = 202;
                    }
                    //订单编号验证通过后执行
                    bool r = bll.UpdateField(ordernum, "trade_no='" + tradeno + "',status=2,payment_status=2,payment_time='" + DateTime.Now + "'");
                    if (r)
                    {
                        var articlebll = new BLL.article();
                        foreach (var g in model.order_goods)
                        {
                            //判断是否有卡片商品，如果有卡片商品，添加卡片和用户卡片
                            //todo:此处需要增加事务性操作
                            if (articlebll.IsCard(g.article_id))
                            {
                                var article = articlebll.GetModel(g.article_id);
                                string callindex = article.fields["cardcategorycallindex"];
                                var user = new BLL.users().GetModel(model.user_name);
                                var cardcategory = new BLL.CardCategory().GetModel(callindex);
                                var card = new Model.Card();
                                card.CardCategoryId = cardcategory.CardCategoryId;
                                card.Code = Utils.GetCheckCode(7);
                                card.CreateDate = DateTime.Now;
                                card.StartDate = DateTime.Now;
                                card.EndDate = DateTime.Now.AddDays((double)cardcategory.Duration);
                                var cardBll = new BLL.Card();
                                int cardId = cardBll.Add(card);
                                var usercardBll = new BLL.UserCard();
                                var usercard = new Model.UserCard();
                                usercard.CardId = cardId;
                                usercard.CardCategoryId = cardcategory.CardCategoryId;
                                usercard.UserId = user.id;
                                usercardBll.Add(usercard);
                            }
                        }
                        result.msg = "支付成功";
                        result.status = 200;
                        result.success = true;
                    }
                    else
                    {
                        result.msg = "商品订单信息更新失败";
                        result.status = 204;
                        result.success = false;
                    }
                }
            }
            else
            {
                result.msg = "订单号不正确";
                result.success = false;
                result.status = 203;
            }
            return Ok(result);
        }
        [Route("validate")]
        public IHttpActionResult Validate(string ordernum)
        {
            return Ok(ValidateOrderNum(ordernum));
        }

        private PayResult ValidateOrderNum(string ordernum)
        {
            var result = new PayResult();
            if (ordernum.StartsWith("R")) //充值订单
            {
                BLL.user_recharge bll = new BLL.user_recharge();
                Model.user_recharge model = bll.GetModel(ordernum);
                if (model == null)
                {
                    result.msg = "该订单号不存在";
                    result.success = false;
                    result.status = 201;
                    return result;
                }
                if (model.status == 1)
                {
                    result.msg = "该订单已经支付，请不要重复支付";
                    result.success = false;
                    result.status = 202;
                    return result;
                }
                result.msg = "验证通过";
                result.success = true;
                result.status = 200;
                return result;
            }
            else if (ordernum.StartsWith("B"))
            {
                BLL.orders bll = new BLL.orders();
                Model.orders model = bll.GetModel(ordernum);
                if (model == null)
                {
                    result.msg = "该订单号不存在";
                    result.success = false;
                    result.status = 201;
                    return result;
                }
                if (model.payment_status == 2) //已付款
                {
                    result.msg = "该订单已经支付，请不要重复支付";
                    result.success = false;
                    result.status = 202;
                    return result;
                }
                result.msg = "验证通过";
                result.success = true;
                result.status = 200;
                return result;
            }
            else
            {
                result.msg = "订单号不正确";
                result.success = false;
                result.status = 203;
                return result;
            }
        }
    }
}
