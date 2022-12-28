using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{

    internal abstract class AlphabetRule
    {
        public AlphabetRule(string customerName)
        {
            CustomerName = customerName;
        }
       // public abstract SectionWiseNumRule Rule { get; }
        public AlphabetRule(string customerName,int count,int patternCharIndex)

        {
            CustomerName = customerName;
            OrgNameCharCount = count;
            CharIndex = patternCharIndex;
        }
        protected string CustomerName { get; set; }
        protected int OrgNameCharCount { get; set; }
        protected int CharIndex { get; set; }

        public abstract bool Validate(string enteredkey);

        public abstract List<char> GenerateList();

        private int getIndexOfChar(char Alphabate)
        {          
            int ind = Array.IndexOf(Constants.AlphabetsArray, Alphabate);
            return ind; //Array.IndexOf(AlphabetsArrey, Alphabate);
        }
    }


    internal class AlphabetRule1 : AlphabetRule
    {
        internal AlphabetRule1(Dictionary<int, List<char>> alphaDict, string customerName)
            :base(customerName)
        {
            ProbableAlphabets = alphaDict;
        }

        public AlphabetRule1(string customerName, int orgCount,int patternCharIndex) : base(customerName, orgCount, patternCharIndex) { }
        public Dictionary<int, List<char>> ProbableAlphabets { get; set; }

        public override List<char> GenerateList()
        {          
            char char1 = getFinalAlphabetRule1();
            char char2 = getFinalAlphabetRule2(char1);
            char char3 = getFinalAlphabetRule3();
            return new List<char> { char1,char2,char3 };
        }

        public override bool Validate(string alphaKeys)
        {
            bool validateFlg = false;
            List<char> charList = null;

            for (int i = 1; i <= alphaKeys.Length; i++)
            {
                charList = new List<char>();
                int CharPositionIndex = Helper.GetCharPositionIndexIndex(i);
                if(ProbableAlphabets.ContainsKey(CharPositionIndex) && ProbableAlphabets[CharPositionIndex].Contains(Convert.ToChar(alphaKeys.Substring(i-1, 1))))
                {
                    validateFlg = true;
                }
                else
                { 
                    validateFlg = false;
                    break;
                }
            }
            return validateFlg;
        }

        private char getFinalAlphabetRule1()
        {
            int remainder = 0;
            int charIndex = 0;
            remainder = OrgNameCharCount % 5;
            charIndex = (remainder * Helper.GetCharPositionIndexIndex(CharIndex)) <= OrgNameCharCount - 1 ? remainder * Helper.GetCharPositionIndexIndex(CharIndex) : 
                (remainder * Helper.GetCharPositionIndexIndex(CharIndex)) % OrgNameCharCount;

            charIndex = charIndex <= 0 ? 0 : charIndex-1;

            return CustomerName[charIndex];
        }

        private char getFinalAlphabetRule2(Char Alphabate)
        {
            int indexOfAlphabate = 25 - Array.IndexOf(Constants.AlphabetsArray, Alphabate);
            return Constants.AlphabetsArray[indexOfAlphabate - 1];
        }

        private char getFinalAlphabetRule3()
        {
            int remainder = 0;
            int charIndex = 0;
            remainder = OrgNameCharCount % 5;

            charIndex = (remainder * Helper.GetCharPositionIndexIndex(CharIndex)) + CustomerName.Length - 1;
            if (charIndex >= 26)
            {
                charIndex = charIndex % 26;
            }

            return Constants.AlphabetsArray[charIndex];
        }


        private void AddOrUpdateAlphaDictionary(int i, List<char> list)
        {
            if (ProbableAlphabets.ContainsKey(i))
            {
                ProbableAlphabets[i] = list;
            }
            else
            {
                ProbableAlphabets.Add(i, list);
            }
        }

    }

    public static class Helper
    {
        public static int GetCharPositionIndexIndex(int charIndex)
        {
            var charPositionIndex = 1;
            switch (charIndex)
            {
                case 1:
                    charPositionIndex = 2;
                    break;
                case 2:
                    charPositionIndex = 4;
                    break;
                case 3:
                    charPositionIndex = 5;
                    break;
                case 4:
                    charPositionIndex = 7;
                    break;
                case 5:
                    charPositionIndex = 12;

                    break;
                case 6:
                    charPositionIndex = 14;
                    break;
                case 7:
                    charPositionIndex = 15;
                    break;
                case 8:
                    charPositionIndex = 17;
                    break;
            }
            return charPositionIndex;
        }
    }
}
