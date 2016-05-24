using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using DTcms.DBUtility;
using DTcms.Common;
namespace DTcms.DAL  
{
	 	//媒体类别表
		public partial class MediaCategory
	{
		private string databaseprefix; //数据库表名前缀
        public MediaCategory(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }
   		#region 基本方法================================
   		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int MediaCategoryId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from "+ databaseprefix +"MediaCategory");
			strSql.Append(" where ");
			                                       strSql.Append(" MediaCategoryId = @MediaCategoryId  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@MediaCategoryId", SqlDbType.Int,4)
			};
			parameters[0].Value = MediaCategoryId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
		
		/// <summary>
        /// 返回数据总数
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from " + databaseprefix + "MediaCategory");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(DTcms.Model.MediaCategory model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into " + databaseprefix + "MediaCategory(");			
            strSql.Append("FullName,CallIndex,Layer,ParentId,Remark,DeleteMark,Enable,CreateDate,CreateUserId,CreateUserName,ModifyDate,ModifyUserId,ModifyUserName");
			strSql.Append(") values (");
            strSql.Append("@FullName,@CallIndex,@Layer,@ParentId,@Remark,@DeleteMark,@Enable,@CreateDate,@CreateUserId,@CreateUserName,@ModifyDate,@ModifyUserId,@ModifyUserName");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@FullName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@CallIndex", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Layer", SqlDbType.Int,4) ,            
                        new SqlParameter("@ParentId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@DeleteMark", SqlDbType.Int,4) ,            
                        new SqlParameter("@Enable", SqlDbType.Int,4) ,            
                        new SqlParameter("@CreateDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@CreateUserId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@CreateUserName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ModifyDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@ModifyUserId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ModifyUserName", SqlDbType.VarChar,50)             
              
            };
			            
            parameters[0].Value = model.FullName;                        
            parameters[1].Value = model.CallIndex;                        
            parameters[2].Value = model.Layer;                        
            parameters[3].Value = model.ParentId;                        
            parameters[4].Value = model.Remark;                        
            parameters[5].Value = model.DeleteMark;                        
            parameters[6].Value = model.Enable;                        
            parameters[7].Value = model.CreateDate;                        
            parameters[8].Value = model.CreateUserId;                        
            parameters[9].Value = model.CreateUserName;                        
            parameters[10].Value = model.ModifyDate;                        
            parameters[11].Value = model.ModifyUserId;                        
            parameters[12].Value = model.ModifyUserName;                        
			   
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
		public bool Update(DTcms.Model.MediaCategory model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update " + databaseprefix + "MediaCategory set ");
			                                                
            strSql.Append(" FullName = @FullName , ");                                    
            strSql.Append(" CallIndex = @CallIndex , ");                                    
            strSql.Append(" Layer = @Layer , ");                                    
            strSql.Append(" ParentId = @ParentId , ");                                    
            strSql.Append(" Remark = @Remark , ");                                    
            strSql.Append(" DeleteMark = @DeleteMark , ");                                    
            strSql.Append(" Enable = @Enable , ");                                    
            strSql.Append(" CreateDate = @CreateDate , ");                                    
            strSql.Append(" CreateUserId = @CreateUserId , ");                                    
            strSql.Append(" CreateUserName = @CreateUserName , ");                                    
            strSql.Append(" ModifyDate = @ModifyDate , ");                                    
            strSql.Append(" ModifyUserId = @ModifyUserId , ");                                    
            strSql.Append(" ModifyUserName = @ModifyUserName  ");            			
			strSql.Append(" where MediaCategoryId=@MediaCategoryId ");
						
			SqlParameter[] parameters = {
			            new SqlParameter("@MediaCategoryId", SqlDbType.Int,4) ,            
                        new SqlParameter("@FullName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@CallIndex", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Layer", SqlDbType.Int,4) ,            
                        new SqlParameter("@ParentId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@DeleteMark", SqlDbType.Int,4) ,            
                        new SqlParameter("@Enable", SqlDbType.Int,4) ,            
                        new SqlParameter("@CreateDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@CreateUserId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@CreateUserName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ModifyDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@ModifyUserId", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ModifyUserName", SqlDbType.VarChar,50)             
              
            };
						            
            parameters[0].Value = model.MediaCategoryId;                        
            parameters[1].Value = model.FullName;                        
            parameters[2].Value = model.CallIndex;                        
            parameters[3].Value = model.Layer;                        
            parameters[4].Value = model.ParentId;                        
            parameters[5].Value = model.Remark;                        
            parameters[6].Value = model.DeleteMark;                        
            parameters[7].Value = model.Enable;                        
            parameters[8].Value = model.CreateDate;                        
            parameters[9].Value = model.CreateUserId;                        
            parameters[10].Value = model.CreateUserName;                        
            parameters[11].Value = model.ModifyDate;                        
            parameters[12].Value = model.ModifyUserId;                        
            parameters[13].Value = model.ModifyUserName;                        
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
		public bool Delete(int MediaCategoryId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from " + databaseprefix + "MediaCategory ");
			strSql.Append(" where MediaCategoryId=@MediaCategoryId");
						SqlParameter[] parameters = {
					new SqlParameter("@MediaCategoryId", SqlDbType.Int,4)
			};
			parameters[0].Value = MediaCategoryId;


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
		public bool DeleteList(string MediaCategoryIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from " + databaseprefix + "MediaCategory ");
			strSql.Append(" where ID in ("+MediaCategoryIdlist + ")  ");
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
		public DTcms.Model.MediaCategory GetModel(int MediaCategoryId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select MediaCategoryId, FullName, CallIndex, Layer, ParentId, Remark, DeleteMark, Enable, CreateDate, CreateUserId, CreateUserName, ModifyDate, ModifyUserId, ModifyUserName  ");			
			strSql.Append("  from " + databaseprefix + "MediaCategory ");
			strSql.Append(" where MediaCategoryId=@MediaCategoryId");
						SqlParameter[] parameters = {
					new SqlParameter("@MediaCategoryId", SqlDbType.Int,4)
			};
			parameters[0].Value = MediaCategoryId;

			
			DTcms.Model.MediaCategory model=new DTcms.Model.MediaCategory();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																if(ds.Tables[0].Rows[0]["MediaCategoryId"].ToString()!="")
				{
					model.MediaCategoryId=int.Parse(ds.Tables[0].Rows[0]["MediaCategoryId"].ToString());
				}
																																								model.FullName= ds.Tables[0].Rows[0]["FullName"].ToString();
																																				model.CallIndex= ds.Tables[0].Rows[0]["CallIndex"].ToString();
																																if(ds.Tables[0].Rows[0]["Layer"].ToString()!="")
				{
					model.Layer=int.Parse(ds.Tables[0].Rows[0]["Layer"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["ParentId"].ToString()!="")
				{
					model.ParentId=int.Parse(ds.Tables[0].Rows[0]["ParentId"].ToString());
				}
																																								model.Remark= ds.Tables[0].Rows[0]["Remark"].ToString();
																																if(ds.Tables[0].Rows[0]["DeleteMark"].ToString()!="")
				{
					model.DeleteMark=int.Parse(ds.Tables[0].Rows[0]["DeleteMark"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["Enable"].ToString()!="")
				{
					model.Enable=int.Parse(ds.Tables[0].Rows[0]["Enable"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["CreateDate"].ToString()!="")
				{
					model.CreateDate=DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
				}
																																								model.CreateUserId= ds.Tables[0].Rows[0]["CreateUserId"].ToString();
																																				model.CreateUserName= ds.Tables[0].Rows[0]["CreateUserName"].ToString();
																																if(ds.Tables[0].Rows[0]["ModifyDate"].ToString()!="")
				{
					model.ModifyDate=DateTime.Parse(ds.Tables[0].Rows[0]["ModifyDate"].ToString());
				}
																																								model.ModifyUserId= ds.Tables[0].Rows[0]["ModifyUserId"].ToString();
																																				model.ModifyUserName= ds.Tables[0].Rows[0]["ModifyUserName"].ToString();
																										
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
			strSql.Append(" FROM " + databaseprefix + "MediaCategory ");
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
			strSql.Append(" FROM " + databaseprefix + "MediaCategory ");
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
            strSql.Append(" FROM " + databaseprefix + "MediaCategory order by MediaCategoryId desc");
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
                               				if(dr[i]["MediaCategoryId"].ToString()!="")
				{
					row["MediaCategoryId"]=int.Parse(dr[i]["MediaCategoryId"].ToString());
				}
																				                               								row["FullName"]= dr[i]["FullName"].ToString();
																                               								row["CallIndex"]= dr[i]["CallIndex"].ToString();
																                               				if(dr[i]["Layer"].ToString()!="")
				{
					row["Layer"]=int.Parse(dr[i]["Layer"].ToString());
				}
																				                               				if(dr[i]["ParentId"].ToString()!="")
				{
					row["ParentId"]=int.Parse(dr[i]["ParentId"].ToString());
				}
																				                               								row["Remark"]= dr[i]["Remark"].ToString();
																                               				if(dr[i]["DeleteMark"].ToString()!="")
				{
					row["DeleteMark"]=int.Parse(dr[i]["DeleteMark"].ToString());
				}
																				                               				if(dr[i]["Enable"].ToString()!="")
				{
					row["Enable"]=int.Parse(dr[i]["Enable"].ToString());
				}
																				                               				if(dr[i]["CreateDate"].ToString()!="")
				{
					row["CreateDate"]=DateTime.Parse(dr[i]["CreateDate"].ToString());
				}
																				                               								row["CreateUserId"]= dr[i]["CreateUserId"].ToString();
																                               								row["CreateUserName"]= dr[i]["CreateUserName"].ToString();
																                               				if(dr[i]["ModifyDate"].ToString()!="")
				{
					row["ModifyDate"]=DateTime.Parse(dr[i]["ModifyDate"].ToString());
				}
																				                               								row["ModifyUserId"]= dr[i]["ModifyUserId"].ToString();
																                               								row["ModifyUserName"]= dr[i]["ModifyUserName"].ToString();
																                                newData.Rows.Add(row);
                //调用自身迭代
                this.GetChilds(oldData, newData, int.Parse(dr[i]["MediaCategoryId"].ToString()));
            }
        }
				
		/// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + "MediaCategory ");
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

