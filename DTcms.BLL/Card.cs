using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using DTcms.Common;

namespace DTcms.BLL {
	 	//卡片信息表
		public partial class Card
	{
   		private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息     
		private readonly DTcms.DAL.Card dal;
		public Card()
		{
			dal=new DTcms.DAL.Card(siteConfig.edudatabaseprefix);
		}
		
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int CardId,int CardCategoryId)
		{
			return dal.Exists(CardId,CardCategoryId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(DTcms.Model.Card model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(DTcms.Model.Card model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int CardId)
		{
			
			return dal.Delete(CardId);
		}
				/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string CardIdlist )
		{
			return dal.DeleteList(CardIdlist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public DTcms.Model.Card GetModel(int CardId)
		{
			
			return dal.GetModel(CardId);
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
		public List<DTcms.Model.Card> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<DTcms.Model.Card> DataTableToList(DataTable dt)
		{
			List<DTcms.Model.Card> modelList = new List<DTcms.Model.Card>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				DTcms.Model.Card model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new DTcms.Model.Card();					
										
												if(dt.Rows[n]["CardId"].ToString()!="")
				{
					model.CardId=int.Parse(dt.Rows[n]["CardId"].ToString());
				}
																													
												if(dt.Rows[n]["CardCategoryId"].ToString()!="")
				{
					model.CardCategoryId=int.Parse(dt.Rows[n]["CardCategoryId"].ToString());
				}
																													
																model.Code= dt.Rows[n]["Code"].ToString();
																									
												if(dt.Rows[n]["CreateDate"].ToString()!="")
				{
					model.CreateDate=DateTime.Parse(dt.Rows[n]["CreateDate"].ToString());
				}
																													
												if(dt.Rows[n]["StartDate"].ToString()!="")
				{
					model.StartDate=DateTime.Parse(dt.Rows[n]["StartDate"].ToString());
				}
																													
												if(dt.Rows[n]["EndDate"].ToString()!="")
				{
					model.EndDate=DateTime.Parse(dt.Rows[n]["EndDate"].ToString());
				}
																										
				
					modelList.Add(model);
				}
			}
			return modelList;
		}
		#endregion
   
	}
}