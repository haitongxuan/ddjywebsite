using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using DTcms.Common;

namespace DTcms.BLL
{
    //课程
    public partial class Curriculum
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息     
        private readonly DTcms.DAL.Curriculum dal;
        public Curriculum()
        {
            dal = new DTcms.DAL.Curriculum(siteConfig.edudatabaseprefix);
        }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int CurriculumId)
        {
            return dal.Exists(CurriculumId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(DTcms.Model.Curriculum model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DTcms.Model.Curriculum model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int CurriculumId)
        {

            return dal.Delete(CurriculumId);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string CurriculumIdlist)
        {
            return dal.DeleteList(CurriculumIdlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DTcms.Model.Curriculum GetModel(int CurriculumId)
        {

            return dal.GetModel(CurriculumId);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
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

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DTcms.Model.Curriculum> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DTcms.Model.Curriculum> DataTableToList(DataTable dt)
        {
            List<DTcms.Model.Curriculum> modelList = new List<DTcms.Model.Curriculum>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                DTcms.Model.Curriculum model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new DTcms.Model.Curriculum();

                    if (dt.Rows[n]["CurriculumId"].ToString() != "")
                    {
                        model.CurriculumId = int.Parse(dt.Rows[n]["CurriculumId"].ToString());
                    }

                    model.FullName = dt.Rows[n]["FullName"].ToString();

                    model.TeacherName = dt.Rows[n]["TeacherName"].ToString();

                    model.Describe = dt.Rows[n]["Describe"].ToString();

                    if (dt.Rows[n]["DeleteMark"].ToString() != "")
                    {
                        model.DeleteMark = int.Parse(dt.Rows[n]["DeleteMark"].ToString());
                    }

                    if (dt.Rows[n]["Enable"].ToString() != "")
                    {
                        model.Enable = int.Parse(dt.Rows[n]["Enable"].ToString());
                    }

                    if (dt.Rows[n]["Click"].ToString() != "")
                    {
                        model.Click = int.Parse(dt.Rows[n]["Click"].ToString());
                    }

                    if (dt.Rows[n]["Status"].ToString() != "")
                    {
                        model.Status = int.Parse(dt.Rows[n]["Status"].ToString());
                    }

                    if (dt.Rows[n]["IsMsg"].ToString() != "")
                    {
                        model.IsMsg = int.Parse(dt.Rows[n]["IsMsg"].ToString());
                    }

                    if (dt.Rows[n]["IsTop"].ToString() != "")
                    {
                        model.IsTop = int.Parse(dt.Rows[n]["IsTop"].ToString());
                    }

                    if (dt.Rows[n]["IsRed"].ToString() != "")
                    {
                        model.IsRed = int.Parse(dt.Rows[n]["IsRed"].ToString());
                    }

                    if (dt.Rows[n]["IsHot"].ToString() != "")
                    {
                        model.IsHot = int.Parse(dt.Rows[n]["IsHot"].ToString());
                    }

                    if (dt.Rows[n]["IsSlide"].ToString() != "")
                    {
                        model.IsSlide = int.Parse(dt.Rows[n]["IsSlide"].ToString());
                    }

                    if (dt.Rows[n]["IsSys"].ToString() != "")
                    {
                        model.IsSys = int.Parse(dt.Rows[n]["IsSys"].ToString());
                    }

                    if (dt.Rows[n]["CreateDate"].ToString() != "")
                    {
                        model.CreateDate = DateTime.Parse(dt.Rows[n]["CreateDate"].ToString());
                    }

                    model.CreateUserName = dt.Rows[n]["CreateUserName"].ToString();

                    if (dt.Rows[n]["ModifyDate"].ToString() != "")
                    {
                        model.ModifyDate = DateTime.Parse(dt.Rows[n]["ModifyDate"].ToString());
                    }

                    model.ModifyUserName = dt.Rows[n]["ModifyUserName"].ToString();
                    model.ImgUrl = dt.Rows[n]["ImgUrl"].ToString();


                    modelList.Add(model);
                }
            }
            return modelList;
        }
        #endregion

    }
}