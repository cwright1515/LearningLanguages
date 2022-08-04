using HtmlAgilityPack;
using LearnFrench1000.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LearnFrench1000.Scrapers
{
    class Scraper
    {
        public static List<Word> scrape()
        {
            try
            {
                HtmlWeb web = new HtmlWeb();
                string url = "https://1000mostcommonwords.com/1000-most-common-french-words/";
                HtmlDocument doc = web.Load(url);

                var wordsTable = doc.DocumentNode.SelectNodes("//tr");
                wordsTable.RemoveAt(0);
                List<Word> words = new List<Word>();
                foreach (HtmlNode row in wordsTable)
                {
                    List<string> elements = row.InnerText.Split('\n').ToList();
                    words.Add(new Word(elements[2], elements[3]));
                }
                return words;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }
    }
}
