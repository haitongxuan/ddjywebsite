using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using DTcms.DBUtility;
using DTcms.Common;
namespace DTcms.DAL
{
    //article_item
    public partial class article_item
    {
        private string databaseprefix; //数据库表名前缀
        public article_item(string _databaseprefix)
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
            strSql.Append("select count(1) from " + databaseprefix + "article_item");
            strSql.Append(" where ");
            strSql.Append(" id = @id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回数据总数
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from " + databaseprefix + "article_item");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(DTcms.Model.article_item model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "article_item(");
            strSql.Append("article_id,item_article_id,channel_id,item_channel_id,sort_id,item_title,item_img_url");
            strSql.Append(") values (");
            strSql.Append("@article_id,@item_article_id,@channel_id,@item_channel_id,@sort_id,@item_title,@item_img_url");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@article_id", SqlDbType.Int,4) ,
                        new SqlParameter("@item_article_id", SqlDbType.Int,4) ,
                        new SqlParameter("@channel_id", SqlDbType.Int,4) ,
                        new SqlParameter("@item_channel_id", SqlDbType.Int,4) ,
                        new SqlParameter("@sort_id", SqlDbType.Int,4) ,
                        new SqlParameter("@item_title", SqlDbType.VarChar,200) ,
                        new SqlParameter("@item_img_url", SqlDbType.VarChar,200)

            };

            parameters[0].Value = model.article_id;
            parameters[1].Value = model.item_article_id;
            parameters[2].Value = model.channel_id;
            parameters[3].Value = model.item_channel_id;
            parameters[4].Value = model.sort_id;
            parameters[5].Value = model.item_title;
            parameters[6].Value = model.item_img_url;

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
        public bool Update(DTcms.Model.article_item model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "article_item set ");

            strSql.Append(" article_id = @article_id , ");
            strSql.Append(" item_article_id = @item_article_id , ");
            strSql.Append(" channel_id = @channel_id , ");
            strSql.Append(" item_channel_id = @item_channel_id , ");
            strSql.Append(" sort_id = @sort_id , ");
            strSql.Append(" item_title = @item_title , ");
            strSql.Append(" item_img_url = @item_img_url  ");
            strSql.Append(" where id=@id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@id", SqlDbType.Int,4) ,
                        new SqlParameter("@article_id", SqlDbType.Int,4) ,
                        new SqlParameter("@item_article_id", SqlDbType.Int,4) ,
                        new SqlParameter("@channel_id", SqlDbType.Int,4) ,
                        new SqlParameter("@item_channel_id", SqlDbType.Int,4) ,
                        new SqlParameter("@sort_id", SqlDbType.Int,4) ,
                        new SqlParameter("@item_title", SqlDbType.VarChar,200) ,
                        new SqlParameter("@item_img_url", SqlDbType.VarChar,200)

            };

            parameters[0].Value = model.id;
            parameters[1].Value = model.article_id;
            parameters[2].Value = model.item_article_id;
            parameters[3].Value = model.channel_id;
            parameters[4].Value = model.item_channel_id;
            parameters[5].Value = model.sort_id;
            parameters[6].Value = model.item_title;
            parameters[7].Value = model.item_img_url;
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
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "article_item ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;


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
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "article_item ");
            strSql.Append(" where ID in (" + idlist + ")  ");
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
        public DTcms.Model.article_item GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id, article_id, item_article_id, channel_id, item_channel_id, sort_id, item_title, item_img_url  ");
            strSql.Append("  from " + databaseprefix + "article_item ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;


            DTcms.Model.article_item model = new DTcms.Model.article_item();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["article_id"].ToString() != "")
                {
                    model.article_id = int.Parse(ds.Tables[0].Rows[0]["article_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["item_article_id"].ToString() != "")
                {
                    model.item_article_id = int.Parse(ds.Tables[0].Rows[0]["item_article_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["channel_id"].ToString() != "")
                {
                    model.channel_id = int.Parse(ds.Tables[0].Rows[0]["channel_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["item_channel_id"].ToString() != "")
                {
                    model.item_channel_id = int.Parse(ds.Tables[0].Rows[0]["item_channel_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(ds.Tables[0].Rows[0]["sort_id"].ToString());
                }
                model.item_title = ds.Tables[0].Rows[0]["item_title"].ToString();
                model.item_img_url = ds.Tables[0].Rows[0]["item_img_url"].ToString();

                return model;
            }
            else
            {
                return null;
            }
        }

        DTcms.Model.article_item GetModel(string CallIndex)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id, article_id, item_article_id, channel_id, item_channel_id, sort_id, item_title, item_img_url  ");
            strSql.Append("  from " + databaseprefix + "article_item ");
            strSql.Append(" where CallIndex=@CallIndex");
            SqlParameter[] parameters = {
                    new SqlParameter("@CallIndex", SqlDbType.Int,4)
            };
            parameters[0].Value = CallIndex;

            DTcms.Model.article_item model = new DTcms.Model.article_item();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["article_id"].ToString() != "")
                {
                    model.article_id = int.Parse(ds.Tables[0].Rows[0]["article_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["item_article_id"].ToString() != "")
                {
                    model.item_article_id = int.Parse(ds.Tables[0].Rows[0]["item_article_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["channel_id"].ToString() != "")
                {
                    model.channel_id = int.Parse(ds.Tables[0].Rows[0]["channel_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["item_channel_id"].ToString() != "")
                {
                    model.item_channel_id = int.Parse(ds.Tables[0].Rows[0]["item_channel_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(ds.Tables[0].Rows[0]["sort_id"].ToString());
                }
                model.item_title = ds.Tables[0].Rows[0]["item_title"].ToString();
                model.item_img_url = ds.Tables[0].Rows[0]["item_img_url"].ToString();

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
            strSql.Append(" FROM " + databaseprefix + "article_item ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public List<Model.article_item> GetList(int article_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM " + databaseprefix + "article_item where article_id=" + article_id);
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            List<Model.article_item> list = new List<Model.article_item>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var model = new Model.article_item();
                    if (row["id"].ToString() != "")
                    {
                        model.id = int.Parse(row["id"].ToString());
                    }
                    if (row["article_id"].ToString() != "")
                    {
                        model.article_id = int.Parse(row["article_id"].ToString());
                    }
                    if (row["item_article_id"].ToString() != "")
                    {
                        model.item_article_id = int.Parse(row["item_article_id"].ToString());
                    }
                    if (row["channel_id"].ToString() != "")
                    {
                        model.channel_id = int.Parse(row["channel_id"].ToString());
                    }
                    if (row["item_channel_id"].ToString() != "")
                    {
                        model.item_channel_id = int.Parse(row["item_channel_id"].ToString());
                    }
                    if (row["sort_id"].ToString() != "")
                    {
                        model.sort_id = int.Parse(row["sort_id"].ToString());
                    }
                    model.item_title = row["item_title"].ToString();
                    model.item_img_url = row["item_img_url"].ToString();

                    list.Add(model);
                }
                return list;
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
            strSql.Append(" * ");
            strSql.Append(" FROM " + databaseprefix + "article_item ");
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
            strSql.Append("select * FROM " + databaseprefix + "article_item ");
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

