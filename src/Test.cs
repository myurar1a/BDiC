using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDiC
{
    internal class Test
    {
        private String? _latestDate;
        public async Task ConfirmationOfArrivalDate()
        {
            ParseBD pbd = new ParseBD();
            this._latestDate = await pbd.OutputDate();
            Console.WriteLine(this._latestDate);

            TimeRelated tr = new TimeRelated();
            bool a = tr.ConfirmationOfArrivalDate(this._latestDate);
            Console.WriteLine(a);
        }

        /*
        static readonly HttpClient client = new HttpClient();
        public async Task GetTest()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://www.buffalo-direct.com/directshop/");
            var response1 = await client.SendAsync(request);
            var response2 = await client.GetAsync("https://www.buffalo-direct.com/directshop/");
            Console.WriteLine("OK");
        }
        */
    }
}
