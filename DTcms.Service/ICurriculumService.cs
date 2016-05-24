using System.Collections.Generic;
using System.Web;
using DTcms.Model;

namespace DTcms.Service
{
    public interface ICurriculumService
    {
        #region 列表========
        /// <summary>
        /// 获取课程列表
        /// </summary>
        /// <param name="top">显示条数，0全部</param>
        /// <param name="categoryId">类别编号，全部类别</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        List<Model.Curriculum> GetCurriculumList(int top, int categoryId, string strwhere);
        /// <summary>
        /// 获取课程列表
        /// </summary>
        /// <param name="top">显示条数，0全部</param>
        /// <param name="categoryId">类别编号，全部类别</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        List<Model.Curriculum> GetCurriculumList(int top, int categoryId, string strwhere, string orderby);
        /// <summary>
        /// 获取课程列表
        /// </summary>
        /// <param name="categoryId">类别编号，全部类别</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="strWhere">查询</param>
        /// <param name="orderby">排序</param>
        /// <param name="totalCount">总数</param>
        /// <returns></returns>
        List<Model.Curriculum> GetCurriculumList(int categoryId, int pageSize, int pageIndex,
            string strWhere, string orderby, out int totalCount);
        #endregion

        #region 关联课程列表================
        /// <summary>
        /// 获取关联课程列表
        /// </summary>
        /// <param name="top">显示个数</param>
        /// <param name="curriculumId">被关联课程主键</param>
        /// <param name="strwhere">查询语句</param>
        /// <returns></returns>
        List<Model.Curriculum> GetRelationCurriculumList(int top, int curriculumId, string strwhere);
        /// <summary>
        /// 获取关联课程列表
        /// </summary>
        /// <param name="top">显示个数</param>
        /// <param name="curriculumId">被关联课程主键</param>
        /// <param name="strwhere">查询语句</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        List<Model.Curriculum> GetRelationCurriculumList(int top, int curriculumId, string strwhere, string orderby);
        /// <summary>
        /// 获取关联课程列表
        /// </summary>
        /// <param name="curriculumId">被关联课程主键</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="strWhere">查询</param>
        /// <param name="orderby">排序</param>
        /// <param name="totalCount">总数</param>
        /// <returns></returns>
        List<Model.Curriculum> GetRelationCurriculumList(int curriculumId, int pageSize, int pageIndex,
            string strWhere, string orderby, out int totalCount);
        #endregion

        #region 内容===========
        /// <summary>
        /// 获取课程描述
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string GetCurriculumDescribe(int id);
        /// <summary>
        /// 获取课程封面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string GetCurriculumImgUrl(int id);
        /// <summary>
        /// 获取课程实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Model.Curriculum GetCurriculum(int id);

        #endregion

        #region 用户课程列表================

        List<Model.Curriculum> GetUserCurriculumList(int top, string username, string whereStr);

        List<Model.Curriculum> GetUserCurriculumList(int top, string username, int categoryId, string whereStr);

        List<Model.Curriculum> GetUserCurriculumList(string username, int pageSize, int pageIndex,
            string strWhere, string orderby, out int totalCount);

        List<Model.Curriculum> GetUserCurriculumList(string username, int categoryId, int pageSize,
            int pageIndex, string strWhere, string orderby, out int totalCount);

        #endregion

        #region 课程类别列表===========

        List<Model.Category> GetCurriculumCategories(int parentId, int type);

        #endregion

        #region 用户课程观看章节========
        /// <summary>
        /// 检查用户数是否观看过该课程
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="curriculumId">课程主键</param>
        /// <returns></returns>
        bool CheckUserCurriculum(string username, int curriculumId);
        /// <summary>
        /// 获取用户已经观看过的课程章节
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="curriculumId">课程主键</param>
        /// <returns></returns>
        List<Model.CurriculumItem> UserCurriculumItems(string username, int curriculumId);
        /// <summary>
        /// 用户观看课程内容，新增观看记录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="curriulumId">课程主键</param>
        /// <param name="curriculumItemId">课程章节主键</param>
        /// <returns></returns>
        void UserCurriculumAdd(string username, int curriulumId, int curriculumItemId);

        #endregion
        /// <summary>
        /// 检查课程是否需要vip身份才能观看
        /// </summary>
        /// <param name="curriculumId">课程主键</param>
        /// <returns></returns>
        bool CheckCurriculumVip(int curriculumId);
        /// <summary>
        /// 获取课程章节对应媒体地址
        /// </summary>
        /// <param name="mediaCode">媒体编号</param>
        /// <returns></returns>
        string GetMediaRealPath(string mediaCode);
        string GetMediaRealPath(string mediaCode, string username);
        /// <summary>
        /// 获取课程的时长
        /// </summary>
        /// <param name="curriculumId">课程主键</param>
        /// <returns></returns>
        string GetCurriculumTimeLength(int curriculumId);
    }
}