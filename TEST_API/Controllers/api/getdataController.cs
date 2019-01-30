using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using TEST_API.Helpers;
using TEST_API.Models;

namespace TEST_API.Controllers.api
{
    public class getdataController : ApiController
    {
        private readonly DBC _ctx;
        public getdataController()
        {
            _ctx = new DBC();
        }

        [Route("api/getprovider/{cardColor}/{Govt}/{serviceCity}/{typeOfService}")]
        [HttpGet]
        /*
         * //////////W//A//R//N//I//N//G////////////////////
         * DO NOT INTEGRATE THIS API WITH ANY MESSENGER!! IT WILL FLOOD YOUR INBOX
         */
        public IHttpActionResult getprov(string cardColor, string Govt, string serviceCity, string typeOfService)
        {
            //Type = Type.Replace(" ", "%20");
            //AxaProvider x = new AxaProvider();
            var providerList = _ctx.axaProviders.Select(w => new { w.Provider, w.NetworkName, w.Governorate, w.City, w.Address, w.Type, w.Phone, w.LocationOnMap });
            if (cardColor.Contains("Green"))
            {
                providerList = _ctx.axaProviders.Select(w => new { w.Provider, w.NetworkName, w.Governorate, w.City, w.Address, w.Type, w.Phone, w.LocationOnMap })
                    .Where(q => q.NetworkName.Contains(cardColor) && q.Governorate.Contains(Govt) && q.City.Contains(serviceCity) &&
                    q.Type.Contains(typeOfService));
            }
            else if (cardColor.Contains("Silver"))
            {
                providerList = _ctx.axaProviders.Select(w => new { w.Provider, w.NetworkName, w.Governorate, w.City, w.Address, w.Type, w.Phone, w.LocationOnMap })
                    .Where(q =>( q.NetworkName.Contains(cardColor) ||q.NetworkName.Contains("Green"))
                    && q.Governorate.Contains(Govt) && q.City.Contains(serviceCity) &&
                    q.Type.Contains(typeOfService));
            }
            else
                providerList = _ctx.axaProviders.Select(w => new { w.Provider, w.NetworkName, w.Governorate, w.City, w.Address, w.Type, w.Phone, w.LocationOnMap })
                    .Where(q => (q.NetworkName.Contains(cardColor) || q.NetworkName.Contains("Green") || q.NetworkName.Contains("Silver"))
                    && q.Governorate.Contains(Govt) && q.City.Contains(serviceCity) &&
                    q.Type.Contains(typeOfService));

            var xx = _ctx.axaProviders.Select(w => new { w.Provider, w.NetworkName, w.Governorate, w.City, w.Address, w.Type, w.Phone, w.LocationOnMap })
               .Where(q => q.NetworkName.Contains(cardColor));
            //string x = "this is your address" + providerList.Select(w => w.Address).SingleOrDefault();
            //.Where(e=>e.Governate == Govt).Where(r=>r.City == City)
            //.Where(t=>t.Type == Type).SingleOrDefault();
            //string json = JsonConvert.SerializeObject(providerList, Formatting.Indented, new customFbFormat(typeof(IQueryable)));
           // var ww = providerList.Count();
            int count = 0;
            //string xx = "";
            List<messages> qwe = new List<messages>();
            foreach (var axa in providerList)
            {
                messages qw = new messages();

                count++;
                qw.text = "address " + count + ": " + axa.Address.Replace("\"\r\n", "") + "\n Phone(s): "+axa.Phone + "\n Location on map: "+axa.LocationOnMap;
                qwe.Add(qw);
                
            }
            var x = new { messages = qwe };
            //string json = JsonConvert.SerializeObject(x);
            //var x = Regex.Replace(providerList, @"[^\u0009\u000A\u000D\u0020-\u007E]", "*");
            return Ok(x);
        }
    }
}