using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using DTcms.Common;

namespace DTcms.BLL {
	 	//课程类别关系
		public partial class CurriculumCategory
	{
   		private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息     
		private readonly DTcms.DAL.CurriculumCategory dal;
		public CurriculumCategory()
		{
			dal=new DTcms.DAL.CurriculumCategory(siteConfig.edudatabaseprefix);
		}
		
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int CurriculumCategoryId,int CategoryId,int CurriculumId)
		{
			return dal.Exists(CurriculumCategoryId,CategoryId,CurriculumId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(DTcms.Model.CurriculumCategory model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(DTcms.Model.CurriculumCategory model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int CurriculumCategoryId)
		{
			
			return dal.Delete(CurriculumCategoryId);
		}
				/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string CurriculumCategoryIdlist )
		{
			return dal.DeleteList(CurriculumCategoryIdlist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public DTcms.Model.CurriculumCategory GetModel(int CurriculumCategoryId)
		{
			
			return dal.GetModel(CurriculumCategoryId);
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
		public List<DTcms.Model.CurriculumCategory> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<DTcms.Model.CurriculumCategory> DataTableToList(DataTable dt)
		{
			List<DTcms.Model.CurriculumCategory> modelList = new List<DTcms.Model.CurriculumCategory>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				DTcms.Model.CurriculumCategory model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new DTcms.Model.CurriculumCategory();					
										
												if(dt.Rows[n]["CurriculumCategoryId"].ToString()!="")
				{
					model.CurriculumCategoryId=int.Parse(dt.Rows[n]["CurriculumCategoryId"].ToString());
				}
																													
												if(dt.Rows[n]["CategoryId"].ToString()!="")
				{
					model.CategoryId=int.Parse(dt.Rows[n]["CategoryId"].ToString());
				}
																													
												if(dt.Rows[n]["CurriculumId"].ToString()!="")
				{
					model.CurriculumId=int.Parse(dt.Rows[n]["CurriculumId"].ToString());
				}
																										
				
					modelList.Add(model);
				}
			}
			return modelList;
		}
		#endregion
   
	}
}