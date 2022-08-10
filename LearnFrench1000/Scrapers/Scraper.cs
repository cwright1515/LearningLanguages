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
        public static List<Language> scrape(Dictionary<string, string> langDict)
        {
            List<Language> languages = new List<Language>();
            try
            {
                foreach(KeyValuePair<string, string> lang in langDict)
                {
                    HtmlWeb web = new HtmlWeb();
                    
                    HtmlDocument doc = web.Load(lang.Value);

                    var wordsTable = doc.DocumentNode.SelectNodes("//tr");
                    wordsTable.RemoveAt(0);
                    List<Word> words = new List<Word>();
                    foreach (HtmlNode row in wordsTable)
                    {
                        List<string> elements = row.InnerText.Split('\n').ToList();
                        List<string> synonyms = new List<string>() {  };
                        words.Add(new Word(elements[2], elements[3], synonyms));
                    }

                    languages.Add(new Language(lang.Key, words));
                }
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
            return languages;
        }
    }
}
