using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace DTcms.Model{
	 	//媒体关系类别
		public class MediaRelationshipCategory
	{
   		     
      	/// <summary>
		/// 媒体关系类别主键
        /// </summary>		
		private int _mediarelationshipstyleid;
        public int MediaRelationshipStyleId
        {
            get{ return _mediarelationshipstyleid; }
            set{ _mediarelationshipstyleid = value; }
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
		/// 代码
        /// </summary>		
		private string _code;
        public string Code
        {
            get{ return _code; }
            set{ _code = value; }
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
		/// Layer
        /// </summary>		
		private int _layer;
        public int Layer
        {
            get{ return _layer; }
            set{ _layer = value; }
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
		   
	}
}