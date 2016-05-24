using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace DTcms.Model{
	 	//用户Token，用于APP用户认证
		public class UserToken
	{
   		     
      	/// <summary>
		/// 用户令牌主键
        /// </summary>		
		private string _usertokenid;
        public string UserTokenId
        {
            get{ return _usertokenid; }
            set{ _usertokenid = value; }
        }        
		/// <summary>
		/// 用户主键
        /// </summary>		
		private string _userid;
        public string UserId
        {
            get{ return _userid; }
            set{ _userid = value; }
        }        
		/// <summary>
		/// 用户名
        /// </summary>		
		private string _username;
        public string UserName
        {
            get{ return _username; }
            set{ _username = value; }
        }        
		/// <summary>
		/// 令牌
        /// </summary>		
		private string _token;
        public string Token
        {
            get{ return _token; }
            set{ _token = value; }
        }        
		/// <summary>
		/// 创建时间
        /// </summary>		
		private DateTime _createtime;
        public DateTime CreateTime
        {
            get{ return _createtime; }
            set{ _createtime = value; }
        }        
		/// <summary>
		/// 过期时间
        /// </summary>		
		private DateTime _overduetime;
        public DateTime OverdueTime
        {
            get{ return _overduetime; }
            set{ _overduetime = value; }
        }        
		/// <summary>
		/// 是否过期
        /// </summary>		
		private int _isoverdue;
        public int IsOverdue
        {
            get{ return _isoverdue; }
            set{ _isoverdue = value; }
        }        
		/// <summary>
		/// 登录设备编号
        /// </summary>		
		private string _deviceid;
        public string DeviceId
        {
            get{ return _deviceid; }
            set{ _deviceid = value; }
        }        
		/// <summary>
		/// 登录设备IP
        /// </summary>		
		private string _ipaddress;
        public string IPAddress
        {
            get{ return _ipaddress; }
            set{ _ipaddress = value; }
        }        
		   
	}
}