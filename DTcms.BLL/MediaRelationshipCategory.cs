using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using DTcms.Common;

namespace DTcms.BLL {
	 	//媒体关系类别
		public partial class MediaRelationshipCategory
	{
   		private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息     
		private readonly DTcms.DAL.MediaRelationshipCategory dal;
		public MediaRelationshipCategory()
		{
			dal=new DTcms.DAL.MediaRelationshipCategory(siteConfig.edudatabaseprefix);
		}
		
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int MediaRelationshipStyleId)
		{
			return dal.Exists(MediaRelationshipStyleId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void  Add(DTcms.Model.MediaRelationshipCategory model)
		{
						dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(DTcms.Model.MediaRelationshipCategory model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int MediaRelationshipStyleId)
		{
			
			return dal.Delete(MediaRelationshipStyleId);
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public DTcms.Model.MediaRelationshipCategory GetModel(int MediaRelationshipStyleId)
		{
			
			return dal.GetModel(MediaRelationshipStyleId);
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
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
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
		public List<DTcms.Model.MediaRelationshipCategory> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<DTcms.Model.MediaRelationshipCategory> DataTableToList(DataTable dt)
		{
			List<DTcms.Model.MediaRelationshipCategory> modelList = new List<DTcms.Model.MediaRelationshipCategory>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				DTcms.Model.MediaRelationshipCategory model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new DTcms.Model.MediaRelationshipCategory();					
										
												if(dt.Rows[n]["MediaRelationshipStyleId"].ToString()!="")
				{
					model.MediaRelationshipStyleId=int.Parse(dt.Rows[n]["MediaRelationshipStyleId"].ToString());
				}
																													
																model.FullName= dt.Rows[n]["FullName"].ToString();
																									
																model.Code= dt.Rows[n]["Code"].ToString();
																									
												if(dt.Rows[n]["ParentId"].ToString()!="")
				{
					model.ParentId=int.Parse(dt.Rows[n]["ParentId"].ToString());
				}
																													
												if(dt.Rows[n]["Layer"].ToString()!="")
				{
					model.Layer=int.Parse(dt.Rows[n]["Layer"].ToString());
				}
																													
																model.Remark= dt.Rows[n]["Remark"].ToString();
																									
												if(dt.Rows[n]["DeleteMark"].ToString()!="")
				{
					model.DeleteMark=int.Parse(dt.Rows[n]["DeleteMark"].ToString());
				}
																													
												if(dt.Rows[n]["Enable"].ToString()!="")
				{
					model.Enable=int.Parse(dt.Rows[n]["Enable"].ToString());
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