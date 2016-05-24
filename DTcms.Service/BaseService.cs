using System.Web;
using DTcms.Model;

namespace DTcms.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseService : IBaseService
    {
        public string VerifySmsCode(string username, string token, string strcode)
        {
            throw new System.NotImplementedException();
        }

        public string VerifyInviteReg(string userName, string inviteCode)
        {
            throw new System.NotImplementedException();
        }

        public string SendVerifySmsCode(string username, string token)
        {
            throw new System.NotImplementedException();
        }

        public string SendVerifyEmail(string username)
        {
            throw new System.NotImplementedException();
        }

        public bool CheckUserLogined(string username)
        {
            throw new System.NotImplementedException();
        }

        public users GetUserInfo(string username, string token)
        {
            throw new System.NotImplementedException();
        }

        public bool CheckAppKey(string appKey)
        {
            throw new System.NotImplementedException();
        }
    }
}