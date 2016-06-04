using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using DTcms.Common;
using DTcms.WebApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace DTcms.WebApi.Controllers
{
    [System.Web.Http.RoutePrefix("api/subimt")]
    public class SubmitController : ApiController
    {
        Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig();
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        #region 添加文章评论
        /// <summary>
        /// 添加文章评论
        /// </summary>
        /// <param name="amodel"></param>
        /// <returns></returns>
        [System.Web.Http.Authorize]
        [System.Web.Http.Route("comment_add")]
        public async Task<StatusModel> CommentAdd(CommandAddModel amodel)
        {
            StringBuilder strTxt = new StringBuilder();
            BLL.article_comment bll = new BLL.article_comment();
            Model.article_comment model = new Model.article_comment();

            if (amodel.articleId == 0)
            {
                return new StatusModel { status = 0, msg = "对不起，参数传输有误！" };
            }
            if (string.IsNullOrEmpty(amodel.content))
            {
                return new StatusModel() { status = 0, msg = "对不起，请输入评论的内容！" };
            }
            //检查该文章是否存在
            Model.article artModel = new BLL.article().GetModel(amodel.articleId);
            if (artModel == null)
            {
                return new StatusModel() { status = 0, msg = "对不起，主题不存在或已删除！" };
            }
            //检查用户是否登录
            int user_id = 0;
            string user_name = "匿名用户";
            var user = await UserManager.FindByIdAsync(int.Parse(User.Identity.GetUserId()));

            if (user != null)
            {
                user_id = user.id;
                user_name = user.user_name;
            }
            model.channel_id = artModel.channel_id;
            model.article_id = artModel.id;
            model.content = Utils.ToHtml(amodel.content);
            model.user_id = user_id;
            model.user_name = user_name;
            model.user_ip = DTRequest.GetIP();
            model.is_lock = siteConfig.commentstatus; //审核开关
            model.add_time = DateTime.Now;
            model.is_reply = 0;
            if (bll.Add(model) > 0)
            {
                return new StatusModel() { status = 1, msg = "留言提交成功!" };

            }
            return new StatusModel() { status = 0, msg = "对不起，保存过程中发生错误！" };
        }
        #endregion

        #region 取得评论列表方法=============================

        public List<CommentModel> CommentList(CommentListPostModel model)
        {
            int totalcount;
            if (model.articleId == 0 || model.pageSize == 0)
            {
                throw new Exception("获取失败，传输参数有误！");
            }
            BLL.article_comment bll = new BLL.article_comment();
            DataSet ds = bll.GetList(model.pageSize, model.pageIndex, string.Format("is_lock=0 and article_id={0}", model.articleId.ToString()), "add_time asc", out totalcount);
            var list = new List<CommentModel>();
            //如果记录存在
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    var comment = new CommentModel();
                    comment.user_id = int.Parse(dr["user_id"].ToString());
                    comment.user_name = dr["user_name"].ToString();
                    if (Convert.ToInt32(dr["user_id"]) > 0)
                    {
                        Model.users userModel = new BLL.users().GetModel(Convert.ToInt32(dr["user_id"]));
                        if (userModel != null)
                        {
                            comment.avatar = userModel.avatar;
                        }
                    }
                    comment.content = dr["content"].ToString();
                }
            }
            return list;
        }
        #endregion

        [System.Web.Http.Authorize]
        [System.Web.Http.Route("getuserinfo")]
        public async Task<ApplicationUser> GetUserInfo()
        {
            return await GetUser();
        }
        [System.Web.Http.Authorize]
        [System.Web.Http.Route("getusername")]
        public string GetUserName()
        {
            return User.Identity.Name;
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Authorize]
        [System.Web.Http.Route("helloworld")]
        public string HelloWorld()
        {
            return "Hello world!";
        }

        private async Task<ApplicationUser> GetUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                string username = User.Identity.GetUserName();
                string uid = User.Identity.GetUserId();
                try
                {
                    var user = await UserManager.FindByNameAsync(username);
                    return user;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return null;
        }
    }
}
