using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace DTcms.Model
{
    //卡片信息表
    public class Card
    {

        /// <summary>
        /// 卡片主键
        /// </summary>		
        private int _cardid;
        public int CardId
        {
            get { return _cardid; }
            set { _cardid = value; }
        }
        /// <summary>
        /// 卡片类型主键
        /// </summary>		
        private int _cardcategoryid;
        public int CardCategoryId
        {
            get { return _cardcategoryid; }
            set { _cardcategoryid = value; }
        }
        /// <summary>
        /// 卡片编号
        /// </summary>		
        private string _code;
        public string Code
        {
            get { return _code; }
            set { _code = value; }
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
        /// 开始时间
        /// </summary>		
        private DateTime _startdate;

        public DateTime StartDate
        {
            get { return _startdate; }
            set { _startdate = value; }
        }
        /// <summary>
        /// 结束时间
        /// </summary>		
        private DateTime _enddate;
        public DateTime EndDate
        {
            get { return _enddate; }
            set { _enddate = value; }
        }

    }
}