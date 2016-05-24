using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTcms.Model
{
    /// <summary>
    /// 论坛帖子实体类
    /// </summary>
    [Serializable]
    public class forum_posts
    {
        public forum_posts()
        {}

        #region Model
        private int _id;
        private int _board_id = 0;
        private int _user_id = 0;
        private string _user_ip = string.Empty;
        private int _parent_post_id = 0;
        private int _quote_id = 0;
        private int _class_layer = 0;
        private int _sort_id = 1;
        private string _title = string.Empty;
        private string _zhaiyao = string.Empty;
        private string _content = string.Empty;
        private int _is_top = 0;
        private int _is_red = 0;
        private int _is_hot = 0;
        private int _is_lock = 0;
        private int _click = 0;
        private int _reply_count = 0;
        private int _reply_user_id = 0;
        private DateTime _add_time = DateTime.Now;
        private DateTime? _reply_time;
        private int _post_type = 0;
        
        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 版块ID
        /// </summary>
        public int board_id
        {
            get { return _board_id; }
            set { _board_id = value; }
        }
        /// <summary>
        /// 发帖用户ID
        /// </summary>
        public int user_id
        {
            get { return _user_id; }
            set { _user_id = value; }
        }
        /// <summary>
        /// 发帖用户IP
        /// </summary>
        public string user_ip
        {
            get { return _user_ip; }
            set { _user_ip = value; }
        }
        /// <summary>
        /// 主题帖ID,如果为0则自己为主题贴
        /// </summary>
        public int parent_post_id
        {
            get { return _parent_post_id; }
            set { _parent_post_id = value; }
        }
        /// <summary>
        /// 引用的帖子ID,即在同一主题下回复指定的帖子
        /// </summary>
        public int quote_id
        {
            get { return _quote_id; }
            set { _quote_id = value; }
        }
        /// <summary>
        /// 帖子深度
        /// </summary>
        public int class_layer
        {
            set { _class_layer = value; }
            get { return _class_layer; }
        }
        /// <summary>
        /// 排序数字
        /// </summary>
        public int sort_id
        {
            set { _sort_id = value; }
            get { return _sort_id; }
        }
        /// <summary>
        /// 帖子标题
        /// </summary>
        public string title
        {
            get { return _title; }
            set { _title = value; }
        }
        /// <summary>
        /// 帖子摘要
        /// </summary>
        public string zhaiyao
        {
            get { return _zhaiyao; }
            set { _zhaiyao = value; }
        }
        /// <summary>
        /// 帖子内容
        /// </summary>
        public string content
        {
            get { return _content; }
            set { _content = value; }
        }
        /// <summary>
        /// 是否置顶
        /// </summary>
        public int is_top
        {
            get { return _is_top; }
            set { _is_top = value; }
        }
        /// <summary>
        /// 是否精华
        /// </summary>
        public int is_red
        {
            get { return _is_red; }
            set { _is_red = value; }
        }
        /// <summary>
        /// 是否热门
        /// </summary>
        public int is_hot
        {
            get { return _is_hot; }
            set { _is_hot = value; }
        }
        /// <summary>
        /// 是否隐藏
        /// </summary>
        public int is_lock
        {
            get { return _is_lock; }
            set { _is_lock = value; }
        }
        /// <summary>
        /// 帖子查看次数
        /// </summary>
        public int click
        {
            get { return _click; }
            set { _click = value; }
        }
        /// <summary>
        /// 帖子回复数
        /// </summary>
        public int reply_count
        {
            get { return _reply_count; }
            set { _reply_count = value; }
        }
        /// <summary>
        /// 最后回复用户ID
        /// </summary>
        public int reply_user_id
        {
            get { return _reply_user_id; }
            set { _reply_user_id = value; }
        }
        /// <summary>
        /// 发帖时间
        /// </summary>
        public DateTime add_time
        {
            get { return _add_time; }
            set { _add_time = value; }
        }
        /// <summary>
        /// 最后回复时间
        /// </summary>
        public DateTime? reply_time
        {
            set { _reply_time = value; }
            get { return _reply_time; }
        }
        /// <summary>
        /// 帖子类型
        /// </summary>
        public int post_type
        {
            get { return _post_type; }
            set { _post_type = value; }
        }
        #endregion Model

    }
}
