using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using DTcms.DBUtility;
using DTcms.Common;
namespace DTcms.DAL  
{
	 	//卡片信息表
		public partial class Card
	{
		private string databaseprefix; //数据库表名前缀
        public Card(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }
   		#region 基本方法================================
   		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int CardId,int CardCategoryId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from "+ databaseprefix +"Card");
			strSql.Append(" where ");
			                                       strSql.Append(" CardId = @CardId and  ");
                                                                   strSql.Append(" CardCategoryId = @CardCategoryId  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@CardId", SqlDbType.Int,4),
					new SqlParameter("@CardCategoryId", SqlDbType.Int,4)			};
			parameters[0].Value = CardId;
			parameters[1].Value = CardCategoryId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
		
		/// <summary>
        /// 返回数据总数
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from " + databaseprefix + "Card");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(DTcms.Model.Card model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into " + databaseprefix + "Card(");			
            strSql.Append("CardCategoryId,Code,CreateDate,StartDate,EndDate");
			strSql.Append(") values (");
            strSql.Append("@CardCategoryId,@Code,@CreateDate,@StartDate,@EndDate");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@CardCategoryId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Code", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@CreateDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@StartDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@EndDate", SqlDbType.DateTime)             
              
            };
			            
            parameters[0].Value = model.CardCategoryId;                        
            parameters[1].Value = model.Code;                        
            parameters[2].Value = model.CreateDate;                        
            parameters[3].Value = model.StartDate;                        
            parameters[4].Value = model.EndDate;                        
			   
			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);			
			if (obj == null)
			{
				return 0;
			}
			else
			{
				                    
            	return Convert.ToInt32(obj);
                                                                  
			}			   
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(DTcms.Model.Card model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update " + databaseprefix + "Card set ");
			                                                
            strSql.Append(" CardCategoryId = @CardCategoryId , ");                                    
            strSql.Append(" Code = @Code , ");                                    
            strSql.Append(" CreateDate = @CreateDate , ");                                    
            strSql.Append(" StartDate = @StartDate , ");                                    
            strSql.Append(" EndDate = @EndDate  ");            			
			strSql.Append(" where CardId=@CardId ");
						
			SqlParameter[] parameters = {
			            new SqlParameter("@CardId", SqlDbType.Int,4) ,            
                        new SqlParameter("@CardCategoryId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Code", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@CreateDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@StartDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@EndDate", SqlDbType.DateTime)             
              
            };
						            
            parameters[0].Value = model.CardId;                        
            parameters[1].Value = model.CardCategoryId;                        
            parameters[2].Value = model.Code;                        
            parameters[3].Value = model.CreateDate;                        
            parameters[4].Value = model.StartDate;                        
            parameters[5].Value = model.EndDate;                        
            int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int CardId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from " + databaseprefix + "Card ");
			strSql.Append(" where CardId=@CardId");
						SqlParameter[] parameters = {
					new SqlParameter("@CardId", SqlDbType.Int,4)
			};
			parameters[0].Value = CardId;


			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
				/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string CardIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from " + databaseprefix + "Card ");
			strSql.Append(" where ID in ("+CardIdlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
				
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public DTcms.Model.Card GetModel(int CardId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select CardId, CardCategoryId, Code, CreateDate, StartDate, EndDate  ");			
			strSql.Append("  from " + databaseprefix + "Card ");
			strSql.Append(" where CardId=@CardId");
						SqlParameter[] parameters = {
					new SqlParameter("@CardId", SqlDbType.Int,4)
			};
			parameters[0].Value = CardId;

			
			DTcms.Model.Card model=new DTcms.Model.Card();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																				if(ds.Tables[0].Rows[0]["CardId"].ToString()!="")
				{
					model.CardId=int.Parse(ds.Tables[0].Rows[0]["CardId"].ToString());
				}
																																								if(ds.Tables[0].Rows[0]["CardCategoryId"].ToString()!="")
				{
					model.CardCategoryId=int.Parse(ds.Tables[0].Rows[0]["CardCategoryId"].ToString());
				}
																																												model.Code= ds.Tables[0].Rows[0]["Code"].ToString();
																																				if(ds.Tables[0].Rows[0]["CreateDate"].ToString()!="")
				{
					model.CreateDate=DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
				}
																																								if(ds.Tables[0].Rows[0]["StartDate"].ToString()!="")
				{
					model.StartDate=DateTime.Parse(ds.Tables[0].Rows[0]["StartDate"].ToString());
				}
																																								if(ds.Tables[0].Rows[0]["EndDate"].ToString()!="")
				{
					model.EndDate=DateTime.Parse(ds.Tables[0].Rows[0]["EndDate"].ToString());
				}
																														
				return model;
			}
			else
			{
				return null;
			}
		}
		
		DTcms.Model.Card GetModel(string CallIndex)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select CardId, CardCategoryId, Code, CreateDate, StartDate, EndDate  ");			
			strSql.Append("  from " + databaseprefix + "Card ");
			strSql.Append(" where CallIndex=@CallIndex");
			SqlParameter[] parameters = {
                    new SqlParameter("@CallIndex", SqlDbType.Int,4)
            };
            parameters[0].Value = CallIndex;
			
			DTcms.Model.Card model=new DTcms.Model.Card();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																				if(ds.Tables[0].Rows[0]["CardId"].ToString()!="")
				{
					model.CardId=int.Parse(ds.Tables[0].Rows[0]["CardId"].ToString());
				}
																																								if(ds.Tables[0].Rows[0]["CardCategoryId"].ToString()!="")
				{
					model.CardCategoryId=int.Parse(ds.Tables[0].Rows[0]["CardCategoryId"].ToString());
				}
																																												model.Code= ds.Tables[0].Rows[0]["Code"].ToString();
																																				if(ds.Tables[0].Rows[0]["CreateDate"].ToString()!="")
				{
					model.CreateDate=DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
				}
																																								if(ds.Tables[0].Rows[0]["StartDate"].ToString()!="")
				{
					model.StartDate=DateTime.Parse(ds.Tables[0].Rows[0]["StartDate"].ToString());
				}
																																								if(ds.Tables[0].Rows[0]["EndDate"].ToString()!="")
				{
					model.EndDate=DateTime.Parse(ds.Tables[0].Rows[0]["EndDate"].ToString());
				}
																														
				return model;
			}
			else
			{
				return null;
			}
		}
		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM " + databaseprefix + "Card ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}
		
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM " + databaseprefix + "Card ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}
		
				
		/// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + "Card ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
	#endregion
   
	}
}

