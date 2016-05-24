using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace DTcms.Model{
	 	//课程关系类型
		public class CRelationshipStyle
	{
   		     
      	/// <summary>
		/// 课程关系类型主键
        /// </summary>		
		private int _crelationshipstyleid;
        public int CRelationshipStyleId
        {
            get{ return _crelationshipstyleid; }
            set{ _crelationshipstyleid = value; }
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
		/// 名称
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
		private string _remark;
        public string Remark
        {
            get{ return _remark; }
            set{ _remark = value; }
        }        
		/// <summary>
		/// 源课程分类主键
        /// </summary>		
		private int _sourcecurricularstyleid;
        public int SourceCurricularStyleId
        {
            get{ return _sourcecurricularstyleid; }
            set{ _sourcecurricularstyleid = value; }
        }        
		/// <summary>
		/// 目标课程分类主键
        /// </summary>		
		private int _targetcurricularstyleid;
        public int TargetCurricularStyleId
        {
            get{ return _targetcurricularstyleid; }
            set{ _targetcurricularstyleid = value; }
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