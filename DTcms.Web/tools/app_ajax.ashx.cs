using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using DTcms.Common;
using DTcms.Service;
using DTcms.Service.Exeception;
using Newtonsoft.Json;

namespace DTcms.Web.tools
{
    /// <summary>
    /// app_ajax 的摘要说明
    /// </summary>
    public class app_ajax : IHttpHandler
    {
        private IArticleService _articleService;
        private IBaseService _commonService;
        private IPaymentService _paymentService;
        private ICurriculumService _curriculumService;
        private IGoodsService _goodsService;
        private IExpressService _expressService;
        private IUserService _userService;
        private IForumService _forumService;
        private IOrderService _orderService;
        private ISiteService _siteService;

        public app_ajax(IArticleService articleService, IBaseService commonService, IPaymentService paymentService,
            ICurriculumService curriculumService, IGoodsService goodsService, IExpressService expressService,
            IUserService userService, IForumService formService, IOrderService orderService, ISiteService siteService)
        {
            _articleService = articleService;
            _commonService = commonService;
            _paymentService = paymentService;
            _curriculumService = curriculumService;
            _goodsService = goodsService;
            _expressService = expressService;
            _userService = userService;
            _forumService = formService;
            _orderService = orderService;
            _siteService = siteService;
        }

        public void ProcessRequest(HttpContext context)
        {
            string action = DTRequest.GetFormString("action");
            string appKey = DTRequest.GetFormString("appkey");
            if (_commonService.CheckAppKey(appKey))
            {
                switch (action)
                {
                    case "GetOauthAppList":
                        GetOauthAppList(context);
                        break;
                    case "UserGetPassword":
                        UserGetPassword(context);
                        break;
                    case "UserResetPassword":
                        UserResetPassword(context);
                        break;
                    case "UserLogin":
                        UserLogin(context);
                        break;
                    case "CheckUserLogin":
                        CheckUserLogin(context);
                        break;
                    case "UserRegister":
                        UserRegister(context);
                        break;
                    case "UserVerifySmscode":
                        UserVerifySmscode(context);
                        break;
                    case "GetArticleList":
                        GetArticleList(context);
                        break;
                    case "GetArticleCategories":
                        GetArticleCategories(context);
                        break;
                    case "GetArticleTags":
                        GetArticleTags(context);
                        break;
                    case "GetArticleContent":
                        GetArticleContent(context);
                        break;
                    case "CommentList":
                        CommentList(context);
                        break;
                    case "ViewArticleClick":
                        ViewArticleClick(context);
                        break;
                    case "ViewCommentCount":
                        ViewCommentCount(context);
                        break;
                    case "ViewAttachCount":
                        ViewAttachCount(context);
                        break;
                    case "GetCurriculumList":
                        GetCurriculumList(context);
                        break;
                    case "GetRelationCurriculumList":
                        GetRelationCurriculumList(context);
                        break;
                    case "GetCurriculumCategories":
                        GetCurriculumCategories(context);
                        break;
                    case "CheckCurriculumVip":
                        CheckCurriculumVip(context);
                        break;
                    case "GetCurriculum":
                        GetCurriculum(context);
                        break;
                    case "GetCurriculumImgUrl":
                        GetCurriculumImgUrl(context);
                        break;
                    case "GetCurriculumDescribe":
                        GetCurriculumDescribe(context);
                        break;
                    case "GetMediaRealPath":
                        GetMediaRealPath(context);
                        break;
                    case "GetExpressTitle":
                        GetExpressTitle(context);
                        break;
                    case "GetExpressList":
                        GetExpressList(context);
                        break;
                    case "GetBoardList":
                        GetBoardList(context);
                        break;
                    case "GetBoardGetchildlist":
                        GetBoardGetchildlist(context);
                        break;
                    case "GetCategoryId":
                        GetCategoryId(context);
                        break;
                    case "GetArticleGoodsSpec":
                        GetArticleGoodsSpec(context);
                        break;
                    case "GetArticleSpecParent":
                        GetArticleSpecParent(context);
                        break;
                    case "GetArticleSpecChild":
                        GetArticleSpecChild(context);
                        break;
                    case "GetOrderGoodsList":
                        GetOrderGoodsList(context);
                        break;
                    case "GetOrderStatus":
                        GetOrderStatus(context);
                        break;
                    case "GetOrderPaymentStatus":
                        GetOrderPaymentStatus(context);
                        break;
                    case "GetOrderTaxamount":
                        GetOrderTaxamount(context);
                        break;
                    case "GetPaymentPoundageAmount":
                        GetPaymentPoundageAmount(context);
                        break;
                    case "GetPaymentTitle":
                        GetPaymentTitle(context);
                        break;
                    case "GetPaymentList":
                        GetPaymentList(context);
                        break;
                    case "GetValiCode":
                        GetValiCode(context);
                        break;
                    case "ArticleClickAdd":
                        ArticleClickAdd(context);
                        break;
                    case "GetPostList":
                        GetPostList(context);
                        break;
                }
            }
            else
            {
                context.Response.Write("{\"status\":103,\"msg\":\"AppKey无效！\"}");
            }
        }

        #region OAuth平台列表======

        private void GetOauthAppList(HttpContext context)
        {
            try
            {
                int top = DTRequest.GetFormInt("top", 0);
                string whereStr = DTRequest.GetFormString("whereStr");
                var tlist = _userService.GetOauthAppList(top, whereStr);
                string jsonStr = JsonConvert.SerializeObject(new
                {
                    status = 1,
                    list = tlist
                });
                context.Response.Write(jsonStr);
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }

        #endregion

        #region 取回用户密码======

        private void UserGetPassword(HttpContext context)
        {
            try
            {
                string code = DTRequest.GetFormString("code");
                string mobile = DTRequest.GetFormString("mobile");
                _userService.UserGetPassword(code, mobile);
                context.Response.Write("{\"status\":1,\"msg\":\"密码重置短信正在发往您的手机！\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }

        #endregion

        #region 重置用户密码======

        private void UserResetPassword(HttpContext context)
        {
            try
            {
                string code = DTRequest.GetFormString("code");
                string npassword = DTRequest.GetFormString("npassword");
                _userService.UserResetPassword(code, npassword);
                context.Response.Write("{\"status\":1,\"msg\":\"密码重置成功！\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }

        #endregion

        #region 登录验证======

        private void UserLogin(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetFormString("username");
                string password = DTRequest.GetFormString("password");
                string token;
                bool r = _userService.UserLogin(username, password, out token);
                if (r)
                {
                    context.Response.Write("{\"status\":1," +
                                           "\"msg\":\"您已登录成功！\"," +
                                           "\"token\":\"" + token + "\"," +
                                           "\"result\":true}");
                }
                else
                {
                    context.Response.Write("{\"status\":0," +
                                           "\"msg\":\"登录失败，用户名或密码错误！\"," +
                                           "\"result\":false}");
                }
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }

        #endregion

        #region 判断用户是否已经登录======

        private void CheckUserLogin(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetFormString("username");
                bool r = _userService.CheckUserLogin(username);
                if (r)
                {
                    context.Response.Write("{\"status\":1," +
                                           "\"result\":true}");
                }
                else
                {
                    context.Response.Write("{\"status\":0," +
                                           "\"result\":false}");
                }
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }

        #endregion

        #region 注册用户信息======

        private void UserRegister(HttpContext context)
        {
            try
            {
                string username = DTRequest.GetFormString("username");
                string email = DTRequest.GetFormString("email");
                string password = DTRequest.GetFormString("password");
                string mobile = DTRequest.GetFormString("mobile");
                _userService.UserRegister(username, password, email, mobile);
                context.Response.Write("{\"status\":1,\"msg\":\"恭喜您已注册成为会员!\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }

        #endregion

        #region 发送手机短信验证码======

        private void UserVerifySmscode(HttpContext context)
        {
            try
            {
                string mobile = DTRequest.GetFormString("mobile");
                _userService.UserVerifySmscode(mobile);
                context.Response.Write("{\"status\":1,\"msg\":\"验证码已发送到您手机\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }

        #endregion

        #region 文章列表======

        private void GetArticleList(HttpContext context)
        {
            try
            {
                int type = DTRequest.GetFormInt("type");
                switch (type)
                {
                    case 1:
                        {
                            string channelName = DTRequest.GetFormString("channelName");
                            string whereStr = DTRequest.GetFormString("whereStr");
                            int top = DTRequest.GetFormInt("top", 0);
                            var result = _articleService.GetArticleList(channelName, top, whereStr);
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
                            string channelName = DTRequest.GetFormString("channelName");
                            string whereStr = DTRequest.GetFormString("whereStr");
                            int top = DTRequest.GetFormInt("top", 0);
                            int categoryId = DTRequest.GetFormInt("categoryId", 0);
                            var result = _articleService.GetArticleList(channelName, categoryId, top, whereStr);
                            string jsonStr = JsonConvert.SerializeObject(new
                            {
                                status = 1,
                                list = result
                            });
                            context.Response.Write(jsonStr);
                        }
                        break;
                    case 3:
                        {
                            string channelName = DTRequest.GetFormString("channelName");
                            string whereStr = DTRequest.GetFormString("whereStr");
                            int top = DTRequest.GetFormInt("top", 0);
                            int categoryId = DTRequest.GetFormInt("categoryId", 0);
                            string orderby = DTRequest.GetFormString("orderby");
                            var result = _articleService.GetArticleList(channelName, categoryId, top, whereStr, orderby);
                            string jsonStr = JsonConvert.SerializeObject(new
                            {
                                status = 1,
                                list = result
                            });
                            context.Response.Write(jsonStr);
                        }
                        break;
                    case 4:
                        {
                            string channelName = DTRequest.GetFormString("channelName");
                            string whereStr = DTRequest.GetFormString("whereStr");
                            int pageIndex = DTRequest.GetFormInt("pageIndex", 0);
                            int pageSize = DTRequest.GetFormInt("pageSize", 0);
                            int categoryId = DTRequest.GetFormInt("categoryId", 0);
                            string orderby = DTRequest.GetFormString("orderby");
                            int count;
                            var result = _articleService.GetArticleList(channelName, categoryId, pageSize, pageIndex,
                                whereStr, orderby, out count);
                            string jsonStr = JsonConvert.SerializeObject(new
                            {
                                status = 1,
                                totalCount = count,
                                list = result
                            });
                            context.Response.Write(jsonStr);
                        }
                        break;
                    case 5:
                        {
                            string channelName = DTRequest.GetFormString("channelName");
                            string whereStr = DTRequest.GetFormString("whereStr");
                            int pageIndex = DTRequest.GetFormInt("pageIndex", 0);
                            int pageSize = DTRequest.GetFormInt("pageSize", 0);
                            int categoryId = DTRequest.GetFormInt("categoryId", 0);
                            string orderby = DTRequest.GetFormString("orderby");
                            string jsonSpeIds = DTRequest.GetFormString("speIds");
                            var speIds = JsonHelper.JSONToObject<Dictionary<string, string>>(jsonSpeIds);
                            int count;
                            var result = _articleService.GetArticleList(channelName, categoryId, speIds, pageSize, pageIndex,
                                whereStr, orderby, out count);
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

        private void GetArticleCategories(HttpContext context)
        {
            try
            {
                int parentId = DTRequest.GetFormInt("parentId", 0);
                int type = DTRequest.GetFormInt("type");
                var result = _articleService.GetArticleCategories(parentId, type);
                ;
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

        #region 文章Tags标签列表======

        private void GetArticleTags(HttpContext context)
        {
            try
            {
                int top = DTRequest.GetFormInt("top");
                string whereStr = DTRequest.GetFormString("whereStr");
                var result = _articleService.GetArticleTags(top, whereStr);
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

        #region 根据调用标识取得内容======

        private void GetArticleContent(HttpContext context)
        {
            try
            {
                string callIndex = DTRequest.GetFormString("callIndex");
                var result = _articleService.GetArticleContent(callIndex);
                context.Response.Write("{\"status\":1,\"result\":\"" + Utils.UrlEncode(result) + "\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }

        #endregion

        #region 获取对应的图片路径======

        private void GetArticleImgUrl(HttpContext context)
        {
            try
            {
                int articleId = DTRequest.GetFormInt("articleId");
                var result = _articleService.GetArticleImgUrl(articleId);
                context.Response.Write("{\"status\":1,\"result\":\"" + Utils.UrlEncode(result) + "\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }

        #endregion

        #region 获取文章扩展属性======

        private void GetArticleField(HttpContext context)
        {
            try
            {
                int type = DTRequest.GetFormInt("type");
                switch (type)
                {
                    case 1:
                        {
                            int articleId = DTRequest.GetFormInt("articleId");
                            string fieldName = DTRequest.GetFormString("fieldName");
                            var result = _articleService.GetArticleField(articleId, fieldName);
                            context.Response.Write("{\"status\":1,\"field\":\"" + result + "\"}");
                        }
                        break;
                    case 2:
                        {
                            string callIndex = DTRequest.GetFormString("callIndex");
                            string fieldName = DTRequest.GetFormString("fieldName");
                            var result = _articleService.GetArticleField(callIndex, fieldName);
                            context.Response.Write("{\"status\":1,\"field\":\"" + result + "\"}");
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

        #region 获取文章对象======

        private void GetArticle(HttpContext context)
        {
            try
            {
                int type = DTRequest.GetFormInt("type");
                switch (type)
                {
                    case 1:
                        {
                            int articleId = DTRequest.GetFormInt("articleId");
                            var result = _articleService.GetArticle(articleId);
                            string jsonStr = JsonConvert.SerializeObject(new
                            {
                                status = 1,
                                article = result
                            });
                            context.Response.Write(jsonStr);
                        }
                        break;
                    case 2:
                        {
                            string callIndex = DTRequest.GetFormString("callIndex");
                            var result = _articleService.GetArticle(callIndex);
                            string jsonStr = JsonConvert.SerializeObject(new
                            {
                                status = 1,
                                article = result
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

        #region 获取上一条下一条的文章主键======

        private void GetPrevandNextArticle(HttpContext context)
        {
            try
            {
                int type = DTRequest.GetFormInt("type");
                int articleId = DTRequest.GetFormInt("articleId");
                string defaultvalue = DTRequest.GetFormString("defaultvalue");
                var result = _articleService.GetPrevandNextArticleId(type, articleId);
                context.Response.Write("{\"status\":1,\"result\":\"" + articleId + "\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }

        #endregion

        #region 取得评论列表方法======

        private void CommentList(HttpContext context)
        {
            try
            {
                int articleId = DTRequest.GetFormInt("articleId");
                int pageIndex = DTRequest.GetFormInt("pageIndex");
                int pageSize = DTRequest.GetFormInt("pageSize");
                int count;
                var result = _articleService.CommentList(articleId, pageIndex, pageSize, out count);
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

        #region 统计及输出阅读次数======

        private void ViewArticleClick(HttpContext context)
        {
            try
            {
                int articleId = DTRequest.GetFormInt("articleId");
                var result = _articleService.ViewArticleClick(articleId);
                context.Response.Write("{\"status\":1,\"result\":" + result + "}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }

        #endregion

        #region 输出评论总数======

        private void ViewCommentCount(HttpContext context)
        {
            try
            {
                int articleId = DTRequest.GetFormInt("articleId");
                var result = _articleService.ViewCommentCount(articleId);
                context.Response.Write("{\"status\":1,\"result\":" + result + "}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }

        #endregion

        #region 输出附件下载总数======

        private void ViewAttachCount(HttpContext context)
        {
            try
            {
                int articleId = DTRequest.GetFormInt("articleId");
                var result = _articleService.ViewAttachCount(articleId);
                context.Response.Write("{\"status\":1,\"result\":" + result + "}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }

        #endregion

        #region 获取课程列表======

        private void GetCurriculumList(HttpContext context)
        {
            try
            {
                int type = DTRequest.GetFormInt("type");
                switch (type)
                {
                    case 1:
                        {
                            int top = DTRequest.GetFormInt("top", 0);
                            int categoryId = DTRequest.GetFormInt("categoryId", 0);
                            string whereStr = DTRequest.GetFormString("whereStr");
                            var cList = _curriculumService.GetCurriculumList(top, categoryId, whereStr);
                            var result = from p in cList
                                         select new
                                         {
                                             CurriculumId = p.CurriculumId,
                                             TimeLength = _curriculumService.GetCurriculumTimeLength(p.CurriculumId),
                                             FullName = p.FullName,
                                             TeacherName = p.TeacherName,
                                             Describe = p.Describe,
                                             DeleteMark = p.DeleteMark,
                                             Enable = p.Enable,
                                             Click = p.Click,
                                             Status = p.Status,
                                             IsMsg = p.IsMsg,
                                             IsTop = p.IsTop,
                                             IsRed = p.IsRed,
                                             IsHot = p.IsHot,
                                             IsSlide = p.IsSlide,
                                             IsSys = p.IsSys,
                                             CreateDate = p.CreateDate,
                                             CreateUserName = p.CreateUserName,
                                             ModifyDate = p.ModifyDate,
                                             ModifyUserName = p.ModifyUserName,
                                             ImgUrl = p.ImgUrl,
                                             CurriculumItems = p.CurriculumItems
                                         };
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
                            int top = DTRequest.GetFormInt("top", 0);
                            int categoryId = DTRequest.GetFormInt("categoryId", 0);
                            string whereStr = DTRequest.GetFormString("whereStr");
                            string orderby = DTRequest.GetFormString("orderby");
                            var cList = _curriculumService.GetCurriculumList(top, categoryId, whereStr, orderby);
                            var result = from p in cList
                                         select new
                                         {
                                             CurriculumId = p.CurriculumId,
                                             TimeLength = _curriculumService.GetCurriculumTimeLength(p.CurriculumId),
                                             FullName = p.FullName,
                                             TeacherName = p.TeacherName,
                                             Describe = p.Describe,
                                             DeleteMark = p.DeleteMark,
                                             Enable = p.Enable,
                                             Click = p.Click,
                                             Status = p.Status,
                                             IsMsg = p.IsMsg,
                                             IsTop = p.IsTop,
                                             IsRed = p.IsRed,
                                             IsHot = p.IsHot,
                                             IsSlide = p.IsSlide,
                                             IsSys = p.IsSys,
                                             CreateDate = p.CreateDate,
                                             CreateUserName = p.CreateUserName,
                                             ModifyDate = p.ModifyDate,
                                             ModifyUserName = p.ModifyUserName,
                                             ImgUrl = p.ImgUrl,
                                             CurriculumItems = p.CurriculumItems
                                         };
                            string jsonStr = JsonConvert.SerializeObject(new
                            {
                                status = 1,
                                list = result
                            })
                                ;
                            context.Response.Write(jsonStr);
                        }
                        break;
                    case 3:
                        {
                            int pageIndex = DTRequest.GetFormInt("pageIndex", 1);
                            int pageSize = DTRequest.GetFormInt("pageSize", 10);
                            int categoryId = DTRequest.GetFormInt("categoryId", 0);
                            string whereStr = DTRequest.GetFormString("whereStr");
                            string orderby = DTRequest.GetFormString("orderby");
                            int count;
                            var cList = _curriculumService.GetCurriculumList(categoryId, pageSize, pageIndex,
                                whereStr, orderby, out count);
                            var result = from p in cList
                                         select new
                                         {
                                             CurriculumId = p.CurriculumId,
                                             TimeLength = _curriculumService.GetCurriculumTimeLength(p.CurriculumId),
                                             FullName = p.FullName,
                                             TeacherName = p.TeacherName,
                                             Describe = p.Describe,
                                             DeleteMark = p.DeleteMark,
                                             Enable = p.Enable,
                                             Click = p.Click,
                                             Status = p.Status,
                                             IsMsg = p.IsMsg,
                                             IsTop = p.IsTop,
                                             IsRed = p.IsRed,
                                             IsHot = p.IsHot,
                                             IsSlide = p.IsSlide,
                                             IsSys = p.IsSys,
                                             CreateDate = p.CreateDate,
                                             CreateUserName = p.CreateUserName,
                                             ModifyDate = p.ModifyDate,
                                             ModifyUserName = p.ModifyUserName,
                                             ImgUrl = p.ImgUrl,
                                             CurriculumItems = p.CurriculumItems
                                         };
                            string jsonStr = JsonConvert.SerializeObject(new
                            {
                                status = 1,
                                totalCount = count,
                                list = result
                            })
                                ;
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

        #region 获取关联课程列表======

        private void GetRelationCurriculumList(HttpContext context)
        {
            try
            {
                int type = DTRequest.GetFormInt("type");
                switch (type)
                {
                    case 1:
                        {
                            int top = DTRequest.GetFormInt("top", 0);
                            int curriculumId = DTRequest.GetFormInt("curriculumId", 0);
                            string whereStr = DTRequest.GetFormString("whereStr");
                            var result = _curriculumService.GetRelationCurriculumList(top, curriculumId, whereStr);
                            string jsonStr = JsonConvert.SerializeObject(new
                            {
                                status = 1,
                                list = result
                            })
                                ;
                            context.Response.Write(jsonStr);
                        }
                        break;
                    case 2:
                        {
                            int top = DTRequest.GetFormInt("top", 0);
                            int curriculumId = DTRequest.GetFormInt("curriculumId", 0);
                            string whereStr = DTRequest.GetFormString("whereStr");
                            string orderby = DTRequest.GetFormString("orderby");
                            var result = _curriculumService.GetRelationCurriculumList(top, curriculumId, whereStr, orderby);
                            string jsonStr = JsonConvert.SerializeObject(new
                            {
                                status = 1,
                                list = result
                            })
                                ;
                            context.Response.Write(jsonStr);
                        }
                        break;
                    case 3:
                        {
                            int curriculumId = DTRequest.GetFormInt("curriculumId", 0);
                            int pageSize = DTRequest.GetFormInt("pageSize", 10);
                            int pageIndex = DTRequest.GetFormInt("pageIndex", 1);
                            string whereStr = DTRequest.GetFormString("whereStr");
                            string orderby = DTRequest.GetFormString("orderby");
                            int count;
                            var result = _curriculumService.GetRelationCurriculumList(curriculumId, pageSize,
                                pageIndex, whereStr, orderby, out count);
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

        #region 获取课程类别列表======

        private void GetCurriculumCategories(HttpContext context)
        {
            try
            {
                int parentId = DTRequest.GetFormInt("parentId", 0);
                int type = DTRequest.GetFormInt("type");
                var result = _curriculumService.GetCurriculumCategories(parentId,type);
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

        #region 检查课程是否需要vip身份才能观看======

        private void CheckCurriculumVip(HttpContext context)
        {
            try
            {
                int id = DTRequest.GetFormInt("id", 0);
                var result = _curriculumService.CheckCurriculumVip(id);
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

        #region 获取课程实体======

        private void GetCurriculum(HttpContext context)
        {
            try
            {
                int id = DTRequest.GetFormInt("id", 0);
                var result = _curriculumService.GetCurriculum(id);
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

        #region 获取课程封面======

        private void GetCurriculumImgUrl(HttpContext context)
        {
            try
            {
                int id = DTRequest.GetFormInt("id", 0);
                var result = _curriculumService.GetCurriculumImgUrl(id);
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

        #region 获取课程描述======

        private void GetCurriculumDescribe(HttpContext context)
        {
            try
            {
                int id = DTRequest.GetFormInt("id", 0);
                var result = _curriculumService.GetCurriculumDescribe(id);
                string jsonStr = JsonConvert.SerializeObject(new
                {
                    status = 1,
                    result = Utils.UrlEncode(result)
                });
                context.Response.Write(jsonStr);
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
                string mediaCode = DTRequest.GetFormString("code");
                var result = _curriculumService.GetMediaRealPath(mediaCode);
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

        #region 返回配送方式的标题======

        private void GetExpressTitle(HttpContext context)
        {
            try
            {
                int expressId = DTRequest.GetFormInt("id", 0);
                var result = _expressService.GetExpressTitle(expressId);
                context.Response.Write("{\"status\":1,\"result\":\"" + result + "\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }

        #endregion

        #region 返回配送列表======

        private void GetExpressList(HttpContext context)
        {
            try
            {
                int top = DTRequest.GetFormInt("top", 0);
                string whereStr = DTRequest.GetFormString("whereStr");
                var result = _expressService.GetExpressList(top, whereStr);
                string jsonStr = JsonConvert.SerializeObject(new
                {
                    status = 1,
                    list = result
                })
                    ;
                context.Response.Write(jsonStr);
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }

        #endregion

        #region 获取所有板块列表======

        private void GetBoardList(HttpContext context)
        {
            try
            {
                int parentId = DTRequest.GetFormInt("parentId");
                var result = _forumService.GetBoardList(parentId);
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

        #region 获得板块子版块列表======

        private void GetBoardGetchildlist(HttpContext context)
        {
            try
            {
                int parentId = DTRequest.GetFormInt("parentId");
                var result = _forumService.GetBoardGetchildlist(parentId);
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

        #region 返回父类别ID======

        private void GetCategoryId(HttpContext context)
        {
            try
            {
                int categoryId = DTRequest.GetFormInt("categoryId");
                var result = _forumService.GetCategoryId(categoryId);
                context.Response.Write("{\"status\":1,\"msg\":\"" + result + "\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }

        #endregion

        #region 获得该商品的规格列表======

        private void GetArticleGoodsSpec(HttpContext context)
        {
            try
            {
                int articleId = DTRequest.GetFormInt("articleId");
                string whereStr = DTRequest.GetFormString("whereStr");
                var result = _goodsService.GetArticleGoodsSpec(articleId, whereStr);
                string jsonStr = JsonConvert.SerializeObject(new
                {
                    status = 1,
                    list = result
                })
                    ;
                context.Response.Write(jsonStr);
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }

        #endregion

        #region 根据频道名称及类别ID查询父规格列表(慎用)======

        private void GetArticleSpecParent(HttpContext context)
        {
            try
            {
                string channelName = DTRequest.GetFormString("channelName");
                int categoryId = DTRequest.GetFormInt("categoryId");
                var result = _goodsService.GetArticleSpecParent(channelName, categoryId);
                string jsonStr = JsonConvert.SerializeObject(new
                {
                    status = 1,
                    list = result
                })
                    ;
                context.Response.Write(jsonStr);
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }

        #endregion

        #region 根据父ID查询规格列表======

        private void GetArticleSpecChild(HttpContext context)
        {
            try
            {
                int parentId = DTRequest.GetFormInt("parentId");
                var result = _goodsService.GetArticleSpecChild(parentId);
                string jsonStr = JsonConvert.SerializeObject(new
                {
                    status = 1,
                    list = result
                })
                    ;
                context.Response.Write(jsonStr);
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }

        #endregion

        #region 返回订单商品列表======

        private void GetOrderGoodsList(HttpContext context)
        {
            try
            {
                int orderId = DTRequest.GetFormInt("id");
                var result = _orderService.GetOrderGoodsList(orderId);
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

        #region 返回订单状态======

        private void GetOrderStatus(HttpContext context)
        {
            try
            {
                int id = DTRequest.GetFormInt("id");
                var result = _orderService.GetOrderStatus(id);
                context.Response.Write("{\"status\":1,\"result\":\"" + result + "\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }

        #endregion

        #region 返回订单是否需要在线支付======

        private void GetOrderPaymentStatus(HttpContext context)
        {
            try
            {
                int id = DTRequest.GetFormInt("id");
                var result = _orderService.GetOrderPaymentStatus(id);
                context.Response.Write("{\"status\":1,\"result\":" + result + "}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }

        #endregion

        #region 返回税金费用金额======

        private void GetOrderTaxamount(HttpContext context)
        {
            try
            {
                decimal totalAmount = DTRequest.GetFormDecimal("amount", 0);
                var result = _orderService.GetOrderTaxamount(totalAmount);
                context.Response.Write("{\"status\":1,\"result\":" + result + "}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }

        #endregion

        #region 返回支付费用金额======

        private void GetPaymentPoundageAmount(HttpContext context)
        {
            try
            {
                int paymentId = DTRequest.GetFormInt("paymentId");
                decimal totalAmount = DTRequest.GetFormDecimal("amount", 0);
                var result = _paymentService.GetPaymentPoundageAmount(paymentId, totalAmount);
                context.Response.Write("{\"status\":1,\"result\":" + result + "}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }

        #endregion

        #region 返回支付类型的标题======

        private void GetPaymentTitle(HttpContext context)
        {
            try
            {
                int paymentId = DTRequest.GetFormInt("paymentId");
                var result = _paymentService.GetPaymentTitle(paymentId);
                context.Response.Write("{\"status\":1,\"result\":\"" + result + "\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }

        #endregion

        #region 返回支付列表======

        private void GetPaymentList(HttpContext context)
        {
            try
            {
                int top = DTRequest.GetFormInt("top");
                string whereStr = DTRequest.GetFormString("whereStr");
                var result = _paymentService.GetPaymentList(top, whereStr);
                string jsonStr = JsonConvert.SerializeObject(new
                {
                    status = 1,
                    list = result
                })
                    ;
                context.Response.Write(jsonStr);
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }

        #endregion

        #region 获取验证码======

        private void GetValiCode(HttpContext context)
        {
            try
            {
                string deviceId = DTRequest.GetFormString("deviceId");
                string code = _siteService.GetValiCode(deviceId);
                context.Response.Write("{\"status\":1,\"msg\":\"" + code + "\"}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }

        #endregion

        #region 增加文章点击======

        private void ArticleClickAdd(HttpContext context)
        {
            try
            {
                int articleId = DTRequest.GetFormInt("articleId");
                _articleService.ArticleClickAdd(articleId);
                context.Response.Write("{\"status\":1}");
            }
            catch (AppCommonException ex)
            {
                context.Response.Write("{\"status\":" + ex.Status + ",\"msg\":\"" + ex.Message + "\"}");
            }
        }

        #endregion

        #region 获取帖子列表======
        private void GetPostList(HttpContext context)
        {
            try
            {
                int type = DTRequest.GetQueryInt("type");
                switch (type)
                {
                    case 1:
                        {
                            int boardId = DTRequest.GetFormInt("boardId", 0);
                            int pageSize = DTRequest.GetFormInt("pageSize", 10);
                            int pageIndex = DTRequest.GetFormInt("pageIndex", 1);
                            string whereStr = DTRequest.GetFormString("whereStr");
                            int count;
                            var result = _forumService.GetPostList(boardId, pageSize, pageIndex, whereStr, out count);
                            string jsonStr = JsonConvert.SerializeObject(new
                            {
                                status = 1,
                                totalCount = count,
                                list = result
                            })
                            ; context.Response.Write(jsonStr);
                        }
                        break;
                    case 2:
                        {
                            int boardId = DTRequest.GetFormInt("boardId", 0);
                            string whereStr = DTRequest.GetFormString("whereStr");
                            int top = DTRequest.GetFormInt("top");
                            var result = _forumService.GetPostList(boardId, top, whereStr);
                            string jsonStr = JsonConvert.SerializeObject(new
                            {
                                status = 1,
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
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}