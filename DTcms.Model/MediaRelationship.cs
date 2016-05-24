using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace DTcms.Model{
	 	//媒体类别关系表
		public class MediaRelationship
	{
   		     
      	/// <summary>
		/// 媒体类别关系主键
        /// </summary>		
		private int _mediacategoryrelationshipid;
        public int MediaCategoryRelationshipId
        {
            get{ return _mediacategoryrelationshipid; }
            set{ _mediacategoryrelationshipid = value; }
        }        
		/// <summary>
		/// 媒体主键
        /// </summary>		
		private int _mediaid;
        public int MediaId
        {
            get{ return _mediaid; }
            set{ _mediaid = value; }
        }        
		/// <summary>
		/// MediaRelationshipCategoryId
        /// </summary>		
		private int _mediarelationshipcategoryid;
        public int MediaRelationshipCategoryId
        {
            get{ return _mediarelationshipcategoryid; }
            set{ _mediarelationshipcategoryid = value; }
        }        
		   
	}
}