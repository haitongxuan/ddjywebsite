using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using DTcms.DBUtility;
using DTcms.Common;
namespace DTcms.DAL
{
    //卡片类别
    public partial class CardCategory
    {
        private string databaseprefix; //数据库表名前缀
        public CardCategory(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }
        #region 基本方法================================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int CardCategoryId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "CardCategory");
            strSql.Append(" where ");
            strSql.Append(" CardCategoryId = @CardCategoryId  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@CardCategoryId", SqlDbType.Int,4)
            };
            parameters[0].Value = CardCategoryId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回数据总数
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from " + databaseprefix + "CardCategory");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(DTcms.Model.CardCategory model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "CardCategory(");
            strSql.Append("CreateDate,CreateUserName,ModifyDate,ModifyUserName,ParentId,CallIndex,Layer,FullName,Describe,ImagUrl,BackImageUrl,Duration,UserGroupCallIndex");
            strSql.Append(") values (");
            strSql.Append("@CreateDate,@CreateUserName,@ModifyDate,@ModifyUserName,@ParentId,@CallIndex,@Layer,@FullName,@Describe,@ImagUrl,@BackImageUrl,@Duration,@UserGroupCallIndex");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@CreateDate", SqlDbType.DateTime) ,
                        new SqlParameter("@CreateUserName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ModifyDate", SqlDbType.DateTime) ,
                        new SqlParameter("@ModifyUserName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ParentId", SqlDbType.Int,4) ,
                        new SqlParameter("@CallIndex", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Layer", SqlDbType.Int,4) ,
                        new SqlParameter("@FullName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Describe", SqlDbType.VarChar,200) ,
                        new SqlParameter("@ImagUrl", SqlDbType.VarChar,200) ,
                        new SqlParameter("@BackImageUrl", SqlDbType.VarChar,200) ,
                        new SqlParameter("@Duration", SqlDbType.Decimal,9),
                        new SqlParameter("@UserGroupCallIndex",SqlDbType.NVarChar,50), 

            };

            parameters[0].Value = model.CreateDate;
            parameters[1].Value = model.CreateUserName;
            parameters[2].Value = model.ModifyDate;
            parameters[3].Value = model.ModifyUserName;
            parameters[4].Value = model.ParentId;
            parameters[5].Value = model.CallIndex;
            parameters[6].Value = model.Layer;
            parameters[7].Value = model.FullName;
            parameters[8].Value = model.Describe;
            parameters[9].Value = model.ImagUrl;
            parameters[10].Value = model.BackImageUrl;
            parameters[11].Value = model.Duration;
            parameters[12].Value = model.UserGroupCallIndex;

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
        public bool Update(DTcms.Model.CardCategory model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "CardCategory set ");

            strSql.Append(" CreateDate = @CreateDate , ");
            strSql.Append(" CreateUserName = @CreateUserName , ");
            strSql.Append(" ModifyDate = @ModifyDate , ");
            strSql.Append(" ModifyUserName = @ModifyUserName , ");
            strSql.Append(" ParentId = @ParentId , ");
            strSql.Append(" CallIndex = @CallIndex , ");
            strSql.Append(" Layer = @Layer , ");
            strSql.Append(" FullName = @FullName , ");
            strSql.Append(" Describe = @Describe , ");
            strSql.Append(" ImagUrl = @ImagUrl , ");
            strSql.Append(" BackImageUrl = @BackImageUrl , ");
            strSql.Append(" Duration = @Duration , ");
            strSql.Append(" UserGroupCallIndex=@UserGroupCallIndex");
            strSql.Append(" where CardCategoryId=@CardCategoryId ");

            SqlParameter[] parameters = {
                        new SqlParameter("@CardCategoryId", SqlDbType.Int,4) ,
                        new SqlParameter("@CreateDate", SqlDbType.DateTime) ,
                        new SqlParameter("@CreateUserName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ModifyDate", SqlDbType.DateTime) ,
                        new SqlParameter("@ModifyUserName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ParentId", SqlDbType.Int,4) ,
                        new SqlParameter("@CallIndex", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Layer", SqlDbType.Int,4) ,
                        new SqlParameter("@FullName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Describe", SqlDbType.VarChar,200) ,
                        new SqlParameter("@ImagUrl", SqlDbType.VarChar,200) ,
                        new SqlParameter("@BackImageUrl", SqlDbType.VarChar,200) ,
                        new SqlParameter("@Duration", SqlDbType.Decimal,9),
                        new SqlParameter("@UserGroupCallIndex",SqlDbType.NVarChar,50), 
            };

            parameters[0].Value = model.CardCategoryId;
            parameters[1].Value = model.CreateDate;
            parameters[2].Value = model.CreateUserName;
            parameters[3].Value = model.ModifyDate;
            parameters[4].Value = model.ModifyUserName;
            parameters[5].Value = model.ParentId;
            parameters[6].Value = model.CallIndex;
            parameters[7].Value = model.Layer;
            parameters[8].Value = model.FullName;
            parameters[9].Value = model.Describe;
            parameters[10].Value = model.ImagUrl;
            parameters[11].Value = model.BackImageUrl;
            parameters[12].Value = model.Duration;
            parameters[13].Value = model.UserGroupCallIndex;
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
        public bool Delete(int CardCategoryId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "CardCategory ");
            strSql.Append(" where CardCategoryId=@CardCategoryId");
            SqlParameter[] parameters = {
                    new SqlParameter("@CardCategoryId", SqlDbType.Int,4)
            };
            parameters[0].Value = CardCategoryId;


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
        public bool DeleteList(string CardCategoryIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "CardCategory ");
            strSql.Append(" where ID in (" + CardCategoryIdlist + ")  ");
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
        public DTcms.Model.CardCategory GetModel(int CardCategoryId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CardCategoryId, CreateDate, CreateUserName, ModifyDate, ModifyUserName, ParentId, CallIndex, Layer, FullName, Describe, ImagUrl, BackImageUrl, Duration,UserGroupCallIndex  ");
            strSql.Append("  from " + databaseprefix + "CardCategory ");
            strSql.Append(" where CardCategoryId=@CardCategoryId");
            SqlParameter[] parameters = {
                    new SqlParameter("@CardCategoryId", SqlDbType.Int,4)
            };
            parameters[0].Value = CardCategoryId;


            DTcms.Model.CardCategory model = new DTcms.Model.CardCategory();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["CardCategoryId"].ToString() != "")
                {
                    model.CardCategoryId = int.Parse(ds.Tables[0].Rows[0]["CardCategoryId"].ToString());
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
                if (ds.Tables[0].Rows[0]["ParentId"].ToString() != "")
                {
                    model.ParentId = int.Parse(ds.Tables[0].Rows[0]["ParentId"].ToString());
                }
                model.CallIndex = ds.Tables[0].Rows[0]["CallIndex"].ToString();
                if (ds.Tables[0].Rows[0]["Layer"].ToString() != "")
                {
                    model.Layer = int.Parse(ds.Tables[0].Rows[0]["Layer"].ToString());
                }
                model.FullName = ds.Tables[0].Rows[0]["FullName"].ToString();
                model.Describe = ds.Tables[0].Rows[0]["Describe"].ToString();
                model.ImagUrl = ds.Tables[0].Rows[0]["ImagUrl"].ToString();
                model.BackImageUrl = ds.Tables[0].Rows[0]["BackImageUrl"].ToString();
                if (ds.Tables[0].Rows[0]["Duration"].ToString() != "")
                {
                    model.Duration = decimal.Parse(ds.Tables[0].Rows[0]["Duration"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserGroupCallIndex"].ToString() != "")
                {
                    model.UserGroupCallIndex = ds.Tables[0].Rows[0]["UserGroupCallIndex"].ToString();
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
        public DTcms.Model.CardCategory GetModel(string CallIndex)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CardCategoryId, CreateDate, CreateUserName, ModifyDate, ModifyUserName, ParentId, CallIndex, Layer, FullName, Describe, ImagUrl, BackImageUrl, Duration,UserGroupCallIndex  ");
            strSql.Append("  from " + databaseprefix + "CardCategory ");
            strSql.Append(" where CallIndex=@CallIndex");
            SqlParameter[] parameters = {
                    new SqlParameter("@CallIndex", SqlDbType.NVarChar,50)
            };
            parameters[0].Value = CallIndex;

            DTcms.Model.CardCategory model = new DTcms.Model.CardCategory();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["CardCategoryId"].ToString() != "")
                {
                    model.CardCategoryId = int.Parse(ds.Tables[0].Rows[0]["CardCategoryId"].ToString());
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
                if (ds.Tables[0].Rows[0]["ParentId"].ToString() != "")
                {
                    model.ParentId = int.Parse(ds.Tables[0].Rows[0]["ParentId"].ToString());
                }
                model.CallIndex = ds.Tables[0].Rows[0]["CallIndex"].ToString();
                if (ds.Tables[0].Rows[0]["Layer"].ToString() != "")
                {
                    model.Layer = int.Parse(ds.Tables[0].Rows[0]["Layer"].ToString());
                }
                model.FullName = ds.Tables[0].Rows[0]["FullName"].ToString();
                model.Describe = ds.Tables[0].Rows[0]["Describe"].ToString();
                model.ImagUrl = ds.Tables[0].Rows[0]["ImagUrl"].ToString();
                model.BackImageUrl = ds.Tables[0].Rows[0]["BackImageUrl"].ToString();
                if (ds.Tables[0].Rows[0]["Duration"].ToString() != "")
                {
                    model.Duration = decimal.Parse(ds.Tables[0].Rows[0]["Duration"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserGroupCallIndex"].ToString() != "")
                {
                    model.UserGroupCallIndex = ds.Tables[0].Rows[0]["UserGroupCallIndex"].ToString();
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
            strSql.Append(" FROM " + databaseprefix + "CardCategory ");
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
            strSql.Append(" FROM " + databaseprefix + "CardCategory ");
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
            strSql.Append(" FROM " + databaseprefix + "CardCategory order by CardCategoryId desc");
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
                if (dr[i]["CardCategoryId"].ToString() != "")
                {
                    row["CardCategoryId"] = int.Parse(dr[i]["CardCategoryId"].ToString());
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
                if (dr[i]["ParentId"].ToString() != "")
                {
                    row["ParentId"] = int.Parse(dr[i]["ParentId"].ToString());
                }
                row["CallIndex"] = dr[i]["CallIndex"].ToString();
                if (dr[i]["Layer"].ToString() != "")
                {
                    row["Layer"] = int.Parse(dr[i]["Layer"].ToString());
                }
                row["FullName"] = dr[i]["FullName"].ToString();
                row["Describe"] = dr[i]["Describe"].ToString();
                row["ImagUrl"] = dr[i]["ImagUrl"].ToString();
                row["BackImageUrl"] = dr[i]["BackImageUrl"].ToString();
                if (dr[i]["Duration"].ToString() != "")
                {
                    row["Duration"] = decimal.Parse(dr[i]["Duration"].ToString());
                }
                if (dr[i]["UserGroupCallIndex"].ToString() != "")
                {
                    row["UserGroupCallIndex"] = dr[i]["UserGroupCallIndex"].ToString();
                }
                newData.Rows.Add(row);
                //调用自身迭代
                this.GetChilds(oldData, newData, int.Parse(dr[i]["CardCategoryId"].ToString()));
            }
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + "CardCategory ");
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

