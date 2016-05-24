using System.Collections.Generic;
using System.Data;
using System.Web;

namespace DTcms.Service
{
    /// <summary>
    /// 用户服务接口
    /// </summary>
    public interface IUserService : IBaseService
    {
        #region 用户信息=================
        /// <summary>
        /// OAuth平台列表
        /// </summary>
        /// <param name="top">显示条数</param>
        /// <param name="strwhere">查询条件</param>
        /// <returns>List</returns>
        List<Model.user_oauth_app> GetOauthAppList(int top, string strwhere);
        /// <summary>
        /// 返回用户头像图片地址
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>String</returns>
        string GetUserAvatar(string userName);
        /// <summary>
        /// 统计短消息数量
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="strwhere">查询条件</param>
        /// <returns>Int</returns>
        int GetUserMessageCount(string username, string strwhere);
        /// <summary>
        /// 短消息列表
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="top">显示条数</param>
        /// <param name="strwhere">查询条件</param>
        /// <returns>List</returns>
        List<Model.user_message> GetUserMessageList(string username, int top, string strwhere);

        /// <summary>
        /// 短信息分页列表
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="totalcount">总记录数</param>
        /// <returns>List</returns>
        List<Model.user_message> GetUserMessageList(string username, int pageSize, int pageIndex, string strwhere, out int totalcount);

        /// <summary>
        /// 积分明细分页列表
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="totalcount">总记录数</param>
        /// <returns>List</returns>
        List<Model.user_point_log> GetUserPointList(string username, int pageSize, int pageIndex, string strwhere, out int totalcount);

        /// <summary>
        /// 余额明细分页列表
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="totalcount">总记录数</param>
        /// <returns>List</returns>
        List<Model.user_amount_log> GetUserAmountList(string username, int pageSize, int pageIndex, string strwhere, out int totalcount);

        /// <summary>
        /// 充值记录分页列表
        /// </summary>
        /// <param name="token">用户token</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="totalcount">总记录数</param>
        /// <returns>List</returns>
        List<Model.user_recharge> GetUserRechargeList(string token, int pageSize, int pageIndex, string strwhere, out int totalcount);

        /// <summary>
        /// 用户邀请码列表
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="top">显示条数</param>
        /// <param name="strwhere">查询条件</param>
        /// <returns></returns>
        List<Model.user_code> GetUserInviteList(string username, int top, string strwhere);

        /// <summary>
        /// 返回邀请码状态
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="strCode">邀请码</param>
        /// <returns>bool</returns>
        bool GetInviteStatus(string username, string strCode);

        /// <summary>
        /// 收货地址列表
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="top">显示条数</param>
        /// <param name="strwhere">查询条件</param>
        /// <returns>DataTable</returns>
        List<Model.user_addr_book> GetUserAddrBookList(string username, int top, string strwhere);

        /// <summary>
        /// 收货地址分页列表
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="page_size">页面大小</param>
        /// <param name="page_index">当前页码</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="totalcount">总记录数</param>
        /// <returns>DateTable</returns>
        List<Model.user_addr_book> GetUserAddrBookList(string username, int page_size, int page_index, string strwhere,
            out int totalcount);
        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="nickName">用户昵称</param>
        /// <param name="sex">性别</param>
        /// <param name="birthday">生日</param>
        /// <param name="email">邮箱地址</param>
        /// <param name="mobile">手机号</param>
        /// <param name="telephone">电话</param>
        /// <param name="qq">QQ号</param>
        /// <param name="msn">msn地址</param>
        /// <param name="province">省标识</param>
        /// <param name="city">市标识</param>
        /// <param name="area">区标识</param>
        /// <param name="address">地址</param>
        void UserInfoEdit(string username, string nickName, string sex, string birthday, string email, string mobile,
            string telephone, string qq, string msn, string province, string city, string area, string address);
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        void UserEditPassword(string username, string password);
        /// <summary>
        /// 取回用户密码
        /// </summary>
        /// <param name="code"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        void UserGetPassword(string code,string mobile);

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="code"></param>
        /// <param name="npassword"></param>
        /// <returns></returns>
        void UserResetPassword(string code, string npassword);
        #endregion

        #region 用户行为=================
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Json String</returns>
        bool UserLogin(string username, string password, out string token);

        /// <summary>
        /// 判断用户是否已经登录
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Json String</returns>
        bool CheckUserLogin(string username);

        /// <summary>
        /// 注册用户信息
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="email">邮箱</param>
        /// <param name="mobile">手机号</param>
        /// <returns></returns>
        void UserRegister(string username, string password, string email, string mobile);
        /// <summary>
        /// 发送手机短信验证码
        /// </summary>
        /// <param name="mobile">手机号码</param>
        /// <returns></returns>
        void UserVerifySmscode(string mobile);

        /// <summary>
        /// 用户申请邀请码
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        void UserInviteCode(string username);

        /// <summary>
        /// 用户积分兑换
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="amount">兑换积分数量</param>
        /// <returns></returns>
        void UserPointConvert(decimal amount, string username);
        /// <summary>
        /// 用户在线充值
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="paymentId">支付主键</param>
        /// <param name="amount">充值金额</param>
        /// <returns></returns>
        void UserAmountRecharge(string username, int paymentId, decimal amount);
        /// <summary>
        /// 发送站内短消息
        /// </summary>
        /// <param name="username"></param>
        /// <param name="sendSave">发送保存，0不保存，1保存</param>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        void UserMessageAdd(string username, int sendSave, string title, string content);
        /// <summary>
        /// 删除用户积分明细
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="id">ID</param>
        /// <returns></returns>
        void UserPointDelete(string username, int id);
        /// <summary>
        /// 删除用户收支明细
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="id">ID</param>
        /// <returns></returns>
        void UserAmountDelete(string username, int id);
        /// <summary>
        /// 删除用户充值记录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="id">ID</param>
        void UserRechargeDelete(string username, int id);
        /// <summary>
        /// 删除用户消息
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="id">ID</param>
        void UserMessageDelete(string username, int id);

        #endregion
    }
}