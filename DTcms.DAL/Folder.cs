using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using DTcms.DBUtility;
using DTcms.Common;
namespace DTcms.DAL  
{
	 	//文件夹
		public partial class Folder
	{
		private string databaseprefix; //数据库表名前缀
        public Folder(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }
   		#region 基本方法================================
   		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int FolderId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from "+ databaseprefix +"Folder");
			strSql.Append(" where ");
			                                       strSql.Append(" FolderId = @FolderId  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@FolderId", SqlDbType.Int,4)
			};
			parameters[0].Value = FolderId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
		
		/// <summary>
        /// 返回数据总数
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from " + databaseprefix + "Folder");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(DTcms.Model.Folder model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into " + databaseprefix + "Folder(");			
            strSql.Append("ParentId,Layer,FullName,DeleteMark,Enable");
			strSql.Append(") values (");
            strSql.Append("@ParentId,@Layer,@FullName,@DeleteMark,@Enable");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@ParentId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Layer", SqlDbType.Int,4) ,            
                        new SqlParameter("@FullName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@DeleteMark", SqlDbType.Int,4) ,            
                        new SqlParameter("@Enable", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.ParentId;                        
            parameters[1].Value = model.Layer;                        
            parameters[2].Value = model.FullName;                        
            parameters[3].Value = model.DeleteMark;                        
            parameters[4].Value = model.Enable;                        
			   
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
		public bool Update(DTcms.Model.Folder model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update " + databaseprefix + "Folder set ");
			                                                
            strSql.Append(" ParentId = @ParentId , ");                                    
            strSql.Append(" Layer = @Layer , ");                                    
            strSql.Append(" FullName = @FullName , ");                                    
            strSql.Append(" DeleteMark = @DeleteMark , ");                                    
            strSql.Append(" Enable = @Enable  ");            			
			strSql.Append(" where FolderId=@FolderId ");
						
			SqlParameter[] parameters = {
			            new SqlParameter("@FolderId", SqlDbType.Int,4) ,            
                        new SqlParameter("@ParentId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Layer", SqlDbType.Int,4) ,            
                        new SqlParameter("@FullName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@DeleteMark", SqlDbType.Int,4) ,            
                        new SqlParameter("@Enable", SqlDbType.Int,4)             
              
            };
						            
            parameters[0].Value = model.FolderId;                        
            parameters[1].Value = model.ParentId;                        
            parameters[2].Value = model.Layer;                        
            parameters[3].Value = model.FullName;                        
            parameters[4].Value = model.DeleteMark;                        
            parameters[5].Value = model.Enable;                        
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
		public bool Delete(int FolderId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from " + databaseprefix + "Folder ");
			strSql.Append(" where FolderId=@FolderId");
						SqlParameter[] parameters = {
					new SqlParameter("@FolderId", SqlDbType.Int,4)
			};
			parameters[0].Value = FolderId;


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
		public bool DeleteList(string FolderIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from " + databaseprefix + "Folder ");
			strSql.Append(" where ID in ("+FolderIdlist + ")  ");
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
		public DTcms.Model.Folder GetModel(int FolderId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select FolderId, ParentId, Layer, FullName, DeleteMark, Enable  ");			
			strSql.Append("  from " + databaseprefix + "Folder ");
			strSql.Append(" where FolderId=@FolderId");
						SqlParameter[] parameters = {
					new SqlParameter("@FolderId", SqlDbType.Int,4)
			};
			parameters[0].Value = FolderId;

			
			DTcms.Model.Folder model=new DTcms.Model.Folder();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																if(ds.Tables[0].Rows[0]["FolderId"].ToString()!="")
				{
					model.FolderId=int.Parse(ds.Tables[0].Rows[0]["FolderId"].ToString());
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
			strSql.Append(" FROM " + databaseprefix + "Folder ");
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
			strSql.Append(" FROM " + databaseprefix + "Folder ");
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
            strSql.Append(" FROM " + databaseprefix + "Folder order by FolderId desc");
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
                               				if(dr[i]["FolderId"].ToString()!="")
				{
					row["FolderId"]=int.Parse(dr[i]["FolderId"].ToString());
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
                this.GetChilds(oldData, newData, int.Parse(dr[i]["FolderId"].ToString()));
            }
        }
				
		/// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + "Folder ");
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

