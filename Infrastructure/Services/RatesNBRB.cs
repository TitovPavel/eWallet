using Core.Entities;
using Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class RatesNBRB : IRateService
    {
        public async Task<decimal?> GetRateOnDate(DateTime dateTime, string currency)
        {
            if(currency == "BYN")
            {
                return 1;
            }

            var date = dateTime.ToString("MM.dd.yyyy");
            string url = $"http://www.nbrb.by/API/ExRates/Rates/{currency}?onDate={date}&ParamMode=2";
            using var client = new HttpClient();

            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            var resp = await response.Content.ReadAsStringAsync();

            RateShort rate = JsonConvert.DeserializeObject<RateShort>(resp);

            return rate.Cur_OfficialRate / rate.Cur_Scale;
        }
    }
}
