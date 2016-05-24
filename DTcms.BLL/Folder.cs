using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using DTcms.Common;

namespace DTcms.BLL {
	 	//文件夹
		public partial class Folder
	{
   		private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息     
		private readonly DTcms.DAL.Folder dal;
		public Folder()
		{
			dal=new DTcms.DAL.Folder(siteConfig.edudatabaseprefix);
		}
		
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int FolderId)
		{
			return dal.Exists(FolderId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(DTcms.Model.Folder model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(DTcms.Model.Folder model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int FolderId)
		{
			
			return dal.Delete(FolderId);
		}
				/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string FolderIdlist )
		{
			return dal.DeleteList(FolderIdlist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public DTcms.Model.Folder GetModel(int FolderId)
		{
			
			return dal.GetModel(FolderId);
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
		public List<DTcms.Model.Folder> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<DTcms.Model.Folder> DataTableToList(DataTable dt)
		{
			List<DTcms.Model.Folder> modelList = new List<DTcms.Model.Folder>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				DTcms.Model.Folder model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new DTcms.Model.Folder();					
										
												if(dt.Rows[n]["FolderId"].ToString()!="")
				{
					model.FolderId=int.Parse(dt.Rows[n]["FolderId"].ToString());
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