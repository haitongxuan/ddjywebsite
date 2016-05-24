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
    public partial class post_show : Web.UI.BasePage
    {
        protected int page; //当前页码
        protected int is_moderator = 0;
        protected int post_id;  //主题ID
        protected int totalcount; //OUT数据总数
        protected string pagelist;  //分页页码

        protected Model.forum_posts model = new Model.forum_posts(); //分类的实体
        protected BLL.forum_posts bll = new BLL.forum_posts(); //分类的实体
        protected Model.forum_board bmodel = new Model.forum_board(); //分类的实体
        protected BLL.forum_board bbll = new BLL.forum_board(); //分类的实体
        protected DTcms.Model.users umodel = new DTcms.Model.users(); //用户实体


        /// <summary>
        /// 重写虚方法,此方法将在Init事件前执行
        /// </summary>
        protected override void ShowPage()
        {
            this.Init += new EventHandler(forum_Init); //加入IInit事件
        }

        void forum_Init(object sender, EventArgs e)
        {
            page = DTRequest.GetQueryInt("page", 1);
            post_id = DTRequest.GetQueryInt("post_id");

            if (post_id > 0) //如果ID获取到，将使用ID
            {
                if (bll.Exists(post_id))
                    model = bll.GetModel(post_id);
                bll.UpdateField(post_id, "click=click+1");

                if (model.board_id > 0)
                {
                    int bid = int.Parse(new board().get_category_id(model.board_id));
                    string auid = new BLL.forum_board().GetModel(bid).allow_usergroupid_list;  //获取父板块访问权限列表
                    auid += ",";
                    string[] alist = auid.Split(',');
                    int ugid = 0;
                    foreach (string item in alist)
                    {
                        if (item == "" || item == null) //如果父板块访问权限为空当前板块为所有权限
                        {
                            if (IsUserLogin()) //判断用户是否登陆
                            {
                                umodel = GetUserInfo();
                                string moderator = new BLL.forum_board().GetModel(bid).moderator_list;
                                moderator += ",";
                                string[] mlist = moderator.Split(',');
                                foreach (string mitem in mlist)
                                {
                                  if (mitem != "" && mitem == umodel.user_name)
                                  {
                                     is_moderator = 1;
                                  }
                                }
                            }
                        }
                        else
                        {
                            if (IsUserLogin()) //判断用户是否登陆
                            {
                                umodel = GetUserInfo();
                                if (item == umodel.group_id.ToString()) //如果父板块访问权限列表等于当前用户的用户组，允许访问
                                {
                                    string moderator = new BLL.forum_board().GetModel(bid).moderator_list;
                                    moderator += ",";
                                    string[] mlist = moderator.Split(',');
                                    foreach (string mitem in mlist)
                                    {
                                        if (mitem != "" && mitem == umodel.user_name)
                                        {
                                            is_moderator = 1;
                                        }
                                    }
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
                                HttpContext.Current.Response.Redirect(linkurl("error", "?msg=" + Utils.UrlEncode("本帖子禁止游客查看！")));
                                break;
                            }
                        }
                    }
                    if (ugid == 2)
                    {
                        HttpContext.Current.Response.Redirect(linkurl("error", "?msg=" + Utils.UrlEncode("您没有查看本帖子的权限！")));
                    }
                }
                else
                {
                    HttpContext.Current.Response.Redirect(linkurl("error", "?msg=" + Utils.UrlEncode("非法进入！")));
                }
            }
        }

        public DTcms.Model.users get_user_model(int userid)
        {
            DTcms.Model.users model = new DTcms.BLL.users().GetModel(userid);
            return model;
        }

        public string get_board_name(int board__id)
        {
            string boardname = "所有主题";
            if (new BLL.forum_board().GetBoardName(board__id) != "")
            {
                boardname = new BLL.forum_board().GetBoardName(board__id);
            }
            return boardname;
        }


        public string GetUserGroupName(int groupid)
        {
            string groupname = "未知分组";
            if (new DTcms.BLL.user_groups().GetTitle(groupid) != "")
            {
                groupname = new DTcms.BLL.user_groups().GetTitle(groupid);
            }
            return groupname;
        }



        /// <summary>
        /// 帖子分页列表
        /// </summary>
        /// <param name="page_size">页面大小</param>
        /// <param name="page_index">当前页码</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="totalcount">总记录数</param>
        /// <returns>DateTable</returns>
        public DataTable get_reply_list(int board_id, int pid, int page_size, int page_index, string strwhere, out int totalcount)
        {
            DataTable dt = new DataTable();
            string _where = " id=" + pid + " or parent_post_id = " + pid;
            if (!string.IsNullOrEmpty(strwhere))
            {
                _where += " and " + strwhere;
            }
            dt = new BLL.forum_posts().GetList(board_id, page_size, page_index, _where, "add_time asc", out totalcount).Tables[0];
            return dt;
        }
    }
}
