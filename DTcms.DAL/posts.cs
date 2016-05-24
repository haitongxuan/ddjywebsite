using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL
{
    /// <summary>
    /// 数据访问类:帖子
    /// </summary>
    public partial class forum_posts
    {
        private string databaseprefix; //数据库表名前缀
        public forum_posts(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }
        #region 基本方法================================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "forum_posts");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回板块名称
        /// </summary>
        public string GetUserName(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 user_name from " + databaseprefix + "users");
            strSql.Append(" where id=" + id);
            string title = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(title))
            {
                return "";
            }
            return title;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.forum_posts model)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        #region 添加主表数据====================
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("insert into " + databaseprefix + "forum_posts(");
                        strSql.Append("board_id,user_id,user_ip,parent_post_id,quote_id,title,zhaiyao,content,is_top,is_red,is_hot,is_lock,click,reply_count,reply_user_id,add_time,reply_time,post_type)");
                        strSql.Append(" values (");
                        strSql.Append("@board_id,@user_id,@user_ip,@parent_post_id,@quote_id,@title,@zhaiyao,@content,@is_top,@is_red,@is_hot,@is_lock,@click,@reply_count,@reply_user_id,@add_time,@reply_time,@post_type)");
                        strSql.Append(";select @@IDENTITY");
                        SqlParameter[] parameters = {
					            new SqlParameter("@board_id", SqlDbType.Int,4),
					            new SqlParameter("@user_id", SqlDbType.Int,4),
					            new SqlParameter("@user_ip", SqlDbType.NVarChar,50),
                                new SqlParameter("@parent_post_id", SqlDbType.Int,4),
                                new SqlParameter("@quote_id", SqlDbType.Int,4),
					            new SqlParameter("@title", SqlDbType.NVarChar,100),
					            new SqlParameter("@zhaiyao", SqlDbType.NVarChar,255),
					            new SqlParameter("@content", SqlDbType.NText),
					            new SqlParameter("@is_top", SqlDbType.TinyInt,1),
					            new SqlParameter("@is_red", SqlDbType.TinyInt,1),
					            new SqlParameter("@is_hot", SqlDbType.TinyInt,1),
					            new SqlParameter("@is_lock", SqlDbType.TinyInt,1),
                                new SqlParameter("@click", SqlDbType.Int,4),
                                new SqlParameter("@reply_count", SqlDbType.Int,4),
                                new SqlParameter("@reply_user_id", SqlDbType.Int,4),
					            new SqlParameter("@add_time", SqlDbType.DateTime),
                                new SqlParameter("@reply_time", SqlDbType.DateTime),
					            new SqlParameter("@post_type", SqlDbType.Int,4)};
                        parameters[0].Value = model.board_id;
                        parameters[1].Value = model.user_id;
                        parameters[2].Value = model.user_ip;
                        parameters[3].Value = model.parent_post_id;
                        parameters[4].Value = model.quote_id;
                        parameters[5].Value = model.title;
                        parameters[6].Value = model.zhaiyao;
                        parameters[7].Value = model.content;
                        parameters[8].Value = model.is_top;
                        parameters[9].Value = model.is_red;
                        parameters[10].Value = model.is_hot;
                        parameters[11].Value = model.is_lock;
                        parameters[12].Value = model.click;
                        parameters[13].Value = model.reply_count;
                        parameters[14].Value = model.reply_user_id;
                        parameters[15].Value = model.add_time;
                        parameters[16].Value = model.reply_time;
                        parameters[17].Value = model.post_type;
                        //添加主表数据
                        object obj = DbHelperSQL.GetSingle(conn, trans, strSql.ToString(), parameters); //带事务
                        model.id = Convert.ToInt32(obj);
                        #endregion

                        #region 更新板块帖子数量==============================
                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append("update " + databaseprefix + "forum_board set ");
                        if (model.post_type == 1)
                        {
                            strSql2.Append(" subject_count=subject_count+1,post_count=post_count+1 ");
                        }
                        else
                        {
                            strSql2.Append(" post_count=post_count+1 ");
                        }
                        strSql2.Append(" where id = @id");
                        SqlParameter[] parameters2 = {
					                    new SqlParameter("@id", SqlDbType.Int,4)
                                                     };
                        parameters2[0].Value = model.board_id;
                        DbHelperSQL.GetSingle(conn, trans, strSql2.ToString(),parameters2); //带事务
                        #endregion

                        #region 更新主题回复数和最后回复人====================
                        if (model.post_type == 2)
                        {
                            StringBuilder strSql3 = new StringBuilder();
                            strSql3.Append(" update " + databaseprefix + "forum_posts set ");
                            strSql3.Append(" reply_count=reply_count+1,reply_user_id=@reply_user_id ");
                            strSql3.Append(" where id = @id ");
                            SqlParameter[] parameters3 = {
					                        new SqlParameter("@reply_user_id", SqlDbType.Int,4),
                                            new SqlParameter("@id", SqlDbType.Int,4)};
                            parameters3[0].Value = model.user_id;
                            parameters3[1].Value = model.parent_post_id;

                            DbHelperSQL.GetSingle(conn, trans, strSql3.ToString(), parameters3); //带事务
                        }
                        #endregion

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        return 0;
                    }
                }
            }
            return model.id;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.forum_posts model)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        #region 修改主表数据==========================
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("update " + databaseprefix + "forum_posts set ");
                        strSql.Append("board_id=@board_id,");
                        strSql.Append("user_id=@user_id,");
                        strSql.Append("user_ip=@user_ip,");
                        strSql.Append("parent_post_id=@parent_post_id,");
                        strSql.Append("quote_id=@quote_id,");
                        strSql.Append("title=@title,");
                        strSql.Append("zhaiyao=@zhaiyao,");
                        strSql.Append("content=@content,");
                        strSql.Append("is_top=@is_top,");
                        strSql.Append("is_red=@is_red,");
                        strSql.Append("is_hot=@is_hot,");
                        strSql.Append("is_lock=@is_lock,");
                        strSql.Append("click=@click,");
                        strSql.Append("reply_count=@reply_count,");
                        strSql.Append("reply_user_id=@reply_user_id,");
                        strSql.Append("add_time=@add_time,");
                        strSql.Append("reply_time=@reply_time,");
                        strSql.Append("post_type=@post_type");
                        strSql.Append(" where id=@id");
                        SqlParameter[] parameters = {
					            new SqlParameter("@board_id", SqlDbType.Int,4),
					            new SqlParameter("@user_id", SqlDbType.Int,4),
					            new SqlParameter("@user_ip", SqlDbType.NVarChar,50),
					            new SqlParameter("@parent_post_id", SqlDbType.Int,4),
					            new SqlParameter("@quote_id", SqlDbType.Int,4),
					            new SqlParameter("@title", SqlDbType.NVarChar,100),
					            new SqlParameter("@zhaiyao", SqlDbType.NVarChar,255),
					            new SqlParameter("@content", SqlDbType.NText),
					            new SqlParameter("@is_top", SqlDbType.TinyInt,1),
					            new SqlParameter("@is_red", SqlDbType.TinyInt,1),
					            new SqlParameter("@is_hot", SqlDbType.TinyInt,1),
					            new SqlParameter("@is_lock", SqlDbType.TinyInt,1),
					            new SqlParameter("@click", SqlDbType.Int,4),
					            new SqlParameter("@reply_count", SqlDbType.Int,4),
					            new SqlParameter("@reply_user_id", SqlDbType.Int,4),
					            new SqlParameter("@add_time", SqlDbType.DateTime),
					            new SqlParameter("@reply_time", SqlDbType.DateTime),
					            new SqlParameter("@post_type", SqlDbType.Int,4),
					            new SqlParameter("@id", SqlDbType.Int,4)};
                        parameters[0].Value = model.board_id;
                        parameters[1].Value = model.user_id;
                        parameters[2].Value = model.user_ip;
                        parameters[3].Value = model.parent_post_id;
                        parameters[4].Value = model.quote_id;
                        parameters[5].Value = model.title;
                        parameters[6].Value = model.zhaiyao;
                        parameters[7].Value = model.content;
                        parameters[8].Value = model.is_top;
                        parameters[9].Value = model.is_red;
                        parameters[10].Value = model.is_hot;
                        parameters[11].Value = model.is_lock;
                        parameters[12].Value = model.click;
                        parameters[13].Value = model.reply_count;
                        parameters[14].Value = model.reply_user_id;
                        parameters[15].Value = model.add_time;
                        parameters[16].Value = model.reply_time;
                        parameters[17].Value = model.post_type;
                        parameters[18].Value = model.id;
                        DbHelperSQL.ExecuteSql(conn, trans, strSql.ToString(), parameters);
                        #endregion

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            int subject_count = 0;
            int post_count = 0;
            Model.forum_posts model = GetModel(id);
            int board_id = model.board_id;

            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo();
            post_count -= 1;
            if(model.post_type == 1)  //主题帖
            {
                subject_count -= 1;
                post_count -= GetCount("parent_post_id=" + id);
                //先删除主题帖的回帖
                StringBuilder strSql1 = new StringBuilder();
                strSql1.Append("delete from " + databaseprefix + "forum_posts ");
                strSql1.Append(" where parent_post_id=@id ");
                SqlParameter[] parameters1 = {
					new SqlParameter("@id", SqlDbType.Int,4)};
                parameters1[0].Value = id;
                cmd = new CommandInfo(strSql1.ToString(), parameters1);
                sqllist.Add(cmd);
            }
            

            //删除帖子
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete from " + databaseprefix + "forum_posts ");
            strSql2.Append(" where id=@id ");
            SqlParameter[] parameters2 = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters2[0].Value = id;
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);


            //更改板块贴子数量
            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("update " + databaseprefix + "forum_board set ");
            strSql3.Append(" subject_count=subject_count+@subject_count, post_count=post_count+@post_count ");
            strSql3.Append(" where id = @board_id");
            SqlParameter[] parameters3 = {
                    new SqlParameter("@subject_count", SqlDbType.Int,4),
                    new SqlParameter("@post_count", SqlDbType.Int,4),
                    new SqlParameter("@board_id", SqlDbType.Int,4)};
            parameters3[0].Value = subject_count;
            parameters3[1].Value = post_count;
            parameters3[2].Value = board_id;
            cmd = new CommandInfo(strSql3.ToString(), parameters3);
            sqllist.Add(cmd);

            int rowsAffected = DbHelperSQL.ExecuteSqlTran(sqllist);
            if (rowsAffected > 0)
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
        public Model.forum_posts GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id,board_id,user_id,user_ip,parent_post_id,quote_id,title,zhaiyao,content,is_top,is_red,is_hot,is_lock,click,reply_count,reply_user_id,add_time,reply_time,post_type");
            strSql.Append(" from " + databaseprefix + "forum_posts");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.forum_posts model = new Model.forum_posts();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
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
            strSql.Append(" id,board_id,user_id,user_ip,parent_post_id,quote_id,title,zhaiyao,content,is_top,is_red,is_hot,is_lock,click,reply_count,reply_user_id,add_time,reply_time,post_type");
            strSql.Append(" FROM " + databaseprefix + "forum_posts ");
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
        public DataSet GetList(int board_id, int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + "forum_posts");
            if (board_id > 0)
            {
                strSql.Append(" where board_id in(select id from " + databaseprefix + "forum_board where class_list like '%," + board_id + ",%')");
            }
            if (strWhere.Trim() != "")
            {
                if (board_id > 0)
                {
                    strSql.Append(" and " + strWhere);
                }
                else
                {
                    strSql.Append(" where " + strWhere);
                }
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
        #endregion

        #region 扩展方法================================

        /// <summary>
        /// 返回信息标题
        /// </summary>
        public string GetTitle(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 title from " + databaseprefix + "forum_posts");
            strSql.Append(" where id=" + id);
            string title = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(title))
            {
                return string.Empty;
            }
            return title;
        }

        /// <summary>
        /// 返回信息内容
        /// </summary>
        public string GetContent(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 content from " + databaseprefix + "forum_posts");
            strSql.Append(" where id=" + id);
            string content = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(content))
            {
                return "";
            }
            return content;
        }

        /// <summary>
        /// 获取阅读次数
        /// </summary>
        public int GetClick(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 click from " + databaseprefix + "forum_posts");
            strSql.Append(" where id=" + id);
            string str = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            return Convert.ToInt32(str);
        }

        /// <summary>
        /// 返回数据总数
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from " + databaseprefix + "forum_posts");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }


        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "forum_posts set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        #endregion

        #region 私有方法================================
        /// <summary>
        /// 将对象转换为实体
        /// </summary>
        private Model.forum_posts DataRowToModel(DataRow row)
        {
            Model.forum_posts model = new Model.forum_posts();
            if (row != null)
            {
                #region 主表信息======================
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["board_id"] != null && row["board_id"].ToString() != "")
                {
                    model.board_id = int.Parse(row["board_id"].ToString());
                }
                if (row["user_id"] != null && row["user_id"].ToString() != "")
                {
                    model.user_id = int.Parse(row["user_id"].ToString());
                }
                if (row["user_ip"] != null)
                {
                    model.user_ip = row["user_ip"].ToString();
                }
                if (row["parent_post_id"] != null && row["parent_post_id"].ToString() != "")
                {
                    model.parent_post_id = int.Parse(row["parent_post_id"].ToString());
                }
                if (row["quote_id"] != null && row["quote_id"].ToString() != "")
                {
                    model.quote_id = int.Parse(row["quote_id"].ToString());
                }
                if (row["title"] != null)
                {
                    model.title = row["title"].ToString();
                }
                if (row["zhaiyao"] != null)
                {
                    model.zhaiyao = row["zhaiyao"].ToString();
                }
                if (row["content"] != null)
                {
                    model.content = row["content"].ToString();
                }
                if (row["is_top"] != null && row["is_top"].ToString() != "")
                {
                    model.is_top = int.Parse(row["is_top"].ToString());
                }
                if (row["is_red"] != null && row["is_red"].ToString() != "")
                {
                    model.is_red = int.Parse(row["is_red"].ToString());
                }
                if (row["is_hot"] != null && row["is_hot"].ToString() != "")
                {
                    model.is_hot = int.Parse(row["is_hot"].ToString());
                }
                if (row["is_lock"] != null && row["is_lock"].ToString() != "")
                {
                    model.is_lock = int.Parse(row["is_lock"].ToString());
                }
                if (row["click"] != null && row["click"].ToString() != "")
                {
                    model.click = int.Parse(row["click"].ToString());
                }
                if (row["reply_count"] != null && row["reply_count"].ToString() != "")
                {
                    model.reply_count = int.Parse(row["reply_count"].ToString());
                }
                if (row["reply_user_id"] != null && row["reply_user_id"].ToString() != "")
                {
                    model.reply_user_id = int.Parse(row["reply_user_id"].ToString());
                }
                if (row["add_time"] != null && row["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(row["add_time"].ToString());
                }
                if (row["reply_time"] != null && row["reply_time"].ToString() != "")
                {
                    model.reply_time = DateTime.Parse(row["reply_time"].ToString());
                }
                if (row["post_type"] != null && row["post_type"].ToString() != "")
                {
                    model.post_type = int.Parse(row["post_type"].ToString());
                }
                #endregion
            }
            return model;
        }

        #endregion

    }
}