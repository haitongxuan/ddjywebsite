using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace DTcms.Model
{
    //卡片类别
    public class CardCategory
    {

        /// <summary>
        /// 卡片类别主键
        /// </summary>		
        private int _cardcategoryid;
        public int CardCategoryId
        {
            get { return _cardcategoryid; }
            set { _cardcategoryid = value; }
        }
        /// <summary>
        /// 父主键
        /// </summary>		
        private int _parentid;
        public int ParentId
        {
            get { return _parentid; }
            set { _parentid = value; }
        }
        /// <summary>
        /// 调用名称
        /// </summary>		
        private string _callindex;
        public string CallIndex
        {
            get { return _callindex; }
            set { _callindex = value; }
        }
        /// <summary>
        /// Layer
        /// </summary>		
        private int _layer;
        public int Layer
        {
            get { return _layer; }
            set { _layer = value; }
        }
        /// <summary>
        /// 名称
        /// </summary>		
        private string _fullname;
        public string FullName
        {
            get { return _fullname; }
            set { _fullname = value; }
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
        /// 卡片图片
        /// </summary>		
        private string _imagurl;
        public string ImagUrl
        {
            get { return _imagurl; }
            set { _imagurl = value; }
        }
        /// <summary>
        /// 背面图片
        /// </summary>		
        private string _backimageurl;
        public string BackImageUrl
        {
            get { return _backimageurl; }
            set { _backimageurl = value; }
        }
        /// <summary>
        /// 有效时长（天）
        /// </summary>		
        private decimal _duration;
        public decimal Duration
        {
            get { return _duration; }
            set { _duration = value; }
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
		/// 用户组调用名称
        /// </summary>		
		private string _usergroupcallindex;
        public string UserGroupCallIndex
        {
            get { return _usergroupcallindex; }
            set { _usergroupcallindex = value; }
        }
    }
}