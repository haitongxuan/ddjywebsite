using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using DTcms.Common;

namespace DTcms.BLL {
	 	//用户课程历史
		public partial class UserCurriculum
	{
   		private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息     
		private readonly DTcms.DAL.UserCurriculum dal;
		public UserCurriculum()
		{
			dal=new DTcms.DAL.UserCurriculum(siteConfig.edudatabaseprefix);
		}
		
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int UserCurriculumId)
		{
			return dal.Exists(UserCurriculumId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(DTcms.Model.UserCurriculum model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(DTcms.Model.UserCurriculum model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int UserCurriculumId)
		{
			
			return dal.Delete(UserCurriculumId);
		}
				/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string UserCurriculumIdlist )
		{
			return dal.DeleteList(UserCurriculumIdlist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public DTcms.Model.UserCurriculum GetModel(int UserCurriculumId)
		{
			
			return dal.GetModel(UserCurriculumId);
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
		public List<DTcms.Model.UserCurriculum> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<DTcms.Model.UserCurriculum> DataTableToList(DataTable dt)
		{
			List<DTcms.Model.UserCurriculum> modelList = new List<DTcms.Model.UserCurriculum>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				DTcms.Model.UserCurriculum model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new DTcms.Model.UserCurriculum();					
													if(dt.Rows[n]["UserCurriculumId"].ToString()!="")
				{
					model.UserCurriculumId=int.Parse(dt.Rows[n]["UserCurriculumId"].ToString());
				}
																																if(dt.Rows[n]["CurriculumId"].ToString()!="")
				{
					model.CurriculumId=int.Parse(dt.Rows[n]["CurriculumId"].ToString());
				}
																																if(dt.Rows[n]["UserId"].ToString()!="")
				{
					model.UserId=int.Parse(dt.Rows[n]["UserId"].ToString());
				}
																																if(dt.Rows[n]["CurriculumItemId"].ToString()!="")
				{
					model.CurriculumItemId=int.Parse(dt.Rows[n]["CurriculumItemId"].ToString());
				}
																																if(dt.Rows[n]["CreateDate"].ToString()!="")
				{
					model.CreateDate=DateTime.Parse(dt.Rows[n]["CreateDate"].ToString());
				}
																										
				
					modelList.Add(model);
				}
			}
			return modelList;
		}
#endregion
   
	}
}