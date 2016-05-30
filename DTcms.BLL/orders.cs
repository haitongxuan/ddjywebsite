using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DTcms.Common;
using DTcms.DBUtility;

namespace DTcms.BLL
{
    /// <summary>
    /// 订单表
    /// </summary>
    public partial class orders
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
        private DAL.orders dal;
        public orders()
        {
            dal = new DAL.orders(siteConfig.sysdatabaseprefix);
        }

        #region 基本方法================================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string order_no)
        {
            return dal.Exists(order_no);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.orders model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.orders model)
        {
            //计算订单总金额:商品总金额+配送费用+支付手续费
            model.order_amount = model.real_amount + model.express_fee + model.payment_fee + model.invoice_taxes;
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            return dal.Delete(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.orders GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 根据订单号返回一个实体
        /// </summary>
        public Model.orders GetModel(string order_no)
        {
            return dal.GetModel(order_no);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        #endregion

        #region 扩展方法================================
        /// <summary>
        /// 返回数据数
        /// </summary>
        public int GetCount(string strWhere)
        {
            return dal.GetCount(strWhere);
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            dal.UpdateField(id, strValue);
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(string order_no, string strValue)
        {
            return dal.UpdateField(order_no, strValue);
        }


        public bool UpdatePaied(string order_no, string strValue)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql1 = new StringBuilder();
                        strSql1.Append("update " + siteConfig.sysdatabaseprefix + "orders set " + strValue);
                        strSql1.Append(" where order_no='@orderno';");
                        SqlParameter[] parameters1 =
                        {
                            new SqlParameter("@orderno", SqlDbType.NVarChar, 50)
                        };
                        parameters1[0].Value = order_no;
                        DbHelperSQL.GetSingle(conn, trans, strSql1.ToString(), parameters1); //带事务

                        var model = GetModel(order_no);
                        var user = new users().GetModel(model.user_name);
                        foreach (var g in model.order_goods)
                        {
                            var abll = new article();
                            if (abll.IsCard(g.article_id))
                            {
                                StringBuilder strSql = new StringBuilder();
                                var article = abll.GetModel(g.article_id);
                                string callindex = article.fields["cardcategorycallindex"];
                                var cardcategory = new BLL.CardCategory().GetModel(callindex);
                                var ug = new BLL.user_groups().GetModel(cardcategory.UserGroupCallIndex);
                                strSql.Append("insert into " + siteConfig.edudatabaseprefix + "Card(");
                                strSql.Append("CardCategoryId,Code,CreateDate,StartDate,EndDate");
                                strSql.Append(") values (");
                                strSql.Append("@CardCategoryId,@Code,@CreateDate,@StartDate,@EndDate");
                                strSql.Append(") ");
                                strSql.Append(";select @@IDENTITY");
                                SqlParameter[] parameters =
                                {
                                    new SqlParameter("@CardCategoryId", SqlDbType.Int, 4),
                                    new SqlParameter("@Code", SqlDbType.VarChar, 50),
                                    new SqlParameter("@CreateDate", SqlDbType.DateTime),
                                    new SqlParameter("@StartDate", SqlDbType.DateTime),
                                    new SqlParameter("@EndDate", SqlDbType.DateTime)

                                };
                                var startTime = DateTime.Now;
                                var endTime = DateTime.Now.AddDays((double)cardcategory.Duration);
                                parameters[0].Value = cardcategory.CardCategoryId;
                                parameters[1].Value = Utils.GetCheckCode(7); ;
                                parameters[2].Value = startTime;
                                parameters[3].Value = startTime;
                                parameters[4].Value = endTime;
                                object obj = DbHelperSQL.GetSingle(conn, trans, strSql.ToString(), parameters); //带事务

                                int id = Convert.ToInt32(obj);
                                StringBuilder strSqlUC = new StringBuilder();
                                strSqlUC.Append("insert into " + siteConfig.edudatabaseprefix + "UserCard(");
                                strSqlUC.Append("CardId,UserId,CardCategoryId");
                                strSqlUC.Append(") values (");
                                strSqlUC.Append("@CardId,@UserId,@CardCategoryId");
                                strSqlUC.Append(") ");
                                strSqlUC.Append(";select @@IDENTITY");
                                SqlParameter[] parametersUC =
                                {
                                    new SqlParameter("@CardId", SqlDbType.Int, 4),
                                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                                    new SqlParameter("@CardCategoryId", SqlDbType.Int, 4)
                                };

                                parametersUC[0].Value = id;
                                parametersUC[1].Value = model.user_id;
                                parametersUC[2].Value = cardcategory.CardCategoryId;

                                DbHelperSQL.GetSingle(conn, trans, strSqlUC.ToString(), parametersUC);

                                StringBuilder strSqlUU = new StringBuilder();
                                strSqlUU.Append("update " + siteConfig.sysdatabaseprefix + "users set ");
                                strSqlUU.Append(" group_id=@group_id,");
                                strSqlUU.Append(" group_start_time=@group_start_time,");
                                strSqlUU.Append(" group_end_time=@group_end_time ");
                                strSqlUU.Append(" where id=@id");
                                SqlParameter[] parametersUU =
                                {
                                    new SqlParameter("@group_id",SqlDbType.Int,4),
                                    new SqlParameter("@group_start_time",SqlDbType.DateTime),
                                    new SqlParameter("@group_end_time",SqlDbType.DateTime),
                                    new SqlParameter("@id",SqlDbType.Int,4)
                                };
                                parametersUU[0].Value = ug.id;
                                parametersUU[1].Value = startTime;
                                parametersUU[2].Value = endTime;
                                parametersUU[3].Value = user.id;
                                DbHelperSQL.GetSingle(conn, trans, strSqlUU.ToString(), parametersUU);
                            }
                        }
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
        #endregion
    }
}