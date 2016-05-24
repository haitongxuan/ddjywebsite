using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using DTcms.DBUtility;
using DTcms.Common;
namespace DTcms.DAL  
{
	 	//媒体类别关系表
		public partial class MediaRelationship
	{
		private string databaseprefix; //数据库表名前缀
        public MediaRelationship(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }
   		#region 基本方法================================
   		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int MediaCategoryRelationshipId,int MediaId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from "+ databaseprefix +"MediaRelationship");
			strSql.Append(" where ");
			                                       strSql.Append(" MediaCategoryRelationshipId = @MediaCategoryRelationshipId and  ");
                                                                   strSql.Append(" MediaId = @MediaId  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@MediaCategoryRelationshipId", SqlDbType.Int,4),
					new SqlParameter("@MediaId", SqlDbType.Int,4)			};
			parameters[0].Value = MediaCategoryRelationshipId;
			parameters[1].Value = MediaId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
		
		/// <summary>
        /// 返回数据总数
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from " + databaseprefix + "MediaRelationship");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(DTcms.Model.MediaRelationship model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into " + databaseprefix + "MediaRelationship(");			
            strSql.Append("MediaId,MediaRelationshipCategoryId");
			strSql.Append(") values (");
            strSql.Append("@MediaId,@MediaRelationshipCategoryId");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@MediaId", SqlDbType.Int,4) ,            
                        new SqlParameter("@MediaRelationshipCategoryId", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.MediaId;                        
            parameters[1].Value = model.MediaRelationshipCategoryId;                        
			   
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
		public bool Update(DTcms.Model.MediaRelationship model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update " + databaseprefix + "MediaRelationship set ");
			                                                
            strSql.Append(" MediaId = @MediaId , ");                                    
            strSql.Append(" MediaRelationshipCategoryId = @MediaRelationshipCategoryId  ");            			
			strSql.Append(" where MediaCategoryRelationshipId=@MediaCategoryRelationshipId ");
						
			SqlParameter[] parameters = {
			            new SqlParameter("@MediaCategoryRelationshipId", SqlDbType.Int,4) ,            
                        new SqlParameter("@MediaId", SqlDbType.Int,4) ,            
                        new SqlParameter("@MediaRelationshipCategoryId", SqlDbType.Int,4)             
              
            };
						            
            parameters[0].Value = model.MediaCategoryRelationshipId;                        
            parameters[1].Value = model.MediaId;                        
            parameters[2].Value = model.MediaRelationshipCategoryId;                        
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
		public bool Delete(int MediaCategoryRelationshipId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from " + databaseprefix + "MediaRelationship ");
			strSql.Append(" where MediaCategoryRelationshipId=@MediaCategoryRelationshipId");
						SqlParameter[] parameters = {
					new SqlParameter("@MediaCategoryRelationshipId", SqlDbType.Int,4)
			};
			parameters[0].Value = MediaCategoryRelationshipId;


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
		public bool DeleteList(string MediaCategoryRelationshipIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from " + databaseprefix + "MediaRelationship ");
			strSql.Append(" where ID in ("+MediaCategoryRelationshipIdlist + ")  ");
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
		public DTcms.Model.MediaRelationship GetModel(int MediaCategoryRelationshipId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select MediaCategoryRelationshipId, MediaId, MediaRelationshipCategoryId  ");			
			strSql.Append("  from " + databaseprefix + "MediaRelationship ");
			strSql.Append(" where MediaCategoryRelationshipId=@MediaCategoryRelationshipId");
						SqlParameter[] parameters = {
					new SqlParameter("@MediaCategoryRelationshipId", SqlDbType.Int,4)
			};
			parameters[0].Value = MediaCategoryRelationshipId;

			
			DTcms.Model.MediaRelationship model=new DTcms.Model.MediaRelationship();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																if(ds.Tables[0].Rows[0]["MediaCategoryRelationshipId"].ToString()!="")
				{
					model.MediaCategoryRelationshipId=int.Parse(ds.Tables[0].Rows[0]["MediaCategoryRelationshipId"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["MediaId"].ToString()!="")
				{
					model.MediaId=int.Parse(ds.Tables[0].Rows[0]["MediaId"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["MediaRelationshipCategoryId"].ToString()!="")
				{
					model.MediaRelationshipCategoryId=int.Parse(ds.Tables[0].Rows[0]["MediaRelationshipCategoryId"].ToString());
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
			strSql.Append(" FROM " + databaseprefix + "MediaRelationship ");
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
			strSql.Append(" FROM " + databaseprefix + "MediaRelationship ");
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
            strSql.Append("select * FROM " + databaseprefix + "MediaRelationship ");
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

