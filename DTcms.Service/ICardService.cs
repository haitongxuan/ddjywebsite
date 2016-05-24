using System.Collections.Generic;
using DTcms.Model;

namespace DTcms.Service
{
    /// <summary>
    /// 卡片服务
    /// </summary>
    public interface ICardService : IBaseService
    {
        #region token===========
        /// <summary>
        /// 根据卡片类别调用名称和用户名，为用户创建卡片
        /// </summary>
        /// <param name="callindex">卡片类别调用名称</param>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        int CreateUserCard(string callindex, string username);

        /// <summary>
        /// 根据卡片类别调用名称和用户名，检查用户卡片是否有效
        /// </summary>
        /// <param name="callIndex">卡片类别调用名称</param>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        bool CheckUserCard(string callIndex, string username);
        /// <summary>
        /// 检验卡片是否有效
        /// </summary>
        /// <param name="code">卡片编号</param>
        /// <returns></returns>
        bool CheckUserCard(string code);

        /// <summary>
        /// 根据卡片类别调用名称和用户名，获取卡片信息
        /// </summary>
        /// <param name="callIndex">调用名称</param>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        Model.Card GetUsersCard(string callIndex, string username);
        /// <summary>
        /// 根据卡片编号获得卡片信息
        /// </summary>
        /// <param name="code">卡片编号</param>
        /// <returns></returns>
        Model.Card GetUsersCard(string code);

        /// <summary>
        /// 获取用户的有效期内的卡片
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>List<Model.Card></returns>
        List<Model.Card> GetCards(string username);

        /// <summary>
        /// 获取用户的所有的卡片
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        List<Model.Card> GetAllCards(string username);
        #endregion
    }
}