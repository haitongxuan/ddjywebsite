using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace DTcms.Model
{
    //dt_article_item
    public class article_item
    {

        /// <summary>
        /// id
        /// </summary>		
        private int _id;
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 文章主键
        /// </summary>		
        private int _article_id;
        public int article_id
        {
            get { return _article_id; }
            set { _article_id = value; }
        }
        /// <summary>
        /// 文章明细主键
        /// </summary>		
        private int _item_article_id;
        public int item_article_id
        {
            get { return _item_article_id; }
            set { _item_article_id = value; }
        }
        /// <summary>
        /// 频道主键
        /// </summary>		
        private int _channel_id;
        public int channel_id
        {
            get { return _channel_id; }
            set { _channel_id = value; }
        }
        /// <summary>
        /// 明细频道主键
        /// </summary>		
        private int _item_channel_id;
        public int item_channel_id
        {
            get { return _item_channel_id; }
            set { _item_channel_id = value; }
        }
        /// <summary>
        /// 排序编号
        /// </summary>		
        private int _sort_id;
        public int sort_id
        {
            get { return _sort_id; }
            set { _sort_id = value; }
        }
        /// <summary>
        /// 明细标题
        /// </summary>		
        private string _item_title;
        public string item_title
        {
            get { return _item_title; }
            set { _item_title = value; }
        }
        /// <summary>
        /// 明细图片链接
        /// </summary>		
        private string _item_img_url;
        public string item_img_url
        {
            get { return _item_img_url; }
            set { _item_img_url = value; }
        }

    }
}