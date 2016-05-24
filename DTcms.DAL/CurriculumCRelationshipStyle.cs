using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using DTcms.DBUtility;
using DTcms.Common;
namespace DTcms.DAL  
{
	 	//课程关系
		public partial class CurriculumCRelationshipStyle
	{
		private string databaseprefix; //数据库表名前缀
        public CurriculumCRelationshipStyle(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }
   		#region 基本方法================================
   		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int CurriculumCRelationshipStyleId,int SourceCurricularStyleCurriculumId,int TargetCurricularStyleCurriculumId,int CRelationshipStyleId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from "+ databaseprefix +"CurriculumCRelationshipStyle");
			strSql.Append(" where ");
			                                       strSql.Append(" CurriculumCRelationshipStyleId = @CurriculumCRelationshipStyleId and  ");
                                                                   strSql.Append(" SourceCurricularStyleCurriculumId = @SourceCurricularStyleCurriculumId and  ");
                                                                   strSql.Append(" TargetCurricularStyleCurriculumId = @TargetCurricularStyleCurriculumId and  ");
                                                                   strSql.Append(" CRelationshipStyleId = @CRelationshipStyleId  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@CurriculumCRelationshipStyleId", SqlDbType.Int,4),
					new SqlParameter("@SourceCurricularStyleCurriculumId", SqlDbType.Int,4),
					new SqlParameter("@TargetCurricularStyleCurriculumId", SqlDbType.Int,4),
					new SqlParameter("@CRelationshipStyleId", SqlDbType.Int,4)			};
			parameters[0].Value = CurriculumCRelationshipStyleId;
			parameters[1].Value = SourceCurricularStyleCurriculumId;
			parameters[2].Value = TargetCurricularStyleCurriculumId;
			parameters[3].Value = CRelationshipStyleId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
		
		/// <summary>
        /// 返回数据总数
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from " + databaseprefix + "CurriculumCRelationshipStyle");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(DTcms.Model.CurriculumCRelationshipStyle model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into " + databaseprefix + "CurriculumCRelationshipStyle(");			
            strSql.Append("StartDate,EndDate,Remark,SourceCurricularStyleCurriculumId,TargetCurricularStyleCurriculumId,CRelationshipStyleId,DeleteMark,Enable");
			strSql.Append(") values (");
            strSql.Append("@StartDate,@EndDate,@Remark,@SourceCurricularStyleCurriculumId,@TargetCurricularStyleCurriculumId,@CRelationshipStyleId,@DeleteMark,@Enable");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@StartDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@EndDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@SourceCurricularStyleCurriculumId", SqlDbType.Int,4) ,            
                        new SqlParameter("@TargetCurricularStyleCurriculumId", SqlDbType.Int,4) ,            
                        new SqlParameter("@CRelationshipStyleId", SqlDbType.Int,4) ,            
                        new SqlParameter("@DeleteMark", SqlDbType.Int,4) ,            
                        new SqlParameter("@Enable", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.StartDate;                        
            parameters[1].Value = model.EndDate;                        
            parameters[2].Value = model.Remark;                        
            parameters[3].Value = model.SourceCurricularStyleCurriculumId;                        
            parameters[4].Value = model.TargetCurricularStyleCurriculumId;                        
            parameters[5].Value = model.CRelationshipStyleId;                        
            parameters[6].Value = model.DeleteMark;                        
            parameters[7].Value = model.Enable;                        
			   
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
		public bool Update(DTcms.Model.CurriculumCRelationshipStyle model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update " + databaseprefix + "CurriculumCRelationshipStyle set ");
			                                                
            strSql.Append(" StartDate = @StartDate , ");                                    
            strSql.Append(" EndDate = @EndDate , ");                                    
            strSql.Append(" Remark = @Remark , ");                                    
            strSql.Append(" SourceCurricularStyleCurriculumId = @SourceCurricularStyleCurriculumId , ");                                    
            strSql.Append(" TargetCurricularStyleCurriculumId = @TargetCurricularStyleCurriculumId , ");                                    
            strSql.Append(" CRelationshipStyleId = @CRelationshipStyleId , ");                                    
            strSql.Append(" DeleteMark = @DeleteMark , ");                                    
            strSql.Append(" Enable = @Enable  ");            			
			strSql.Append(" where CurriculumCRelationshipStyleId=@CurriculumCRelationshipStyleId ");
						
			SqlParameter[] parameters = {
			            new SqlParameter("@CurriculumCRelationshipStyleId", SqlDbType.Int,4) ,            
                        new SqlParameter("@StartDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@EndDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@SourceCurricularStyleCurriculumId", SqlDbType.Int,4) ,            
                        new SqlParameter("@TargetCurricularStyleCurriculumId", SqlDbType.Int,4) ,            
                        new SqlParameter("@CRelationshipStyleId", SqlDbType.Int,4) ,            
                        new SqlParameter("@DeleteMark", SqlDbType.Int,4) ,            
                        new SqlParameter("@Enable", SqlDbType.Int,4)             
              
            };
						            
            parameters[0].Value = model.CurriculumCRelationshipStyleId;                        
            parameters[1].Value = model.StartDate;                        
            parameters[2].Value = model.EndDate;                        
            parameters[3].Value = model.Remark;                        
            parameters[4].Value = model.SourceCurricularStyleCurriculumId;                        
            parameters[5].Value = model.TargetCurricularStyleCurriculumId;                        
            parameters[6].Value = model.CRelationshipStyleId;                        
            parameters[7].Value = model.DeleteMark;                        
            parameters[8].Value = model.Enable;                        
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
		public bool Delete(int CurriculumCRelationshipStyleId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from " + databaseprefix + "CurriculumCRelationshipStyle ");
			strSql.Append(" where CurriculumCRelationshipStyleId=@CurriculumCRelationshipStyleId");
						SqlParameter[] parameters = {
					new SqlParameter("@CurriculumCRelationshipStyleId", SqlDbType.Int,4)
			};
			parameters[0].Value = CurriculumCRelationshipStyleId;


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
		public bool DeleteList(string CurriculumCRelationshipStyleIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from " + databaseprefix + "CurriculumCRelationshipStyle ");
			strSql.Append(" where ID in ("+CurriculumCRelationshipStyleIdlist + ")  ");
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
		public DTcms.Model.CurriculumCRelationshipStyle GetModel(int CurriculumCRelationshipStyleId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select CurriculumCRelationshipStyleId, StartDate, EndDate, Remark, SourceCurricularStyleCurriculumId, TargetCurricularStyleCurriculumId, CRelationshipStyleId, DeleteMark, Enable  ");			
			strSql.Append("  from " + databaseprefix + "CurriculumCRelationshipStyle ");
			strSql.Append(" where CurriculumCRelationshipStyleId=@CurriculumCRelationshipStyleId");
						SqlParameter[] parameters = {
					new SqlParameter("@CurriculumCRelationshipStyleId", SqlDbType.Int,4)
			};
			parameters[0].Value = CurriculumCRelationshipStyleId;

			
			DTcms.Model.CurriculumCRelationshipStyle model=new DTcms.Model.CurriculumCRelationshipStyle();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																if(ds.Tables[0].Rows[0]["CurriculumCRelationshipStyleId"].ToString()!="")
				{
					model.CurriculumCRelationshipStyleId=int.Parse(ds.Tables[0].Rows[0]["CurriculumCRelationshipStyleId"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["StartDate"].ToString()!="")
				{
					model.StartDate=DateTime.Parse(ds.Tables[0].Rows[0]["StartDate"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["EndDate"].ToString()!="")
				{
					model.EndDate=DateTime.Parse(ds.Tables[0].Rows[0]["EndDate"].ToString());
				}
																																								model.Remark= ds.Tables[0].Rows[0]["Remark"].ToString();
																																if(ds.Tables[0].Rows[0]["SourceCurricularStyleCurriculumId"].ToString()!="")
				{
					model.SourceCurricularStyleCurriculumId=int.Parse(ds.Tables[0].Rows[0]["SourceCurricularStyleCurriculumId"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["TargetCurricularStyleCurriculumId"].ToString()!="")
				{
					model.TargetCurricularStyleCurriculumId=int.Parse(ds.Tables[0].Rows[0]["TargetCurricularStyleCurriculumId"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["CRelationshipStyleId"].ToString()!="")
				{
					model.CRelationshipStyleId=int.Parse(ds.Tables[0].Rows[0]["CRelationshipStyleId"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["DeleteMark"].ToString()!="")
				{
					model.DeleteMark=int.Parse(ds.Tables[0].Rows[0]["DeleteMark"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["Enable"].ToString()!="")
				{
					model.Enable=int.Parse(ds.Tables[0].Rows[0]["Enable"].ToString());
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
			strSql.Append(" FROM " + databaseprefix + "CurriculumCRelationshipStyle ");
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
			strSql.Append(" FROM " + databaseprefix + "CurriculumCRelationshipStyle ");
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
            strSql.Append("select * FROM " + databaseprefix + "CurriculumCRelationshipStyle ");
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

