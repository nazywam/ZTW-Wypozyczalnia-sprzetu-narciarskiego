using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;
using AutoMapper;
using SkiRent.Entities;
using SkiRent.Entities.FilterModels;
using SkiRent.Extensions;
using SkiRent.ViewModels.Payment;

namespace SkiRent.Services
{
    public class NBPApiService
    {
        private static string API_URL = "http://api.nbp.pl/api/exchangerates/tables/A/";

        public NBPApiService()
        {
        }

        public static Dictionary<string, string> GetExchangeTable()
        {
            string[] CODES = new string[] { "USD", "EUR", "HUF", "UAH", "CZK", "RUB" };
            Dictionary<string, string> ans = new Dictionary<string, string>();
            Stream responseFromServer = null;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "application/xml");
                Task<Stream> request = client.GetStreamAsync(API_URL);
                request.Wait();

                responseFromServer = request.Result;
            }

            if (responseFromServer != null)
            {

                XmlSerializer xml = new XmlSerializer(typeof(NbpTables));

                NbpTables tables = (NbpTables)xml.Deserialize(responseFromServer);

                foreach (NbpTableRate rate in tables.Tables[0].Rates.Where(i => CODES.Contains(i.Code)))
                {
                    ans.Add(rate.Code, rate.Mid.ToString());
                }
            }


            return ans;
        }

        [XmlRoot(ElementName = "ArrayOfExchangeRatesTable")]
        public class NbpTables
        {
            [XmlElement("ExchangeRatesTable")]
            public NbpTable[] Tables;
        }

        public class NbpTable
        {
            public string Table;
            public string No;
            public string EffectiveDate;

            [XmlArray("Rates")]
            [XmlArrayItem("Rate", typeof(NbpTableRate))]
            public List<NbpTableRate> Rates;
        }

        public class NbpTableRates
        {
        }

        public class NbpTableRate : NbpTableRates
        {
            public string Currency { get; set; }
            public string Code { get; set; }
            public double Mid;
        }
    }
}