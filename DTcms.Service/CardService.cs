using System;
using System.Collections.Generic;
using System.Linq;
using DTcms.Model;
using DTcms.Service.Exeception;

namespace DTcms.Service
{
    public class CardService : BaseService, ICardService
    {
        /// <summary>
        /// 根据卡片类别调用名称和用户名，为用户创建卡片
        /// </summary>
        /// <param name="callindex">卡片类别调用名称</param>
        /// <param name="username">用户名</param>
        /// <param name="token">token</param>
        /// <returns></returns>
        public int CreateUserCard(string callindex, string username)
        {
            BLL.Card cardBll = new BLL.Card();
            BLL.CardCategory cardCategoryBll = new BLL.CardCategory();
            BLL.UserCard ucBLL = new BLL.UserCard();
            BLL.users usersBll = new BLL.users();

            var user = usersBll.GetModel(username);
            var cardCategory = cardCategoryBll.GetModel(callindex);
            var card = new Model.Card();
            card.CardCategoryId = cardCategory.CardCategoryId;
            card.Code = Common.Utils.GetCheckCode(7);
            card.CreateDate = DateTime.Now;
            card.StartDate = DateTime.Now;
            card.EndDate = card.StartDate.AddDays(double.Parse(cardCategory.Duration.ToString()));

            int cardId = cardBll.Add(card);
            var uc = new Model.UserCard();
            uc.CardId = cardId;
            uc.UserId = user.id;
            uc.CardCategoryId = cardCategory.CardCategoryId;
            return ucBLL.Add(uc);
        }

        /// <summary>
        /// 根据卡片类别调用名称和用户名，检查用户卡片是否有效
        /// </summary>
        /// <param name="callIndex">卡片类别调用名称</param>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public bool CheckUserCard(string callIndex, string username)
        {
            bool check = false;
            BLL.Card cardBll = new BLL.Card();
            BLL.CardCategory cardCategoryBll = new BLL.CardCategory();
            BLL.UserCard ucBLL = new BLL.UserCard();
            BLL.users usersBll = new BLL.users();

            var user = usersBll.GetModel(username);
            var cardCategory = cardCategoryBll.GetModel(callIndex);
            var uclist = ucBLL.GetModelList("UserId=" + user.id);
            var cardList = cardBll.GetModelList("CardCategoryId=" + cardCategory.CardCategoryId);
            List<Model.UserCard> ucl = (from uc in uclist
                                        join c in cardList on uc.CardId equals c.CardId
                                        select new Model.UserCard()
                                        {
                                            UserId = uc.UserId,
                                            CardId = uc.CardId,
                                            UserCardId = uc.UserCardId,
                                            CardCategoryId = cardCategory.CardCategoryId
                                        }).ToList();
            foreach (var uc in ucl)
            {
                var card = cardBll.GetModel(uc.CardId);
                if (card.StartDate <= DateTime.Now && card.EndDate >= DateTime.Now)
                {
                    check = true;
                }
            }
            return check;
        }

        public bool CheckUserCard(string code)
        {
            bool check = false;
            BLL.Card cardBll = new BLL.Card();

            var model = cardBll.GetModelList("Code=" + code)[0];
            if (model.StartDate < DateTime.Now && model.EndDate > DateTime.Now)
                check = true;
            return check;
        }

        /// <summary>
        /// 根据卡片类别调用名称和用户名，获取卡片信息
        /// </summary>
        /// <param name="callIndex"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public Model.Card GetUsersCard(string callIndex, string username)
        {
            BLL.Card cardBll = new BLL.Card();
            BLL.CardCategory cardCategoryBll = new BLL.CardCategory();
            BLL.UserCard ucBLL = new BLL.UserCard();
            BLL.users usersBll = new BLL.users();

            var user = usersBll.GetModel(username);
            var cardCategory = cardCategoryBll.GetModel(callIndex);
            var uclist = ucBLL.GetModelList("UserId=" + user.id);
            var cardList = cardBll.GetModelList("CardCategoryId=" + cardCategory.CardCategoryId);
            List<Model.UserCard> ucl = (from uc in uclist
                                        join c in cardList on uc.CardId equals c.CardId
                                        select new Model.UserCard()
                                        {
                                            UserId = uc.UserId,
                                            CardId = uc.CardId,
                                            UserCardId = uc.UserCardId,
                                            CardCategoryId = cardCategory.CardCategoryId
                                        }).ToList();
            Model.Card tcard = null;
            foreach (var uc in ucl)
            {
                Model.Card card = cardBll.GetModel(uc.CardId);
                if (card.StartDate <= DateTime.Now && card.EndDate >= DateTime.Now)
                {
                    if (tcard != null)
                    {
                        if (tcard.EndDate < card.EndDate)
                        {
                            tcard = card;
                        }
                    }
                    else
                    {
                        tcard = card;
                    }
                }
            }
            return tcard;
        }

        public Model.Card GetUsersCard(string code)
        {
            BLL.Card cardBll = new BLL.Card();
            Model.Card model;
            var list = cardBll.GetModelList("Code='" + code + "'");
            if (list.Count > 0)
                model = list[0];
            else
            {
                throw new AppCommonException("没有找到卡片！");
            }
            return model;
        }

        /// <summary>
        /// 获取用户的有效期内的卡片
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<Model.Card> GetCards(string username)
        {
            BLL.Card cardBll = new BLL.Card();
            BLL.CardCategory cardCategoryBll = new BLL.CardCategory();
            BLL.UserCard ucBLL = new BLL.UserCard();
            BLL.users usersBll = new BLL.users();

            var user = usersBll.GetModel(username);
            var uclist = ucBLL.GetModelList("UserId=" + user.id);
            var categoryIdlist = from uc in uclist
                                 group uc by uc.CardCategoryId into g
                                 select new { id = g.Key };
            List<Model.Card> cardList = new List<Model.Card>();
            foreach (var i in categoryIdlist)
            {
                var cc = cardCategoryBll.GetModel(i.id);
                var card = GetUsersCard(cc.CallIndex, user.user_name);
                if (card != null)
                    cardList.Add(card);
            }
            return cardList;
        }

        /// <summary>
        /// 获取用户的所有的卡片
        /// </summary>
        /// <param name="username"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public List<Model.Card> GetAllCards(string username)
        {
            BLL.Card cardBll = new BLL.Card();
            BLL.CardCategory cardCategoryBll = new BLL.CardCategory();
            BLL.UserCard ucBLL = new BLL.UserCard();
            BLL.users usersBll = new BLL.users();

            var user = usersBll.GetModel(username);
            var uclist = ucBLL.GetModelList("UserId=" + user.id);
            List<Model.Card> cardList = new List<Model.Card>();
            foreach (var uc in uclist)
            {
                cardList.Add(cardBll.GetModel(uc.CardId));
            }
            return cardList;
        }
    }
}