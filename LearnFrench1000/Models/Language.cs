using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnFrench1000.Models
{
    public class Language
    {
        #region Attributes

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private List<Word> words;

        public List<Word> Words
        {
            get { return words; }
            set { words = value; }
        }



        #endregion

        public Language(string name, List<Word> words)
        {
            Name = name;
            Words = words;
        }
    }
}
