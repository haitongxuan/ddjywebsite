using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace DTcms.Model{
	 	//用户课程历史
		public class UserCurriculum
	{
   		     
      	/// <summary>
		/// 用户课程主键
        /// </summary>		
		private int _usercurriculumid;
        public int UserCurriculumId
        {
            get{ return _usercurriculumid; }
            set{ _usercurriculumid = value; }
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
		/// 用户主键
        /// </summary>		
		private int _userid;
        public int UserId
        {
            get{ return _userid; }
            set{ _userid = value; }
        }        
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
		/// 创建时间
        /// </summary>		
		private DateTime _createdate;
        public DateTime CreateDate
        {
            get{ return _createdate; }
            set{ _createdate = value; }
        }        
		   
	}
}