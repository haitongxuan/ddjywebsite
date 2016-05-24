using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace DTcms.Model
{
    //用户卡片表
    public class UserCard
    {

        /// <summary>
        /// 用户卡片主键
        /// </summary>		
        private int _usercardid;
        public int UserCardId
        {
            get { return _usercardid; }
            set { _usercardid = value; }
        }
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
        /// 用户主键
        /// </summary>		
        private int _userid;
        public int UserId
        {
            get { return _userid; }
            set { _userid = value; }
        }
        /// <summary>
        /// 卡片类别主键
        /// </summary>		
        private int _cardcategoryid;
        public int CardCategoryId
        {
            get { return _cardcategoryid; }
            set { _cardcategoryid = value; }
        }

    }
}