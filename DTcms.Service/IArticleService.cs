using System.Collections.Generic;
using System.Web;
using DTcms.Common;

namespace DTcms.Service
{
    public interface IArticleService : IBaseService
    {
        #region no token===========

        #region 列表=====================

        /// <summary>
        /// 文章列表
        /// </summary>
        /// <param name="channelName">频道名称</param>
        /// <param name="top">显示条数</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns>DataTable</returns>
        List<Model.article> GetArticleList(string channelName, int top, string strWhere);

        /// <summary>
        /// 文章列表
        /// </summary>
        /// <param name="channelName">频道名称</param>
        /// <param name="categoryId">分类ID</param>
        /// <param name="top">显示条数</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns>DataTable</returns>
        List<Model.article> GetArticleList(string channelName, int categoryId, int top, string strWhere);

        /// <summary>
        /// 文章列表
        /// </summary>
        /// <param name="channel_name">频道名称</param>
        /// <param name="category_id">分类ID</param>
        /// <param name="top">显示条数</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        List<Model.article> GetArticleList(string channelName, int categoryId, int top, string strWhere, string orderby);

        /// <summary>
        /// 文章分页列表(自定义页面大小)
        /// </summary>
        /// <param name="channel_name">频道名称</param>
        /// <param name="category_id">分类ID</param>
        /// <param name="page_size">页面大小</param>
        /// <param name="page_index">当前页码</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="totalcount">总记录数</param>
        /// <returns></returns>
        List<Model.article> GetArticleList(string channelName, int categoryId, int pageSize, int pageIndex,
            string strWhere, string orderby, out int totalCount);
        
        /// <summary>
        /// 根据频道及规格获得分页列表
        /// </summary>
        /// <param name="channelName">频道名称</param>
        /// <param name="categoryId">分类ID</param>
        /// <param name="speIds">规格列表</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="totalcount">总记录数</param>
        /// <returns>DataTable</returns>
        List<Model.article> GetArticleList(string channelName, int categoryId, Dictionary<string, string> speIds,
            int pageSize, int pageIndex, string strwhere, string orderby, out int totalcount);

        /// <summary>
        /// 文章Tags标签列表
        /// </summary>
        /// <param name="top">显示条数</param>
        /// <param name="strwhere">查询条件</param>
        /// <returns>DataTable</returns>
        List<Model.article> GetArticleTags(int top, string strwhere);

        #endregion

        #region 内容====================

        /// <summary>
        /// 根据调用标识取得内容
        /// </summary>
        /// <param name="callIndex">调用别名</param>
        /// <returns>String</returns>
        string GetArticleContent(string callIndex);

        /// <summary>
        /// 获取对应的图片路径
        /// </summary>
        /// <param name="articleId">信息ID</param>
        /// <returns>String</returns>
        string GetArticleImgUrl(int articleId);

        /// <summary>
        /// 获取文章扩展属性
        /// </summary>
        /// <param name="articleId">文章主键</param>
        /// <param name="fieldName">扩展属性名称</param>
        /// <returns></returns>
        string GetArticleField(int articleId, string fieldName);

        /// <summary>
        /// 获取文章扩展属性
        /// </summary>
        /// <param name="callIndex">调用名称</param>
        /// <param name="fieldName">扩展属性名称</param>
        /// <returns></returns>
        string GetArticleField(string callIndex, string fieldName);

        /// <summary>
        /// 获取文章对象
        /// </summary>
        /// <param name="articleId">调用名称</param>
        /// <returns></returns>
        Model.article GetArticle(int articleId);

        /// <summary>
        /// 获取文章对象
        /// </summary>
        /// <param name="callIndex">调用名称</param>
        /// <returns></returns>
        Model.article GetArticle(string callIndex);

        /// <summary>
        /// 获取上一条下一条的编号
        /// </summary>
        /// <param name="type">-1代表上一条，1代表下一条</param>
        /// <param name="articleId">文章主键</param>
        /// <returns>Int</returns>
        int GetPrevandNextArticleId( int type, int articleId);

        #endregion

        #region 统计信息===============

        #region 统计及输出阅读次数===========================

        /// <summary>
        /// 统计及输出阅读次数
        /// </summary>
        /// <param name="articleId">文章主键</param>
        /// <returns></returns>
        int ViewArticleClick(int articleId);

        #endregion
        /// <summary>
        /// 增加文章点击量
        /// </summary>
        /// <param name="artcleId"></param>
        void ArticleClickAdd(int artcleId);

        #region 输出评论总数=================================

        /// <summary>
        /// 输出评论总数
        /// </summary>
        /// <param name="articleId">文章主键</param>
        /// <returns></returns>
        int ViewCommentCount(int articleId);

        #endregion

        #region 输出附件下载总数=============================

        /// <summary>
        /// 输出附件下载总数
        /// </summary>
        /// <param name="articleAttachId">附件主键</param>
        /// <returns></returns>
        int ViewAttachCount(int articleAttachId);

        #endregion


        #endregion

        #region 文章评论===============

        /// <summary>
        /// 提交评论的处理方法
        /// </summary>
        /// <param name="articleId">文章主键</param>
        /// <param name="content">评论内容</param>
        /// <param name="username">用户名</param>
        void CommentAdd(string username, int articleId, string content);

        /// <summary>
        /// 取得评论列表方法
        /// </summary>
        /// <param name="articleId">文章主键</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">分页数量</param>
        /// <param name="totalCount">总数</param>
        /// <returns></returns>
        List<Model.article_comment> CommentList(int articleId, int pageIndex, int pageSize,out int totalCount);
        #endregion

        /// <summary>
        /// 获取文章类型列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        List<Model.article_category> GetArticleCategories(int parentId, int type);

        #endregion
    }

}