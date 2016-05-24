using System;
using System.Collections.Generic;
using DTcms.Model;

namespace DTcms.Service
{
    public class UserTokenService : IUserTokenService
    {
        public string CreateUserToken(string username, decimal seconds, string ipAddress, string deviceId)
        {
            var bll = new BLL.UserToken();
            var userbll = new BLL.users();

            var model = new Model.UserToken();
            var user = userbll.GetModel(username);
            model.UserName = username;
            model.UserId = user.id.ToString();
            model.CreateTime = DateTime.Now;
            model.DeviceId = deviceId;
            model.IsOverdue = 0;
            model.IPAddress = ipAddress;
            model.OverdueTime = DateTime.Now.AddSeconds(double.Parse(seconds.ToString()));
            model.Token = Guid.NewGuid().ToString();

            try
            {
                bll.Add(model);
            }
            catch (Exception ex)
            {
                return "{err:" + ex.Message + "}";
            }
            return model.Token;
        }

        public UserToken GetUserToken(string username)
        {
            var bll = new BLL.UserToken();
            List<Model.UserToken> utList = bll.DataTableToList(bll.GetList(0, "UserName=" + username +
                " and IsOverdue<>1 and OverdueTime>'" + DateTime.Now + "'", "OverdueTime desc").Tables[0]);
            return utList[0];
        }

        public bool CheckUserToken(string token, string deviceId)
        {
            var bll = new BLL.UserToken();
            var model = bll.GetModelList("Token='" + token)[0];
            return (model.OverdueTime > DateTime.Now && model.DeviceId == deviceId);
        }

        public bool OverdueUserToken(string token)
        {
            var bll = new BLL.UserToken();
            var model = bll.GetModelList("Token='" + token)[0];
            model.IsOverdue = 1;
            try
            {
                return bll.Update(model);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}