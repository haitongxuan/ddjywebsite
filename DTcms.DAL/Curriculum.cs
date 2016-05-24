using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using DTcms.DBUtility;
using DTcms.Common;
namespace DTcms.DAL
{
    //课程
    public partial class Curriculum
    {
        private string databaseprefix; //数据库表名前缀
        public Curriculum(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }
        #region 基本方法================================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int CurriculumId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "Curriculum");
            strSql.Append(" where ");
            strSql.Append(" CurriculumId = @CurriculumId  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@CurriculumId", SqlDbType.Int,4)
            };
            parameters[0].Value = CurriculumId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回数据总数
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from " + databaseprefix + "Curriculum");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(DTcms.Model.Curriculum model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "Curriculum(");
            strSql.Append("FullName,TeacherName,Describe,DeleteMark,Enable,Click,Status,IsMsg,IsTop,IsRed,IsHot,IsSlide,IsSys,CreateDate,CreateUserName,ModifyDate,ModifyUserName,ImgUrl");
            strSql.Append(") values (");
            strSql.Append("@FullName,@TeacherName,@Describe,@DeleteMark,@Enable,@Click,@Status,@IsMsg,@IsTop,@IsRed,@IsHot,@IsSlide,@IsSys,@CreateDate,@CreateUserName,@ModifyDate,@ModifyUserName,@ImgUrl");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@FullName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@TeacherName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Describe", SqlDbType.VarChar,200) ,
                        new SqlParameter("@DeleteMark", SqlDbType.Int,4) ,
                        new SqlParameter("@Enable", SqlDbType.Int,4) ,
                        new SqlParameter("@Click", SqlDbType.Int,4) ,
                        new SqlParameter("@Status", SqlDbType.Int,4) ,
                        new SqlParameter("@IsMsg", SqlDbType.Int,4) ,
                        new SqlParameter("@IsTop", SqlDbType.Int,4) ,
                        new SqlParameter("@IsRed", SqlDbType.Int,4) ,
                        new SqlParameter("@IsHot", SqlDbType.Int,4) ,
                        new SqlParameter("@IsSlide", SqlDbType.Int,4) ,
                        new SqlParameter("@IsSys", SqlDbType.Int,4) ,
                        new SqlParameter("@CreateDate", SqlDbType.DateTime) ,
                        new SqlParameter("@CreateUserName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ModifyDate", SqlDbType.DateTime) ,
                        new SqlParameter("@ModifyUserName", SqlDbType.VarChar,50),
                        new SqlParameter("@ImgUrl",SqlDbType.VarChar,200),

            };

            parameters[0].Value = model.FullName;
            parameters[1].Value = model.TeacherName;
            parameters[2].Value = model.Describe;
            parameters[3].Value = model.DeleteMark;
            parameters[4].Value = model.Enable;
            parameters[5].Value = model.Click;
            parameters[6].Value = model.Status;
            parameters[7].Value = model.IsMsg;
            parameters[8].Value = model.IsTop;
            parameters[9].Value = model.IsRed;
            parameters[10].Value = model.IsHot;
            parameters[11].Value = model.IsSlide;
            parameters[12].Value = model.IsSys;
            parameters[13].Value = model.CreateDate;
            parameters[14].Value = model.CreateUserName;
            parameters[15].Value = model.ModifyDate;
            parameters[16].Value = model.ModifyUserName;
            parameters[17].Value = model.ImgUrl;

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
        public bool Update(DTcms.Model.Curriculum model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "Curriculum set ");

            strSql.Append(" FullName = @FullName , ");
            strSql.Append(" TeacherName = @TeacherName , ");
            strSql.Append(" Describe = @Describe , ");
            strSql.Append(" DeleteMark = @DeleteMark , ");
            strSql.Append(" Enable = @Enable , ");
            strSql.Append(" Click = @Click , ");
            strSql.Append(" Status = @Status , ");
            strSql.Append(" IsMsg = @IsMsg , ");
            strSql.Append(" IsTop = @IsTop , ");
            strSql.Append(" IsRed = @IsRed , ");
            strSql.Append(" IsHot = @IsHot , ");
            strSql.Append(" IsSlide = @IsSlide , ");
            strSql.Append(" IsSys = @IsSys , ");
            strSql.Append(" CreateDate = @CreateDate , ");
            strSql.Append(" CreateUserName = @CreateUserName , ");
            strSql.Append(" ModifyDate = @ModifyDate , ");
            strSql.Append(" ModifyUserName = @ModifyUserName,  ");
            strSql.Append(" ImgUrl=@ImgUrl  ");
            strSql.Append(" where CurriculumId=@CurriculumId ");

            SqlParameter[] parameters = {
                        new SqlParameter("@CurriculumId", SqlDbType.Int,4) ,
                        new SqlParameter("@FullName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@TeacherName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Describe", SqlDbType.VarChar,200) ,
                        new SqlParameter("@DeleteMark", SqlDbType.Int,4) ,
                        new SqlParameter("@Enable", SqlDbType.Int,4) ,
                        new SqlParameter("@Click", SqlDbType.Int,4) ,
                        new SqlParameter("@Status", SqlDbType.Int,4) ,
                        new SqlParameter("@IsMsg", SqlDbType.Int,4) ,
                        new SqlParameter("@IsTop", SqlDbType.Int,4) ,
                        new SqlParameter("@IsRed", SqlDbType.Int,4) ,
                        new SqlParameter("@IsHot", SqlDbType.Int,4) ,
                        new SqlParameter("@IsSlide", SqlDbType.Int,4) ,
                        new SqlParameter("@IsSys", SqlDbType.Int,4) ,
                        new SqlParameter("@CreateDate", SqlDbType.DateTime) ,
                        new SqlParameter("@CreateUserName", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ModifyDate", SqlDbType.DateTime) ,
                        new SqlParameter("@ModifyUserName", SqlDbType.VarChar,50),
                        new SqlParameter("@ImgUrl",SqlDbType.VarChar,200),
            };

            parameters[0].Value = model.CurriculumId;
            parameters[1].Value = model.FullName;
            parameters[2].Value = model.TeacherName;
            parameters[3].Value = model.Describe;
            parameters[4].Value = model.DeleteMark;
            parameters[5].Value = model.Enable;
            parameters[6].Value = model.Click;
            parameters[7].Value = model.Status;
            parameters[8].Value = model.IsMsg;
            parameters[9].Value = model.IsTop;
            parameters[10].Value = model.IsRed;
            parameters[11].Value = model.IsHot;
            parameters[12].Value = model.IsSlide;
            parameters[13].Value = model.IsSys;
            parameters[14].Value = model.CreateDate;
            parameters[15].Value = model.CreateUserName;
            parameters[16].Value = model.ModifyDate;
            parameters[17].Value = model.ModifyUserName;
            parameters[18].Value = model.ImgUrl;
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
        public bool Delete(int CurriculumId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "Curriculum ");
            strSql.Append(" where CurriculumId=@CurriculumId");
            SqlParameter[] parameters = {
                    new SqlParameter("@CurriculumId", SqlDbType.Int,4)
            };
            parameters[0].Value = CurriculumId;


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
        public bool DeleteList(string CurriculumIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "Curriculum ");
            strSql.Append(" where ID in (" + CurriculumIdlist + ")  ");
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
        public DTcms.Model.Curriculum GetModel(int CurriculumId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CurriculumId, FullName, TeacherName, Describe, DeleteMark, Enable, Click, Status, IsMsg, IsTop, IsRed, IsHot, IsSlide, IsSys, CreateDate, CreateUserName, ModifyDate, ModifyUserName,ImgUrl  ");
            strSql.Append("  from " + databaseprefix + "Curriculum ");
            strSql.Append(" where CurriculumId=@CurriculumId");
            SqlParameter[] parameters = {
                    new SqlParameter("@CurriculumId", SqlDbType.Int,4)
            };
            parameters[0].Value = CurriculumId;


            DTcms.Model.Curriculum model = new DTcms.Model.Curriculum();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["CurriculumId"].ToString() != "")
                {
                    model.CurriculumId = int.Parse(ds.Tables[0].Rows[0]["CurriculumId"].ToString());
                }
                model.FullName = ds.Tables[0].Rows[0]["FullName"].ToString();
                model.TeacherName = ds.Tables[0].Rows[0]["TeacherName"].ToString();
                model.Describe = ds.Tables[0].Rows[0]["Describe"].ToString();
                if (ds.Tables[0].Rows[0]["DeleteMark"].ToString() != "")
                {
                    model.DeleteMark = int.Parse(ds.Tables[0].Rows[0]["DeleteMark"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Enable"].ToString() != "")
                {
                    model.Enable = int.Parse(ds.Tables[0].Rows[0]["Enable"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Click"].ToString() != "")
                {
                    model.Click = int.Parse(ds.Tables[0].Rows[0]["Click"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsMsg"].ToString() != "")
                {
                    model.IsMsg = int.Parse(ds.Tables[0].Rows[0]["IsMsg"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsTop"].ToString() != "")
                {
                    model.IsTop = int.Parse(ds.Tables[0].Rows[0]["IsTop"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsRed"].ToString() != "")
                {
                    model.IsRed = int.Parse(ds.Tables[0].Rows[0]["IsRed"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsHot"].ToString() != "")
                {
                    model.IsHot = int.Parse(ds.Tables[0].Rows[0]["IsHot"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsSlide"].ToString() != "")
                {
                    model.IsSlide = int.Parse(ds.Tables[0].Rows[0]["IsSlide"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsSys"].ToString() != "")
                {
                    model.IsSys = int.Parse(ds.Tables[0].Rows[0]["IsSys"].ToString());
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
                model.ImgUrl = ds.Tables[0].Rows[0]["ImgUrl"].ToString();

                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append("select CurriculumItemId, CurriculumId, FullName, Describe, Sort, DeleteMark, Enable, CreateDate, CreateUserName, ModifyDate, ModifyUserName, ImageUrl, MediaCode  ");
                strSql2.Append("  from " + databaseprefix + "CurriculumItem ");
                strSql2.Append(" where CurriculumId=@CurriculumId order by Sort");
                SqlParameter[] parameters2 = {
                    new SqlParameter("@CurriculumId", SqlDbType.Int,4)
            };
                parameters2[0].Value = CurriculumId;
                DataSet ds2 = DbHelperSQL.Query(strSql.ToString());
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds2.Tables[0].Rows)
                    {
                        var item = new Model.CurriculumItem();
                        if (row["CurriculumItemId"].ToString() != "")
                        {
                            item.CurriculumItemId = int.Parse(row["CurriculumItemId"].ToString());
                        }
                        if (row["CurriculumId"].ToString() != "")
                        {
                            item.CurriculumId = int.Parse(row["CurriculumId"].ToString());
                        }
                        model.FullName = row["FullName"].ToString();
                        model.Describe = row["Describe"].ToString();
                        if (row["Sort"].ToString() != "")
                        {
                            item.Sort = int.Parse(row["Sort"].ToString());
                        }
                        if (row["DeleteMark"].ToString() != "")
                        {
                            item.DeleteMark = int.Parse(row["DeleteMark"].ToString());
                        }
                        if (row["Enable"].ToString() != "")
                        {
                            item.Enable = int.Parse(row["Enable"].ToString());
                        }
                        if (row["CreateDate"].ToString() != "")
                        {
                            item.CreateDate = DateTime.Parse(row["CreateDate"].ToString());
                        }
                        model.CreateUserName = row["CreateUserName"].ToString();
                        if (row["ModifyDate"].ToString() != "")
                        {
                            item.ModifyDate = DateTime.Parse(row["ModifyDate"].ToString());
                        }
                        item.ModifyUserName = row["ModifyUserName"].ToString();
                        item.ImageUrl = row["ImageUrl"].ToString();
                        item.MediaCode = row["MediaCode"].ToString();
                        model.CurriculumItems.Add(item);
                    }
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
            strSql.Append(" FROM " + databaseprefix + "Curriculum ");
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
            strSql.Append(" FROM " + databaseprefix + "Curriculum ");
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
            strSql.Append("select * FROM " + databaseprefix + "Curriculum ");
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

