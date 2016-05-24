using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTcms.Web.Plugin.Forum.Model
{
    /// <summary>
    /// 论坛版块实体类
    /// </summary>
    [Serializable]
    public class forum_board
    {
        public forum_board()
        {}

        #region Model
        private int _id;
        private string _boardname= "";
        private int _parent_id = 0;
        private string _class_list = "";
        private int _class_layer = 0;
        private int _sort_id = 99;
        private string _img_url = "";
        private string _content = "";
        private int _is_lock = 0;
        private string _allow_usergroupid_list = "";
        private string _moderator_list = "";
        private int _subject_count = 0;
        private int _post_count = 0;


        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 版块名称
        /// </summary>
        public string boardname
        {
            get { return _boardname; }
            set { _boardname = value; }
        }
        /// <summary>
        /// 上级板块名称
        /// </summary>
        public int parent_id
        {
            get { return _parent_id; }
            set { _parent_id = value; }
        }
        /// <summary>
        /// 板块ID列表(逗号分隔开)
        /// </summary>
        public string class_list
        {
            set { _class_list = value; }
            get { return _class_list; }
        }
        /// <summary>
        /// 板块深度
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
        /// 图片地址
        /// </summary>
        public string img_url
        {
            set { _img_url = value; }
            get { return _img_url; }
        }
        /// <summary>
        /// 备注说明
        /// </summary>
        public string content
        {
            set { _content = value; }
            get { return _content; }
        }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public int is_lock
        {
            set { _is_lock = value; }
            get { return _is_lock; }
        }
        /// <summary>
        /// 允许访问的用户组ID
        /// </summary>
        public string allow_usergroupid_list
        {
            set { _allow_usergroupid_list = value; }
            get { return _allow_usergroupid_list; }
        }
        /// <summary>
        /// 版主列表
        /// </summary>
        public string moderator_list
        {
            set { _moderator_list = value; }
            get { return _moderator_list; }
        }
        /// <summary>
        /// 主题数量
        /// </summary>
        public int subject_count
        {
            set { _subject_count = value; }
            get { return _subject_count; }
        }
        /// <summary>
        /// 帖子数量
        /// </summary>
        public int post_count
        {
            set { _post_count = value; }
            get { return _post_count; }
        }
        #endregion
    }
}
