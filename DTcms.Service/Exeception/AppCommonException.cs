using System;
namespace DTcms.Service.Exeception
{
    /// <summary>
    /// 普通代码异常
    /// </summary>
    public class AppCommonException : Exception
    {
        /// <summary>
        /// 101
        /// </summary>
        private int _status = 101;
        public int Status
        {
            get { return _status; }
        }

        public AppCommonException(string msg) : base(msg) { }

        public AppCommonException(int status, string msg) : base(msg)
        {
            _status = status;
        }
        public AppCommonException() : base()
        {
        }
    }
}