using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using DTcms.DBUtility;
using DTcms.Common;
namespace DTcms.DAL  
{
	 	//用户课程历史
		public partial class UserCurriculum
	{
		private string databaseprefix; //数据库表名前缀
        public UserCurriculum(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }
   		#region 基本方法================================
   		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int UserCurriculumId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from "+ databaseprefix +"UserCurriculum");
			strSql.Append(" where ");
			                                       strSql.Append(" UserCurriculumId = @UserCurriculumId  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@UserCurriculumId", SqlDbType.Int,4)
			};
			parameters[0].Value = UserCurriculumId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
		
		/// <summary>
        /// 返回数据总数
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from " + databaseprefix + "UserCurriculum");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(DTcms.Model.UserCurriculum model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into " + databaseprefix + "UserCurriculum(");			
            strSql.Append("CurriculumId,UserId,CurriculumItemId,CreateDate");
			strSql.Append(") values (");
            strSql.Append("@CurriculumId,@UserId,@CurriculumItemId,@CreateDate");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@CurriculumId", SqlDbType.Int,4) ,            
                        new SqlParameter("@UserId", SqlDbType.Int,4) ,            
                        new SqlParameter("@CurriculumItemId", SqlDbType.Int,4) ,            
                        new SqlParameter("@CreateDate", SqlDbType.DateTime)             
              
            };
			            
            parameters[0].Value = model.CurriculumId;                        
            parameters[1].Value = model.UserId;                        
            parameters[2].Value = model.CurriculumItemId;                        
            parameters[3].Value = model.CreateDate;                        
			   
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
		public bool Update(DTcms.Model.UserCurriculum model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update " + databaseprefix + "UserCurriculum set ");
			                                                
            strSql.Append(" CurriculumId = @CurriculumId , ");                                    
            strSql.Append(" UserId = @UserId , ");                                    
            strSql.Append(" CurriculumItemId = @CurriculumItemId , ");                                    
            strSql.Append(" CreateDate = @CreateDate  ");            			
			strSql.Append(" where UserCurriculumId=@UserCurriculumId ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@UserCurriculumId", SqlDbType.Int,4) ,            
                        new SqlParameter("@CurriculumId", SqlDbType.Int,4) ,            
                        new SqlParameter("@UserId", SqlDbType.Int,4) ,            
                        new SqlParameter("@CurriculumItemId", SqlDbType.Int,4) ,            
                        new SqlParameter("@CreateDate", SqlDbType.DateTime)             
              
            };
						            
            parameters[0].Value = model.UserCurriculumId;                        
            parameters[1].Value = model.CurriculumId;                        
            parameters[2].Value = model.UserId;                        
            parameters[3].Value = model.CurriculumItemId;                        
            parameters[4].Value = model.CreateDate;                        
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
		public bool Delete(int UserCurriculumId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from " + databaseprefix + "UserCurriculum ");
			strSql.Append(" where UserCurriculumId=@UserCurriculumId");
						SqlParameter[] parameters = {
					new SqlParameter("@UserCurriculumId", SqlDbType.Int,4)
			};
			parameters[0].Value = UserCurriculumId;


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
		public bool DeleteList(string UserCurriculumIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from " + databaseprefix + "UserCurriculum ");
			strSql.Append(" where ID in ("+UserCurriculumIdlist + ")  ");
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
		public DTcms.Model.UserCurriculum GetModel(int UserCurriculumId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select UserCurriculumId, CurriculumId, UserId, CurriculumItemId, CreateDate  ");			
			strSql.Append("  from UserCurriculum ");
			strSql.Append(" where UserCurriculumId=@UserCurriculumId");
						SqlParameter[] parameters = {
					new SqlParameter("@UserCurriculumId", SqlDbType.Int,4)
			};
			parameters[0].Value = UserCurriculumId;

			
			DTcms.Model.UserCurriculum model=new DTcms.Model.UserCurriculum();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["UserCurriculumId"].ToString()!="")
				{
					model.UserCurriculumId=int.Parse(ds.Tables[0].Rows[0]["UserCurriculumId"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["CurriculumId"].ToString()!="")
				{
					model.CurriculumId=int.Parse(ds.Tables[0].Rows[0]["CurriculumId"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["UserId"].ToString()!="")
				{
					model.UserId=int.Parse(ds.Tables[0].Rows[0]["UserId"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["CurriculumItemId"].ToString()!="")
				{
					model.CurriculumItemId=int.Parse(ds.Tables[0].Rows[0]["CurriculumItemId"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["CreateDate"].ToString()!="")
				{
					model.CreateDate=DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
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
			strSql.Append(" FROM UserCurriculum ");
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
			strSql.Append(" FROM UserCurriculum ");
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
            strSql.Append("select * FROM " + databaseprefix + "UserCurriculum ");
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

