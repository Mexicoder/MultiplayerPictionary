using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pictionary
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
    }
}
