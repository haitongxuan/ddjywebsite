using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace DTcms.Model
{
    //课程
    public class Curriculum
    {

        /// <summary>
        /// 课程主键
        /// </summary>		
        private int _curriculumid;
        public int CurriculumId
        {
            get { return _curriculumid; }
            set { _curriculumid = value; }
        }
        /// <summary>
        /// 课程名称
        /// </summary>		
        private string _fullname;
        public string FullName
        {
            get { return _fullname; }
            set { _fullname = value; }
        }
        /// <summary>
        /// 讲师
        /// </summary>		
        private string _teacherName;
        public string TeacherName
        {
            get { return _teacherName; }
            set { _teacherName = value; }
        }
        /// <summary>
        /// 描述
        /// </summary>		
        private string _describe;
        public string Describe
        {
            get { return _describe; }
            set { _describe = value; }
        }
        /// <summary>
        /// 删除标志
        /// </summary>		
        private int _deletemark;
        public int DeleteMark
        {
            get { return _deletemark; }
            set { _deletemark = value; }
        }
        /// <summary>
        /// 启用
        /// </summary>		
        private int _enable;
        public int Enable
        {
            get { return _enable; }
            set { _enable = value; }
        }
        /// <summary>
        /// 点击量
        /// </summary>		
        private int _click;
        public int Click
        {
            get { return _click; }
            set { _click = value; }
        }
        /// <summary>
        /// 状态
        /// </summary>		
        private int _status;
        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }
        /// <summary>
        /// 是否准许评论
        /// </summary>		
        private int _ismsg;
        public int IsMsg
        {
            get { return _ismsg; }
            set { _ismsg = value; }
        }
        /// <summary>
        /// 是否置顶
        /// </summary>		
        private int _istop;
        public int IsTop
        {
            get { return _istop; }
            set { _istop = value; }
        }
        /// <summary>
        /// 是否推荐
        /// </summary>		
        private int _isred;
        public int IsRed
        {
            get { return _isred; }
            set { _isred = value; }
        }
        /// <summary>
        /// 是否热门
        /// </summary>		
        private int _ishot;
        public int IsHot
        {
            get { return _ishot; }
            set { _ishot = value; }
        }
        /// <summary>
        /// 是否幻灯片
        /// </summary>		
        private int _isslide;
        public int IsSlide
        {
            get { return _isslide; }
            set { _isslide = value; }
        }
        /// <summary>
        /// 是否管理员发布0不是1是
        /// </summary>		
        private int _issys;
        public int IsSys
        {
            get { return _issys; }
            set { _issys = value; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>		
        private DateTime _createdate;
        public DateTime CreateDate
        {
            get { return _createdate; }
            set { _createdate = value; }
        }
        /// <summary>
        /// 创建用户名
        /// </summary>		
        private string _createusername;
        public string CreateUserName
        {
            get { return _createusername; }
            set { _createusername = value; }
        }
        /// <summary>
        /// 修改时间
        /// </summary>		
        private DateTime _modifydate;
        public DateTime ModifyDate
        {
            get { return _modifydate; }
            set { _modifydate = value; }
        }
        /// <summary>
        /// 修改用户名
        /// </summary>		
        private string _modifyusername;
        public string ModifyUserName
        {
            get { return _modifyusername; }
            set { _modifyusername = value; }
        }

        /// <summary>
        /// 课程封面
        /// </summary>
        private string _imgUrl;

        public string ImgUrl
        {
            get { return _imgUrl; }
            set { _imgUrl = value; }
        }

        private List<CurriculumItem> _curriculumItems;

        public List<CurriculumItem> CurriculumItems
        {
            get { return _curriculumItems; }
            set { _curriculumItems = value; }
        }
    }
}