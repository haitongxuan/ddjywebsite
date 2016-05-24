using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.BLL;
using DTcms.Common;

namespace DTcms.Web.admin.card
{
    public partial class CardCategory_edit : Web.UI.ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作类型
        private int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = DTRequest.GetQueryString("action");
            if (!string.IsNullOrEmpty(_action) && _action == DTEnums.ActionEnum.Edit.ToString())
            {
                this.action = DTEnums.ActionEnum.Edit.ToString();//修改类型
                if (!int.TryParse(Request.QueryString["id"] as string, out this.id))
                {
                    JscriptMsg("传输参数不正确！", "back");
                    return;
                }
                if (!new BLL.CardCategory().Exists(this.id))
                {
                    JscriptMsg("记录不存在或已被删除！", "back");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("CardCategory_list", DTEnums.ActionEnum.View.ToString()); //检查权限
                Model.manager model = GetAdminInfo(); //取得管理员信息
                ParentBind();
                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
            }
        }

        #region 绑定父节点=================================
        private void ParentBind()
        {
            BLL.CardCategory bll = new BLL.CardCategory();
            DataTable dt = bll.GetList(0);

            this.ddlParentId.Items.Clear();
            this.ddlParentId.Items.Add(new ListItem("请选择类别...", ""));
            this.ddlParentId.Items.Add(new ListItem("顶级类别", "0"));
            foreach (DataRow dr in dt.Rows)
            {
                string Id = dr["CardCategoryId"].ToString();
                int layer = int.Parse(dr["Layer"].ToString());
                string name = dr["FullName"].ToString().Trim();

                if (layer == 1)
                {
                    this.ddlParentId.Items.Add(new ListItem(name, Id));
                }
                else
                {
                    name = "├ " + name;
                    name = Utils.StringOfChar(layer - 1, "　") + Title;
                    this.ddlParentId.Items.Add(new ListItem(name, Id));
                }
            }
        }
        #endregion

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.CardCategory bll = new BLL.CardCategory();
            Model.CardCategory model = bll.GetModel(_id);
            //编写赋值操作Begin
            ddlParentId.SelectedValue = model.ParentId.ToString();
            txtFullName.Text = model.FullName;
            txtBackImgUrl.Text = model.BackImageUrl;
            txtImgUrl.Text = model.ImagUrl;
            txtDescribe.Text = model.Describe;
            txtDuration.Text = model.Duration.ToString();
            txtCallIndex.Text = model.CallIndex;
            //编写赋值操作End
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            Model.CardCategory model = new Model.CardCategory();
            BLL.CardCategory bll = new BLL.CardCategory();
            //编写添加操作Begin
            model.FullName = txtFullName.Text;
            model.Describe = txtDescribe.Text;
            model.ImagUrl = txtImgUrl.Text;
            if (!string.IsNullOrEmpty(ddlParentId.SelectedValue))
                model.ParentId = Convert.ToInt32(ddlParentId.SelectedValue);
            else
            {
                model.ParentId = 0;
            }
            var parent = bll.GetModel(model.ParentId);
            model.Layer = parent.Layer + 1;
            model.BackImageUrl = txtBackImgUrl.Text;
            model.CallIndex = txtCallIndex.Text;
            model.Duration = decimal.Parse(txtDuration.Text);
            model.CreateDate = DateTime.Now;
            model.CreateUserName = GetAdminInfo().user_name;
            model.ModifyDate = DateTime.Now;
            //编写添加操作End

            if (bll.Add(model) > 0)
            {
                AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "添加卡片类别:"
                    + model.FullName); //记录日志
                return true;
            }
            return false;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = false;
            BLL.CardCategory bll = new BLL.CardCategory();
            Model.CardCategory model = bll.GetModel(_id);

            //编写编辑操作Begin
            model.FullName = txtFullName.Text;
            model.Describe = txtDescribe.Text;
            model.ImagUrl = txtImgUrl.Text;
            model.BackImageUrl = txtBackImgUrl.Text;
            model.CallIndex = txtCallIndex.Text;
            model.Duration = decimal.Parse(txtDuration.Text);
            if (!string.IsNullOrEmpty(ddlParentId.SelectedValue))
            {
                model.ParentId = Convert.ToInt32(ddlParentId.SelectedValue);
            }
            else
            {
                model.ParentId = 0;
            }
            model.Layer = model.ParentId + 1;
            model.ModifyDate = DateTime.Now;
            model.ModifyUserName = GetAdminInfo().user_name;
            //编写编辑操作End

            if (bll.Update(model))
            {
                AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改卡片类型:" + model.FullName); //记录日志
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
                ChkAdminLevel("CardCategory_list", DTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误！", "");
                    return;
                }
                JscriptMsg("修改卡片类型信息成功！", "CardCategory_list.aspx");
            }
            else //添加
            {
                ChkAdminLevel("CardCategory_list", DTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误！", "");
                    return;
                }
                JscriptMsg("添加卡片类型信息成功！", "CardCategory_list.aspx");
            }
        }
    }
}