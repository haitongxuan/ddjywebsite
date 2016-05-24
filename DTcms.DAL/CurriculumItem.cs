using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using DTcms.DBUtility;
using DTcms.Common;
namespace DTcms.DAL
{
    //课程章节
    public partial class CurriculumItem
    {
        private string databaseprefix; //数据库表名前缀
        public CurriculumItem(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }
        #region 基本方法================================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int CurriculumItemId, int CurriculumId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "CurriculumItem");
            strSql.Append(" where ");
            strSql.Append(" CurriculumItemId = @CurriculumItemId and  ");
            strSql.Append(" CurriculumId = @CurriculumId  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@CurriculumItemId", SqlDbType.Int,4),
                    new SqlParameter("@CurriculumId", SqlDbType.Int,4)          };
            parameters[0].Value = CurriculumItemId;
            parameters[1].Value = CurriculumId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回数据总数
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from " + databaseprefix + "CurriculumItem");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(DTcms.Model.CurriculumItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "CurriculumItem(");
            strSql.Append("CurriculumId,FullName,Describe,Sort,DeleteMark,Enable,CreateDate,CreateUserName,ModifyDate,ModifyUserName,ImageUrl,MediaCode");
            strSql.Append(") values (");
            strSql.Append("@CurriculumId,@FullName,@Describe,@Sort,@DeleteMark,@Enable,@CreateDate,@CreateUserName,@ModifyDate,@ModifyUserName,@ImageUrl,@MediaCode");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@CurriculumId", SqlDbType.Int,4) ,
                        new SqlParameter("@FullName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Describe", SqlDbType.VarChar,200) ,
                        new SqlParameter("@Sort", SqlDbType.Int,4) ,
                        new SqlParameter("@DeleteMark", SqlDbType.Int,4) ,
                        new SqlParameter("@Enable", SqlDbType.Int,4) ,
                        new SqlParameter("@CreateDate", SqlDbType.DateTime) ,
                        new SqlParameter("@CreateUserName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ModifyDate", SqlDbType.DateTime) ,
                        new SqlParameter("@ModifyUserName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ImageUrl", SqlDbType.VarChar,200) ,
                        new SqlParameter("@MediaCode", SqlDbType.VarChar,200)

            };

            parameters[0].Value = model.CurriculumId;
            parameters[1].Value = model.FullName;
            parameters[2].Value = model.Describe;
            parameters[3].Value = model.Sort;
            parameters[4].Value = model.DeleteMark;
            parameters[5].Value = model.Enable;
            parameters[6].Value = model.CreateDate;
            parameters[7].Value = model.CreateUserName;
            parameters[8].Value = model.ModifyDate;
            parameters[9].Value = model.ModifyUserName;
            parameters[10].Value = model.ImageUrl;
            parameters[11].Value = model.MediaCode;

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
        public bool Update(DTcms.Model.CurriculumItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "CurriculumItem set ");

            strSql.Append(" CurriculumId = @CurriculumId , ");
            strSql.Append(" FullName = @FullName , ");
            strSql.Append(" Describe = @Describe , ");
            strSql.Append(" Sort = @Sort , ");
            strSql.Append(" DeleteMark = @DeleteMark , ");
            strSql.Append(" Enable = @Enable , ");
            strSql.Append(" CreateDate = @CreateDate , ");
            strSql.Append(" CreateUserName = @CreateUserName , ");
            strSql.Append(" ModifyDate = @ModifyDate , ");
            strSql.Append(" ModifyUserName = @ModifyUserName , ");
            strSql.Append(" ImageUrl = @ImageUrl , ");
            strSql.Append(" MediaCode = @MediaCode  ");
            strSql.Append(" where CurriculumItemId=@CurriculumItemId ");

            SqlParameter[] parameters = {
                        new SqlParameter("@CurriculumItemId", SqlDbType.Int,4) ,
                        new SqlParameter("@CurriculumId", SqlDbType.Int,4) ,
                        new SqlParameter("@FullName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Describe", SqlDbType.VarChar,200) ,
                        new SqlParameter("@Sort", SqlDbType.Int,4) ,
                        new SqlParameter("@DeleteMark", SqlDbType.Int,4) ,
                        new SqlParameter("@Enable", SqlDbType.Int,4) ,
                        new SqlParameter("@CreateDate", SqlDbType.DateTime) ,
                        new SqlParameter("@CreateUserName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ModifyDate", SqlDbType.DateTime) ,
                        new SqlParameter("@ModifyUserName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ImageUrl", SqlDbType.VarChar,200) ,
                        new SqlParameter("@MediaCode", SqlDbType.VarChar,200)

            };

            parameters[0].Value = model.CurriculumItemId;
            parameters[1].Value = model.CurriculumId;
            parameters[2].Value = model.FullName;
            parameters[3].Value = model.Describe;
            parameters[4].Value = model.Sort;
            parameters[5].Value = model.DeleteMark;
            parameters[6].Value = model.Enable;
            parameters[7].Value = model.CreateDate;
            parameters[8].Value = model.CreateUserName;
            parameters[9].Value = model.ModifyDate;
            parameters[10].Value = model.ModifyUserName;
            parameters[11].Value = model.ImageUrl;
            parameters[12].Value = model.MediaCode;
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
        public bool Delete(int CurriculumItemId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "CurriculumItem ");
            strSql.Append(" where CurriculumItemId=@CurriculumItemId");
            SqlParameter[] parameters = {
                    new SqlParameter("@CurriculumItemId", SqlDbType.Int,4)
            };
            parameters[0].Value = CurriculumItemId;


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
        public bool DeleteList(string CurriculumItemIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "CurriculumItem ");
            strSql.Append(" where ID in (" + CurriculumItemIdlist + ")  ");
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
        public DTcms.Model.CurriculumItem GetModel(int CurriculumItemId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CurriculumItemId, CurriculumId, FullName, Describe, Sort, DeleteMark, Enable, CreateDate, CreateUserName, ModifyDate, ModifyUserName, ImageUrl, MediaCode  ");
            strSql.Append("  from " + databaseprefix + "CurriculumItem ");
            strSql.Append(" where CurriculumItemId=@CurriculumItemId");
            SqlParameter[] parameters = {
                    new SqlParameter("@CurriculumItemId", SqlDbType.Int,4)
            };
            parameters[0].Value = CurriculumItemId;


            DTcms.Model.CurriculumItem model = new DTcms.Model.CurriculumItem();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["CurriculumItemId"].ToString() != "")
                {
                    model.CurriculumItemId = int.Parse(ds.Tables[0].Rows[0]["CurriculumItemId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CurriculumId"].ToString() != "")
                {
                    model.CurriculumId = int.Parse(ds.Tables[0].Rows[0]["CurriculumId"].ToString());
                }
                model.FullName = ds.Tables[0].Rows[0]["FullName"].ToString();
                model.Describe = ds.Tables[0].Rows[0]["Describe"].ToString();
                if (ds.Tables[0].Rows[0]["Sort"].ToString() != "")
                {
                    model.Sort = int.Parse(ds.Tables[0].Rows[0]["Sort"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DeleteMark"].ToString() != "")
                {
                    model.DeleteMark = int.Parse(ds.Tables[0].Rows[0]["DeleteMark"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Enable"].ToString() != "")
                {
                    model.Enable = int.Parse(ds.Tables[0].Rows[0]["Enable"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() != "")
                {
                    model.CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                }
                model.CreateUserName = ds.Tables[0].Rows[0]["CreateUserName"].ToString();
                if (ds.Tables[0].Rows[0]["ModifyDate"].ToString() != "")
                {
                    model.ModifyDate = DateTime.Parse(ds.Tables[0].Rows[0]["ModifyDate"].ToString());
                }
                model.ModifyUserName = ds.Tables[0].Rows[0]["ModifyUserName"].ToString();
                model.ImageUrl = ds.Tables[0].Rows[0]["ImageUrl"].ToString();
                model.MediaCode = ds.Tables[0].Rows[0]["MediaCode"].ToString();

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
            strSql.Append(" FROM " + databaseprefix + "CurriculumItem ");
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
            strSql.Append(" FROM " + databaseprefix + "CurriculumItem ");
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
            strSql.Append("select * FROM " + databaseprefix + "CurriculumItem ");
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

