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
    public partial class post_list : DTcms.Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;

        protected int board_id;
        protected string property = string.Empty;
        protected string keywords = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.board_id = DTRequest.GetQueryInt("board_id");
            this.keywords = DTRequest.GetQueryString("keywords");
            this.property = DTRequest.GetQueryString("property");

            this.pageSize = 10; //每页数量

            if (!Page.IsPostBack)
            {
                ChkAdminLevel("plugin_forum_post", DTEnums.ActionEnum.View.ToString()); //检查权限
                TreeBind(); //绑定类别
                RptBind(this.board_id, "post_type=1" + CombSqlTxt(this.keywords, this.property), "add_time desc,id desc");
            }
        }

        #region 绑定类别=================================
        private void TreeBind()
        {
            BLL.forum_board bll = new BLL.forum_board();
            DataTable dt = bll.GetAllList(0);

            this.ddlCategoryId.Items.Clear();
            this.ddlCategoryId.Items.Add(new ListItem("所有类别", ""));
            this.ddlMoveId.Items.Clear();
            this.ddlMoveId.Items.Add(new ListItem("批量移动...", ""));
            foreach (DataRow dr in dt.Rows)
            {
                string Id = dr["id"].ToString();
                int ClassLayer = int.Parse(dr["class_layer"].ToString());
                string Title = dr["boardname"].ToString().Trim();

                if (ClassLayer == 1)
                {
                    this.ddlCategoryId.Items.Add(new ListItem(Title, Id));
                    this.ddlMoveId.Items.Add(new ListItem(Title, Id));
                }
                else
                {
                    Title = "├ " + Title;
                    Title = Utils.StringOfChar(ClassLayer - 1, "　") + Title;
                    this.ddlCategoryId.Items.Add(new ListItem(Title, Id));
                    this.ddlMoveId.Items.Add(new ListItem(Title, Id));
                }
            }
        }
        #endregion

        #region 数据绑定=================================
        private void RptBind(int _board_id, string _strWhere, string _orderby)
        {
            this.page = DTRequest.GetQueryInt("page", 1);
            if (this.board_id > 0)
            {
                this.ddlCategoryId.SelectedValue = _board_id.ToString();
            }
            this.ddlProperty.SelectedValue = this.property;
            this.txtKeywords.Text = this.keywords;

            BLL.forum_posts bll = new BLL.forum_posts();
            this.rptList1.DataSource = bll.GetList(_board_id, this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            this.rptList1.DataBind();

            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("post_list.aspx", "board_id={0}&keywords={1}&property={2}&page={3}",_board_id.ToString(), this.keywords, this.property, "__id__");
            PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(string _keywords, string _property)
        {
            StringBuilder strTemp = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and title like '%" + _keywords + "%'");
            }
            if (!string.IsNullOrEmpty(_property))
            {
                switch (_property)
                {
                    case "isLock":
                        strTemp.Append(" and is_lock=1");
                        break;
                    case "isTop":
                        strTemp.Append(" and is_top=1");
                        break;
                    case "isRed":
                        strTemp.Append(" and is_red=1");
                        break;
                    case "isHot":
                        strTemp.Append(" and is_hot=1");
                        break;
                }
            }
            return strTemp.ToString();
        }
        #endregion

        //设置操作
        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ChkAdminLevel("plugin_forum_post", DTEnums.ActionEnum.Edit.ToString()); //检查权限
            int id = Convert.ToInt32(((HiddenField)e.Item.FindControl("hidId")).Value);
            BLL.forum_posts bll = new BLL.forum_posts();
            Model.forum_posts model = bll.GetModel(id);
            switch (e.CommandName)
            {
                case "lbtnIsTop":
                    if (model.is_top == 1)
                        bll.UpdateField(id, "is_top=0");
                    else
                        bll.UpdateField(id, "is_top=1");
                    break;
                case "lbtnIsRed":
                    if (model.is_red == 1)
                        bll.UpdateField(id, "is_red=0");
                    else
                        bll.UpdateField(id, "is_red=1");
                    break;
                case "lbtnIsHot":
                    if (model.is_hot == 1)
                        bll.UpdateField(id, "is_hot=0");
                    else
                        bll.UpdateField(id, "is_hot=1");
                    break;
                case "lbtnIsLock":
                    if (model.is_lock == 1)
                        bll.UpdateField(id, "is_lock=0");
                    else
                        bll.UpdateField(id, "is_lock=1");
                    break;
            }
            this.RptBind(this.board_id, "post_type=1" + CombSqlTxt(this.keywords, this.property), "add_time desc,id desc");
        }

        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("post_list.aspx", "board_id={0}&keywords={1}&property={2}",
                this.board_id.ToString(), txtKeywords.Text, this.property));
        }

        //筛选类别
        protected void ddlCategoryId_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("post_list.aspx", "board_id={0}&keywords={1}&property={2}",
                ddlCategoryId.SelectedValue, this.keywords, this.property));
        }

        //筛选属性
        protected void ddlProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("post_list.aspx", "board_id={0}&keywords={1}&property={2}",
               this.board_id.ToString(), this.keywords, ddlProperty.SelectedValue));
        }

        //设置分页数量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Utils.WriteCookie("article_page_size", "DTcmsPage", _pagesize.ToString(), 43200);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("post_list.aspx", "board_id={0}&keywords={1}&property={2}",
                this.board_id.ToString(), this.keywords, this.property));
        }

        //批量移动
        protected void ddlMoveId_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChkAdminLevel("plugin_forum_post", DTEnums.ActionEnum.Edit.ToString()); //检查权限
            int sucCount = 0; //成功数量
            int postcount = 0;
            int replycount = 0;

            if (ddlMoveId.SelectedValue == "")
            {
                ddlMoveId.SelectedIndex = 0;
                JscriptMsg("请选择要移动的类别！", string.Empty);
                return;
            }
            BLL.forum_posts bll = new BLL.forum_posts();
            BLL.forum_board bbll = new BLL.forum_board();
            Model.forum_board bmodel = new Model.forum_board();

            Repeater rptList = new Repeater();
            rptList = this.rptList1;

            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                int oldboardid = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidBid")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    DataTable dt = bll.GetList(0, "id=" + id + " or parent_post_id=" + id, "id desc").Tables[0];
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
                        sucCount++;
                        bll.UpdateField(int.Parse(dr["id"].ToString()), "board_id=" + ddlMoveId.SelectedValue);
                    }
                        bmodel = bbll.GetModel(oldboardid);
                        bmodel.subject_count -= postcount;
                        bmodel.post_count -= replycount;
                        bbll.Update(bmodel);

                        bmodel = bbll.GetModel(int.Parse(ddlMoveId.SelectedValue));
                        bmodel.subject_count += postcount;
                        bmodel.post_count += replycount;
                        bbll.Update(bmodel);
                }
            }
            AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "批量移动频道内容分类"); //记录日志
            JscriptMsg("批量移动成功" + sucCount + "条", Utils.CombUrlTxt("post_list.aspx", "board_id={0}&keywords={1}&property={2}",
                this.board_id.ToString(), this.keywords, this.property));
        }

        //批量审核
        protected void btnAudit_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("plugin_forum_post", DTEnums.ActionEnum.Audit.ToString()); //检查权限
            BLL.forum_posts bll = new BLL.forum_posts();
            Repeater rptList = new Repeater();
            rptList = this.rptList1;

            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    bll.UpdateField(id, "is_lock=0");
                }
            }
            AddAdminLog(DTEnums.ActionEnum.Audit.ToString(), "审核频道内容信息"); //记录日志
            JscriptMsg("批量审核成功！", Utils.CombUrlTxt("post_list.aspx", "board_id={0}&keywords={1}&property={2}",
                this.board_id.ToString(), this.keywords, this.property));
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("plugin_forum_post", DTEnums.ActionEnum.Delete.ToString()); //检查权限
            int sucCount = 0; //成功数量
            int errorCount = 0; //失败数量
            BLL.forum_posts bll = new BLL.forum_posts();
            Repeater rptList = new Repeater();
            rptList = this.rptList1;

            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    if (bll.Delete(id))
                    {
                        sucCount++;
                    }
                    else
                    {
                        errorCount++;
                    }
                }
            }
            AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "删除帖子成功" + sucCount + "条，失败" + errorCount + "条"); //记录日志
            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", Utils.CombUrlTxt("post_list.aspx", "board_id={0}&keywords={1}&property={2}",
                this.board_id.ToString(), this.keywords, this.property));
        }

    }
}