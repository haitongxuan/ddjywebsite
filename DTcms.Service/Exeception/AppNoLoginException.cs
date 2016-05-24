using System;

namespace DTcms.Service.Exeception
{
    /// <summary>
    /// 未登录异常
    /// </summary>
    public class AppNoLoginException : Exception
    {
        /// <summary>
        /// 0
        /// </summary>
        public int Status
        {
            get { return 0; }
        }
        public override string Message
        {
            get
            {
                var message = "对不起，用户尚未登录或已超时!";
                return message;
            }
        }
    }
}