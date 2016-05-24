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
    /// 数据访问类:内容类别
    /// </summary>
    public partial class forum_board
    {
        private string databaseprefix; //数据库表名前缀
        public forum_board(string _databaseprefix)
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
            strSql.Append("select count(1) from " + databaseprefix + "forum_board");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回板块名称
        /// </summary>
        public string GetBoardName(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 boardname from " + databaseprefix + "forum_board");
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
        public int Add(Model.forum_board model)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("insert into " + databaseprefix + "forum_board(");
                        strSql.Append("boardname,parent_id,class_list,class_layer,sort_id,img_url,content,is_lock,allow_usergroupid_list,moderator_list,subject_count,post_count)");
                        strSql.Append(" values (");
                        strSql.Append("@boardname,@parent_id,@class_list,@class_layer,@sort_id,@img_url,@content,@is_lock,@allow_usergroupid_list,@moderator_list,@subject_count,@post_count)");
                        strSql.Append(";select @@IDENTITY");
                        SqlParameter[] parameters = {
					            new SqlParameter("@boardname", SqlDbType.NVarChar,100),
					            new SqlParameter("@parent_id", SqlDbType.Int,4),
					            new SqlParameter("@class_list", SqlDbType.NVarChar,500),
					            new SqlParameter("@class_layer", SqlDbType.Int,4),
					            new SqlParameter("@sort_id", SqlDbType.Int,4),
					            new SqlParameter("@img_url", SqlDbType.NVarChar,255),
					            new SqlParameter("@content", SqlDbType.NText),
                                new SqlParameter("@is_lock", SqlDbType.Int,4),
                                new SqlParameter("@allow_usergroupid_list", SqlDbType.NVarChar,255),
					            new SqlParameter("@moderator_list", SqlDbType.NVarChar,255),
                                new SqlParameter("@subject_count", SqlDbType.Int,4),
                                new SqlParameter("@post_count", SqlDbType.Int,4)};
                        parameters[0].Value = model.boardname;
                        parameters[1].Value = model.parent_id;
                        parameters[2].Value = model.class_list;
                        parameters[3].Value = model.class_layer;
                        parameters[4].Value = model.sort_id;
                        parameters[5].Value = model.img_url;
                        parameters[6].Value = model.content;
                        parameters[7].Value = model.is_lock;
                        parameters[8].Value = model.allow_usergroupid_list;
                        parameters[9].Value = model.moderator_list;
                        parameters[10].Value = model.subject_count;
                        parameters[11].Value = model.post_count;

                        object obj = DbHelperSQL.GetSingle(conn, trans, strSql.ToString(), parameters); //带事务
                        model.id = Convert.ToInt32(obj);

                        if (model.parent_id > 0)
                        {
                            Model.forum_board model2 = GetModel(conn, trans, model.parent_id); //带事务
                            model.class_list = model2.class_list + model.id + ",";
                            model.class_layer = model2.class_layer + 1;
                        }
                        else
                        {
                            model.class_list = "," + model.id + ",";
                            model.class_layer = 1;
                        }
                        //修改节点列表和深度
                        DbHelperSQL.ExecuteSql(conn, trans, "update " + databaseprefix + "forum_board set class_list='" + model.class_list + "', class_layer=" + model.class_layer + " where id=" + model.id); //带事务
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
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "forum_board set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.forum_board model)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        //先判断选中的父节点是否被包含
                        if (IsContainNode(model.id, model.parent_id))
                        {
                            //查找旧数据
                            Model.forum_board oldModel = GetModel(model.id);
                            //查找旧父节点数据
                            string class_list = "," + model.parent_id + ",";
                            int class_layer = 1;
                            if (oldModel.parent_id > 0)
                            {
                                Model.forum_board oldParentModel = GetModel(conn, trans, oldModel.parent_id); //带事务
                                class_list = oldParentModel.class_list + model.parent_id + ",";
                                class_layer = oldParentModel.class_layer + 1;
                            }
                            //先提升选中的父节点
                            DbHelperSQL.ExecuteSql(conn, trans, "update " + databaseprefix + "forum_board set parent_id=" + oldModel.parent_id + ",class_list='" + class_list + "', class_layer=" + class_layer + " where id=" + model.parent_id); //带事务
                            UpdateChilds(conn, trans, model.parent_id); //带事务
                        }
                        //更新子节点
                        if (model.parent_id > 0)
                        {
                            Model.forum_board model2 = GetModel(conn, trans, model.parent_id); //带事务
                            model.class_list = model2.class_list + model.id + ",";
                            model.class_layer = model2.class_layer + 1;
                        }
                        else
                        {
                            model.class_list = "," + model.id + ",";
                            model.class_layer = 1;
                        }


                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("update " + databaseprefix + "forum_board set ");
                        strSql.Append("boardname=@boardname,");
                        strSql.Append("parent_id=@parent_id,");
                        strSql.Append("class_list=@class_list,");
                        strSql.Append("class_layer=@class_layer,");
                        strSql.Append("sort_id=@sort_id,");
                        strSql.Append("img_url=@img_url,");
                        strSql.Append("content=@content,");
                        strSql.Append("is_lock=@is_lock,");
                        strSql.Append("allow_usergroupid_list=@allow_usergroupid_list,");
                        strSql.Append("moderator_list=@moderator_list,");
                        strSql.Append("subject_count=@subject_count,");
                        strSql.Append("post_count=@post_count");
                        strSql.Append(" where id=@id");
                        SqlParameter[] parameters = {
					            new SqlParameter("@boardname", SqlDbType.NVarChar,100),
					            new SqlParameter("@parent_id", SqlDbType.Int,4),
					            new SqlParameter("@class_list", SqlDbType.NVarChar,500),
					            new SqlParameter("@class_layer", SqlDbType.Int,4),
					            new SqlParameter("@sort_id", SqlDbType.Int,4),
					            new SqlParameter("@img_url", SqlDbType.NVarChar,255),
					            new SqlParameter("@content", SqlDbType.NText),
                                new SqlParameter("@is_lock", SqlDbType.Int,4),
                                new SqlParameter("@allow_usergroupid_list", SqlDbType.NVarChar,255),
					            new SqlParameter("@moderator_list", SqlDbType.NVarChar,255),
                                new SqlParameter("@subject_count", SqlDbType.Int,4),
                                new SqlParameter("@post_count", SqlDbType.Int,4),
					            new SqlParameter("@id", SqlDbType.Int,4)};
                        parameters[0].Value = model.boardname;
                        parameters[1].Value = model.parent_id;
                        parameters[2].Value = model.class_list;
                        parameters[3].Value = model.class_layer;
                        parameters[4].Value = model.sort_id;
                        parameters[5].Value = model.img_url;
                        parameters[6].Value = model.content;
                        parameters[7].Value = model.is_lock;
                        parameters[8].Value = model.allow_usergroupid_list;
                        parameters[9].Value = model.moderator_list;
                        parameters[10].Value = model.subject_count;
                        parameters[11].Value = model.post_count;
                        parameters[12].Value = model.id;
                        DbHelperSQL.ExecuteSql(conn, trans, strSql.ToString(), parameters);

                        //更新子节点
                        UpdateChilds(conn, trans, model.id);
                        trans.Commit();
                    }
                    catch (Exception ex)
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
            List<CommandInfo> sqllist = new List<CommandInfo>();

            //删除主表
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "forum_board ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
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
        public Model.forum_board GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,boardname,parent_id,class_list,class_layer,sort_id,img_url,content,is_lock,allow_usergroupid_list,moderator_list,subject_count,post_count");
            strSql.Append(" from " + databaseprefix + "forum_board ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.forum_board model = new Model.forum_board();
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
        /// 得到一个对象实体(重载，带事务)
        /// </summary>
        public Model.forum_board GetModel(SqlConnection conn, SqlTransaction trans, int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1  id,boardname,parent_id,class_list,class_layer,sort_id,img_url,content,is_lock,allow_usergroupid_list,moderator_list,subject_count,post_count");
            strSql.Append(" from " + databaseprefix + "forum_board ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.forum_board model = new Model.forum_board();
            DataSet ds = DbHelperSQL.Query(conn, trans, strSql.ToString(), parameters);
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
        /// 取得指定类别下的列表
        /// </summary>
        /// <param name="parent_id">父ID</param>
        /// <param name="channel_id">频道ID</param>
        /// <returns></returns>
        public DataTable GetChildList(int parent_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,boardname,parent_id,class_list,class_layer,sort_id,img_url,content,is_lock,allow_usergroupid_list,moderator_list,subject_count,post_count");
            strSql.Append(" from " + databaseprefix + "forum_board");
            strSql.Append(" where parent_id=" + parent_id + " order by sort_id asc,id desc");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 取得所有类别列表
        /// </summary>
        /// <param name="parent_id">父ID</param>
        /// <param name="channel_id">频道ID</param>
        /// <returns></returns>
        public DataTable GetAllList(int parent_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,boardname,parent_id,class_list,class_layer,sort_id,img_url,content,is_lock,allow_usergroupid_list,moderator_list,subject_count,post_count");
            strSql.Append(" from " + databaseprefix + "forum_board");
            strSql.Append(" order by sort_id asc,id desc");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            DataTable oldData = ds.Tables[0] as DataTable;
            if (oldData == null)
            {
                return null;
            }
            //复制结构
            DataTable newData = oldData.Clone();
            //调用迭代组合成DAGATABLE
            GetChilds(oldData, newData, parent_id);
            return newData;
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + "forum_board");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
        #endregion

        #region 扩展方法================================

        public int GetParentId(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 parent_id from " + databaseprefix + "forum_board");
            strSql.Append(" where id=" + id);
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }
        #endregion

        #region 私有方法================================
        /// <summary>
        /// 将对象转换为实体
        /// </summary>
        public Model.forum_board DataRowToModel(DataRow row)
        {
            Model.forum_board model = new Model.forum_board();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["boardname"] != null)
                {
                    model.boardname = row["boardname"].ToString();
                }
                if (row["parent_id"] != null && row["parent_id"].ToString() != "")
                {
                    model.parent_id = int.Parse(row["parent_id"].ToString());
                }
                if (row["class_list"] != null)
                {
                    model.class_list = row["class_list"].ToString();
                }
                if (row["class_layer"] != null && row["class_layer"].ToString() != "")
                {
                    model.class_layer = int.Parse(row["class_layer"].ToString());
                }
                if (row["sort_id"] != null && row["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(row["sort_id"].ToString());
                }
                if (row["img_url"] != null)
                {
                    model.img_url = row["img_url"].ToString();
                }
                if (row["content"] != null)
                {
                    model.content = row["content"].ToString();
                }
                if (row["is_lock"] != null && row["is_lock"].ToString() != "")
                {
                    model.is_lock = int.Parse(row["is_lock"].ToString());
                }
                if (row["allow_usergroupid_list"] != null)
                {
                    model.allow_usergroupid_list = row["allow_usergroupid_list"].ToString();
                }
                if (row["moderator_list"] != null)
                {
                    model.moderator_list = row["moderator_list"].ToString();
                }
                if (row["subject_count"] != null && row["subject_count"].ToString() != "")
                {
                    model.subject_count = int.Parse(row["subject_count"].ToString());
                }
                if (row["post_count"] != null && row["post_count"].ToString() != "")
                {
                    model.post_count = int.Parse(row["post_count"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 从内存中取得所有下级类别列表（自身迭代）
        /// </summary>
        private void GetChilds(DataTable oldData, DataTable newData, int parent_id)
        {
            DataRow[] dr = oldData.Select("parent_id=" + parent_id);
            for (int i = 0; i < dr.Length; i++)
            {
                //添加一行数据
                DataRow row = newData.NewRow();
                row["id"] = int.Parse(dr[i]["id"].ToString());
                row["boardname"] = dr[i]["boardname"].ToString();
                row["parent_id"] = int.Parse(dr[i]["parent_id"].ToString());
                row["class_list"] = dr[i]["class_list"].ToString();
                row["class_layer"] = int.Parse(dr[i]["class_layer"].ToString());
                row["sort_id"] = int.Parse(dr[i]["sort_id"].ToString());
                row["img_url"] = dr[i]["img_url"].ToString();
                row["content"] = dr[i]["content"].ToString();
                row["is_lock"] = int.Parse(dr[i]["is_lock"].ToString());
                row["allow_usergroupid_list"] = dr[i]["allow_usergroupid_list"].ToString();
                row["moderator_list"] = dr[i]["moderator_list"].ToString();
                row["subject_count"] = int.Parse(dr[i]["subject_count"].ToString());
                row["post_count"] = int.Parse(dr[i]["post_count"].ToString());
                newData.Rows.Add(row);
                //调用自身迭代
                this.GetChilds(oldData, newData, int.Parse(dr[i]["id"].ToString()));
            }
        }

        /// <summary>
        /// 修改子节点的ID列表及深度（自身迭代）
        /// </summary>
        /// <param name="parent_id"></param>
        private void UpdateChilds(SqlConnection conn, SqlTransaction trans, int parent_id)
        {
            //查找父节点信息
            Model.forum_board model = GetModel(conn, trans, parent_id);
            if (model != null)
            {
                //查找子节点
                string strSql = "select id from " + databaseprefix + "forum_board where parent_id=" + parent_id;
                DataSet ds = DbHelperSQL.Query(conn, trans, strSql); //带事务
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    //修改子节点的ID列表及深度
                    int id = int.Parse(dr["id"].ToString());
                    string class_list = model.class_list + id + ",";
                    int class_layer = model.class_layer + 1;
                    DbHelperSQL.ExecuteSql(conn, trans, "update " + databaseprefix + "forum_board set class_list='" + class_list + "', class_layer=" + class_layer + " where id=" + id); //带事务

                    //调用自身迭代
                    this.UpdateChilds(conn, trans, id); //带事务
                }
            }
        }

        /// <summary>
        /// 验证节点是否被包含
        /// </summary>
        /// <param name="id">待查询的节点</param>
        /// <param name="parent_id">父节点</param>
        /// <returns></returns>
        private bool IsContainNode(int id, int parent_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "forum_board ");
            strSql.Append(" where class_list like '%," + id + ",%' and id=" + parent_id);
            return DbHelperSQL.Exists(strSql.ToString());
        }

        #endregion
    }
}
