using System;
using System.Collections.Generic;
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
        Model.userconfig userConfig = new BLL.userconfig().loadConfig();
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
        [System.Web.Http.Authorize]
        [System.Web.Http.Route("comment_add")]
        public async Task<StatusModel> CommandAdd(CommandAddModel amodel)
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

        [System.Web.Http.Authorize]
        [System.Web.Http.Route("getuserinfo")]
        public async Task<ApplicationUser> GetUserInfo()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId<int>());
            return user;
        }
    }
}
