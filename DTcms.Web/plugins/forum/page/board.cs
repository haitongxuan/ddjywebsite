using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DTcms.Common;

namespace DTcms.Web.Plugin.Forum
{
    public partial class board : Web.UI.BasePage
    {
        /// <summary>
        /// 重写虚方法,此方法将在Init事件前执行
        /// </summary>
        protected override void ShowPage()
        {
        }

        /// <summary>
        /// 获取所有板块列表
        /// </summary>
        /// <param name="top">显示条数</param>
        /// <param name="strwhere">查询条件</param>
        /// <returns>DataTable</returns>
        public DataTable get_board_list(int parent_id)
        {
            DataTable dt = new DataTable();
            dt = new BLL.forum_board().GetAllList(parent_id);
            return dt;
        }

        /// <summary>
        /// 取得该频道指定类别下的列表
        /// </summary>
        /// <param name="top">显示条数</param>
        /// <param name="strwhere">查询条件</param>
        /// <returns>DataTable</returns>
        public DataTable get_board_getchildlist(int parent_id)
        {
            DataTable dt = new DataTable();
            dt = new BLL.forum_board().GetChildList(parent_id);
            return dt;
        }

        /// <summary>
        /// 返回类别ID
        /// </summary>
        /// <param name="category_id">类别ID</param>
        /// <returns>String</returns>
        public string get_category_id(int category_id)
        {
            StringBuilder strTxt = new StringBuilder();
            if (category_id > 0)
            {
                LoopChannelMenu(strTxt, category_id);
            }
            return strTxt.ToString();
        }


        #region 私有方法===========================================
        /// <summary>
        /// 递归找到父节点
        /// </summary>
        private void LoopChannelMenu(StringBuilder strTxt, int category_id)
        {
            BLL.forum_board bll = new BLL.forum_board();
            int parentId = bll.GetParentId(category_id);
            strTxt.Append(bll.GetParentId(category_id));
        }

        #endregion

    }
}
