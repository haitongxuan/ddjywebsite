using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using DTcms.Common;

namespace DTcms.BLL
{
    //用户Token，用于APP用户认证
    public partial class UserToken
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息     
        private readonly DTcms.DAL.UserToken dal;
        public UserToken()
        {
            dal = new DTcms.DAL.UserToken(siteConfig.edudatabaseprefix);
        }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string UserTokenId)
        {
            return dal.Exists(UserTokenId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(DTcms.Model.UserToken model)
        {
            dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DTcms.Model.UserToken model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string UserTokenId)
        {

            return dal.Delete(UserTokenId);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DTcms.Model.UserToken GetModel(string UserTokenId)
        {

            return dal.GetModel(UserTokenId);
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
        public List<DTcms.Model.UserToken> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DTcms.Model.UserToken> DataTableToList(DataTable dt)
        {
            List<DTcms.Model.UserToken> modelList = new List<DTcms.Model.UserToken>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                DTcms.Model.UserToken model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new DTcms.Model.UserToken();
                    model.UserTokenId = dt.Rows[n]["UserTokenId"].ToString();
                    model.UserId = dt.Rows[n]["UserId"].ToString();
                    model.UserName = dt.Rows[n]["UserName"].ToString();
                    model.Token = dt.Rows[n]["Token"].ToString();
                    if (dt.Rows[n]["CreateTime"].ToString() != "")
                    {
                        model.CreateTime = DateTime.Parse(dt.Rows[n]["CreateTime"].ToString());
                    }
                    if (dt.Rows[n]["OverdueTime"].ToString() != "")
                    {
                        model.OverdueTime = DateTime.Parse(dt.Rows[n]["OverdueTime"].ToString());
                    }
                    if (dt.Rows[n]["IsOverdue"].ToString() != "")
                    {
                        model.IsOverdue = int.Parse(dt.Rows[n]["IsOverdue"].ToString());
                    }
                    model.DeviceId = dt.Rows[n]["DeviceId"].ToString();
                    model.IPAddress = dt.Rows[n]["IPAddress"].ToString();


                    modelList.Add(model);
                }
            }
            return modelList;
        }
        #endregion

    }
}