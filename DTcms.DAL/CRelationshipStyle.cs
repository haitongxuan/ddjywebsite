using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using DTcms.DBUtility;
using DTcms.Common;
namespace DTcms.DAL  
{
	 	//课程关系类型
		public partial class CRelationshipStyle
	{
		private string databaseprefix; //数据库表名前缀
        public CRelationshipStyle(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }
   		#region 基本方法================================
   		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int CRelationshipStyleId,int SourceCurricularStyleId,int TargetCurricularStyleId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from "+ databaseprefix +"CRelationshipStyle");
			strSql.Append(" where ");
			                                       strSql.Append(" CRelationshipStyleId = @CRelationshipStyleId and  ");
                                                                   strSql.Append(" SourceCurricularStyleId = @SourceCurricularStyleId and  ");
                                                                   strSql.Append(" TargetCurricularStyleId = @TargetCurricularStyleId  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@CRelationshipStyleId", SqlDbType.Int,4),
					new SqlParameter("@SourceCurricularStyleId", SqlDbType.Int,4),
					new SqlParameter("@TargetCurricularStyleId", SqlDbType.Int,4)			};
			parameters[0].Value = CRelationshipStyleId;
			parameters[1].Value = SourceCurricularStyleId;
			parameters[2].Value = TargetCurricularStyleId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
		
		/// <summary>
        /// 返回数据总数
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from " + databaseprefix + "CRelationshipStyle");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(DTcms.Model.CRelationshipStyle model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into " + databaseprefix + "CRelationshipStyle(");			
            strSql.Append("ParentId,Layer,FullName,Remark,SourceCurricularStyleId,TargetCurricularStyleId,DeleteMark,Enable");
			strSql.Append(") values (");
            strSql.Append("@ParentId,@Layer,@FullName,@Remark,@SourceCurricularStyleId,@TargetCurricularStyleId,@DeleteMark,@Enable");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@ParentId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Layer", SqlDbType.Int,4) ,            
                        new SqlParameter("@FullName", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@SourceCurricularStyleId", SqlDbType.Int,4) ,            
                        new SqlParameter("@TargetCurricularStyleId", SqlDbType.Int,4) ,            
                        new SqlParameter("@DeleteMark", SqlDbType.Int,4) ,            
                        new SqlParameter("@Enable", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.ParentId;                        
            parameters[1].Value = model.Layer;                        
            parameters[2].Value = model.FullName;                        
            parameters[3].Value = model.Remark;                        
            parameters[4].Value = model.SourceCurricularStyleId;                        
            parameters[5].Value = model.TargetCurricularStyleId;                        
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
		public bool Update(DTcms.Model.CRelationshipStyle model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update " + databaseprefix + "CRelationshipStyle set ");
			                                                
            strSql.Append(" ParentId = @ParentId , ");                                    
            strSql.Append(" Layer = @Layer , ");                                    
            strSql.Append(" FullName = @FullName , ");                                    
            strSql.Append(" Remark = @Remark , ");                                    
            strSql.Append(" SourceCurricularStyleId = @SourceCurricularStyleId , ");                                    
            strSql.Append(" TargetCurricularStyleId = @TargetCurricularStyleId , ");                                    
            strSql.Append(" DeleteMark = @DeleteMark , ");                                    
            strSql.Append(" Enable = @Enable  ");            			
			strSql.Append(" where CRelationshipStyleId=@CRelationshipStyleId ");
						
			SqlParameter[] parameters = {
			            new SqlParameter("@CRelationshipStyleId", SqlDbType.Int,4) ,            
                        new SqlParameter("@ParentId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Layer", SqlDbType.Int,4) ,            
                        new SqlParameter("@FullName", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@SourceCurricularStyleId", SqlDbType.Int,4) ,            
                        new SqlParameter("@TargetCurricularStyleId", SqlDbType.Int,4) ,            
                        new SqlParameter("@DeleteMark", SqlDbType.Int,4) ,            
                        new SqlParameter("@Enable", SqlDbType.Int,4)             
              
            };
						            
            parameters[0].Value = model.CRelationshipStyleId;                        
            parameters[1].Value = model.ParentId;                        
            parameters[2].Value = model.Layer;                        
            parameters[3].Value = model.FullName;                        
            parameters[4].Value = model.Remark;                        
            parameters[5].Value = model.SourceCurricularStyleId;                        
            parameters[6].Value = model.TargetCurricularStyleId;                        
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
		public bool Delete(int CRelationshipStyleId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from " + databaseprefix + "CRelationshipStyle ");
			strSql.Append(" where CRelationshipStyleId=@CRelationshipStyleId");
						SqlParameter[] parameters = {
					new SqlParameter("@CRelationshipStyleId", SqlDbType.Int,4)
			};
			parameters[0].Value = CRelationshipStyleId;


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
		public bool DeleteList(string CRelationshipStyleIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from " + databaseprefix + "CRelationshipStyle ");
			strSql.Append(" where ID in ("+CRelationshipStyleIdlist + ")  ");
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
		public DTcms.Model.CRelationshipStyle GetModel(int CRelationshipStyleId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select CRelationshipStyleId, ParentId, Layer, FullName, Remark, SourceCurricularStyleId, TargetCurricularStyleId, DeleteMark, Enable  ");			
			strSql.Append("  from " + databaseprefix + "CRelationshipStyle ");
			strSql.Append(" where CRelationshipStyleId=@CRelationshipStyleId");
						SqlParameter[] parameters = {
					new SqlParameter("@CRelationshipStyleId", SqlDbType.Int,4)
			};
			parameters[0].Value = CRelationshipStyleId;

			
			DTcms.Model.CRelationshipStyle model=new DTcms.Model.CRelationshipStyle();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																if(ds.Tables[0].Rows[0]["CRelationshipStyleId"].ToString()!="")
				{
					model.CRelationshipStyleId=int.Parse(ds.Tables[0].Rows[0]["CRelationshipStyleId"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["ParentId"].ToString()!="")
				{
					model.ParentId=int.Parse(ds.Tables[0].Rows[0]["ParentId"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["Layer"].ToString()!="")
				{
					model.Layer=int.Parse(ds.Tables[0].Rows[0]["Layer"].ToString());
				}
																																								model.FullName= ds.Tables[0].Rows[0]["FullName"].ToString();
																																				model.Remark= ds.Tables[0].Rows[0]["Remark"].ToString();
																																if(ds.Tables[0].Rows[0]["SourceCurricularStyleId"].ToString()!="")
				{
					model.SourceCurricularStyleId=int.Parse(ds.Tables[0].Rows[0]["SourceCurricularStyleId"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["TargetCurricularStyleId"].ToString()!="")
				{
					model.TargetCurricularStyleId=int.Parse(ds.Tables[0].Rows[0]["TargetCurricularStyleId"].ToString());
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
			strSql.Append(" FROM " + databaseprefix + "CRelationshipStyle ");
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
			strSql.Append(" FROM " + databaseprefix + "CRelationshipStyle ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}
		
				/// <summary>
        /// 取得所有类别列表
        /// </summary>
		public DataTable GetList(int parentId)
		{
			StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM " + databaseprefix + "CRelationshipStyle order by CRelationshipStyleId desc");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            DataTable oldData = ds.Tables[0] as DataTable;
            if (oldData == null)
            {
                return null;
            }
            //复制结构
            DataTable newData = oldData.Clone();
            //调用迭代组合成DAGATABLE
            GetChilds(oldData, newData, parentId);
            return newData;
		}
		
		/// <summary>
        /// 从内存中取得所有下级类别列表（自身迭代）
        /// </summary>
        private void GetChilds(DataTable oldData, DataTable newData, int parent_id)
        {
        	DataRow[] dr = oldData.Select("ParentId=" + parent_id);
        	for (int i = 0; i < dr.Length; i++)
            {
            	//添加一行数据
                DataRow row = newData.NewRow();
                               				if(dr[i]["CRelationshipStyleId"].ToString()!="")
				{
					row["CRelationshipStyleId"]=int.Parse(dr[i]["CRelationshipStyleId"].ToString());
				}
																				                               				if(dr[i]["ParentId"].ToString()!="")
				{
					row["ParentId"]=int.Parse(dr[i]["ParentId"].ToString());
				}
																				                               				if(dr[i]["Layer"].ToString()!="")
				{
					row["Layer"]=int.Parse(dr[i]["Layer"].ToString());
				}
																				                               								row["FullName"]= dr[i]["FullName"].ToString();
																                               								row["Remark"]= dr[i]["Remark"].ToString();
																                               				if(dr[i]["SourceCurricularStyleId"].ToString()!="")
				{
					row["SourceCurricularStyleId"]=int.Parse(dr[i]["SourceCurricularStyleId"].ToString());
				}
																				                               				if(dr[i]["TargetCurricularStyleId"].ToString()!="")
				{
					row["TargetCurricularStyleId"]=int.Parse(dr[i]["TargetCurricularStyleId"].ToString());
				}
																				                               				if(dr[i]["DeleteMark"].ToString()!="")
				{
					row["DeleteMark"]=int.Parse(dr[i]["DeleteMark"].ToString());
				}
																				                               				if(dr[i]["Enable"].ToString()!="")
				{
					row["Enable"]=int.Parse(dr[i]["Enable"].ToString());
				}
																				                                newData.Rows.Add(row);
                //调用自身迭代
                this.GetChilds(oldData, newData, int.Parse(dr[i]["CRelationshipStyleId"].ToString()));
            }
        }
				
		/// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + "CRelationshipStyle ");
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

