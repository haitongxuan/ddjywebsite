using System.Collections.Generic;
using System.Data;

namespace DTcms.Service
{
    public interface IGoodsService
    {
        /// <summary>
        /// 获得该商品的规格列表
        /// </summary>
        /// <param name="article_id">信息ID</param>
        /// <param name="strwhere">条件</param>
        /// <returns>List<T></returns>
        List<Model.article_goods_spec> GetArticleGoodsSpec(int article_id, string strwhere);

        /// <summary>
        /// 根据频道名称及类别ID查询父规格列表(慎用)
        /// </summary>
        /// <param name="channel_name">频道名称</param>
        /// <param name="category_id">分类ID</param>
        /// <returns>List</returns>
        List<Model.article_spec> GetArticleSpecParent(string channel_name, int category_id);

        /// <summary>
        /// 根据父ID查询规格列表
        /// </summary>
        /// <param name="parent_id">父ID</param>
        /// <returns>DataTable</returns>
        List<Model.article_spec> GetArticleSpecChild(int parent_id);
        
    }
}