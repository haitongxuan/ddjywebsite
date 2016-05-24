using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using DTcms.Common;
using DTcms.Service;
using DTcms.Service.Exeception;
using Newtonsoft.Json;

namespace DTcms.Web.tools
{
    /// <summary>
    /// token_ajax 的摘要说明
    /// </summary>
    public class app_token_ajax : IHttpHandler
    {
        Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig();
        Model.userconfig userConfig = new BLL.userconfig().loadConfig();

        private IUserService _userService;
        private IUserTokenService _userTokenService;
        private IUserAddressService _userAddressService;
        private IOrderService _orderService;
        private IForumService _forumService;
        private ICardService _cardService;
        private IBaseService _commonService;
        private ICurriculumService _curriculumService;
        private IShoppingService _shoppingService;
        private IArticleService _articleService;

        public app_token_ajax(IUserService userService, IUserTokenService userTokenService,
            IUserAddressService userAddressService, IOrderService orderService, IForumService forumService,
            ICardService cardService, IBaseService commonService, ICurriculumService curriculumService,
            IShoppingService shoppingService, IArticleService articleService)
        {
            _userService = userService;
            _userTokenService = userTokenService;
            _userAddressService = userAddressService;
            _orderService = orderService;
            _forumService = forumService;
            _cardService = cardService;
            _commonService = commonService;
            _curriculumService = curriculumService;
            _shoppingService = shoppingService;
            _articleService = articleService;
        }

        public void ProcessRequest(HttpContext context)
        {
            //取得处事类型
            string action = DTRequest.GetQueryString("action");
            string token = DTRequest.GetQueryString("token");
            string deviceId = DTRequest.GetQueryString("deviceId");
            string username = DTRequest.GetQueryString("username");
            if (_userService.CheckUserLogined(username))
            {
                if (CheckToken(token, deviceId))
                {
                    switch (action)
                    {
                        case "CreateUserCard":
                            CreateUserCard(context);
                            break;
                        case "CheckUserCard":
                            CheckUserCard(context);
                            break;
                        case "GetUserCard":
                            GetUserCard(context);
                            break;
                        case "GetCards":
                            GetCards(context);
                            break;
                        case "GetUserAvatar":
                            GetUserAvatar(context);
                            break;
                        case "GetUserMessageCount":
                            GetUserMessageCount(context);
                            break;
                        case "GetUserMessageList":
                            GetUserMessageList(context);
                            break;
                        case "GetUserPointList":
                            GetUserPointList(context);
                            break;
                        case "GetUserAmountList":
                            GetUserAmountList(context);
                            break;
                        case "GetUserRechargeList":
                            GetUserRechargeList(context);
                            break;
                        case "GetUserInviteList":
                            GetUserInviteList(context);
                            break;
                        case "GetInviteStatus":
                            GetInviteStatus(context);
                            break;
                        case "GetUserAddrBookList":
                            GetUserAddrBookList(context);
                            break;
                        case "UserInfoEdit":
                            UserInfoEdit(context);
                            break;
                        case "UserEditPassword":
                            UserEditPassword(context);
                            break;
                        case "UserInviteCode":
                            UserInviteCode(context);
                            break;
                        case "UserPointConvert":
                            UserPointConvert(context);
                            break;
                        case "UserAmountRecharge":
                            UserAmountRecharge(context);
                            break;
                        case "UserMessageAdd":
                            UserMessageAdd(context);
                            break;
                        case "UserPointDelete":
                            UserPointDelete(context);
                            break;
                        case "UserAmountDelete":
                            UserAmountDelete(context);
                            break;
                        case "UserRechargeDelete":
                            UserRechargeDelete(context);
                            break;
                        case "UserMessageDelete":
                            UserMessageDelete(context);
                            break;
                        case "CommentAdd":
                            CommentAdd(context);
                            break;
                        case "GetUserCurriculumList":
                            GetUserCurriculumList(context);
                            break;
                        case "GetUserCurriculumItemList":
                            UserCurriculumItems(context);
                            break;
                        case "GetMediaRealPath":
                            GetMediaRealPath(context);
                            break;
                        case "UserCurriculumAdd":
                            UserCurriculumAdd(context);
                            break;
                        case "CheckUserCurriculum":
                            CheckUserCurriculum(context);
                            break;
                        case "PostAdd":
                            PostAdd(context);
                            break;
                        case "PostReply":
                            PostReply(context);
                            break;
                        case "PostEdit":
                            PostEdit(context);
                            break;
                        case "PostMove":
                            PostMove(context);
                            break;
                        case "PostDelete":
                            PostDelete(context);
                            break;
                        case "PostSetLock":
                            PostSetLock(context);
                            break;
                        case "PostSetTop":
                            PostSetTop(context);
                            break;
                        case "PostSetRed":
                            PostSetRed(context);
                            break;
                        case "PostSetHot":
                            PostSetHot(context);
                            break;
                        case "IsModerator":
                            IsModerator(context);
                            break;
                        case "GetUserOrderCount":
                            GetUserOrderCount(context);
                            break;
                        case "GetOrderList":
                            GetOrderList(context);
                            break;
                        case "GetArticleGoodsInfo":
                            GetArticleGoodsInfo(context);
                            break;
                        case "OrderCancel":
                            OrderCancel(context);
                            break;
                        case "ViewCartCount":
                            ViewCartCount(context);
                            break;
                        case "UserAddressEdit":
                            UserAddressEdit(context);
                            break;
                        case "UserAddressDefault":
                            UserAddressDefault(context);
                            break;
                        case "UserAddressDelete":
                            UserAddressDelete(context);
                            break;
                    }
                }
                else
                {
                    context.Response.Write("{\"status\":0,\"msg\":\"验证失败或登录超时，请重新登录！\"}");
                }
            }
            else
            {
                context.Response.Write("{\"status\":0,\"msg\":\"验证失败用户未登录！\"}");
            }
        }

        #region 创建用户卡片======
        private void CreateUserCard(HttpContext context)
        {
            string callIndex = DTRequest.GetQueryString("callIndex");
            string username = DTRequest.GetQueryString("username");
            try
            {
                _cardService.CreateUserCard(callIndex, username);
                context.Response.Write("{\"status\":1,\"msg\":\"制做卡片成功!\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 检验用户卡片有效性======
        private void CheckUserCard(HttpContext context)
        {
            int type = DTRequest.GetQueryInt("type");
            switch (type)
            {
                case 1:
                    {
                        string callIndex = DTRequest.GetQueryString("callIndex");
                        string username = DTRequest.GetQueryString("username");
                        if (string.IsNullOrEmpty(callIndex) || string.IsNullOrEmpty(username))
                            try
                            {
                                bool result = _cardService.CheckUserCard(callIndex, username);
                                if (result)
                                    context.Response.Write("{\"status\":1,\"result\":true}");
                                else
                                {
                                    context.Response.Write("{\"status\":1,\"result\":false}");
                                }
                            }
                            catch (AppCommonException ex)
                            {
                                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
                            }
                    }
                    break;
                case 2:
                    {
                        string code = DTRequest.GetQueryString("code");
                        try
                        {
                            bool result = _cardService.CheckUserCard(code);
                            if (result)
                                context.Response.Write("{\"status\":1,\"result\":true}");
                            else
                            {
                                context.Response.Write("{\"status\":1,\"result\":false}");
                            }
                        }
                        catch (AppCommonException ex)
                        {
                            context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
                        }
                    }
                    break;
            }

        }
        #endregion
        #region 根据卡片类别调用名称和用户名，获取卡片信息=========

        private void GetUserCard(HttpContext context)
        {
            int type = DTRequest.GetQueryInt("type");
            switch (type)
            {
                case 1:
                    {
                        string callIndex = DTRequest.GetQueryString("callIndex");
                        string username = DTRequest.GetQueryString("username");
                        try
                        {
                            var model = _cardService.GetUsersCard(callIndex, username);
                            string jsonStr = JsonConvert.SerializeObject(new
                            {
                                status = 1,
                                cardId = model.CardId,
                                cardCategoryId = model.CardCategoryId,
                                code = model.Code,
                                startDate = model.StartDate,
                                endDate = model.EndDate,
                                createDate = model.CreateDate
                            });
                            context.Response.Write(jsonStr);
                        }
                        catch (AppCommonException ex)
                        {
                            context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
                        }
                    }
                    break;
                case 2:
                    {
                        string code = DTRequest.GetQueryString("code");
                        try
                        {
                            var model = _cardService.GetUsersCard(code);
                            string jsonStr = JsonConvert.SerializeObject(new
                            {
                                status = 1,
                                cardId = model.CardId,
                                cardCategoryId = model.CardCategoryId,
                                code = model.Code,
                                startDate = model.StartDate,
                                endDate = model.EndDate,
                                createDate = model.CreateDate
                            });
                            context.Response.Write(jsonStr);
                        }
                        catch (AppCommonException ex)
                        {
                            context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
                        }
                    }
                    break;
            }
        }

        #endregion
        #region 获取用户的有效期内的卡片======

        private void GetCards(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetQueryString("username");
                List<Model.Card> cardList = _cardService.GetCards(username);
                string jsonStr = JsonConvert.SerializeObject(new
                {
                    status = 1,
                    cards = cardList
                });
                context.Response.Write(jsonStr);
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 获取用户所有的卡片信息======

        private void GetAllCards(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetQueryString("username");
                List<Model.Card> cardList = _cardService.GetAllCards(username);
                string jsonStr = JsonConvert.SerializeObject(new
                {
                    status = 1,
                    cards = cardList
                });
                context.Response.Write(jsonStr);
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 返回用户头像图片地址======
        private void GetUserAvatar(HttpContext context)
        {
            string username = DTRequest.GetQueryString("username");
            try
            {
                string userAvatarUrl = _userService.GetUserAvatar(username);
                context.Response.Write("{\"status\":1,\"userAvatarUrl\":\"" + userAvatarUrl + "\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 统计短消息数量
        private void GetUserMessageCount(HttpContext context)
        {
            string username = DTRequest.GetQueryString("username");
            string whereStr = DTRequest.GetFormString("whereStr");
            try
            {
                int count = _userService.GetUserMessageCount(username, whereStr);
                context.Response.Write("{\"status\":1,\"count\":" + count.ToString() + "}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 短消息列表======

        private void GetUserMessageList(HttpContext context)
        {
            string username = DTRequest.GetQueryString("username");
            int type = DTRequest.GetQueryInt("type");
            try
            {
                switch (type)
                {
                    case 1:
                        {
                            int top = DTRequest.GetQueryInt("top", 0);
                            string whereStr = DTRequest.GetQueryString("whereStr");
                            var list = _userService.GetUserMessageList(username, top, whereStr);
                            string jsonStr = JsonConvert.SerializeObject(new
                            {
                                status = 1,
                                userMessages = list
                            });
                        }
                        break;
                    case 2:
                        {
                            int pageIndex = DTRequest.GetQueryInt("pageIndex", 0);
                            int pageSize = DTRequest.GetQueryInt("pageSize", 0);
                            string whereStr = DTRequest.GetQueryString("whereStr");
                            int count;
                            var result = _userService.GetUserMessageList(username, pageSize, pageIndex, whereStr, out count);
                            string jsonStr = JsonConvert.SerializeObject(new
                            {
                                status = 1,
                                totalCount = count,
                                list = result
                            });
                        }
                        break;
                }
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 积分列表======

        private void GetUserPointList(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetQueryString("username");
                int pageIndex = DTRequest.GetQueryInt("pageIndex", 0);
                int pageSize = DTRequest.GetQueryInt("pageSize", 0);
                string whereStr = DTRequest.GetQueryString("whereStr");
                int count;
                var result = _userService.GetUserPointList(username, pageSize, pageIndex, whereStr, out count);
                string jsonStr = JsonConvert.SerializeObject(new
                {
                    status = 1,
                    totalCount = count,
                    list = result
                });
                context.Response.Write(jsonStr);
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }


        }
        #endregion
        #region 余额明细分页列表======

        private void GetUserAmountList(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetQueryString("username");
                int pageIndex = DTRequest.GetQueryInt("pageIndex", 0);
                int pageSize = DTRequest.GetQueryInt("pageSize", 0);
                string whereStr = DTRequest.GetQueryString("whereStr");
                int count;
                var result = _userService.GetUserAmountList(username, pageSize, pageIndex, whereStr, out count);
                string jsonStr = JsonConvert.SerializeObject(new
                {
                    status = 1,
                    totalCount = count,
                    list = result
                });
                context.Response.Write(jsonStr);
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 充值记录分页列表======

        private void GetUserRechargeList(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetQueryString("username");
                int pageIndex = DTRequest.GetQueryInt("pageIndex", 0);
                int pageSize = DTRequest.GetQueryInt("pageSize", 0);
                string whereStr = DTRequest.GetQueryString("whereStr");
                int count;
                var result = _userService.GetUserRechargeList(username, pageSize, pageIndex, whereStr, out count);
                string jsonStr = JsonConvert.SerializeObject(new
                {
                    status = 1,
                    totalCount = count,
                    list = result
                });
                context.Response.Write(jsonStr);
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 用户邀请码列表======

        private void GetUserInviteList(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetQueryString("username");
                int top = DTRequest.GetQueryInt("top", 0);
                string whereStr = DTRequest.GetFormString("whereStr");
                var result = _userService.GetUserInviteList(username, top, whereStr);
                string jsonStr = JsonConvert.SerializeObject(new
                {
                    status = 1,
                    list = result
                });
                context.Response.Write(jsonStr);
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 返回邀请码状态======
        private void GetInviteStatus(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetQueryString("username");
                string code = DTRequest.GetQueryString("code");
                bool r = _userService.GetInviteStatus(username, code);
                string jsonStr = JsonConvert.SerializeObject(new
                {
                    status = 1,
                    result = r
                });
                context.Response.Write(jsonStr);
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 收货地址列表======

        private void GetUserAddrBookList(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetQueryString("username");
                string whereStr = DTRequest.GetFormString("whereStr");
                int type = DTRequest.GetQueryInt("type");
                switch (type)
                {
                    case 1:
                        {
                            int top = DTRequest.GetQueryInt("top");
                            var result = _userService.GetUserAddrBookList(username, top, whereStr);
                            string jsonStr = JsonConvert.SerializeObject(new
                            {
                                status = 1,
                                list = result
                            });
                            context.Response.Write(jsonStr);
                        }
                        break;
                    case 2:
                        {
                            int pageIndex = DTRequest.GetQueryInt("pageIndex");
                            int pageSize = DTRequest.GetQueryInt("pageSize");
                            int count;
                            var result = _userService.GetUserAddrBookList(username, pageSize, pageIndex, whereStr, out count);
                            string jsonStr = JsonConvert.SerializeObject(new
                            {
                                status = 1,
                                totalCount = count,
                                list = result
                            });
                            context.Response.Write(jsonStr);
                        }
                        break;
                }
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 编辑用户信息======

        private void UserInfoEdit(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetQueryString("username");
                string nickName = Utils.ToHtml(DTRequest.GetFormString("nickName"));
                string sex = DTRequest.GetFormString("sex");
                string birthday = DTRequest.GetFormString("birthday");
                string email = Utils.ToHtml(DTRequest.GetFormString("email"));
                string mobile = Utils.ToHtml(DTRequest.GetFormString("mobile"));
                string telphone = Utils.ToHtml(DTRequest.GetFormString("telephone"));
                string qq = Utils.ToHtml(DTRequest.GetFormString("qq"));
                string msn = Utils.ToHtml(DTRequest.GetFormString("msn"));
                string province = Utils.ToHtml(DTRequest.GetFormString("province"));
                string city = Utils.ToHtml(DTRequest.GetFormString("city"));
                string area = Utils.ToHtml(DTRequest.GetFormString("area"));
                string address = Utils.ToHtml(context.Request.Form["address"]);
                _userService.UserInfoEdit(username, nickName, sex, birthday, email, mobile, telphone, qq,
                    msn, province, city, area, address);
                context.Response.Write("{\"status\":1,\"msg\":\"用户信息编辑成功！\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 修改用户密码======

        private void UserEditPassword(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetQueryString("username");
                string password = DTRequest.GetQueryString("password");
                _userService.UserEditPassword(username, password);
                context.Response.Write("{\"status\":1,\"msg\":\"用户修改密码成功！\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }

        }
        #endregion
        #region 用户申请邀请码======
        private void UserInviteCode(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetQueryString("username");
                _userService.UserInviteCode(username);
                context.Response.Write("{\"status\":1,\"msg\":\"申请邀请码成功！\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 用户积分兑换======
        private void UserPointConvert(HttpContext context)
        {
            try
            {
                decimal amount = DTRequest.GetQueryDecimal("amount", 0);
                string username = DTRequest.GetQueryString("username");
                _userService.UserPointConvert(amount, username);
                context.Response.Write("{\"status\":1,\"msg\":\"积分兑换成功！\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 用户在线充值======
        private void UserAmountRecharge(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetQueryString("username");
                int paymentId = DTRequest.GetQueryInt("paymentId");
                decimal amount = DTRequest.GetQueryDecimal("amount", 0);
                context.Response.Write("{\"status\":1,\"msg\":\"在线充值成功！\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 发送站内短消息======
        private void UserMessageAdd(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetQueryString("username");
                int sendSave = DTRequest.GetQueryInt("sendSave", 0);
                string title = DTRequest.GetQueryString("title");
                string content = Utils.ToHtml(DTRequest.GetQueryString("content"));
                _userService.UserMessageAdd(username, sendSave, title, content);
                context.Response.Write("{\"status\":1,\"msg\":\"发送站内短消息成功！\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 删除用户积分明细======
        private void UserPointDelete(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetQueryString("username");
                int id = DTRequest.GetQueryInt("id");
                _userService.UserPointDelete(username, id);
                context.Response.Write("{\"status\":1,\"msg\":\"删除用户积分明细成功！\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 删除用户收支明细======
        private void UserAmountDelete(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetQueryString("username");
                int id = DTRequest.GetQueryInt("id");
                _userService.UserPointDelete(username, id);
                context.Response.Write("{\"status\":1,\"msg\":\"删除用户收支明细成功！\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 删除用户充值记录======
        private void UserRechargeDelete(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetQueryString("username");
                int id = DTRequest.GetQueryInt("id");
                _userService.UserPointDelete(username, id);
                context.Response.Write("{\"status\":1,\"msg\":\"删除用户充值记录成功！\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 删除用户消息======
        private void UserMessageDelete(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetQueryString("username");
                int id = DTRequest.GetQueryInt("id");
                _userService.UserPointDelete(username, id);
                context.Response.Write("{\"status\":1,\"msg\":\"删除用户消息成功！\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 提交评论的处理方法======
        private void CommentAdd(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetFormString("username");
                int articleId = DTRequest.GetFormInt("articleId");
                string content = Utils.ToHtml(DTRequest.GetFormString("content"));
                _articleService.CommentAdd(username, articleId, content);
                context.Response.Write("{\"status\":1,\"msg\":\"您的评论已提交！\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 获取用户课程列表======
        private void GetUserCurriculumList(HttpContext context)
        {
            try
            {
                int type = DTRequest.GetQueryInt("type");
                switch (type)
                {
                    case 1:
                        {

                            int top = DTRequest.GetFormInt("top", 0);
                            string username = DTRequest.GetFormString("username");
                            string whereStr = DTRequest.GetFormString("whereStr");
                            var result = _curriculumService.GetUserCurriculumList(top, username, whereStr);
                            string jsonStr = JsonConvert.SerializeObject(new
                            {
                                status = 1,
                                list = result
                            })
                            ; context.Response.Write(jsonStr);
                        }
                        break;
                    case 2:
                        {
                            int top = DTRequest.GetFormInt("top", 0);
                            string username = DTRequest.GetFormString("username");
                            string whereStr = DTRequest.GetFormString("whereStr");
                            int categoryId = DTRequest.GetFormInt("categoryId", 0);
                            var result = _curriculumService.GetUserCurriculumList(top, username, categoryId, whereStr);
                            string jsonStr = JsonConvert.SerializeObject(new
                            {
                                status = 1,
                                list = result
                            })
                            ; context.Response.Write(jsonStr);
                        }
                        break;
                    case 3:
                        {
                            string username = DTRequest.GetFormString("username");
                            int pageIndex = DTRequest.GetFormInt("pageIndex", 1);
                            int pageSize = DTRequest.GetFormInt("pageSize", 10);
                            string whereStr = DTRequest.GetFormString("whereStr");
                            string orderby = DTRequest.GetFormString("orderby");
                            int count;
                            var result = _curriculumService.GetUserCurriculumList(username, pageSize, pageIndex,
                                whereStr, orderby, out count);
                            string jsonStr = JsonConvert.SerializeObject(new
                            {
                                status = 1,
                                totalCount = count,
                                list = result
                            })
                            ; context.Response.Write(jsonStr);
                        }
                        break;
                    case 4:
                        {
                            int categoryId = DTRequest.GetFormInt("categoryId", 0);
                            string username = DTRequest.GetFormString("username");
                            int pageIndex = DTRequest.GetFormInt("pageIndex", 1);
                            int pageSize = DTRequest.GetFormInt("pageSize", 10);
                            string whereStr = DTRequest.GetFormString("whereStr");
                            string orderby = DTRequest.GetFormString("orderby");
                            int count;
                            var result = _curriculumService.GetUserCurriculumList(username, categoryId, pageSize, pageIndex,
                                whereStr, orderby, out count);
                            string jsonStr = JsonConvert.SerializeObject(new
                            {
                                status = 1,
                                totalCount = count,
                                list = result
                            })
                            ; context.Response.Write(jsonStr);
                        }
                        break;
                }
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 获取用户课程章节列表======
        private void UserCurriculumItems(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetFormString("username");
                int id = DTRequest.GetFormInt("id", 0);
                var result = _curriculumService.UserCurriculumItems(username, id);
                string jsonStr = JsonConvert.SerializeObject(new
                {
                    status = 1,
                    list = result
                })
                ; context.Response.Write(jsonStr);
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 获取课程章节对应媒体地址======
        private void GetMediaRealPath(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetFormString("username");
                string mediaCode = DTRequest.GetFormString("code");
                var result = _curriculumService.GetMediaRealPath(username, mediaCode);
                string jsonStr = JsonConvert.SerializeObject(new
                {
                    status = 1,
                    result = result
                });
                context.Response.Write(jsonStr);
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 用户观看课程内容，新增观看记录======
        private void UserCurriculumAdd(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetFormString("username");
                int curriulumId = DTRequest.GetFormInt("id");
                int curriulumItemId = DTRequest.GetFormInt("itemId");
                _curriculumService.UserCurriculumAdd(username, curriulumId, curriulumItemId);
                context.Response.Write("{\"status\":1,\"msg\":\"新增观看记录!\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 检查用户数是否观看过该课程======
        private void CheckUserCurriculum(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetFormString("username");
                int curriulumId = DTRequest.GetFormInt("id");
                var result = _curriculumService.CheckUserCurriculum(username, curriulumId);
                context.Response.Write("{\"status\":1,\"result\":\"" + result + "\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 发布帖子======
        private void PostAdd(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetFormString("username");
                string title = DTRequest.GetFormString("title");
                string content = DTRequest.GetFormString("content");
                int boardId = DTRequest.GetFormInt("boardId");
                int postId = DTRequest.GetFormInt("postId");
                _forumService.PostAdd(username, title, content, boardId, postId);
                context.Response.Write("{\"status\":1,\"msg\":\"发帖成功！\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 回复帖子======
        private void PostReply(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetFormString("username");
                string title = DTRequest.GetFormString("title");
                string content = DTRequest.GetFormString("content");
                int boardId = DTRequest.GetFormInt("boardId");
                int postId = DTRequest.GetFormInt("postId");
                _forumService.PostReply(username, title, content, boardId, postId);
                context.Response.Write("{\"status\":1,\"msg\":\"回复成功！\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 编辑帖子======
        private void PostEdit(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetFormString("username");
                string title = DTRequest.GetFormString("title");
                string content = DTRequest.GetFormString("content");
                int postId = DTRequest.GetFormInt("postId");
                _forumService.PostEdit(username, title, content, postId);
                context.Response.Write("{\"status\":1,\"msg\":\"修改成功！\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 移动帖子======
        private void PostMove(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetFormString("username");
                int boardId = DTRequest.GetFormInt("toBoardId");
                int postId = DTRequest.GetFormInt("postId");
                string opRemark = DTRequest.GetFormString("opRemark");
                _forumService.PostMove(username, boardId, postId, opRemark);
                context.Response.Write("{\"status\":1,\"msg\":\"帖子移动成功！\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 删除帖子======
        private void PostDelete(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetFormString("username");
                int postId = DTRequest.GetFormInt("postId");
                string opRemark = DTRequest.GetFormString("opRemark");
                _forumService.PostDelete(username, postId, opRemark);
                context.Response.Write("{\"status\":1,\"msg\":\"帖子删除成功！\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 锁定帖子======
        private void PostSetLock(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetFormString("username");
                int postId = DTRequest.GetFormInt("postId");
                string optIp = DTRequest.GetFormString("optIp");
                string opRemark = DTRequest.GetFormString("opRemark");
                _forumService.PostSetLock(username, postId, optIp, opRemark);
                context.Response.Write("{\"status\":1,\"msg\":\"帖子锁定成功！\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 置顶帖子======
        private void PostSetTop(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetFormString("username");
                int postId = DTRequest.GetFormInt("postId");
                string optIp = DTRequest.GetFormString("optIp");
                string opRemark = DTRequest.GetFormString("opRemark");
                _forumService.PostSetTop(username, postId, optIp, opRemark);
                context.Response.Write("{\"status\":1,\"msg\":\"帖子置顶成功！\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 设置精华======
        private void PostSetRed(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetFormString("username");
                int postId = DTRequest.GetFormInt("postId");
                string optIp = DTRequest.GetFormString("optIp");
                string opRemark = DTRequest.GetFormString("opRemark");
                _forumService.PostSetRed(username, postId, optIp, opRemark);
                context.Response.Write("{\"status\":1,\"msg\":\"帖子设置精华成功！\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 设置热门======
        private void PostSetHot(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetFormString("username");
                int postId = DTRequest.GetFormInt("postId");
                string optIp = DTRequest.GetFormString("optIp");
                string opRemark = DTRequest.GetFormString("opRemark");
                _forumService.PostSetHot(username, postId, optIp, opRemark);
                context.Response.Write("{\"status\":1,\"msg\":\"帖子设置热门成功！\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 判断是否版主======
        private void IsModerator(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetFormString("username");
                int boardId = DTRequest.GetFormInt("boardId");
                var result = _forumService.IsModerator(boardId, username);
                context.Response.Write("{\"status\":1,\"result\":" + result.ToString() + "}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion.
        #region 获取帖子分页列表======
        private void GetReplyList(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetFormString("username");
                int boardId = DTRequest.GetFormInt("boardId", 0);
                int postId = DTRequest.GetFormInt("postId", 0);
                int pageSize = DTRequest.GetFormInt("pageSize", 10);
                int pageIndex = DTRequest.GetFormInt("pageIndex", 1);
                string whereStr = DTRequest.GetFormString("whereStr");
                int count;
                var result = _forumService.GetReplyList(boardId, postId, username, pageSize, pageIndex,
                    whereStr, out count);
                string jsonStr = JsonConvert.SerializeObject(new
                {
                    status = 1,
                    totalCount = count,
                    list = result
                })
                ; context.Response.Write(jsonStr);
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion

        #region 统计订单数量======
        private void GetUserOrderCount(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetFormString("username");
                string whereStr = DTRequest.GetFormString("whereStr");
                var result = _orderService.GetUserOrderCount(username, whereStr);
                context.Response.Write("{\"status\":1,\"result\":" + result + "}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 订单列表======
        private void GetOrderList(HttpContext context)
        {
            try
            {
                int type = DTRequest.GetQueryInt("type");
                switch (type)
                {
                    case 1:
                        {
                            string username = DTRequest.GetFormString("username");
                            int top = DTRequest.GetFormInt("top");
                            string whereStr = DTRequest.GetFormString("whereStr");
                            var result = _orderService.GetOrderList(username, top, whereStr);
                            string jsonStr = JsonConvert.SerializeObject(new
                            {
                                status = 1,
                                list = result
                            })
                            ; context.Response.Write(jsonStr);
                        }
                        break;
                    case 2:
                        {
                            string username = DTRequest.GetFormString("username");
                            int pageSize = DTRequest.GetFormInt("pageSize", 10);
                            int pageIndex = DTRequest.GetFormInt("pageIndex", 1);
                            string whereStr = DTRequest.GetFormString("whereStr");
                            int count;
                            var result = _orderService.GetOrderList(username, pageSize, pageIndex, whereStr, out count);
                            string jsonStr = JsonConvert.SerializeObject(new
                            {
                                status = 1,
                                totalCount = count,
                                list = result
                            })
                            ; context.Response.Write(jsonStr);
                        }
                        break;
                }
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 返回购物车商品总数======
        private void GetCartQuantity(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetFormString("username");
                var result = _shoppingService.GetCartQuantity(username);
                context.Response.Write("{\"status\":1,\"result\":" + result + "}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 返回购物车列表======
        private void GetCartList(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetFormString("username");
                var result = _shoppingService.GetCartList(username);
                string jsonStr = JsonConvert.SerializeObject(new
                {
                    status = 1,
                    list = result
                })
                ; context.Response.Write(jsonStr);
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 购物车加入商品======
        private void CartGoodsAdd(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetFormString("username");
                int articleId = DTRequest.GetFormInt("articleId", 0);
                int goodsId = DTRequest.GetFormInt("goodsId", 0);
                int quantity = DTRequest.GetFormInt("quantity", 0);
                _shoppingService.CartGoodsAdd(username, articleId, goodsId, quantity);
                context.Response.Write("{\"status\":1,\"msg\":\"商品加入购物车！\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 加入结账商品======
        private void CartGoodsBuy(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetFormString("username");
                string jsonData = DTRequest.GetFormString("jsondata");
                _shoppingService.CartGoodsBuy(username, jsonData);
                context.Response.Write("{\"status\":1, \"msg\":\"商品已成功加入购物清单！\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 修改购物车商品======
        private void CartGoodsUpdate(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetFormString("username");
                int articleId = DTRequest.GetFormInt("articleId", 0);
                int goodsId = DTRequest.GetFormInt("goodsId", 0);
                int quantity = DTRequest.GetFormInt("quantity", 0);
                _shoppingService.CartGoodsUpdate(username, articleId, goodsId, quantity);
                context.Response.Write("{\"status\":1, \"msg\":\"商品数量修改成功！\", \"article_id\":"
                    + articleId + ", \"goods_id\":" + goodsId + ", \"quantity\":" + quantity + "}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 删除购物车商品======
        private void CartGoodsDelete(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetFormString("username");
                int clear = DTRequest.GetFormInt("clear", 0);
                int articleId = DTRequest.GetFormInt("articleId", 0);
                int goodsId = DTRequest.GetFormInt("goodsId", 0);
                _shoppingService.CartGoodsDelete(username, articleId, goodsId, clear);
                context.Response.Write("{\"status\":1, \"msg\":\"商品移除成功！\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 保存购物订单======
        private void OrderSave(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetFormString("username");
                string hideGoodsJson = DTRequest.GetFormString("hideGoodsJson"); //获取商品JSON数据
                int bookId = DTRequest.GetFormInt("bookId", 1);
                int paymentId = DTRequest.GetFormInt("paymentId");
                int expressId = DTRequest.GetFormInt("expressId");
                int isInvoice = DTRequest.GetFormInt("isInvoice", 0);
                string acceptName = Utils.ToHtml(DTRequest.GetFormString("acceptName"));
                string province = Utils.ToHtml(DTRequest.GetFormString("province"));
                string city = Utils.ToHtml(DTRequest.GetFormString("city"));
                string area = Utils.ToHtml(DTRequest.GetFormString("area"));
                string address = Utils.ToHtml(DTRequest.GetFormString("address"));
                string telephone = Utils.ToHtml(DTRequest.GetFormString("telephone"));
                string mobile = Utils.ToHtml(DTRequest.GetFormString("mobile"));
                string email = Utils.ToHtml(DTRequest.GetFormString("email"));
                string postCode = Utils.ToHtml(DTRequest.GetFormString("postCode"));
                string message = Utils.ToHtml(DTRequest.GetFormString("message"));
                string invoiceTitle = Utils.ToHtml(DTRequest.GetFormString("invoiceTitle"));
                _shoppingService.OrderSave(username, hideGoodsJson, bookId, paymentId,
                    expressId, isInvoice, acceptName, province, city, area, address, telephone, mobile, email,
                    postCode, message, invoiceTitle);
                context.Response.Write("{\"status\":1, \"msg\":\"恭喜您，订单已成功提交！\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 获取对应的商品信息======
        private void GetArticleGoodsInfo(HttpContext context)
        {
            try
            {
                int articleId = DTRequest.GetFormInt("articleId");
                string specIds = DTRequest.GetFormString("specIds");
                var result = _shoppingService.GetArticleGoodsInfo(articleId, specIds);
                string jsonStr = JsonConvert.SerializeObject(new
                {
                    status = 1,
                    result = result
                });
                context.Response.Write(jsonStr);
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 用户取消订单======
        private void OrderCancel(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetFormString("username");
                string orderNo = DTRequest.GetFormString("orderNo");
                _shoppingService.OrderCancel(username, orderNo);
                context.Response.Write("{\"status\":1, \"msg\":\"取消订单成功！\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 输出购物车总数======
        private void ViewCartCount(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetFormString("username");
                var result = _shoppingService.ViewCartCount(username);
                context.Response.Write("{\"status\":1, \"result\":" + result + "}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 编辑用户地址======
        private void UserAddressEdit(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetFormString("username");
                int addressId = DTRequest.GetFormInt("addressId", 0);
                string acceptName = DTRequest.GetFormString("acceptName");
                string province = DTRequest.GetFormString("province");
                string city = DTRequest.GetFormString("city");
                string area = DTRequest.GetFormString("area");
                string address = DTRequest.GetFormString("address");
                string mobile = DTRequest.GetFormString("mobile");
                string telephone = DTRequest.GetFormString("telephone");
                string email = DTRequest.GetFormString("email");
                string postCode = DTRequest.GetFormString("postCode");
                _userAddressService.UserAddressEdit(username, addressId, acceptName, province, city,
                    area, address, mobile, telephone, email, postCode);
                if (addressId == 0)
                {
                    context.Response.Write("{\"status\":1, \"msg\":\"修改收货地址成功！\"}");
                }
                else
                {
                    context.Response.Write("{\"status\":1, \"msg\":\"新增收货地址成功！\"}");
                }
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 用户设置默认地址======
        private void UserAddressDefault(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetFormString("username");
                int id = DTRequest.GetFormInt("id");
                _userAddressService.UserAddressDefault(username, id);
                context.Response.Write("{\"status\":1, \"msg\":\"默认收货地址设置成功！\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion
        #region 用户删除收货地址======
        private void UserAddressDelete(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetFormString("username");
                int id = DTRequest.GetFormInt("id");
                _userAddressService.UserAddressDelete(username, id);
                context.Response.Write("{\"status\":1, \"msg\":\"收货地址删除成功！\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }
        #endregion





        private bool CheckToken(string token, string deviceId)
        {
            return _userTokenService.CheckUserToken(token, deviceId);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}