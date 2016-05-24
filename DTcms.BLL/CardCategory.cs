using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using DTcms.Common;

namespace DTcms.BLL
{
    //卡片类别
    public partial class CardCategory
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息     
        private readonly DTcms.DAL.CardCategory dal;
        public CardCategory()
        {
            dal = new DTcms.DAL.CardCategory(siteConfig.edudatabaseprefix);
        }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int CardCategoryId)
        {
            return dal.Exists(CardCategoryId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(DTcms.Model.CardCategory model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DTcms.Model.CardCategory model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int CardCategoryId)
        {

            return dal.Delete(CardCategoryId);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string CardCategoryIdlist)
        {
            return dal.DeleteList(CardCategoryIdlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DTcms.Model.CardCategory GetModel(int CardCategoryId)
        {

            return dal.GetModel(CardCategoryId);
        }







        public DTcms.Model.CardCategory GetModel(string CallIndex)
        {
            return dal.GetModel(CallIndex);
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
        public List<DTcms.Model.CardCategory> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DTcms.Model.CardCategory> DataTableToList(DataTable dt)
        {
            List<DTcms.Model.CardCategory> modelList = new List<DTcms.Model.CardCategory>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                DTcms.Model.CardCategory model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new DTcms.Model.CardCategory();

                    if (dt.Rows[n]["CardCategoryId"].ToString() != "")
                    {
                        model.CardCategoryId = int.Parse(dt.Rows[n]["CardCategoryId"].ToString());
                    }

                    if (dt.Rows[n]["CreateDate"].ToString() != "")
                    {
                        model.CreateDate = DateTime.Parse(dt.Rows[n]["CreateDate"].ToString());
                    }

                    model.CreateUserName = dt.Rows[n]["CreateUserName"].ToString();

                    if (dt.Rows[n]["ModifyDate"].ToString() != "")
                    {
                        model.ModifyDate = DateTime.Parse(dt.Rows[n]["ModifyDate"].ToString());
                    }

                    model.ModifyUserName = dt.Rows[n]["ModifyUserName"].ToString();

                    if (dt.Rows[n]["ParentId"].ToString() != "")
                    {
                        model.ParentId = int.Parse(dt.Rows[n]["ParentId"].ToString());
                    }

                    model.CallIndex = dt.Rows[n]["CallIndex"].ToString();

                    if (dt.Rows[n]["Layer"].ToString() != "")
                    {
                        model.Layer = int.Parse(dt.Rows[n]["Layer"].ToString());
                    }

                    model.FullName = dt.Rows[n]["FullName"].ToString();

                    model.Describe = dt.Rows[n]["Describe"].ToString();

                    model.ImagUrl = dt.Rows[n]["ImagUrl"].ToString();

                    model.BackImageUrl = dt.Rows[n]["BackImageUrl"].ToString();

                    if (dt.Rows[n]["Duration"].ToString() != "")
                    {
                        model.Duration = decimal.Parse(dt.Rows[n]["Duration"].ToString());
                    }


                    modelList.Add(model);
                }
            }
            return modelList;
        }
        /// <summary>
        /// 取得所有类别列表
        /// </summary>
        public DataTable GetList(int parentId)
        {
            return dal.GetList(parentId);
        }
        #endregion

    }
}