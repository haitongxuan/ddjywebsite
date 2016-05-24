using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using DTcms.DBUtility;
using DTcms.Common;
namespace DTcms.DAL  
{
	 	//媒体关系类别
		public partial class MediaRelationshipCategory
	{
		private string databaseprefix; //数据库表名前缀
        public MediaRelationshipCategory(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }
   		#region 基本方法================================
   		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int MediaRelationshipStyleId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from "+ databaseprefix +"MediaRelationshipCategory");
			strSql.Append(" where ");
			                                       strSql.Append(" MediaRelationshipStyleId = @MediaRelationshipStyleId  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@MediaRelationshipStyleId", SqlDbType.Int,4)			};
			parameters[0].Value = MediaRelationshipStyleId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
		
		/// <summary>
        /// 返回数据总数
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from " + databaseprefix + "MediaRelationshipCategory");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(DTcms.Model.MediaRelationshipCategory model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into " + databaseprefix + "MediaRelationshipCategory(");			
            strSql.Append("MediaRelationshipStyleId,FullName,Code,ParentId,Layer,Remark,DeleteMark,Enable");
			strSql.Append(") values (");
            strSql.Append("@MediaRelationshipStyleId,@FullName,@Code,@ParentId,@Layer,@Remark,@DeleteMark,@Enable");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@MediaRelationshipStyleId", SqlDbType.Int,4) ,            
                        new SqlParameter("@FullName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Code", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ParentId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Layer", SqlDbType.Int,4) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@DeleteMark", SqlDbType.Int,4) ,            
                        new SqlParameter("@Enable", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.MediaRelationshipStyleId;                        
            parameters[1].Value = model.FullName;                        
            parameters[2].Value = model.Code;                        
            parameters[3].Value = model.ParentId;                        
            parameters[4].Value = model.Layer;                        
            parameters[5].Value = model.Remark;                        
            parameters[6].Value = model.DeleteMark;                        
            parameters[7].Value = model.Enable;                        
			            DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(DTcms.Model.MediaRelationshipCategory model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update " + databaseprefix + "MediaRelationshipCategory set ");
			                        
            strSql.Append(" MediaRelationshipStyleId = @MediaRelationshipStyleId , ");                                    
            strSql.Append(" FullName = @FullName , ");                                    
            strSql.Append(" Code = @Code , ");                                    
            strSql.Append(" ParentId = @ParentId , ");                                    
            strSql.Append(" Layer = @Layer , ");                                    
            strSql.Append(" Remark = @Remark , ");                                    
            strSql.Append(" DeleteMark = @DeleteMark , ");                                    
            strSql.Append(" Enable = @Enable  ");            			
			strSql.Append(" where MediaRelationshipStyleId=@MediaRelationshipStyleId  ");
						
			SqlParameter[] parameters = {
			            new SqlParameter("@MediaRelationshipStyleId", SqlDbType.Int,4) ,            
                        new SqlParameter("@FullName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Code", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@ParentId", SqlDbType.Int,4) ,            
                        new SqlParameter("@Layer", SqlDbType.Int,4) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,200) ,            
                        new SqlParameter("@DeleteMark", SqlDbType.Int,4) ,            
                        new SqlParameter("@Enable", SqlDbType.Int,4)             
              
            };
						            
            parameters[0].Value = model.MediaRelationshipStyleId;                        
            parameters[1].Value = model.FullName;                        
            parameters[2].Value = model.Code;                        
            parameters[3].Value = model.ParentId;                        
            parameters[4].Value = model.Layer;                        
            parameters[5].Value = model.Remark;                        
            parameters[6].Value = model.DeleteMark;                        
            parameters[7].Value = model.Enable;                        
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
		public bool Delete(int MediaRelationshipStyleId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from " + databaseprefix + "MediaRelationshipCategory ");
			strSql.Append(" where MediaRelationshipStyleId=@MediaRelationshipStyleId ");
						SqlParameter[] parameters = {
					new SqlParameter("@MediaRelationshipStyleId", SqlDbType.Int,4)			};
			parameters[0].Value = MediaRelationshipStyleId;


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
		/// 得到一个对象实体
		/// </summary>
		public DTcms.Model.MediaRelationshipCategory GetModel(int MediaRelationshipStyleId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select MediaRelationshipStyleId, FullName, Code, ParentId, Layer, Remark, DeleteMark, Enable  ");			
			strSql.Append("  from " + databaseprefix + "MediaRelationshipCategory ");
			strSql.Append(" where MediaRelationshipStyleId=@MediaRelationshipStyleId ");
						SqlParameter[] parameters = {
					new SqlParameter("@MediaRelationshipStyleId", SqlDbType.Int,4)			};
			parameters[0].Value = MediaRelationshipStyleId;

			
			DTcms.Model.MediaRelationshipCategory model=new DTcms.Model.MediaRelationshipCategory();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																if(ds.Tables[0].Rows[0]["MediaRelationshipStyleId"].ToString()!="")
				{
					model.MediaRelationshipStyleId=int.Parse(ds.Tables[0].Rows[0]["MediaRelationshipStyleId"].ToString());
				}
																																								model.FullName= ds.Tables[0].Rows[0]["FullName"].ToString();
																																				model.Code= ds.Tables[0].Rows[0]["Code"].ToString();
																																if(ds.Tables[0].Rows[0]["ParentId"].ToString()!="")
				{
					model.ParentId=int.Parse(ds.Tables[0].Rows[0]["ParentId"].ToString());
				}
																																				if(ds.Tables[0].Rows[0]["Layer"].ToString()!="")
				{
					model.Layer=int.Parse(ds.Tables[0].Rows[0]["Layer"].ToString());
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
			strSql.Append(" FROM " + databaseprefix + "MediaRelationshipCategory ");
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
			strSql.Append(" FROM " + databaseprefix + "MediaRelationshipCategory ");
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
            strSql.Append(" FROM " + databaseprefix + "MediaRelationshipCategory order by MediaRelationshipStyleId desc");
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
                               				if(dr[i]["MediaRelationshipStyleId"].ToString()!="")
				{
					row["MediaRelationshipStyleId"]=int.Parse(dr[i]["MediaRelationshipStyleId"].ToString());
				}
																				                               								row["FullName"]= dr[i]["FullName"].ToString();
																                               								row["Code"]= dr[i]["Code"].ToString();
																                               				if(dr[i]["ParentId"].ToString()!="")
				{
					row["ParentId"]=int.Parse(dr[i]["ParentId"].ToString());
				}
																				                               				if(dr[i]["Layer"].ToString()!="")
				{
					row["Layer"]=int.Parse(dr[i]["Layer"].ToString());
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
																				                                newData.Rows.Add(row);
                //调用自身迭代
                this.GetChilds(oldData, newData, int.Parse(dr[i]["MediaRelationshipStyleId"].ToString()));
            }
        }
				
		/// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + "MediaRelationshipCategory ");
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

