using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using DTcms.DBUtility;
using DTcms.Common;
namespace DTcms.DAL
{
    //用户Token，用于APP用户认证
    public partial class UserToken
    {
        private string databaseprefix; //数据库表名前缀
        public UserToken(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }
        #region 基本方法================================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string UserTokenId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "UserToken");
            strSql.Append(" where ");
            strSql.Append(" UserTokenId = @UserTokenId  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserTokenId", SqlDbType.VarChar,50)          };
            parameters[0].Value = UserTokenId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回数据总数
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from " + databaseprefix + "UserToken");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(DTcms.Model.UserToken model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "UserToken(");
            strSql.Append("UserTokenId,UserId,UserName,Token,CreateTime,OverdueTime,IsOverdue,DeviceId,IPAddress");
            strSql.Append(") values (");
            strSql.Append("@UserTokenId,@UserId,@UserName,@Token,@CreateTime,@OverdueTime,@IsOverdue,@DeviceId,@IPAddress");
            strSql.Append(") ");

            SqlParameter[] parameters = {
                        new SqlParameter("@UserTokenId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@UserId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@UserName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Token", SqlDbType.VarChar,50) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@OverdueTime", SqlDbType.DateTime) ,
                        new SqlParameter("@IsOverdue", SqlDbType.Int,4) ,
                        new SqlParameter("@DeviceId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@IPAddress", SqlDbType.VarChar,50)

            };

            parameters[0].Value = model.UserTokenId;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.UserName;
            parameters[3].Value = model.Token;
            parameters[4].Value = model.CreateTime;
            parameters[5].Value = model.OverdueTime;
            parameters[6].Value = model.IsOverdue;
            parameters[7].Value = model.DeviceId;
            parameters[8].Value = model.IPAddress;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DTcms.Model.UserToken model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "UserToken set ");

            strSql.Append(" UserTokenId = @UserTokenId , ");
            strSql.Append(" UserId = @UserId , ");
            strSql.Append(" UserName = @UserName , ");
            strSql.Append(" Token = @Token , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" OverdueTime = @OverdueTime , ");
            strSql.Append(" IsOverdue = @IsOverdue , ");
            strSql.Append(" DeviceId = @DeviceId , ");
            strSql.Append(" IPAddress = @IPAddress  ");
            strSql.Append(" where UserTokenId=@UserTokenId  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@UserTokenId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@UserId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@UserName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Token", SqlDbType.VarChar,50) ,
                        new SqlParameter("@CreateTime", SqlDbType.DateTime) ,
                        new SqlParameter("@OverdueTime", SqlDbType.DateTime) ,
                        new SqlParameter("@IsOverdue", SqlDbType.Int,4) ,
                        new SqlParameter("@DeviceId", SqlDbType.VarChar,50) ,
                        new SqlParameter("@IPAddress", SqlDbType.VarChar,50)

            };

            parameters[0].Value = model.UserTokenId;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.UserName;
            parameters[3].Value = model.Token;
            parameters[4].Value = model.CreateTime;
            parameters[5].Value = model.OverdueTime;
            parameters[6].Value = model.IsOverdue;
            parameters[7].Value = model.DeviceId;
            parameters[8].Value = model.IPAddress;
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
        public bool Delete(string UserTokenId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "UserToken ");
            strSql.Append(" where UserTokenId=@UserTokenId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserTokenId", SqlDbType.VarChar,50)          };
            parameters[0].Value = UserTokenId;


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
        /// 得到一个对象实体
        /// </summary>
        public DTcms.Model.UserToken GetModel(string UserTokenId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserTokenId, UserId, UserName, Token, CreateTime, OverdueTime, IsOverdue, DeviceId, IPAddress  ");
            strSql.Append("  from UserToken ");
            strSql.Append(" where UserTokenId=@UserTokenId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserTokenId", SqlDbType.VarChar,50)          };
            parameters[0].Value = UserTokenId;


            DTcms.Model.UserToken model = new DTcms.Model.UserToken();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.UserTokenId = ds.Tables[0].Rows[0]["UserTokenId"].ToString();
                model.UserId = ds.Tables[0].Rows[0]["UserId"].ToString();
                model.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                model.Token = ds.Tables[0].Rows[0]["Token"].ToString();
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OverdueTime"].ToString() != "")
                {
                    model.OverdueTime = DateTime.Parse(ds.Tables[0].Rows[0]["OverdueTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsOverdue"].ToString() != "")
                {
                    model.IsOverdue = int.Parse(ds.Tables[0].Rows[0]["IsOverdue"].ToString());
                }
                model.DeviceId = ds.Tables[0].Rows[0]["DeviceId"].ToString();
                model.IPAddress = ds.Tables[0].Rows[0]["IPAddress"].ToString();

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
            strSql.Append(" FROM UserToken ");
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
            strSql.Append(" FROM UserToken ");
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
            strSql.Append("select * FROM " + databaseprefix + "UserToken ");
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

