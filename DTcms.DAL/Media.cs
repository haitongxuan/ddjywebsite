using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using DTcms.DBUtility;
using DTcms.Common;
namespace DTcms.DAL
{
    //媒体表
    public partial class Media
    {
        private string databaseprefix; //数据库表名前缀
        public Media(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }
        #region 基本方法================================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int MediaId, int FolderId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "Media");
            strSql.Append(" where ");
            strSql.Append(" MediaId = @MediaId and  ");
            strSql.Append(" FolderId = @FolderId  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@MediaId", SqlDbType.Int,4),
                    new SqlParameter("@FolderId", SqlDbType.Int,4)            };
            parameters[0].Value = MediaId;
            parameters[1].Value = FolderId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回数据总数
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from " + databaseprefix + "Media");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(DTcms.Model.Media model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "Media(");
            strSql.Append("FullName,FileName,FolderId,RealPath,DeleteMark,Enable,MediaCategoryId,MediaCode,TimeLength");
            strSql.Append(") values (");
            strSql.Append("@FullName,@FileName,@FolderId,@RealPath,@DeleteMark,@Enable,@MediaCategoryId,@MediaCode,@TimeLength");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@FullName", SqlDbType.VarChar,200) ,
                        new SqlParameter("@FileName", SqlDbType.VarChar,200) ,
                        new SqlParameter("@FolderId", SqlDbType.Int,4) ,
                        new SqlParameter("@RealPath", SqlDbType.VarChar,200) ,
                        new SqlParameter("@DeleteMark", SqlDbType.Int,4) ,
                        new SqlParameter("@Enable", SqlDbType.Int,4) ,
                        new SqlParameter("@MediaCategoryId", SqlDbType.Int,4) ,
                        new SqlParameter("@MediaCode", SqlDbType.VarChar,50),
                        new SqlParameter("@TimeLength",SqlDbType.Decimal),
            };

            parameters[0].Value = model.FullName;
            parameters[1].Value = model.FileName;
            parameters[2].Value = model.FolderId;
            parameters[3].Value = model.RealPath;
            parameters[4].Value = model.DeleteMark;
            parameters[5].Value = model.Enable;
            parameters[6].Value = model.MediaCategoryId;
            parameters[7].Value = model.MediaCode;
            parameters[8].Value = model.TimeLength;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        public bool Update(DTcms.Model.Media model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "Media set ");

            strSql.Append(" FullName = @FullName , ");
            strSql.Append(" FileName = @FileName , ");
            strSql.Append(" FolderId = @FolderId , ");
            strSql.Append(" RealPath = @RealPath , ");
            strSql.Append(" DeleteMark = @DeleteMark , ");
            strSql.Append(" Enable = @Enable , ");
            strSql.Append(" MediaCategoryId = @MediaCategoryId , ");
            strSql.Append(" MediaCode = @MediaCode , ");
            strSql.Append(" TimeLength = @TimeLength");
            strSql.Append(" where MediaId=@MediaId ");

            SqlParameter[] parameters = {
                        new SqlParameter("@MediaId", SqlDbType.Int,4) ,
                        new SqlParameter("@FullName", SqlDbType.VarChar,200) ,
                        new SqlParameter("@FileName", SqlDbType.VarChar,200) ,
                        new SqlParameter("@FolderId", SqlDbType.Int,4) ,
                        new SqlParameter("@RealPath", SqlDbType.VarChar,200) ,
                        new SqlParameter("@DeleteMark", SqlDbType.Int,4) ,
                        new SqlParameter("@Enable", SqlDbType.Int,4) ,
                        new SqlParameter("@MediaCategoryId", SqlDbType.Int,4) ,
                        new SqlParameter("@MediaCode", SqlDbType.VarChar,50),
                        new SqlParameter("@TimeLength",SqlDbType.Decimal), 
            };

            parameters[0].Value = model.MediaId;
            parameters[1].Value = model.FullName;
            parameters[2].Value = model.FileName;
            parameters[3].Value = model.FolderId;
            parameters[4].Value = model.RealPath;
            parameters[5].Value = model.DeleteMark;
            parameters[6].Value = model.Enable;
            parameters[7].Value = model.MediaCategoryId;
            parameters[8].Value = model.MediaCode;
            parameters[9].Value = model.TimeLength;
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
        public bool Delete(int MediaId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "Media ");
            strSql.Append(" where MediaId=@MediaId");
            SqlParameter[] parameters = {
                    new SqlParameter("@MediaId", SqlDbType.Int,4)
            };
            parameters[0].Value = MediaId;


            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
        public bool DeleteList(string MediaIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "Media ");
            strSql.Append(" where ID in (" + MediaIdlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
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
        public DTcms.Model.Media GetModel(int MediaId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MediaId, FullName, FileName, FolderId, RealPath, DeleteMark, Enable, MediaCategoryId, MediaCode,TimeLength  ");
            strSql.Append("  from " + databaseprefix + "Media ");
            strSql.Append(" where MediaId=@MediaId");
            SqlParameter[] parameters = {
                    new SqlParameter("@MediaId", SqlDbType.Int,4)
            };
            parameters[0].Value = MediaId;


            DTcms.Model.Media model = new DTcms.Model.Media();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["MediaId"].ToString() != "")
                {
                    model.MediaId = int.Parse(ds.Tables[0].Rows[0]["MediaId"].ToString());
                }
                model.FullName = ds.Tables[0].Rows[0]["FullName"].ToString();
                model.FileName = ds.Tables[0].Rows[0]["FileName"].ToString();
                if (ds.Tables[0].Rows[0]["FolderId"].ToString() != "")
                {
                    model.FolderId = int.Parse(ds.Tables[0].Rows[0]["FolderId"].ToString());
                }
                model.RealPath = ds.Tables[0].Rows[0]["RealPath"].ToString();
                if (ds.Tables[0].Rows[0]["DeleteMark"].ToString() != "")
                {
                    model.DeleteMark = int.Parse(ds.Tables[0].Rows[0]["DeleteMark"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Enable"].ToString() != "")
                {
                    model.Enable = int.Parse(ds.Tables[0].Rows[0]["Enable"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MediaCategoryId"].ToString() != "")
                {
                    model.MediaCategoryId = int.Parse(ds.Tables[0].Rows[0]["MediaCategoryId"].ToString());
                }
                model.MediaCode = ds.Tables[0].Rows[0]["MediaCode"].ToString();
                if (ds.Tables[0].Rows[0]["TimeLength"].ToString() != "")
                {
                    model.TimeLength = decimal.Parse(ds.Tables[0].Rows[0]["TimeLength"].ToString());
                }

                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DTcms.Model.Media GetModel(string MediaCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MediaId, FullName, FileName, FolderId, RealPath, DeleteMark, Enable, MediaCategoryId, MediaCode,TimeLength  ");
            strSql.Append("  from " + databaseprefix + "Media ");
            strSql.Append(" where MediaCode=@MediaCode");
            SqlParameter[] parameters = {
                    new SqlParameter("@MediaCode", SqlDbType.Int,4)
            };
            parameters[0].Value = MediaCode;


            DTcms.Model.Media model = new DTcms.Model.Media();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["MediaId"].ToString() != "")
                {
                    model.MediaId = int.Parse(ds.Tables[0].Rows[0]["MediaId"].ToString());
                }
                model.FullName = ds.Tables[0].Rows[0]["FullName"].ToString();
                model.FileName = ds.Tables[0].Rows[0]["FileName"].ToString();
                if (ds.Tables[0].Rows[0]["FolderId"].ToString() != "")
                {
                    model.FolderId = int.Parse(ds.Tables[0].Rows[0]["FolderId"].ToString());
                }
                model.RealPath = ds.Tables[0].Rows[0]["RealPath"].ToString();
                if (ds.Tables[0].Rows[0]["DeleteMark"].ToString() != "")
                {
                    model.DeleteMark = int.Parse(ds.Tables[0].Rows[0]["DeleteMark"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Enable"].ToString() != "")
                {
                    model.Enable = int.Parse(ds.Tables[0].Rows[0]["Enable"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MediaCategoryId"].ToString() != "")
                {
                    model.MediaCategoryId = int.Parse(ds.Tables[0].Rows[0]["MediaCategoryId"].ToString());
                }
                model.MediaCode = ds.Tables[0].Rows[0]["MediaCode"].ToString();
                if (ds.Tables[0].Rows[0]["TimeLength"].ToString() != "")
                {
                    model.TimeLength = decimal.Parse(ds.Tables[0].Rows[0]["TimeLength"].ToString());
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM " + databaseprefix + "Media ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM " + databaseprefix + "Media ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
            strSql.Append("select * FROM " + databaseprefix + "Media ");
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

