using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DTcms.Web.Plugin.Forum.BLL
{
    /// <summary>
    /// 扩展属性表
    /// </summary>
    public partial class forum_board
    {
        private readonly DTcms.Model.siteconfig siteConfig = new DTcms.BLL.siteconfig().loadConfig(); //获得站点配置信息
        private readonly DAL.forum_board dal;
        public forum_board()
        {
            dal = new DAL.forum_board(siteConfig.sysdatabaseprefix);
        }

        #region 基本方法================================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 返回类别名称
        /// </summary>
        public string GetBoardName(int id)
        {
            return dal.GetBoardName(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.forum_board model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            dal.UpdateField(id, strValue);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.forum_board model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            return dal.Delete(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.forum_board GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 取得该频道指定类别下的列表
        /// </summary>
        /// <param name="parent_id">父ID</param>
        /// <param name="channel_id">频道ID</param>
        /// <returns></returns>
        public DataTable GetChildList(int parent_id)
        {
            return dal.GetChildList(parent_id);
        }

        /// <summary>
        /// 取得该频道下所有类别列表
        /// </summary>
        /// <param name="parent_id">父ID</param>
        /// <param name="channel_id">频道ID</param>
        /// <returns>DataTable</returns>
        public DataTable GetAllList(int parent_id)
        {
            return dal.GetAllList(parent_id);
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }
        #endregion

        #region 扩展方法================================
        /// <summary>
        /// 取得父节点的ID
        /// </summary>
        public int GetParentId(int id)
        {
            return dal.GetParentId(id);
        }
        #endregion
    }
}
