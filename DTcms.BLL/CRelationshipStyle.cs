using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using DTcms.Common;

namespace DTcms.BLL {
	 	//课程关系类型
		public partial class CRelationshipStyle
	{
   		private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息     
		private readonly DTcms.DAL.CRelationshipStyle dal;
		public CRelationshipStyle()
		{
			dal=new DTcms.DAL.CRelationshipStyle(siteConfig.edudatabaseprefix);
		}
		
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int CRelationshipStyleId,int SourceCurricularStyleId,int TargetCurricularStyleId)
		{
			return dal.Exists(CRelationshipStyleId,SourceCurricularStyleId,TargetCurricularStyleId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(DTcms.Model.CRelationshipStyle model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(DTcms.Model.CRelationshipStyle model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int CRelationshipStyleId)
		{
			
			return dal.Delete(CRelationshipStyleId);
		}
				/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string CRelationshipStyleIdlist )
		{
			return dal.DeleteList(CRelationshipStyleIdlist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public DTcms.Model.CRelationshipStyle GetModel(int CRelationshipStyleId)
		{
			
			return dal.GetModel(CRelationshipStyleId);
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
		public List<DTcms.Model.CRelationshipStyle> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<DTcms.Model.CRelationshipStyle> DataTableToList(DataTable dt)
		{
			List<DTcms.Model.CRelationshipStyle> modelList = new List<DTcms.Model.CRelationshipStyle>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				DTcms.Model.CRelationshipStyle model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new DTcms.Model.CRelationshipStyle();					
										
												if(dt.Rows[n]["CRelationshipStyleId"].ToString()!="")
				{
					model.CRelationshipStyleId=int.Parse(dt.Rows[n]["CRelationshipStyleId"].ToString());
				}
																													
												if(dt.Rows[n]["ParentId"].ToString()!="")
				{
					model.ParentId=int.Parse(dt.Rows[n]["ParentId"].ToString());
				}
																													
												if(dt.Rows[n]["Layer"].ToString()!="")
				{
					model.Layer=int.Parse(dt.Rows[n]["Layer"].ToString());
				}
																													
																model.FullName= dt.Rows[n]["FullName"].ToString();
																									
																model.Remark= dt.Rows[n]["Remark"].ToString();
																									
												if(dt.Rows[n]["SourceCurricularStyleId"].ToString()!="")
				{
					model.SourceCurricularStyleId=int.Parse(dt.Rows[n]["SourceCurricularStyleId"].ToString());
				}
																													
												if(dt.Rows[n]["TargetCurricularStyleId"].ToString()!="")
				{
					model.TargetCurricularStyleId=int.Parse(dt.Rows[n]["TargetCurricularStyleId"].ToString());
				}
																													
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