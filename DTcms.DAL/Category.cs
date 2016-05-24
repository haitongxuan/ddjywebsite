using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using DTcms.DBUtility;
using DTcms.Common;
namespace DTcms.DAL
{
    //课程类别
    public partial class Category
    {
        private string databaseprefix; //数据库表名前缀
        public Category(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }
        #region 基本方法================================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int CategoryId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "Category");
            strSql.Append(" where ");
            strSql.Append(" CategoryId = @CategoryId  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@CategoryId", SqlDbType.Int,4)
            };
            parameters[0].Value = CategoryId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回数据总数
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from " + databaseprefix + "Category");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(DTcms.Model.Category model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "Category(");
            strSql.Append("FullName,ParentId,Layer,DeleteMark,Enable,CreateDate,CreateUserName,ModifyDate,ModifyUserName");
            strSql.Append(") values (");
            strSql.Append("@FullName,@ParentId,@Layer,@DeleteMark,@Enable,@CreateDate,@CreateUserName,@ModifyDate,@ModifyUserName");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@FullName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ParentId", SqlDbType.Int,4) ,
                        new SqlParameter("@Layer", SqlDbType.Int,4) ,
                        new SqlParameter("@DeleteMark", SqlDbType.Int,4) ,
                        new SqlParameter("@Enable", SqlDbType.Int,4) ,
                        new SqlParameter("@CreateDate", SqlDbType.DateTime) ,
                        new SqlParameter("@CreateUserName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ModifyDate", SqlDbType.DateTime) ,
                        new SqlParameter("@ModifyUserName", SqlDbType.VarChar,50)

            };

            parameters[0].Value = model.FullName;
            parameters[1].Value = model.ParentId;
            parameters[2].Value = model.Layer;
            parameters[3].Value = model.DeleteMark;
            parameters[4].Value = model.Enable;
            parameters[5].Value = model.CreateDate;
            parameters[6].Value = model.CreateUserName;
            parameters[7].Value = model.ModifyDate;
            parameters[8].Value = model.ModifyUserName;

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
        public bool Update(DTcms.Model.Category model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "Category set ");

            strSql.Append(" FullName = @FullName , ");
            strSql.Append(" ParentId = @ParentId , ");
            strSql.Append(" Layer = @Layer , ");
            strSql.Append(" DeleteMark = @DeleteMark , ");
            strSql.Append(" Enable = @Enable , ");
            strSql.Append(" CreateDate = @CreateDate , ");
            strSql.Append(" CreateUserName = @CreateUserName , ");
            strSql.Append(" ModifyDate = @ModifyDate , ");
            strSql.Append(" ModifyUserName = @ModifyUserName  ");
            strSql.Append(" where CategoryId=@CategoryId ");

            SqlParameter[] parameters = {
                        new SqlParameter("@CategoryId", SqlDbType.Int,4) ,
                        new SqlParameter("@FullName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ParentId", SqlDbType.Int,4) ,
                        new SqlParameter("@Layer", SqlDbType.Int,4) ,
                        new SqlParameter("@DeleteMark", SqlDbType.Int,4) ,
                        new SqlParameter("@Enable", SqlDbType.Int,4) ,
                        new SqlParameter("@CreateDate", SqlDbType.DateTime) ,
                        new SqlParameter("@CreateUserName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ModifyDate", SqlDbType.DateTime) ,
                        new SqlParameter("@ModifyUserName", SqlDbType.VarChar,50)

            };

            parameters[0].Value = model.CategoryId;
            parameters[1].Value = model.FullName;
            parameters[2].Value = model.ParentId;
            parameters[3].Value = model.Layer;
            parameters[4].Value = model.DeleteMark;
            parameters[5].Value = model.Enable;
            parameters[6].Value = model.CreateDate;
            parameters[7].Value = model.CreateUserName;
            parameters[8].Value = model.ModifyDate;
            parameters[9].Value = model.ModifyUserName;
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
        public bool Delete(int CategoryId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "Category ");
            strSql.Append(" where CategoryId=@CategoryId");
            SqlParameter[] parameters = {
                    new SqlParameter("@CategoryId", SqlDbType.Int,4)
            };
            parameters[0].Value = CategoryId;


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
        public bool DeleteList(string CategoryIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "Category ");
            strSql.Append(" where ID in (" + CategoryIdlist + ")  ");
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
        public DTcms.Model.Category GetModel(int CategoryId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CategoryId, FullName, ParentId, Layer, DeleteMark, Enable, CreateDate, CreateUserName, ModifyDate, ModifyUserName  ");
            strSql.Append("  from " + databaseprefix + "Category ");
            strSql.Append(" where CategoryId=@CategoryId");
            SqlParameter[] parameters = {
                    new SqlParameter("@CategoryId", SqlDbType.Int,4)
            };
            parameters[0].Value = CategoryId;


            DTcms.Model.Category model = new DTcms.Model.Category();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["CategoryId"].ToString() != "")
                {
                    model.CategoryId = int.Parse(ds.Tables[0].Rows[0]["CategoryId"].ToString());
                }
                model.FullName = ds.Tables[0].Rows[0]["FullName"].ToString();
                if (ds.Tables[0].Rows[0]["ParentId"].ToString() != "")
                {
                    model.ParentId = int.Parse(ds.Tables[0].Rows[0]["ParentId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Layer"].ToString() != "")
                {
                    model.Layer = int.Parse(ds.Tables[0].Rows[0]["Layer"].ToString());
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
            strSql.Append(" FROM " + databaseprefix + "Category ");
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
            strSql.Append(" FROM " + databaseprefix + "Category ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
            strSql.Append(" FROM " + databaseprefix + "Category order by CategoryId desc");
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
                if (dr[i]["CategoryId"].ToString() != "")
                {
                    row["CategoryId"] = int.Parse(dr[i]["CategoryId"].ToString());
                }
                row["FullName"] = dr[i]["FullName"].ToString();
                if (dr[i]["ParentId"].ToString() != "")
                {
                    row["ParentId"] = int.Parse(dr[i]["ParentId"].ToString());
                }
                if (dr[i]["Layer"].ToString() != "")
                {
                    row["Layer"] = int.Parse(dr[i]["Layer"].ToString());
                }
                if (dr[i]["DeleteMark"].ToString() != "")
                {
                    row["DeleteMark"] = int.Parse(dr[i]["DeleteMark"].ToString());
                }
                if (dr[i]["Enable"].ToString() != "")
                {
                    row["Enable"] = int.Parse(dr[i]["Enable"].ToString());
                }
                if (dr[i]["CreateDate"].ToString() != "")
                {
                    row["CreateDate"] = DateTime.Parse(dr[i]["CreateDate"].ToString());
                }
                row["CreateUserName"] = dr[i]["CreateUserName"].ToString();
                if (dr[i]["ModifyDate"].ToString() != "")
                {
                    row["ModifyDate"] = DateTime.Parse(dr[i]["ModifyDate"].ToString());
                }
                row["ModifyUserName"] = dr[i]["ModifyUserName"].ToString();
                newData.Rows.Add(row);
                //调用自身迭代
                this.GetChilds(oldData, newData, int.Parse(dr[i]["CategoryId"].ToString()));
            }
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + "Category ");
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

