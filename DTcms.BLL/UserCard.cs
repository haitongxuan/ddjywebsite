using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using DTcms.Common;

namespace DTcms.BLL
{
    //用户卡片表
    public partial class UserCard
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息     
        private readonly DTcms.DAL.UserCard dal;
        public UserCard()
        {
            dal = new DTcms.DAL.UserCard(siteConfig.edudatabaseprefix);
        }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int UserCardId, int CardId)
        {
            return dal.Exists(UserCardId, CardId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(DTcms.Model.UserCard model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DTcms.Model.UserCard model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int UserCardId)
        {

            return dal.Delete(UserCardId);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string UserCardIdlist)
        {
            return dal.DeleteList(UserCardIdlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DTcms.Model.UserCard GetModel(int UserCardId)
        {

            return dal.GetModel(UserCardId);
        }






        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DTcms.Model.UserCard> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DTcms.Model.UserCard> DataTableToList(DataTable dt)
        {
            List<DTcms.Model.UserCard> modelList = new List<DTcms.Model.UserCard>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                DTcms.Model.UserCard model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new DTcms.Model.UserCard();

                    if (dt.Rows[n]["UserCardId"].ToString() != "")
                    {
                        model.UserCardId = int.Parse(dt.Rows[n]["UserCardId"].ToString());
                    }

                    if (dt.Rows[n]["CardId"].ToString() != "")
                    {
                        model.CardId = int.Parse(dt.Rows[n]["CardId"].ToString());
                    }

                    if (dt.Rows[n]["UserId"].ToString() != "")
                    {
                        model.UserId = int.Parse(dt.Rows[n]["UserId"].ToString());
                    }

                    if (dt.Rows[n]["CardCategoryId"].ToString() != "")
                    {
                        model.CardCategoryId = int.Parse(dt.Rows[n]["CardCategoryId"].ToString());
                    }


                    modelList.Add(model);
                }
            }
            return modelList;
        }
        #endregion

    }
}