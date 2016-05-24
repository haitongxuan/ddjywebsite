namespace DTcms.Service
{
    public interface IUserTokenService
    {
        /// <summary>
        /// 创建用户token
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="seconds">过期时长</param>
        /// <param name="ipAddress">IP地址</param>
        /// <param name="deviceId">设备编号</param>
        /// <returns></returns>
        string CreateUserToken(string username, decimal seconds, string ipAddress, string deviceId);
        /// <summary>
        /// 获取用户的token
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        Model.UserToken GetUserToken(string username);
        /// <summary>
        /// 检查用户token，是否过期，设备号是否一致
        /// </summary>
        /// <param name="token"></param>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        bool CheckUserToken(string token, string deviceId);
        /// <summary>
        /// 设置用户token过期
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        bool OverdueUserToken(string token);
    }
}