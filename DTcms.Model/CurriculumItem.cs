using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace DTcms.Model{
	 	//课程章节
		public class CurriculumItem
	{
   		     
      	/// <summary>
		/// 课程章节主键
        /// </summary>		
		private int _curriculumitemid;
        public int CurriculumItemId
        {
            get{ return _curriculumitemid; }
            set{ _curriculumitemid = value; }
        }        
		/// <summary>
		/// 课程主键
        /// </summary>		
		private int _curriculumid;
        public int CurriculumId
        {
            get{ return _curriculumid; }
            set{ _curriculumid = value; }
        }        
		/// <summary>
		/// 章节名称
        /// </summary>		
		private string _fullname;
        public string FullName
        {
            get{ return _fullname; }
            set{ _fullname = value; }
        }        
		/// <summary>
		/// 描述
        /// </summary>		
		private string _describe;
        public string Describe
        {
            get{ return _describe; }
            set{ _describe = value; }
        }        
		/// <summary>
		/// 排序
        /// </summary>		
		private int _sort;
        public int Sort
        {
            get{ return _sort; }
            set{ _sort = value; }
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
		/// 修改用户名
        /// </summary>		
		private string _modifyusername;
        public string ModifyUserName
        {
            get{ return _modifyusername; }
            set{ _modifyusername = value; }
        }        
		/// <summary>
		/// 截图url
        /// </summary>		
		private string _imageurl;
        public string ImageUrl
        {
            get{ return _imageurl; }
            set{ _imageurl = value; }
        }        
		/// <summary>
		/// 媒体URL
        /// </summary>		
		private string _mediaCode;
        public string MediaCode
        {
            get{ return _mediaCode; }
            set{ _mediaCode = value; }
        }        
		   
	}
}