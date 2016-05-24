using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace DTcms.Model{
	 	//课程类别关系
		public class CurriculumCategory
	{
   		     
      	/// <summary>
		/// 课程分类关系主键
        /// </summary>		
		private int _curriculumcategoryid;
        public int CurriculumCategoryId
        {
            get{ return _curriculumcategoryid; }
            set{ _curriculumcategoryid = value; }
        }        
		/// <summary>
		/// 课程分类主键
        /// </summary>		
		private int _categoryid;
        public int CategoryId
        {
            get{ return _categoryid; }
            set{ _categoryid = value; }
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
		   
	}
}