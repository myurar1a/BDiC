using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;

namespace BDiC
{
    public class ParseBD
    {
        private IHtmlDocument? _bdDocument;
        private String? _latestDate;
        

        // どのメソッドからでもドキュメントを取得するためのメソッド
        public async Task GetBDDocumentAsync()
        {
            var bd = new GetWebPage();
            this._bdDocument = await bd.GetDocumentAsync("https://www.buffalo-direct.com/directshop/");
        }

        // 更新日の確認
        public async Task<String?> OutputDateAsync()
        {
            await GetBDDocumentAsync();
            try
            {
                this._latestDate = this._bdDocument?.QuerySelector("#indexSaleIn > ul:nth-child(5) > li:nth-child(1) > p.columnLeft")?.TextContent;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
            }
            return this._latestDate;
        }

        public async Task IncomeListAsync(bool newInstance)
        {
            // データテーブル作成
            DataTable dt = new DataTable();
            dt.Columns.Add("No.");
            dt.Columns.Add("名称");
            dt.Columns.Add("状態");
            dt.Columns.Add("コンディション");
            dt.Columns.Add("保証期間");
            dt.Columns.Add("価格");
            dt.Columns.Add("リンク");

            if (newInstance)
                await GetBDDocumentAsync();
            var rawIncome = this._bdDocument?.QuerySelectorAll("#indexSaleIn > ul:nth-child(5) > li:nth-child(1) > p.text");

            // 製品情報
            List<String> incomeInfoList = new();
            foreach (var element in rawIncome!)
            {
                int removeIndex = element.InnerHtml.IndexOf("<font");
                if (removeIndex > 0)
                {
                    incomeInfoList.Add(element.InnerHtml.Substring(0, removeIndex));
                }
            }

            // 製品名称・状態・コンディション
            List<String> incomeNameList = new();
            List<String> incomeStateList = new();
            List<String> incomeConditionList = new();
            List<String> incomeWarrantyList = new();
            foreach (var info in incomeInfoList)
            {
                int outletIndex = info.IndexOf("アウトレット");
                int usedIndex = info.IndexOf("中古");
                int newIndex = info.IndexOf("未使用");
                int goodIndex = info.IndexOf("整備済");
                int checkIndex = info.IndexOf("検査済");
                int substringIndex = info.IndexOf("(保証");

                if (outletIndex > 0)
                {
                    incomeNameList.Add(info.Substring(12, substringIndex - 12));
                    incomeStateList.Add("outlet");
                    incomeWarrantyList.Add("1 year");
                    if (newIndex > 0)
                        incomeConditionList.Add("new");
                    else if (goodIndex > 0)
                        incomeConditionList.Add("good");
                    else
                        incomeConditionList.Add("unknow");
                }
                else if (usedIndex > 0)
                {
                    incomeNameList.Add(info.Substring(8, substringIndex - 8));
                    incomeStateList.Add("used");
                    incomeWarrantyList.Add("30 days");
                    if (newIndex > 0)
                        incomeConditionList.Add("new");
                    else if (goodIndex > 0)
                        incomeConditionList.Add("good");
                    else if (checkIndex > 0)
                        incomeConditionList.Add("check");
                    else
                        incomeConditionList.Add("unknow");
                }
                else
                {
                    incomeNameList.Add(info);
                    incomeStateList.Add("other");
                }
            }

            /*
            // 製品保証期間
            List<String> incomeWarrantyList = new();
            foreach (var info in incomeInfoList)
            {
                int substringIndex = info.IndexOf("(保証");
                if (substringIndex > 0)
                {
                    String warranty = info.Substring(substringIndex);
                    int dayIndex = warranty.IndexOf("日");
                    int yearIndex = warranty.IndexOf("年");
                    int boolIndex = warranty.IndexOf("有り");
                    if (dayIndex > 0)
                    {
                        Match period = Regex.Match(warranty, @"\d+");
                        incomeWarrantyList.Add(period.Value + "days");
                    }
                    else if (yearIndex > 0)
                    {
                        Match period = Regex.Match(warranty, @"\d+");
                        incomeWarrantyList.Add(period.Value + "years");
                    }
                    else if (boolIndex > 0)
                        incomeWarrantyList.Add("yes");
                    else
                        incomeWarrantyList.Add("unknow");
                }
                else
                    incomeWarrantyList.Add("unknow");
            }
            */

            // 価格
            List<int> incomeValueList = new();
            foreach (var element in rawIncome!)
            {
                String tmp = element?.QuerySelector("font")?.TextContent!;
                Match productValue = Regex.Match(tmp, @"\d+");
                incomeValueList.Add(int.Parse(productValue.Value));
            }

            // URL
            List<String> incomeUrlList = new();
            foreach (var element in rawIncome!)
            {
                incomeUrlList.Add(element?.QuerySelector("a")?.GetAttribute("href")!);
            }

            // データテーブル書き込み
            for (int i = 0; i < incomeInfoList.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = i + 1;
                dr[1] = incomeNameList[i];
                dr[2] = incomeStateList[i];
                dr[3] = incomeConditionList[i];
                dr[4] = incomeWarrantyList[i];
                dr[5] = incomeValueList[i];
                dr[6] = incomeUrlList[i];
                dt.Rows.Add(dr);
            }
        }

        public String? LatestDate
        {
            get { return this._latestDate; }
        }
    }
}
