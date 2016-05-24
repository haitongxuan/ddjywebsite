using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using DTcms.Common;

namespace DTcms.BLL
{
    //媒体表
    public partial class Media
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息     
        private readonly DTcms.DAL.Media dal;
        public Media()
        {
            dal = new DTcms.DAL.Media(siteConfig.edudatabaseprefix);
        }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int MediaId, int FolderId)
        {
            return dal.Exists(MediaId, FolderId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(DTcms.Model.Media model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DTcms.Model.Media model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int MediaId)
        {

            return dal.Delete(MediaId);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string MediaIdlist)
        {
            return dal.DeleteList(MediaIdlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DTcms.Model.Media GetModel(int MediaId)
        {

            return dal.GetModel(MediaId);
        }

        public DTcms.Model.Media GetModel(string MediaCode)
        {
            return dal.GetModel(MediaCode);
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
        public List<DTcms.Model.Media> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DTcms.Model.Media> DataTableToList(DataTable dt)
        {
            List<DTcms.Model.Media> modelList = new List<DTcms.Model.Media>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                DTcms.Model.Media model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new DTcms.Model.Media();

                    if (dt.Rows[n]["MediaId"].ToString() != "")
                    {
                        model.MediaId = int.Parse(dt.Rows[n]["MediaId"].ToString());
                    }

                    model.FullName = dt.Rows[n]["FullName"].ToString();

                    model.FileName = dt.Rows[n]["FileName"].ToString();

                    if (dt.Rows[n]["FolderId"].ToString() != "")
                    {
                        model.FolderId = int.Parse(dt.Rows[n]["FolderId"].ToString());
                    }

                    model.RealPath = dt.Rows[n]["RealPath"].ToString();

                    if (dt.Rows[n]["DeleteMark"].ToString() != "")
                    {
                        model.DeleteMark = int.Parse(dt.Rows[n]["DeleteMark"].ToString());
                    }

                    if (dt.Rows[n]["Enable"].ToString() != "")
                    {
                        model.Enable = int.Parse(dt.Rows[n]["Enable"].ToString());
                    }

                    if (dt.Rows[n]["MediaCategoryId"].ToString() != "")
                    {
                        model.MediaCategoryId = int.Parse(dt.Rows[n]["MediaCategoryId"].ToString());
                    }

                    model.MediaCode = dt.Rows[n]["MediaCode"].ToString();
                    if (dt.Rows[0]["TimeLength"].ToString() != "")
                    {
                        model.TimeLength = decimal.Parse(dt.Rows[0]["TimeLength"].ToString());
                    }

                    modelList.Add(model);
                }
            }
            return modelList;
        }
        #endregion

    }
}