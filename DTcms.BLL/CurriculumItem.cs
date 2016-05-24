using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using DTcms.Common;

namespace DTcms.BLL
{
    //课程章节
    public partial class CurriculumItem
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息     
        private readonly DTcms.DAL.CurriculumItem dal;
        public CurriculumItem()
        {
            dal = new DTcms.DAL.CurriculumItem(siteConfig.edudatabaseprefix);
        }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int CurriculumItemId, int CurriculumId)
        {
            return dal.Exists(CurriculumItemId, CurriculumId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(DTcms.Model.CurriculumItem model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DTcms.Model.CurriculumItem model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int CurriculumItemId)
        {

            return dal.Delete(CurriculumItemId);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string CurriculumItemIdlist)
        {
            return dal.DeleteList(CurriculumItemIdlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DTcms.Model.CurriculumItem GetModel(int CurriculumItemId)
        {

            return dal.GetModel(CurriculumItemId);
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
        public List<DTcms.Model.CurriculumItem> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DTcms.Model.CurriculumItem> DataTableToList(DataTable dt)
        {
            List<DTcms.Model.CurriculumItem> modelList = new List<DTcms.Model.CurriculumItem>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                DTcms.Model.CurriculumItem model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new DTcms.Model.CurriculumItem();

                    if (dt.Rows[n]["CurriculumItemId"].ToString() != "")
                    {
                        model.CurriculumItemId = int.Parse(dt.Rows[n]["CurriculumItemId"].ToString());
                    }

                    if (dt.Rows[n]["CurriculumId"].ToString() != "")
                    {
                        model.CurriculumId = int.Parse(dt.Rows[n]["CurriculumId"].ToString());
                    }

                    model.FullName = dt.Rows[n]["FullName"].ToString();

                    model.Describe = dt.Rows[n]["Describe"].ToString();

                    if (dt.Rows[n]["Sort"].ToString() != "")
                    {
                        model.Sort = int.Parse(dt.Rows[n]["Sort"].ToString());
                    }

                    if (dt.Rows[n]["DeleteMark"].ToString() != "")
                    {
                        model.DeleteMark = int.Parse(dt.Rows[n]["DeleteMark"].ToString());
                    }

                    if (dt.Rows[n]["Enable"].ToString() != "")
                    {
                        model.Enable = int.Parse(dt.Rows[n]["Enable"].ToString());
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

                    model.ImageUrl = dt.Rows[n]["ImageUrl"].ToString();

                    model.MediaCode = dt.Rows[n]["MediaCode"].ToString();


                    modelList.Add(model);
                }
            }
            return modelList;
        }
        #endregion

    }
}