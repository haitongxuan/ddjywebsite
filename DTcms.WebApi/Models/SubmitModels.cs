using System;

namespace DTcms.WebApi.Models
{
    public class CommentAddModel
    {
        public int articleId { get; set; }
        public string content { get; set; }
    }

    public class CommentListPostModel
    {
        public int articleId { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }

    public class CommentModel
    {
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string avatar { get; set; }
        public string content { get; set; }
        public DateTime add_time { get; set; }
        public int is_reply { get; set; }
        public string reply_content { get; set; }
        public DateTime reply_time { get; set; }
    }
}