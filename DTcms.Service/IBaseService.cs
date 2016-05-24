using System.Web;
using DTcms.Common;

namespace DTcms.Service
{
    public interface IBaseService
    {

        #region 校检手机验证码===============================
        /// <summary>
        /// 校检手机验证码
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="token">token</param>
        /// <param name="strcode">验证码</param>
        /// <returns></returns>
        string VerifySmsCode(string username, string token, string strcode);
        #endregion

        #region 邀请注册处理方法=============================
        /// <summary>
        /// 邀请注册处理方法
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="inviteCode">邀请码</param>
        /// <returns></returns>
        string VerifyInviteReg(string userName, string inviteCode);
        #endregion

        #region 发送手机短信验证码===========================
        /// <summary>
        /// 发送手机短信验证码
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="token">token</param>
        /// <returns></returns>
        string SendVerifySmsCode(string username, string token);
        #endregion

        #region Email验证发送邮件============================
        /// <summary>
        /// Email验证发送邮件
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        string SendVerifyEmail(string username);


        #endregion

        /// <summary>
        /// 验证用户是否登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        bool CheckUserLogined(string username);
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="token">token</param>
        /// <returns></returns>
        Model.users GetUserInfo(string username, string token);
        /// <summary>
        /// 检验程序接口是否能被调用
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        bool CheckAppKey(string appKey);
    }
}