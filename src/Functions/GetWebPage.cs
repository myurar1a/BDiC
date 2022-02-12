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
        static readonly HttpClient client = new HttpClient();
        private String? _url;
        private HttpResponseMessage? _response;
        private String? _source;
        private IHtmlDocument? _document;

        public async Task<HttpResponseMessage> GetResponseAsync(String url)
        {
            this._url = url;

            try
            {
                this._response = await client.GetAsync(this._url);
                return this._response;
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
                return null;
            }
        }

        public async Task<String> GetSourceAsync(String url)
        {
            this._url = url;

            try
            {
                this._response = await client.GetAsync(this._url);
                this._source = await this._response.Content.ReadAsStringAsync();
                return this._source;
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
                return null;
            }
        }

        public async Task<IHtmlDocument> GetDocumentAsync(String url)
        {
            this._url = url;

            try
            {
                this._response = await client.GetAsync(this._url);
                this._source = await this._response.Content.ReadAsStringAsync();
                
                // parser の利用はこのメソッドのみなので、フィールドに含まない
                var parser = new HtmlParser();
                this._document = await parser.ParseDocumentAsync(this._source);
                return this._document;
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
                return null;
            }
        }

        public HttpResponseMessage? Response
        {
            get { return this._response; }
        }
        public String? Source
        {
            get { return this._source; }
        }
        public IHtmlDocument? Document
        {
            get { return this._document; }
        }
    }
}
