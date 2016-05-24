using System.Collections.Generic;

namespace DTcms.Service
{
    public interface IExpressService
    {
        /// <summary>
        /// 返回配送列表
        /// </summary>
        /// <param name="top">显示条数</param>
        /// <param name="strwhere">查询条件</param>
        /// <returns>DataTable</returns>
        List<Model.express> GetExpressList(int top, string strwhere);


        /// <summary>
        /// 返回配送方式的标题
        /// </summary>
        /// <param name="expressId">ID</param>
        /// <returns>String</returns>
        string GetExpressTitle(int expressId);
    }
}