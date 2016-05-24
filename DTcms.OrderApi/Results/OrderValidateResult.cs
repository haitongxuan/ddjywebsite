using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace DTcms.OrderApi.Results
{
    public class PayResult
    {
        public bool success { get; set; }
        public int status { get; set; }
        public string msg { get; set; }
    }
}
