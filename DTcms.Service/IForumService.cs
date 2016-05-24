using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace DTcms.Service
{
    public interface IForumService
    {
        /// <summary>
        /// 获取板块名称
        /// </summary>
        /// <param name="boardId"></param>
        /// <returns></returns>
        string GetBoardName(int boardId);
        /// <summary>
        /// 获取所有板块列表
        /// </summary>
        /// <returns>DataTable</returns>
        List<Model.forum_board> GetBoardList(int parentId);

        /// <summary>
        /// 取得该频道指定类别下的列表
        /// </summary>
        /// <returns>DataTable</returns>
        List<Model.forum_board> GetBoardGetchildlist(int parentId);

        /// <summary>
        /// 返回父类别ID
        /// </summary>
        /// <param name="categoryId">类别ID</param>
        /// <returns>String</returns>
        string GetCategoryId(int categoryId);
        /// <summary>
        /// 获取帖子列表
        /// </summary>
        /// <param name="boardId"></param>
        /// <param name="top"></param>
        /// <param name="whereStr"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        List<Model.forum_posts> GetPostList(int boardId, int top, string whereStr);

        List<Model.forum_posts> GetPostList(int boardId, int pageSize, int pageIndex, string whereStr,
             out int totalCount);
        /// <summary>
        /// 获取跟帖分页列表
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="username"></param>
        /// <param name="top"></param>
        /// <param name="whereStr"></param>
        /// <returns></returns>
        List<Model.forum_posts> GetReplyList(int boardId, int postId, string username, int pageSize, int pageIndex,
            string whereStr, out int totalCount);

        /// <summary>
        /// 递归找到父节点
        /// </summary>
        /// <param name="strTxt"></param>
        /// <param name="categoryId">类别主键</param>
        void LoopChannelMenu(StringBuilder strTxt, int categoryId);
        /// <summary>
        /// 发布帖子
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="content">内容</param>
        /// <param name="title">标题</param>
        /// <param name="boardId">板块主键</param>
        /// <param name="postId">帖子主键</param>
        void PostAdd(string username, string title, string content, int boardId, int postId);
        /// <summary>
        /// 回复帖子
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="content">内容</param>
        /// <param name="title">标题</param>
        /// <param name="boardId">板块主键</param>
        /// <param name="postId">帖子主键</param>
        void PostReply(string username, string title, string content, int boardId, int postId);
        /// <summary>
        /// 编辑帖子
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="content">内容</param>
        /// <param name="title">标题</param>
        /// <param name="postId">帖子主键</param>
        void PostEdit(string username, string title, string content, int postId);
        /// <summary>
        /// 移动帖子
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="postId">帖子主键</param>
        /// <param name="toBoardId">板块主键</param>
        /// <param name="opRemark">操作备注</param>
        void PostMove(string username, int postId, int toBoardId, string opRemark);
        /// <summary>
        /// 删除帖子
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="postId">帖子主键</param>
        /// <param name="opRemark">操作备注</param>
        void PostDelete(string username, int postId, string opRemark);
        /// <summary>
        /// 锁定帖子
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="postId">帖子主键</param>
        /// <param name="optIp">操作说明</param>
        /// <param name="opRemark">操作备注</param>
        void PostSetLock(string username, int postId, string optIp, string opRemark);
        /// <summary>
        /// 置顶帖子
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="postId">帖子主键</param>
        /// <param name="optIp">操作说明</param>
        /// <param name="opRemark">操作备注</param>
        void PostSetTop(string username, int postId, string optIp, string opRemark);
        /// <summary>
        /// 设置精华
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="postId">帖子主键</param>
        /// <param name="optIp">操作说明</param>
        /// <param name="opRemark">操作备注</param>
        void PostSetRed(string username, int postId, string optIp, string opRemark);
        /// <summary>
        /// 设置热门
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="postId">帖子主键</param>
        /// <param name="optIp">操作说明</param>
        /// <param name="opRemark">操作备注</param>
        void PostSetHot(string username, int postId, string optIp, string opRemark);
        /// <summary>
        /// 判断是否版主
        /// </summary>
        /// <param name="boardid">板块主键</param>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        bool IsModerator(int boardid, string username);
    }
}