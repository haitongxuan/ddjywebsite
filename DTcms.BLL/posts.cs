using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DTcms.BLL
{
    public class forum_posts
    {
        private readonly DTcms.Model.siteconfig siteConfig = new DTcms.BLL.siteconfig().loadConfig(); //获得站点配置信息
        private readonly DAL.forum_posts dal;
        public forum_posts()
        {
            dal = new DAL.forum_posts(siteConfig.sysdatabaseprefix);
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
        /// 返回用户名称
        /// </summary>
        public string GetUserName(int id)
        {
            return dal.GetUserName(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.forum_posts model)
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
        public bool Update(Model.forum_posts model)
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
        public Model.forum_posts GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        
        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int board_id, int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(board_id, pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }
        #endregion
    }
}
