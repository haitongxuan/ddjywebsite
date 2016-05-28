using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using DTcms.Common;

namespace DTcms.BLL
{
    //article_item
    public partial class article_item
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息     
        private readonly DTcms.DAL.article_item dal;
        public article_item()
        {
            dal = new DTcms.DAL.article_item(siteConfig.sysdatabaseprefix);
        }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(DTcms.Model.article_item model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DTcms.Model.article_item model)
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
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            return dal.DeleteList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DTcms.Model.article_item GetModel(int id)
        {

            return dal.GetModel(id);
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
        /// 根据文章主键获取明细列表
        /// </summary>
        /// <param name="article_id"></param>
        /// <returns></returns>
        public List<Model.article_item> GetList(int article_id)
        {
            return dal.GetList(article_id);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DTcms.Model.article_item> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DTcms.Model.article_item> DataTableToList(DataTable dt)
        {
            List<DTcms.Model.article_item> modelList = new List<DTcms.Model.article_item>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                DTcms.Model.article_item model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new DTcms.Model.article_item();

                    if (dt.Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(dt.Rows[n]["id"].ToString());
                    }

                    if (dt.Rows[n]["article_id"].ToString() != "")
                    {
                        model.article_id = int.Parse(dt.Rows[n]["article_id"].ToString());
                    }

                    if (dt.Rows[n]["item_article_id"].ToString() != "")
                    {
                        model.item_article_id = int.Parse(dt.Rows[n]["item_article_id"].ToString());
                    }

                    if (dt.Rows[n]["channel_id"].ToString() != "")
                    {
                        model.channel_id = int.Parse(dt.Rows[n]["channel_id"].ToString());
                    }

                    if (dt.Rows[n]["item_channel_id"].ToString() != "")
                    {
                        model.item_channel_id = int.Parse(dt.Rows[n]["item_channel_id"].ToString());
                    }

                    if (dt.Rows[n]["sort_id"].ToString() != "")
                    {
                        model.sort_id = int.Parse(dt.Rows[n]["sort_id"].ToString());
                    }

                    model.item_title = dt.Rows[n]["item_title"].ToString();

                    model.item_img_url = dt.Rows[n]["item_img_url"].ToString();


                    modelList.Add(model);
                }
            }
            return modelList;
        }
        #endregion

    }
}