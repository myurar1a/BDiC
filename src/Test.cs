﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDiC
{
    internal class Test
    {
        private String? _latestDate;
        public async Task ConfirmationOfArrivalDateAsync()
        {
            ParseBD pbd = new ParseBD();
            this._latestDate = await pbd.OutputDateAsync();
            Console.WriteLine("在庫更新日：{0}", this._latestDate?.Trim() ?? "Null");

            if (this._latestDate != null)
                Console.WriteLine("　本　日　：{0}", DateTime.Now.Date);
            else
                Environment.Exit(0);
            
            TimeRelated tr = new TimeRelated();
            bool a = tr.ConfirmationOfArrivalDate(this._latestDate);
            Console.WriteLine(a);
        }

        public async Task 

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
