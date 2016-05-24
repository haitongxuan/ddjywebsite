using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using DTcms.DBUtility;
using DTcms.Common;
namespace DTcms.DAL  
{
	 	//课程类别关系
		public partial class CurriculumCategory
	{
		private string databaseprefix; //数据库表名前缀
        public CurriculumCategory(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }
   		#region 基本方法================================
   		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int CurriculumCategoryId,int CategoryId,int CurriculumId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from "+ databaseprefix +"CurriculumCategory");
			strSql.Append(" where ");
			                                       strSql.Append(" CurriculumCategoryId = @CurriculumCategoryId and  ");
                                                                   strSql.Append(" CategoryId = @CategoryId and  ");
                                                                   strSql.Append(" CurriculumId = @CurriculumId  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@CurriculumCategoryId", SqlDbType.Int,4),
					new SqlParameter("@CategoryId", SqlDbType.Int,4),
					new SqlParameter("@CurriculumId", SqlDbType.Int,4)			};
			parameters[0].Value = CurriculumCategoryId;
			parameters[1].Value = CategoryId;
			parameters[2].Value = CurriculumId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
		
		/// <summary>
        /// 返回数据总数
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from " + databaseprefix + "CurriculumCategory");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(DTcms.Model.CurriculumCategory model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into " + databaseprefix + "CurriculumCategory(");			
            strSql.Append("CategoryId,CurriculumId");
			strSql.Append(") values (");
            strSql.Append("@CategoryId,@CurriculumId");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@CategoryId", SqlDbType.Int,4) ,            
                        new SqlParameter("@CurriculumId", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.CategoryId;                        
            parameters[1].Value = model.CurriculumId;                        
			   
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
		public bool Update(DTcms.Model.CurriculumCategory model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update " + databaseprefix + "CurriculumCategory set ");
			                                                
            strSql.Append(" CategoryId = @CategoryId , ");                                    
            strSql.Append(" CurriculumId = @CurriculumId  ");            			
			strSql.Append(" where CurriculumCategoryId=@CurriculumCategoryId ");
						
			SqlParameter[] parameters = {
			            new SqlParameter("@CurriculumCategoryId", SqlDbType.Int,4) ,            
                        new SqlParameter("@CategoryId", SqlDbType.Int,4) ,            
                        new SqlParameter("@CurriculumId", SqlDbType.Int,4)             
              
            };
						            
            parameters[0].Value = model.CurriculumCategoryId;                        
            parameters[1].Value = model.CategoryId;                        
            parameters[2].Value = model.CurriculumId;                        
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
		public bool Delete(int CurriculumCategoryId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from " + databaseprefix + "CurriculumCategory ");
			strSql.Append(" where CurriculumCategoryId=@CurriculumCategoryId");
						SqlParameter[] parameters = {
					new SqlParameter("@CurriculumCategoryId", SqlDbType.Int,4)
			};
			parameters[0].Value = CurriculumCategoryId;


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
		public bool DeleteList(string CurriculumCategoryIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from " + databaseprefix + "CurriculumCategory ");
			strSql.Append(" where ID in ("+CurriculumCategoryIdlist + ")  ");
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
		public DTcms.Model.CurriculumCategory GetModel(int CurriculumCategoryId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select CurriculumCategoryId, CategoryId, CurriculumId  ");			
			strSql.Append("  from " + databaseprefix + "CurriculumCategory ");
			strSql.Append(" where CurriculumCategoryId=@CurriculumCategoryId");
						SqlParameter[] parameters = {
					new SqlParameter("@CurriculumCategoryId", SqlDbType.Int,4)
			};
			parameters[0].Value = CurriculumCategoryId;

			
			DTcms.Model.CurriculumCategory model=new DTcms.Model.CurriculumCategory();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																if(ds.Tables[0].Rows[0]["CurriculumCategoryId"].ToString()!="")
				{
					model.CurriculumCategoryId=int.Parse(ds.Tables[0].Rows[0]["CurriculumCategoryId"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["CategoryId"].ToString()!="")
				{
					model.CategoryId=int.Parse(ds.Tables[0].Rows[0]["CategoryId"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["CurriculumId"].ToString()!="")
				{
					model.CurriculumId=int.Parse(ds.Tables[0].Rows[0]["CurriculumId"].ToString());
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
			strSql.Append(" FROM " + databaseprefix + "CurriculumCategory ");
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
			strSql.Append(" FROM " + databaseprefix + "CurriculumCategory ");
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
            strSql.Append("select * FROM " + databaseprefix + "CurriculumCategory ");
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

