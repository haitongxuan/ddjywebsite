using System.Web;

namespace DTcms.Service
{
    public interface IUserAddressService
    {
        /// <summary>
        /// 编辑用户地址
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="addressId">地址主键，0为新增</param>
        /// <param name="acceptName">收件人地址</param>
        /// <param name="province">省标识</param>
        /// <param name="city">市标识</param>
        /// <param name="area">区标识</param>
        /// <param name="address">地址</param>
        /// <param name="mobile">手机</param>
        /// <param name="telephone">电话</param>
        /// <param name="email">电邮</param>
        /// <param name="postCode">邮政编码</param>
        void UserAddressEdit(string username, int addressId, string acceptName,
            string province, string city, string area, string address, string mobile, string telephone,
            string email, string postCode);
        /// <summary>
        /// 用户设置默认地址
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="addressId"></param>
        /// <returns></returns>
        void UserAddressDefault(string username, int addressId);
        /// <summary>
        /// 用户删除收货地址
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="addressId">收货地址主键</param>
        /// <returns></returns>
        void UserAddressDelete(string username, int addressId);
    }
}