using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace DTcms.Model
{
    //媒体表
    public class Media
    {

        /// <summary>
        /// 媒体主键
        /// </summary>		
        private int _mediaid;
        public int MediaId
        {
            get { return _mediaid; }
            set { _mediaid = value; }
        }
        /// <summary>
        /// 媒体名称
        /// </summary>		
        private string _fullname;
        public string FullName
        {
            get { return _fullname; }
            set { _fullname = value; }
        }
        /// <summary>
        /// 文件名称
        /// </summary>		
        private string _filename;
        public string FileName
        {
            get { return _filename; }
            set { _filename = value; }
        }
        /// <summary>
        /// 文件夹主键
        /// </summary>		
        private int _folderid;
        public int FolderId
        {
            get { return _folderid; }
            set { _folderid = value; }
        }
        /// <summary>
        /// 真实路径
        /// </summary>		
        private string _realpath;
        public string RealPath
        {
            get { return _realpath; }
            set { _realpath = value; }
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
        /// MediaCategoryId
        /// </summary>		
        private int _mediacategoryid;
        public int MediaCategoryId
        {
            get { return _mediacategoryid; }
            set { _mediacategoryid = value; }
        }
        /// <summary>
        /// MediaCode
        /// </summary>		
        private string _mediacode;
        public string MediaCode
        {
            get { return _mediacode; }
            set { _mediacode = value; }
        }

        private decimal _timeLength;

        public decimal TimeLength
        {
            get { return _timeLength; }
            set { _timeLength = value; }
        }
    }
}