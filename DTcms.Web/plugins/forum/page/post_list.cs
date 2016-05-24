using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Configuration;
using DTcms.Common;
using DTcms.Web.UI;

namespace DTcms.Web.Plugin.Forum
{
    public partial class post_list : Web.UI.BasePage
    {
        protected int page; //当前页码
        protected int board_id;  //类别ID
        protected int totalcount; //OUT数据总数
        protected string pagelist;  //分页页码
        protected int is_moderator = 0;

        protected Model.forum_board model = new Model.forum_board(); //分类的实体
        protected BLL.forum_board bll = new BLL.forum_board(); //分类的实体
        protected DTcms.Model.users umodel = new DTcms.Model.users(); //用户实体
        /// <summary>
        /// 重写虚方法,此方法将在Init事件前执行
        /// </summary>
        protected override void ShowPage()
        {
            this.Init += new EventHandler(forum_Init); //加入IInit事件

            page = DTRequest.GetQueryInt("page", 1);
            model.boardname = "全部帖子";
        }

        void forum_Init(object sender, EventArgs e)
        {
            board_id = DTRequest.GetQueryInt("board_id");

            if (board_id > 0) //如果ID获取到，将使用ID
            {
                if (bll.Exists(board_id))
                    model = bll.GetModel(board_id);

                if (board_id > 0)
                {
                    int bid = int.Parse(new board().get_category_id(board_id));  //根据子板块ID获取父板块ID
                    string auid = new BLL.forum_board().GetModel(bid).allow_usergroupid_list;  //获取父板块访问权限列表
                    auid += ",";
                    string[] mlist = auid.Split(',');
                    int ugid = 0;
                    foreach (string item in mlist) //遍历所有父板块的访问权限
                    {
                        if (item == "" || item == null) //如果父板块访问权限为空当前板块为所有权限
                        {
                            umodel.user_name = "游客";
                            break;
                        }
                        else
                        {
                            if (IsUserLogin()) //判断用户是否登陆
                            {
                                umodel = GetUserInfo();
                                if (item == umodel.group_id.ToString()) //如果父板块访问权限列表等于当前用户的用户组，允许访问
                                {
                                   ugid = 1;
                                   break;
                                }
                                else
                                {
                                    ugid = 2;
                                }
                            }
                            else
                            {
                                HttpContext.Current.Response.Redirect(linkurl("error", "?msg=" + Utils.UrlEncode("本板块禁止游客进入！")));
                                break;
                            }
                        }
                    }
                    if (ugid == 2)
                    {
                        HttpContext.Current.Response.Redirect(linkurl("error", "?msg=" + Utils.UrlEncode("您没有进入本板块的权限！")));
                    }
                }
                else
                {
                    HttpContext.Current.Response.Redirect(linkurl("error", "?msg=" + Utils.UrlEncode("非法进入！")));
                }
            }
        }



        public string get_user_name(int userid)
        {
            DTcms.Model.users model = new DTcms.BLL.users().GetModel(userid);
            if (model == null)
            {
                return "-";
            }
            return model.user_name;
        }


        /// <summary>
        /// 帖子分页列表
        /// </summary>
        /// <param name="page_size">页面大小</param>
        /// <param name="page_index">当前页码</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="totalcount">总记录数</param>
        /// <returns>DateTable</returns>
        public DataTable get_post_list(int board_id,int page_size, int page_index, string strwhere, out int totalcount)
        {
            DataTable dt = new DataTable();
            string _where = "id>0";
            if (!string.IsNullOrEmpty(strwhere))
            {
                _where += " and " + strwhere;
            }
            dt = new BLL.forum_posts().GetList(board_id,page_size, page_index, _where, "is_top desc,reply_time desc,add_time desc", out totalcount).Tables[0];
            return dt;
        }


        /// <summary>
        /// 帖子分页列表
        /// </summary>
        /// <param name="page_size">页面大小</param>
        /// <param name="page_index">当前页码</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="totalcount">总记录数</param>
        /// <returns>DateTable</returns>
        public DataTable get_post_list(int Top, string strwhere, string filedOrder)
        {
            DataTable dt = new DataTable();
            string _where = "id>0";
            if (!string.IsNullOrEmpty(strwhere))
            {
                _where += " and " + strwhere;
            }
            dt = new BLL.forum_posts().GetList(Top, _where, filedOrder).Tables[0];
            return dt;
        }
    }
}
