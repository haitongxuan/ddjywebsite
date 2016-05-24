using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using DTcms.DBUtility;
using DTcms.Common;
namespace DTcms.DAL
{
    //用户卡片表
    public partial class UserCard
    {
        private string databaseprefix; //数据库表名前缀
        public UserCard(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }
        #region 基本方法================================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int UserCardId, int CardId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "UserCard");
            strSql.Append(" where ");
            strSql.Append(" UserCardId = @UserCardId and  ");
            strSql.Append(" CardId = @CardId  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserCardId", SqlDbType.Int,4),
                    new SqlParameter("@CardId", SqlDbType.Int,4)          };
            parameters[0].Value = UserCardId;
            parameters[1].Value = CardId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回数据总数
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from " + databaseprefix + "UserCard");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(DTcms.Model.UserCard model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "UserCard(");
            strSql.Append("CardId,UserId,CardCategoryId");
            strSql.Append(") values (");
            strSql.Append("@CardId,@UserId,@CardCategoryId");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@CardId", SqlDbType.Int,4) ,
                        new SqlParameter("@UserId", SqlDbType.Int,4) ,
                        new SqlParameter("@CardCategoryId", SqlDbType.Int,4)

            };

            parameters[0].Value = model.CardId;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.CardCategoryId;

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
        public bool Update(DTcms.Model.UserCard model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "UserCard set ");

            strSql.Append(" CardId = @CardId , ");
            strSql.Append(" UserId = @UserId , ");
            strSql.Append(" CardCategoryId = @CardCategoryId  ");
            strSql.Append(" where UserCardId=@UserCardId ");

            SqlParameter[] parameters = {
                        new SqlParameter("@UserCardId", SqlDbType.Int,4) ,
                        new SqlParameter("@CardId", SqlDbType.Int,4) ,
                        new SqlParameter("@UserId", SqlDbType.Int,4) ,
                        new SqlParameter("@CardCategoryId", SqlDbType.Int,4)

            };

            parameters[0].Value = model.UserCardId;
            parameters[1].Value = model.CardId;
            parameters[2].Value = model.UserId;
            parameters[3].Value = model.CardCategoryId;
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
        public bool Delete(int UserCardId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "UserCard ");
            strSql.Append(" where UserCardId=@UserCardId");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserCardId", SqlDbType.Int,4)
            };
            parameters[0].Value = UserCardId;


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
        public bool DeleteList(string UserCardIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "UserCard ");
            strSql.Append(" where ID in (" + UserCardIdlist + ")  ");
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
        public DTcms.Model.UserCard GetModel(int UserCardId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserCardId, CardId, UserId, CardCategoryId  ");
            strSql.Append("  from " + databaseprefix + "UserCard ");
            strSql.Append(" where UserCardId=@UserCardId");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserCardId", SqlDbType.Int,4)
            };
            parameters[0].Value = UserCardId;


            DTcms.Model.UserCard model = new DTcms.Model.UserCard();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["UserCardId"].ToString() != "")
                {
                    model.UserCardId = int.Parse(ds.Tables[0].Rows[0]["UserCardId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CardId"].ToString() != "")
                {
                    model.CardId = int.Parse(ds.Tables[0].Rows[0]["CardId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserId"].ToString() != "")
                {
                    model.UserId = int.Parse(ds.Tables[0].Rows[0]["UserId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CardCategoryId"].ToString() != "")
                {
                    model.CardCategoryId = int.Parse(ds.Tables[0].Rows[0]["CardCategoryId"].ToString());
                }

                return model;
            }
            else
            {
                return null;
            }
        }

        DTcms.Model.UserCard GetModel(string CallIndex)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserCardId, CardId, UserId, CardCategoryId  ");
            strSql.Append("  from " + databaseprefix + "UserCard ");
            strSql.Append(" where CallIndex=@CallIndex");
            SqlParameter[] parameters = {
                    new SqlParameter("@CallIndex", SqlDbType.Int,4)
            };
            parameters[0].Value = CallIndex;

            DTcms.Model.UserCard model = new DTcms.Model.UserCard();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["UserCardId"].ToString() != "")
                {
                    model.UserCardId = int.Parse(ds.Tables[0].Rows[0]["UserCardId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CardId"].ToString() != "")
                {
                    model.CardId = int.Parse(ds.Tables[0].Rows[0]["CardId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserId"].ToString() != "")
                {
                    model.UserId = int.Parse(ds.Tables[0].Rows[0]["UserId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CardCategoryId"].ToString() != "")
                {
                    model.CardCategoryId = int.Parse(ds.Tables[0].Rows[0]["CardCategoryId"].ToString());
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
            strSql.Append(" FROM " + databaseprefix + "UserCard ");
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
            strSql.Append(" FROM " + databaseprefix + "UserCard ");
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
            strSql.Append("select * FROM " + databaseprefix + "UserCard ");
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

