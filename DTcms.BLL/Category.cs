using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using DTcms.Common;

namespace DTcms.BLL {
	 	//课程类别
		public partial class Category
	{
   		private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息     
		private readonly DTcms.DAL.Category dal;
		public Category()
		{
			dal=new DTcms.DAL.Category(siteConfig.edudatabaseprefix);
		}
		
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int CategoryId)
		{
			return dal.Exists(CategoryId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(DTcms.Model.Category model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(DTcms.Model.Category model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int CategoryId)
		{
			
			return dal.Delete(CategoryId);
		}
				/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string CategoryIdlist )
		{
			return dal.DeleteList(CategoryIdlist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public DTcms.Model.Category GetModel(int CategoryId)
		{
			
			return dal.GetModel(CategoryId);
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
		public List<DTcms.Model.Category> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<DTcms.Model.Category> DataTableToList(DataTable dt)
		{
			List<DTcms.Model.Category> modelList = new List<DTcms.Model.Category>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				DTcms.Model.Category model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new DTcms.Model.Category();					
										
												if(dt.Rows[n]["CategoryId"].ToString()!="")
				{
					model.CategoryId=int.Parse(dt.Rows[n]["CategoryId"].ToString());
				}
																													
																model.FullName= dt.Rows[n]["FullName"].ToString();
																									
												if(dt.Rows[n]["ParentId"].ToString()!="")
				{
					model.ParentId=int.Parse(dt.Rows[n]["ParentId"].ToString());
				}
																													
												if(dt.Rows[n]["Layer"].ToString()!="")
				{
					model.Layer=int.Parse(dt.Rows[n]["Layer"].ToString());
				}
																													
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
																													
																model.CreateUserName= dt.Rows[n]["CreateUserName"].ToString();
																									
												if(dt.Rows[n]["ModifyDate"].ToString()!="")
				{
					model.ModifyDate=DateTime.Parse(dt.Rows[n]["ModifyDate"].ToString());
				}
																													
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