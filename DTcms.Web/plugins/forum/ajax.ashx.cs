using System;
using System.Text;
using System.Data;
using System.Web;
using DTcms.Common;
using System.Web.SessionState;

namespace DTcms.Web.Plugin.Forum
{
    public class ajax : IHttpHandler, IRequiresSessionState
    {
        DTcms.Model.siteconfig siteConfig = new DTcms.BLL.siteconfig().loadConfig(); //系统配置信息
        DTcms.Model.userconfig userConfig = new DTcms.BLL.userconfig().loadConfig(); //会员配置信息

        public void ProcessRequest(HttpContext context)
        {
            //检查管理员是否登录
            if (!new DTcms.Web.UI.UserPage().IsUserLogin())
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"尚未登录或已超时，请登录后操作！\"}");
                return;
            }
            //取得处事类型
            string action = DTRequest.GetQueryString("action");
            switch (action)
            {
                case "add": //发表主题贴
                    add(context);
                    break;
                case "reply": //回复主题贴
                    reply(context);
                    break;
                case "edit": //编辑帖子
                    edit(context);
                    break;
                case "move": //移动帖子
                    move(context);
                    break;
                case "del": //删除帖子
                    del(context);
                    break;
                case "set_lock": //设置隐藏
                    set_lock(context);
                    break;
                case "set_top": //设置置顶
                    set_top(context);
                    break;
                case "set_red": //设置精华
                    set_red(context);
                    break;
                case "set_hot": //回复主题贴
                    set_hot(context);
                    break;
            }

        }

        #region 发布帖子================================
        private void add(HttpContext context)
        {
            //检查用户是否登录
            DTcms.Model.users umodel = new DTcms.Web.UI.BasePage().GetUserInfo();
            if (umodel == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"请登录后再提交！\"}");
                return;
            }
            BLL.forum_posts bll = new BLL.forum_posts();
            Model.forum_posts model = new Model.forum_posts();

            string _title = DTRequest.GetFormString("txtTitle");
            string _content = DTRequest.GetFormString("txtContent");
            int board_id = DTRequest.GetFormInt("txtBoardID");
            int post_id = DTRequest.GetFormInt("txtPostID");

            int _userid = umodel.id;
            string _userip = System.Web.HttpContext.Current.Request.UserHostAddress;
            model.class_layer = 1;
            model.title = Utils.DropHTML(_title);
            model.content = _content;
            model.user_id = _userid;
            model.user_ip = _userip;
            model.board_id = board_id;
            model.parent_post_id = post_id;
            model.post_type = 1;//主题帖
            model.reply_time = DateTime.Now;
            if (bll.Add(model) > 0)
            {
                context.Response.Write("{\"status\": 1, \"msg\": \"恭喜您，发帖成功！\"}");
                return;
            }
            context.Response.Write("{\"status\": 0, \"msg\": \"对不起，保存过程中发生错误！\"}");
            return;

        }
        #endregion

        #region 回复帖子================================
        private void reply(HttpContext context)
        {
            //检查用户是否登录
            DTcms.Model.users umodel = new DTcms.Web.UI.BasePage().GetUserInfo();
            if (umodel == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"请登录后再提交！\"}");
                return;
            }
            BLL.forum_posts bll = new BLL.forum_posts();
            Model.forum_posts model = new Model.forum_posts();

            string _title = DTRequest.GetFormString("txtTitle");
            string _content = DTRequest.GetFormString("txtContent");
            int board_id = DTRequest.GetFormInt("txtBoardID");
            int post_id = DTRequest.GetFormInt("txtPostID");


            int _userid = umodel.id;
            string _userip = System.Web.HttpContext.Current.Request.UserHostAddress;
            model.class_layer = 2;
            model.title = Utils.DropHTML(_title);
            model.content = _content;
            model.user_id = _userid;
            model.user_ip = _userip;
            model.board_id = board_id;
            model.parent_post_id = post_id;
            model.post_type = 2;//回帖


            Model.forum_posts pmodel = bll.GetModel(post_id);
            pmodel.reply_time = DateTime.Now;
            pmodel.reply_user_id = _userid;
            pmodel.reply_count += 1;

            if (bll.Add(model) > 0&& bll.Update(pmodel))
            {
                context.Response.Write("{\"status\": 1, \"msg\": \"恭喜您，回帖成功！\"}");
                return;
            }
            context.Response.Write("{\"status\": 0, \"msg\": \"对不起，保存过程中发生错误！\"}");
            return;

        }
        #endregion

        #region 编辑帖子================================
        private void edit(HttpContext context)
        {
            //检查用户是否登录
            DTcms.Model.users umodel = new DTcms.Web.UI.BasePage().GetUserInfo();
            if (umodel == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"请登录后再提交！\"}");
                return;
            }
            BLL.forum_posts bll = new BLL.forum_posts();
            string _title = DTRequest.GetFormString("txtTitle");
            string _content = DTRequest.GetFormString("txtContent");
            int post_id = DTRequest.GetFormInt("txtPostID");
            if (post_id == 0)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"参数不正确！\"}");
                return;
            }
            Model.forum_posts model = bll.GetModel(post_id);

            //判断权限
            if (!IsModerator(model.board_id, umodel.id) && model.user_id != umodel.id)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，你无权编辑此帖！\"}");
                return;
            }

            model.title = Utils.DropHTML(_title);
            model.content = _content;

            if (bll.Update(model))
            {
                context.Response.Write("{\"status\": 1, \"msg\": \"编辑帖子成功！\"}");
                return;
            }
            context.Response.Write("{\"status\": 0, \"msg\": \"对不起，保存过程中发生错误！\"}");
            return;

        }
        #endregion

        #region 移动帖子================================
        private void move(HttpContext context)
        {
            //检查用户是否登录
            DTcms.Model.users umodel = new DTcms.Web.UI.BasePage().GetUserInfo();
            if (umodel == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"请登录后再操作！\"}");
                return;
            }
            BLL.forum_posts bll = new BLL.forum_posts();
            Model.forum_posts model = new Model.forum_posts();

            BLL.forum_board bbll = new BLL.forum_board();
            Model.forum_board bmodel = new Model.forum_board();

            int post_id = DTRequest.GetFormInt("postid");
            int to_boardid = DTRequest.GetFormInt("toboardid");
            string opremark = DTRequest.GetString("opremark");

            if (post_id == 0)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"参数不正确！\"}");
                return;
            }
            
            model = bll.GetModel(post_id);
            if (model.parent_post_id != 0)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"非主题贴不可移动！\"}");
                return;
            }

            int postcount = 0;
            int replycount = 0;
            int oldboardid = model.board_id;


            //检查是否是版主
            if (!IsModerator(model.board_id, umodel.id))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"当前用户无权执行此操作！\"}");
                return;
            }

            DataTable dt = bll.GetList(0, "id=" + post_id + " or parent_post_id=" + post_id, "id desc").Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                if (int.Parse(dr["parent_post_id"].ToString()) == 0)
                {
                    postcount += 1;
                    replycount += 1;
                }
                else
                {
                    replycount += 1;
                }
                bll.UpdateField(int.Parse(dr["id"].ToString()), "board_id=" + to_boardid);
            }

            bmodel = bbll.GetModel(oldboardid);
            bmodel.subject_count -= postcount;
            bmodel.post_count -= replycount;
            bbll.Update(bmodel);

            bmodel = bbll.GetModel(to_boardid);
            bmodel.subject_count += postcount;
            bmodel.post_count += replycount;
            bbll.Update(bmodel);

            new DTcms.Web.UI.ManagePage().AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "移动帖子:" + opremark); //记录日志
            
            context.Response.Write("{\"status\": 1, \"msg\": \"恭喜你，移动主题成功！\"}");
            return;

        }
        #endregion

        #region 删除帖子================================
        private void del(HttpContext context)
        {
            //检查用户是否登录
            DTcms.Model.users umodel = new DTcms.Web.UI.BasePage().GetUserInfo();
            if (umodel == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"请登录后再提交！\"}");
                return;
            }

            BLL.forum_posts bll = new BLL.forum_posts();
            Model.forum_posts model = new Model.forum_posts();
            int post_id = DTRequest.GetFormInt("postid");
            string optip = DTRequest.GetFormString("optip");
            string opremark = DTRequest.GetFormString("opremark");

            model = bll.GetModel(post_id);

            //检查是否是版主
            if (!IsModerator(model.board_id, umodel.id))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"当前用户无权执行此操作！\"}");
                return;
            }

            if (bll.Delete(post_id))
            {
                //发送短信息
                string postusername = new DTcms.BLL.users().GetModel(model.user_id).user_name;
                new DTcms.BLL.user_message().Add(1, string.Empty, postusername, "您发布的帖子被管理员进行操作", "您的帖子被管理员进行 " + optip + " 操作，原因：" + opremark);
                new DTcms.Web.UI.ManagePage().AddAdminLog(DTEnums.ActionEnum.Delete.ToString(), "删除帖子:" + opremark); //记录日志

                context.Response.Write("{\"status\": 1, \"msg\": \"恭喜您，删除帖子成功！\"}");
                return;
            }
            context.Response.Write("{\"status\": 0, \"msg\": \"对不起，保存过程中发生错误！\"}");
            return;
        }
        #endregion

        #region 设置锁定================================
        private void set_lock(HttpContext context)
        {
            //检查用户是否登录
            DTcms.Model.users umodel = new DTcms.Web.UI.BasePage().GetUserInfo();
            if (umodel == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"请登录后再提交！\"}");
                return;
            }

            StringBuilder strTxt = new StringBuilder();
            BLL.forum_posts bll = new BLL.forum_posts();
            Model.forum_posts model = new Model.forum_posts();

            int post_id = DTRequest.GetFormInt("postid");
            string optip = DTRequest.GetFormString("optip");
            string opremark = DTRequest.GetFormString("opremark");

            model = bll.GetModel(post_id);


            //检查是否是版主
            if (!IsModerator(model.board_id, umodel.id))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，你无权进行此操作！\"}");
                return;
            }

            string strSet = "is_lock=0";
            if (model.is_lock == 0)
            {
                strSet = "is_lock=1";
            }

            bll.UpdateField(post_id, strSet);
            
            //发送短信息
            string postusername = new DTcms.BLL.users().GetModel(model.user_id).user_name;
            new DTcms.BLL.user_message().Add(1, string.Empty, postusername, "您发布的帖子被管理员进行操作", "您的帖子被管理员进行 " + optip + " 操作，原因：" + opremark);
            new DTcms.Web.UI.ManagePage().AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "锁定帖子:" + opremark); //记录日志

            context.Response.Write("{\"status\": 1, \"msg\": \"恭喜您，操作成功！\"}");
            return;
        }
        #endregion

        #region 设置置顶================================
        private void set_top(HttpContext context)
        {
            //检查用户是否登录
            DTcms.Model.users umodel = new DTcms.Web.UI.BasePage().GetUserInfo();
            if (umodel == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"请登录后再提交！\"}");
                return;
            }

            StringBuilder strTxt = new StringBuilder();
            BLL.forum_posts bll = new BLL.forum_posts();
            Model.forum_posts model = new Model.forum_posts();

            int post_id = DTRequest.GetFormInt("postid");
            string optip = DTRequest.GetFormString("optip");
            string opremark = DTRequest.GetFormString("opremark");
            model = bll.GetModel(post_id);


            //检查是否是版主
            if (!IsModerator(model.board_id, umodel.id))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，你无权编辑此帖！\"}");
                return;
            }

            string strSet = "is_top=0";
            if (model.is_top == 0)
            {
                strSet = "is_top=1";
            }

            bll.UpdateField(post_id, strSet);
            //发送短信息
            string postusername = new DTcms.BLL.users().GetModel(model.user_id).user_name;
            new DTcms.BLL.user_message().Add(1, string.Empty, postusername, "您发布的帖子被管理员进行操作", "您的帖子被管理员进行 " + optip + " 操作，原因：" + opremark);
            new DTcms.Web.UI.ManagePage().AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "置顶帖子:" + opremark); //记录日志

            context.Response.Write("{\"status\": 1, \"msg\": \"恭喜您，操作成功！\"}");
            return;

        }
        #endregion

        #region 设置精华================================
        private void set_red(HttpContext context)
        {
            //检查用户是否登录
            DTcms.Model.users umodel = new DTcms.Web.UI.BasePage().GetUserInfo();
            if (umodel == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"请登录后再提交！\"}");
                return;
            }

            StringBuilder strTxt = new StringBuilder();
            BLL.forum_posts bll = new BLL.forum_posts();
            Model.forum_posts model = new Model.forum_posts();

            int post_id = DTRequest.GetFormInt("postid");
            string optip = DTRequest.GetFormString("optip");
            string opremark = DTRequest.GetFormString("opremark");
            model = bll.GetModel(post_id);


            //检查是否是版主
            if (!IsModerator(model.board_id, umodel.id))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，你无权编辑此帖！\"}");
                return;
            }

            string strSet = "is_red=0";
            if (model.is_red == 0)
            {
                strSet = "is_red=1";
            }

            bll.UpdateField(post_id, strSet);
            //发送短信息
            string postusername = new DTcms.BLL.users().GetModel(model.user_id).user_name;
            new DTcms.BLL.user_message().Add(1, string.Empty, postusername, "您发布的帖子被管理员进行操作", "您的帖子被管理员进行 " + optip + " 操作，原因：" + opremark);
            new DTcms.Web.UI.ManagePage().AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "置顶帖子:" + opremark); //记录日志
            context.Response.Write("{\"status\": 1, \"msg\": \"恭喜您，操作成功！\"}");
            return;
        }
        #endregion

        #region 设置热门================================
        private void set_hot(HttpContext context)
        {
            //检查用户是否登录
            DTcms.Model.users umodel = new DTcms.Web.UI.BasePage().GetUserInfo();
            if (umodel == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"请登录后再提交！\"}");
                return;
            }

            StringBuilder strTxt = new StringBuilder();
            BLL.forum_posts bll = new BLL.forum_posts();
            Model.forum_posts model = new Model.forum_posts();

            int post_id = DTRequest.GetFormInt("postid");
            string optip = DTRequest.GetFormString("optip");
            string opremark = DTRequest.GetFormString("opremark");
            model = bll.GetModel(post_id);


            //检查是否是版主
            if (!IsModerator(model.board_id, umodel.id))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，你无权编辑此帖！\"}");
                return;
            }

            string strSet = "is_hot=0";
            if (model.is_hot == 0)
            {
                strSet = "is_hot=1";
            }

            bll.UpdateField(post_id, strSet);
            //发送短信息
            string postusername = new DTcms.BLL.users().GetModel(model.user_id).user_name;
            new DTcms.BLL.user_message().Add(1, string.Empty, postusername, "您发布的帖子被管理员进行操作", "您的帖子被管理员进行 " + optip + " 操作，原因：" + opremark);
            new DTcms.Web.UI.ManagePage().AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "热门帖子:" + opremark); //记录日志
            context.Response.Write("{\"status\": 1, \"msg\": \"恭喜您，操作成功！\"}");
            return;
        }
        #endregion

        //判断是否是版主
        public bool IsModerator(int boardid, int userid)
        { 
            bool is_moderator = false;
            Model.forum_board bmodel = new Model.forum_board();
            DTcms.Model.users umodel = new DTcms.BLL.users().GetModel(userid);

            int bid = int.Parse(new board().get_category_id(boardid));

            string auid = new BLL.forum_board().GetModel(bid).moderator_list;
            auid += ",";
            string[] mlist = auid.Split(',');

            foreach (string item in mlist)
            {
                if (item != "" && item == umodel.user_name)
                {
                    is_moderator = true;
                }
            }
            return is_moderator;
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
