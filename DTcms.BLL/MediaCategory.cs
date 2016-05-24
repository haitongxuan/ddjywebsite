using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using DTcms.Common;

namespace DTcms.BLL {
	 	//媒体类别表
		public partial class MediaCategory
	{
   		private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息     
		private readonly DTcms.DAL.MediaCategory dal;
		public MediaCategory()
		{
			dal=new DTcms.DAL.MediaCategory(siteConfig.edudatabaseprefix);
		}
		
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int MediaCategoryId)
		{
			return dal.Exists(MediaCategoryId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(DTcms.Model.MediaCategory model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(DTcms.Model.MediaCategory model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int MediaCategoryId)
		{
			
			return dal.Delete(MediaCategoryId);
		}
				/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string MediaCategoryIdlist )
		{
			return dal.DeleteList(MediaCategoryIdlist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public DTcms.Model.MediaCategory GetModel(int MediaCategoryId)
		{
			
			return dal.GetModel(MediaCategoryId);
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
		public List<DTcms.Model.MediaCategory> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<DTcms.Model.MediaCategory> DataTableToList(DataTable dt)
		{
			List<DTcms.Model.MediaCategory> modelList = new List<DTcms.Model.MediaCategory>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				DTcms.Model.MediaCategory model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new DTcms.Model.MediaCategory();					
										
												if(dt.Rows[n]["MediaCategoryId"].ToString()!="")
				{
					model.MediaCategoryId=int.Parse(dt.Rows[n]["MediaCategoryId"].ToString());
				}
																													
																model.FullName= dt.Rows[n]["FullName"].ToString();
																									
																model.CallIndex= dt.Rows[n]["CallIndex"].ToString();
																									
												if(dt.Rows[n]["Layer"].ToString()!="")
				{
					model.Layer=int.Parse(dt.Rows[n]["Layer"].ToString());
				}
																													
												if(dt.Rows[n]["ParentId"].ToString()!="")
				{
					model.ParentId=int.Parse(dt.Rows[n]["ParentId"].ToString());
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
																													
												if(dt.Rows[n]["CreateDate"].ToString()!="")
				{
					model.CreateDate=DateTime.Parse(dt.Rows[n]["CreateDate"].ToString());
				}
																													
																model.CreateUserId= dt.Rows[n]["CreateUserId"].ToString();
																									
																model.CreateUserName= dt.Rows[n]["CreateUserName"].ToString();
																									
												if(dt.Rows[n]["ModifyDate"].ToString()!="")
				{
					model.ModifyDate=DateTime.Parse(dt.Rows[n]["ModifyDate"].ToString());
				}
																													
																model.ModifyUserId= dt.Rows[n]["ModifyUserId"].ToString();
																									
																model.ModifyUserName= dt.Rows[n]["ModifyUserName"].ToString();
																						
				
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