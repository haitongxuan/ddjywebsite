using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using DTcms.Common;

namespace DTcms.BLL {
	 	//课程关系
		public partial class CurriculumCRelationshipStyle
	{
   		private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息     
		private readonly DTcms.DAL.CurriculumCRelationshipStyle dal;
		public CurriculumCRelationshipStyle()
		{
			dal=new DTcms.DAL.CurriculumCRelationshipStyle(siteConfig.edudatabaseprefix);
		}
		
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int CurriculumCRelationshipStyleId,int SourceCurricularStyleCurriculumId,int TargetCurricularStyleCurriculumId,int CRelationshipStyleId)
		{
			return dal.Exists(CurriculumCRelationshipStyleId,SourceCurricularStyleCurriculumId,TargetCurricularStyleCurriculumId,CRelationshipStyleId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(DTcms.Model.CurriculumCRelationshipStyle model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(DTcms.Model.CurriculumCRelationshipStyle model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int CurriculumCRelationshipStyleId)
		{
			
			return dal.Delete(CurriculumCRelationshipStyleId);
		}
				/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string CurriculumCRelationshipStyleIdlist )
		{
			return dal.DeleteList(CurriculumCRelationshipStyleIdlist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public DTcms.Model.CurriculumCRelationshipStyle GetModel(int CurriculumCRelationshipStyleId)
		{
			
			return dal.GetModel(CurriculumCRelationshipStyleId);
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
		public List<DTcms.Model.CurriculumCRelationshipStyle> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<DTcms.Model.CurriculumCRelationshipStyle> DataTableToList(DataTable dt)
		{
			List<DTcms.Model.CurriculumCRelationshipStyle> modelList = new List<DTcms.Model.CurriculumCRelationshipStyle>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				DTcms.Model.CurriculumCRelationshipStyle model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new DTcms.Model.CurriculumCRelationshipStyle();					
										
												if(dt.Rows[n]["CurriculumCRelationshipStyleId"].ToString()!="")
				{
					model.CurriculumCRelationshipStyleId=int.Parse(dt.Rows[n]["CurriculumCRelationshipStyleId"].ToString());
				}
																													
												if(dt.Rows[n]["StartDate"].ToString()!="")
				{
					model.StartDate=DateTime.Parse(dt.Rows[n]["StartDate"].ToString());
				}
																													
												if(dt.Rows[n]["EndDate"].ToString()!="")
				{
					model.EndDate=DateTime.Parse(dt.Rows[n]["EndDate"].ToString());
				}
																													
																model.Remark= dt.Rows[n]["Remark"].ToString();
																									
												if(dt.Rows[n]["SourceCurricularStyleCurriculumId"].ToString()!="")
				{
					model.SourceCurricularStyleCurriculumId=int.Parse(dt.Rows[n]["SourceCurricularStyleCurriculumId"].ToString());
				}
																													
												if(dt.Rows[n]["TargetCurricularStyleCurriculumId"].ToString()!="")
				{
					model.TargetCurricularStyleCurriculumId=int.Parse(dt.Rows[n]["TargetCurricularStyleCurriculumId"].ToString());
				}
																													
												if(dt.Rows[n]["CRelationshipStyleId"].ToString()!="")
				{
					model.CRelationshipStyleId=int.Parse(dt.Rows[n]["CRelationshipStyleId"].ToString());
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
		#endregion
   
	}
}