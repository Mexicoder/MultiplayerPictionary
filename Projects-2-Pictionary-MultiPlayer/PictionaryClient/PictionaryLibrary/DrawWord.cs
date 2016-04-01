using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictionaryLibrary
{
    class DrawWord
    {
        public string word_ { get; set; }
        public string wordType_ { get; set; }
        public string wordHintUnderscores_ { get; set; }
        public string wordHintFirstLetter_ { get; set; }

        public DrawWord(string word, string wordType, string wordHintUnderscores, string wordHintFirstLetter)
        {
            this.word_ = word;
            this.wordType_ = wordType;
            this.wordHintUnderscores_ = wordHintUnderscores;
            this.wordHintFirstLetter_ = wordHintFirstLetter;
        }

        public static DrawWord ChooseDrawWord(List<DrawWord> dwList)
        {
            Random r = new Random();
            DrawWord dw = dwList[r.Next(0, dwList.Count)]; //pick a random DrawWord from the list
            return dw;
        }

        public static List<DrawWord> PopulateDrawWordList()
        {
            List<DrawWord> dwList = new List<DrawWord>();

            //Generate words:
            dwList.Add(new DrawWord("DARK", "ADJECTIVE", "_ _ _ _", "D _ _ _"));
            dwList.Add(new DrawWord("LASER", "NOUN", "_ _ _ _ _", "L _ _ _ _"));
            dwList.Add(new DrawWord("WOBBLE", "VERB", "_ _ _ _ _ _", "W _ _ _ _ _"));
            dwList.Add(new DrawWord("SKI GOGGLES", "NOUN", "_ _ _  _ _ _ _ _ _ _", "S _ _  G _ _ _ _ _ _"));
            dwList.Add(new DrawWord("MAIL MAN", "NOUN", "_ _ _ _  _ _ _ ", "M _ _ _  M _ _"));
            dwList.Add(new DrawWord("SONG", "NOUN", "_ _ _ _ ", "S _ _ _"));
            dwList.Add(new DrawWord("WAX", "NOUN", "_ _ _", "W _ _"));
            dwList.Add(new DrawWord("WAND", "NOUN", "_ _ _ _", "W _ _ _"));
            dwList.Add(new DrawWord("WAX", "NOUN", "_ _ _", "W _ _"));
            dwList.Add(new DrawWord("HONEYMOON", "NOUN", "_ _ _ _ _  _ _ _ _", "H _ _ _ _  M _ _ _"));
            dwList.Add(new DrawWord("NEW YEAR", "NOUN", "_ _ _  _ _ _ _", "N _ _  Y _ _ _"));

            return dwList;
        }

        public static DrawWord GenerateDrawWord()
        {
            var dwList = DrawWord.PopulateDrawWordList();
            var drawWord = DrawWord.ChooseDrawWord(dwList);
            return drawWord;
        }
    }
}
