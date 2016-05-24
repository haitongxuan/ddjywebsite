using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace DTcms.Model{
	 	//文件夹
		public class Folder
	{
   		     
      	/// <summary>
		/// 文件夹主键
        /// </summary>		
		private int _folderid;
        public int FolderId
        {
            get{ return _folderid; }
            set{ _folderid = value; }
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
		/// 文件夹名称
        /// </summary>		
		private string _fullname;
        public string FullName
        {
            get{ return _fullname; }
            set{ _fullname = value; }
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