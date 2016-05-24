using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using DTcms.Common;

namespace DTcms.BLL {
	 	//媒体类别关系表
		public partial class MediaRelationship
	{
   		private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息     
		private readonly DTcms.DAL.MediaRelationship dal;
		public MediaRelationship()
		{
			dal=new DTcms.DAL.MediaRelationship(siteConfig.edudatabaseprefix);
		}
		
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int MediaCategoryRelationshipId,int MediaId)
		{
			return dal.Exists(MediaCategoryRelationshipId,MediaId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(DTcms.Model.MediaRelationship model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(DTcms.Model.MediaRelationship model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int MediaCategoryRelationshipId)
		{
			
			return dal.Delete(MediaCategoryRelationshipId);
		}
				/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string MediaCategoryRelationshipIdlist )
		{
			return dal.DeleteList(MediaCategoryRelationshipIdlist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public DTcms.Model.MediaRelationship GetModel(int MediaCategoryRelationshipId)
		{
			
			return dal.GetModel(MediaCategoryRelationshipId);
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
		public List<DTcms.Model.MediaRelationship> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<DTcms.Model.MediaRelationship> DataTableToList(DataTable dt)
		{
			List<DTcms.Model.MediaRelationship> modelList = new List<DTcms.Model.MediaRelationship>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				DTcms.Model.MediaRelationship model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new DTcms.Model.MediaRelationship();					
										
												if(dt.Rows[n]["MediaCategoryRelationshipId"].ToString()!="")
				{
					model.MediaCategoryRelationshipId=int.Parse(dt.Rows[n]["MediaCategoryRelationshipId"].ToString());
				}
																													
												if(dt.Rows[n]["MediaId"].ToString()!="")
				{
					model.MediaId=int.Parse(dt.Rows[n]["MediaId"].ToString());
				}
																													
												if(dt.Rows[n]["MediaRelationshipCategoryId"].ToString()!="")
				{
					model.MediaRelationshipCategoryId=int.Parse(dt.Rows[n]["MediaRelationshipCategoryId"].ToString());
				}
																										
				
					modelList.Add(model);
				}
			}
			return modelList;
		}
		#endregion
   
	}
}