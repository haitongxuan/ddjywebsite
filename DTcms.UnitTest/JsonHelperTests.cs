using Microsoft.VisualStudio.TestTools.UnitTesting;
using DTcms.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTcms.Model;
using Newtonsoft.Json;

namespace DTcms.Common.Tests
{
    [TestClass()]
    public class JsonHelperTests
    {
        [TestMethod()]
        public void ObjectToJSONTest()
        {
            var list = new List<Card>();
            list.Add(new Card()
            {
                CardId = 1,
                CardCategoryId = 1,
                Code = "20xijncs",
                CreateDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(365)
            });
            list.Add(new Card()
            {
                CardId = 2,
                CardCategoryId = 1,
                Code = "20vdsccs",
                CreateDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(365)
            });
            list.Add(new Card()
            {
                CardId = 3,
                CardCategoryId = 1,
                Code = "30vdsccs",
                CreateDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(365)
            });
            string jsonstr = JsonHelper.ObjectToJSON(new
            {
                status = 1,
                list = list
            });
            //Assert.Fail();
        }
        [TestMethod]
        public void toJsonTest()
        {
            var list = new List<Card>();
            list.Add(new Card()
            {
                CardId = 1,
                CardCategoryId = 1,
                Code = "20xijncs",
                CreateDate = DateTime.Now,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(365)
            });
            list.Add(new Card()
            {
                CardId = 2,
                CardCategoryId = 1,
                Code = "20vdsccs",
                CreateDate = DateTime.Now,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(365)
            });
            list.Add(new Card()
            {
                CardId = 3,
                CardCategoryId = 1,
                Code = "30vdsccs",
                CreateDate = DateTime.Now,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(365)
            });
            string jsonstr = JsonConvert.SerializeObject(new { status = 1, list = list });
        }
    }
}