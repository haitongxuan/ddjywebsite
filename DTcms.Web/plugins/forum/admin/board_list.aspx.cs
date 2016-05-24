using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DTcms.Common;

namespace DTcms.Web.Plugin.Forum.admin
{
    public partial class board_list : DTcms.Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;

        protected string keywords = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.keywords = DTRequest.GetQueryString("keywords");

            this.pageSize = GetPageSize(10); //每页数量
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("plugin_forum_board", DTEnums.ActionEnum.View.ToString()); //检查权限
                RptBind();
            }
        }

        #region 获取访问权限列表=========================
        public string GetAllowUserGroupName(string idlist)
        {
            string strAllowUserGroupName = "";
            if (string.IsNullOrEmpty(idlist.Trim()))
            {
                strAllowUserGroupName = "所有用户";
            }
            else
            {
                string[] usergroupidlist = idlist.Split(',');
                foreach (string gid in usergroupidlist)
                {
                    if (!string.IsNullOrEmpty(gid.Trim()))
                    {
                        strAllowUserGroupName += new DTcms.BLL.user_groups().GetTitle(int.Parse(gid)) + ",";
                    }
                }
                strAllowUserGroupName = strAllowUserGroupName.TrimEnd(',');
            }
            return strAllowUserGroupName;
        }
        #endregion

        #region 数据绑定=================================
        private void RptBind()
        {
            BLL.forum_board bll = new BLL.forum_board();
            DataTable dt = bll.GetAllList(0);
            this.rptList.DataSource = dt;
            this.rptList.DataBind();
        }
        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and (boardname like '%" + _keywords + "%' or content like '%" + _keywords + "%')");
            }
            return strTemp.ToString();
        }
        #endregion

        #region 返回每页数量=============================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Utils.GetCookie("forum_page_size", "DTcmsPage"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion

        //美化列表
        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Literal LitFirst = (Literal)e.Item.FindControl("LitFirst");
                HiddenField hidLayer = (HiddenField)e.Item.FindControl("hidLayer");
                string LitStyle = "<span style=\"display:inline-block;width:{0}px;\"></span>{1}{2}";
                string LitImg1 = "<span class=\"folder-open\"></span>";
                string LitImg2 = "<span class=\"folder-line\"></span>";

                int classLayer = Convert.ToInt32(hidLayer.Value);
                if (classLayer == 1)
                {
                    LitFirst.Text = LitImg1;
                }
                else
                {
                    LitFirst.Text = string.Format(LitStyle, (classLayer - 2) * 24, LitImg2, LitImg1);
                }
            }
        }

        //保存排序
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("plugin_forum", DTEnums.ActionEnum.Delete.ToString()); //检查权限
            BLL.forum_board bll = new BLL.forum_board();
            Repeater rptList = new Repeater();
            rptList = this.rptList;
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                int sortId;
                if (!int.TryParse(((TextBox)rptList.Items[i].FindControl("txtSortId")).Text.Trim(), out sortId))
                {
                    sortId = 99;
                }
                bll.UpdateField(id, "sort_id=" + sortId.ToString());
            }
            AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "保存论坛版块排序"); //记录日志
            JscriptMsg("保存排序成功！", Utils.CombUrlTxt("board_list.aspx", "keywords={0}",this.keywords));
        }

        //批量删除
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("plugin_forum", DTEnums.ActionEnum.Delete.ToString()); //检查权限
            int sucCount = 0;
            int errorCount = 0;
            BLL.forum_board bll = new BLL.forum_board();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    if (bll.Delete(id))
                    {
                        sucCount += 1;
                    }
                    else
                    {
                        errorCount += 1;
                    }
                }
            }
            AddAdminLog(DTEnums.ActionEnum.Delete.ToString(), "删除留言成功" + sucCount + "条，失败" + errorCount + "条"); //记录日志
            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", Utils.CombUrlTxt("board_list.aspx", "keywords={0}", this.keywords));
        }

    }
}