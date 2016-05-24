using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace DTcms.Model{
	 	//课程关系
		public class CurriculumCRelationshipStyle
	{
   		     
      	/// <summary>
		/// 课程关系主键
        /// </summary>		
		private int _curriculumcrelationshipstyleid;
        public int CurriculumCRelationshipStyleId
        {
            get{ return _curriculumcrelationshipstyleid; }
            set{ _curriculumcrelationshipstyleid = value; }
        }        
		/// <summary>
		/// 起始日期
        /// </summary>		
		private DateTime _startdate;
        public DateTime StartDate
        {
            get{ return _startdate; }
            set{ _startdate = value; }
        }        
		/// <summary>
		/// 结束日期
        /// </summary>		
		private DateTime _enddate;
        public DateTime EndDate
        {
            get{ return _enddate; }
            set{ _enddate = value; }
        }        
		/// <summary>
		/// 注释
        /// </summary>		
		private string _remark;
        public string Remark
        {
            get{ return _remark; }
            set{ _remark = value; }
        }        
		/// <summary>
		/// 源课程分类关系
        /// </summary>		
		private int _sourcecurricularstylecurriculumid;
        public int SourceCurricularStyleCurriculumId
        {
            get{ return _sourcecurricularstylecurriculumid; }
            set{ _sourcecurricularstylecurriculumid = value; }
        }        
		/// <summary>
		/// 目标课程分类关系
        /// </summary>		
		private int _targetcurricularstylecurriculumid;
        public int TargetCurricularStyleCurriculumId
        {
            get{ return _targetcurricularstylecurriculumid; }
            set{ _targetcurricularstylecurriculumid = value; }
        }        
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