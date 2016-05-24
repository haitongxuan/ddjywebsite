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
    public partial class post_edit : DTcms.Web.UI.ManagePage
    {
        protected string action = DTEnums.ActionEnum.Add.ToString(); //操作类型
        private int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = DTRequest.GetQueryString("action");
            this.id = DTRequest.GetQueryInt("id");

            if (!string.IsNullOrEmpty(_action) && _action == DTEnums.ActionEnum.Edit.ToString())
            {
                this.action = DTEnums.ActionEnum.Edit.ToString();//修改类型
                
                if (this.id == 0)
                {
                    JscriptMsg("传输参数不正确！", "back");
                    return;
                }
                if (!new BLL.forum_posts().Exists(this.id))
                {
                    JscriptMsg("帖子不存在或已被删除！", "back");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("plugin_forum_post", DTEnums.ActionEnum.View.ToString()); //检查权限
                TreeBind(); //绑定类别
                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
            }
        }

        #region 绑定类别=================================
        private void TreeBind()
        {
            BLL.forum_board bll = new BLL.forum_board();
            DataTable dt = bll.GetAllList(0);

            this.ddlCategoryId.Items.Clear();
            this.ddlCategoryId.Items.Add(new ListItem("所有类别", ""));
            foreach (DataRow dr in dt.Rows)
            {
                string Id = dr["id"].ToString();
                int ClassLayer = int.Parse(dr["class_layer"].ToString());
                string Title = dr["boardname"].ToString().Trim();

                if (ClassLayer == 1)
                {
                    this.ddlCategoryId.Items.Add(new ListItem(Title, Id));
                }
                else
                {
                    Title = "├ " + Title;
                    Title = Utils.StringOfChar(ClassLayer - 1, "　") + Title;
                    this.ddlCategoryId.Items.Add(new ListItem(Title, Id));
                }
            }
        }
        #endregion

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.forum_posts bll = new BLL.forum_posts();
            Model.forum_posts model = bll.GetModel(id);
            DTcms.BLL.users ubll = new DTcms.BLL.users();
            DTcms.Model.users umodel = ubll.GetModel(model.user_id);

            ddlCategoryId.SelectedValue = model.board_id.ToString();

            txtUserName.Text = umodel.user_name;
            txtUserName.ReadOnly = true;
            if (model.is_lock == 1)
            {
                cblItem.Items[0].Selected = true;
            }
            if (model.is_top == 1)
            {
                cblItem.Items[1].Selected = true;
            }
            if (model.is_red == 1)
            {
                cblItem.Items[2].Selected = true;
            }
            if (model.is_hot == 1)
            {
                cblItem.Items[3].Selected = true;
            }

            txtPostName.Text = model.title;
            txtPostContent.Value = model.content;
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = false;
            BLL.forum_posts bll = new BLL.forum_posts();
            Model.forum_posts model = new Model.forum_posts();
            DTcms.BLL.users ubll = new DTcms.BLL.users();
            DTcms.Model.users umodel = new DTcms.Model.users();
            //判断用户名是否存在
            if (!ubll.Exists(txtUserName.Text.Trim()))
            {
                JscriptMsg("用户名不存在，请重新填写！", "");
                return result;
            }

            umodel = ubll.GetModel(txtUserName.Text.Trim());

            string _userip = System.Web.HttpContext.Current.Request.UserHostAddress;
            model.class_layer = 1;
            model.title = txtPostName.Text.Trim();
            model.content = txtPostContent.Value;
            model.user_id = umodel.id;
            model.user_ip = _userip;
            model.board_id = Utils.StrToInt(ddlCategoryId.SelectedValue, 0);
            model.parent_post_id = 0;
            model.post_type = 1;//主题帖
            model.reply_time = DateTime.Now;

            model.click = 0;
            model.is_lock = 0;
            model.is_top = 0;
            model.is_red = 0;
            model.is_hot = 0;
            if (cblItem.Items[0].Selected == true)
            {
                model.is_lock = 1;
            }
            if (cblItem.Items[1].Selected == true)
            {
                model.is_top = 1;
            }
            if (cblItem.Items[2].Selected == true)
            {
                model.is_red = 1;
            }
            if (cblItem.Items[3].Selected == true)
            {
                model.is_hot = 1;
            }
            model.add_time = DateTime.Now;

            if (bll.Add(model) > 0)
            {
                AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "发布了帖子：" + model.title); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = false;
            BLL.forum_posts bll = new BLL.forum_posts();
            Model.forum_posts model = bll.GetModel(_id);

            model.board_id = Utils.StrToInt(ddlCategoryId.SelectedValue, 0);
            model.is_lock = 0;
            model.is_top = 0;
            model.is_red = 0;
            model.is_hot = 0;
            if (cblItem.Items[0].Selected == true)
            {
                model.is_lock = 1;
            }
            if (cblItem.Items[1].Selected == true)
            {
                model.is_top = 1;
            }
            if (cblItem.Items[2].Selected == true)
            {
                model.is_red = 1;
            }
            if (cblItem.Items[3].Selected == true)
            {
                model.is_hot = 1;
            }
            model.title = txtPostName.Text;
            model.content = txtPostContent.Value;

            if (bll.Update(model))
            {
                AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改帖子信息:" + model.title); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("plugin_forum_post", DTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误！", "");
                    return;
                }
                JscriptMsg("修改帖子成功！", "post_list.aspx");
            }
            else //添加
            {
                ChkAdminLevel("plugin_forum_post", DTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误！", string.Empty);
                    return;
                }
                JscriptMsg("添加帖子成功！", "post_list.aspx");
            }
        }

    }
}