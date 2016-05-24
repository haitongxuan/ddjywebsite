using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace DTcms.Model{
	 	//媒体类别表
		public class MediaCategory
	{
   		     
      	/// <summary>
		/// 媒体类别主键
        /// </summary>		
		private int _mediacategoryid;
        public int MediaCategoryId
        {
            get{ return _mediacategoryid; }
            set{ _mediacategoryid = value; }
        }        
		/// <summary>
		/// 名称
        /// </summary>		
		private string _fullname;
        public string FullName
        {
            get{ return _fullname; }
            set{ _fullname = value; }
        }        
		/// <summary>
		/// 调用名称
        /// </summary>		
		private string _callindex;
        public string CallIndex
        {
            get{ return _callindex; }
            set{ _callindex = value; }
        }        
		/// <summary>
		/// Layer
        /// </summary>		
		private int _layer;
        public int Layer
        {
            get{ return _layer; }
            set{ _layer = value; }
        }        
		/// <summary>
		/// 父主键
        /// </summary>		
		private int _parentid;
        public int ParentId
        {
            get{ return _parentid; }
            set{ _parentid = value; }
        }        
		/// <summary>
		/// 描述
        /// </summary>		
		private string _remark;
        public string Remark
        {
            get{ return _remark; }
            set{ _remark = value; }
        }        
		/// <summary>
		/// 删除标志
        /// </summary>		
		private int _deletemark;
        public int DeleteMark
        {
            get{ return _deletemark; }
            set{ _deletemark = value; }
        }        
		/// <summary>
		/// 启用
        /// </summary>		
		private int _enable;
        public int Enable
        {
            get{ return _enable; }
            set{ _enable = value; }
        }        
		/// <summary>
		/// 创建时间
        /// </summary>		
		private DateTime _createdate;
        public DateTime CreateDate
        {
            get{ return _createdate; }
            set{ _createdate = value; }
        }        
		/// <summary>
		/// 创建用户主键
        /// </summary>		
		private string _createuserid;
        public string CreateUserId
        {
            get{ return _createuserid; }
            set{ _createuserid = value; }
        }        
		/// <summary>
		/// 创建用户名
        /// </summary>		
		private string _createusername;
        public string CreateUserName
        {
            get{ return _createusername; }
            set{ _createusername = value; }
        }        
		/// <summary>
		/// 修改时间
        /// </summary>		
		private DateTime _modifydate;
        public DateTime ModifyDate
        {
            get{ return _modifydate; }
            set{ _modifydate = value; }
        }        
		/// <summary>
		/// 修改用户主键
        /// </summary>		
		private string _modifyuserid;
        public string ModifyUserId
        {
            get{ return _modifyuserid; }
            set{ _modifyuserid = value; }
        }        
		/// <summary>
		/// 修改用户名
        /// </summary>		
		private string _modifyusername;
        public string ModifyUserName
        {
            get{ return _modifyusername; }
            set{ _modifyusername = value; }
        }        
		   
	}
}