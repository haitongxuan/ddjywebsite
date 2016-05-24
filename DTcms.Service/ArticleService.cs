using System.Collections.Generic;
using System.Web;
using DTcms.Model;

namespace DTcms.Service
{
    public class ArticleService : BaseService, IArticleService
    {
        public List<article> GetArticleList(string channelName, int top, string strWhere)
        {
            throw new System.NotImplementedException();
        }

        public List<article> GetArticleList(string channelName, int categoryId, int top, string strWhere)
        {
            throw new System.NotImplementedException();
        }

        public List<article> GetArticleList(string channelName, int categoryId, int top, string strWhere, string @orderby)
        {
            throw new System.NotImplementedException();
        }

        public List<article> GetArticleList(string channelName, int categoryId, int pageSize, int pageIndex, string strWhere, string @orderby,
            out int totalCount)
        {
            throw new System.NotImplementedException();
        }

        public List<article> GetArticleList(string channelName, int categoryId, Dictionary<string, string> speIds, int pageSize, int pageIndex, string strwhere,
            string @orderby, out int totalcount)
        {
            throw new System.NotImplementedException();
        }

        public List<article> GetArticleTags(int top, string strwhere)
        {
            throw new System.NotImplementedException();
        }

        public string GetArticleContent(string callIndex)
        {
            throw new System.NotImplementedException();
        }

        public string GetArticleImgUrl(int articleId)
        {
            throw new System.NotImplementedException();
        }

        public string GetArticleField(int articleId, string fieldName)
        {
            throw new System.NotImplementedException();
        }

        public string GetArticleField(string callIndex, string fieldName)
        {
            throw new System.NotImplementedException();
        }

        public article GetArticle(int articleId)
        {
            throw new System.NotImplementedException();
        }

        public article GetArticle(string callIndex)
        {
            throw new System.NotImplementedException();
        }

        public int GetPrevandNextArticleId(int type, int articleId)
        {
            throw new System.NotImplementedException();
        }

        public int ViewArticleClick(int articleId)
        {
            throw new System.NotImplementedException();
        }

        public void ArticleClickAdd(int artcleId)
        {
            throw new System.NotImplementedException();
        }

        public int ViewCommentCount(int articleId)
        {
            throw new System.NotImplementedException();
        }

        public int ViewAttachCount(int articleAttachId)
        {
            throw new System.NotImplementedException();
        }

        public void CommentAdd(string username, int articleId, string content)
        {
            throw new System.NotImplementedException();
        }

        public List<article_comment> CommentList(int articleId, int pageIndex, int pageSize, out int totalCount)
        {
            throw new System.NotImplementedException();
        }

        public List<Model.article_category> GetArticleCategories(int parentId, int type)
        {
            throw new System.NotImplementedException();
        }
    }
}