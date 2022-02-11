using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;

namespace BDiC
{
    public class ParseBD
    {
        private IHtmlDocument? _bdDocument;
        private String? _latestDate;
        public async Task<String> OutputDate()
        {
            var bd = new GetWebPage();
            try
            {
                this._bdDocument = await bd.GetDocument("https://www.buffalo-direct.com/directshop/");
                this._latestDate = this._bdDocument?.QuerySelector("#indexSaleIn > ul:nth-child(5) > li:nth-child(1) > p.columnLeft")?.TextContent ?? "";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
            }
            return this._latestDate;
        }

        public void IncomeList()
        {
            String? rawIncome = this._bdDocument?.QuerySelector("#indexSaleIn > ul:nth-child(5) > li:nth-child(1)")?.TextContent;
            var rawIncomeList = rawIncome?.Split("\n");
        }

        public String? LatestDate
        {
            get { return this._latestDate; }
        }
    }
}
