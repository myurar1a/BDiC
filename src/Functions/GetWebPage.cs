using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Html.Parser;
using AngleSharp.Html.Dom;

namespace BDiC
{
    public class GetWebPage
    {
        private HttpClient client;
        private String url;
        private HttpResponseMessage response;
        private String source;
        private HtmlParser parser;
        private IHtmlDocument _document;

        public async void GetResponse(String url)
        {
            this.url = url;

            try
            {
                this.response = await client.GetAsync(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
            }
        }

        public async void GetSource(String url)
        {
            this.url = url;

            try
            {
                this.response = await client.GetAsync(url);
                this.source = await this.response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
            }
        }

        public async void GetDocument(String url)
        {
            this.url = url;

            try
            {
                this.response = await client.GetAsync(url);
                this.source = await this.response.Content.ReadAsStringAsync();
                this._document = await this.parser.ParseDocumentAsync(this.source);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
            }
        }

        public HttpResponseMessage Response { private set; get; }
        public String Source { private set; get; }
        public IHtmlDocument Document { private set; get; }
    }
}
