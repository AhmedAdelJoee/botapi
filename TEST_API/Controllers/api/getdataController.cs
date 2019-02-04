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
                    q.Type.Contains(typeOfService)).Take(5);
            }
            else if (cardColor.Contains("Silver"))
            {
                providerList = _ctx.axaProviders.Select(w => new { w.Provider, w.NetworkName, w.Governorate, w.City, w.Address, w.Type, w.Phone, w.LocationOnMap })
                    .Where(q =>( q.NetworkName.Contains(cardColor) ||q.NetworkName.Contains("Green"))
                    && q.Governorate.Contains(Govt) && q.City.Contains(serviceCity) &&
                    q.Type.Contains(typeOfService)).Take(5);
            }
            else
                providerList = _ctx.axaProviders.Select(w => new { w.Provider, w.NetworkName, w.Governorate, w.City, w.Address, w.Type, w.Phone, w.LocationOnMap })
                    .Where(q => (q.NetworkName.Contains(cardColor) || q.NetworkName.Contains("Green") || q.NetworkName.Contains("Silver"))
                    && q.Governorate.Contains(Govt) && q.City.Contains(serviceCity) &&
                    q.Type.Contains(typeOfService)).Take(5);

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
                qw.text = "Provider "+ count+" Name: " + axa.Provider + "\n" + "Address: " + axa.Address.Replace("\"\r\n", "") + "\n Phone(s): "+axa.Phone + "\n Location on map: "+axa.LocationOnMap;
                qwe.Add(qw);
                
            }
            var x = new { messages = qwe };
            //string json = JsonConvert.SerializeObject(x);
            //var x = Regex.Replace(providerList, @"[^\u0009\u000A\u000D\u0020-\u007E]", "*");
            return Ok(x);
        }


        #region HelperFunction
        public static decimal Sqrt(decimal x, decimal? guess = null)
        {
            if (x == 0)
                return 0;
            var ourGuess = guess.GetValueOrDefault(x / 2m);
            var result = x / ourGuess;
            var average = (ourGuess + result) / 2m;

            if (average == ourGuess) // This checks for the maximum precision possible with a decimal.
                return average;
            //else if (average==0)
            else
                return Sqrt(x, average);
        }
        #endregion



        [Route("api/postquery")]
        [HttpPost]
        public IHttpActionResult postshit(LongLat x)
        {
            UserQuery oo = new UserQuery();
            oo.latitude = x.lat;
            oo.longitude = x.lon;
            oo.Count = "2";
            oo.MessengerUserID = "2";
            _ctx.userQueries.Add(oo);
            _ctx.SaveChanges();
            return Ok();
        }

        [Route("api/testfn/{longi}/{lat}/{cardcolor}/{type}")]
        [HttpGet]
        public IHttpActionResult test (decimal longi, decimal lat, string cardcolor, string type )
        {
            var axalistnotfiltered = _ctx.axaProviders.Select(w => new { w.Longtude, w.Latitude, w.Provider, w.NetworkName, w.Type, w.Address, w.Phone, w.LocationOnMap }).Where(s => s.NetworkName.Contains(cardcolor) && s.Type.Contains(type)).ToList();
            //List<string> test_long = new List<string>();
            //List<string> test_lat = new List<string>();
            //List<Tuple<string, string>> xx = new List<Tuple<string, string>>();
            //Dictionary<string, string> dic = new Dictionary<string, string>();
            //decimal lon = decimal.Parse(longi);
            //decimal lati = decimal.Parse(lat);
            decimal lon = longi;
            decimal lati = lat;
            List<KeyValuePair<decimal, decimal>> dict = new List<KeyValuePair<decimal, decimal>>();
            List<decimal> euclidian = new List<decimal>();
            int c = 0;
            foreach (var axa in axalistnotfiltered)
            {
                dict.Add(new KeyValuePair<decimal, decimal>(decimal.Parse(axa.Longtude), decimal.Parse(axa.Latitude)));
                //test_lat.Add(axa.Latitude);
                //test_long.Add(axa.Longtude);

            }
            foreach (var x in dict)
            {
                euclidian.Add(Sqrt((lon - dict[c].Key) * (lon - dict[c].Key) + (lati-dict[c].Value)* (lati - dict[c].Value)));
                c++;
            }
            c = 0;
            //var ascending = euclidian.OrderBy(i => i);
            //var res = ascending.Take(5);
            //euclidian.Sort();
            List<decimal> testlist = new List<decimal>(euclidian);
            euclidian.Sort();
            List<decimal> res = new List<decimal>();
            foreach (var rr in euclidian)
            {
                if (c == 5)
                    break;
                res.Add(rr);
                c++;
            }
            c = 0;
            var retres = _ctx.axaProviders.Select(w => new { w.Longtude, w.Latitude, w.Provider, w.NetworkName, w.Type, w.Address, w.Phone, w.LocationOnMap }).Where(s => s.NetworkName.Contains("aaa")).ToList();
            foreach(var t in testlist)
            {
                if (c == 5)
                    break;
                int ind = testlist.IndexOf(res[c]);
                retres.Add(axalistnotfiltered[ind]);
                c++;

            }
            c = 0;
            List<messages> qwe = new List<messages>();
            foreach (var axa in retres)
            {
                messages qw = new messages();

                c++;
                qw.text = "Address " + c + ": " + axa.Address.Replace("\"\r\n", "") + "\n Phone(s): " + axa.Phone + "\n Location on map: " + axa.LocationOnMap;
                qwe.Add(qw);

            }
            var wx = new { messages = qwe };
            //var ee = testlist.IndexOf(res[0]);
            //var ret = axalistnotfiltered[ee];
            return Ok(wx);
        }

        [Route("api/test/{longi}")]
        [HttpGet]
        public IHttpActionResult test2(float longi)
        {
            List<messages> qwe = new List<messages>();
            //foreach (var axa in providerList)
            {
                messages qw = new messages();

                qw.text = "This is echo: " + longi;
                qwe.Add(qw);

            }
            var x = new { messages = qwe };
            return Ok(x);
        }
    }
}