namespace DTcms.Service
{
    public interface ISiteService : IBaseService
    {
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="deviceId">设备号</param>
        /// <returns></returns>
        string GetValiCode(string deviceId);
    }
}