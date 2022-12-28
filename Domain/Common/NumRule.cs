using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{

    public class Licensor
    {
        private string _organisation = string.Empty;
        private ApplicationModel _application;

        public Licensor(string organisation, ApplicationModel application)
        {
            _organisation = organisation;
            _application = application;
        }

        private string CustomerName
        {
            get
            {
                var orgName = _organisation.Replace(" ", "");
                var retVal = orgName;
                switch (_application.id)
                {
                    case 1:
                        retVal = string.Format("{0}FAIRS", orgName);
                        break;
                    case 2:
                        retVal = string.Format("{0}CMF", orgName);
                        break;
                    case 3:
                        retVal = string.Format("{0}UAW", orgName);
                        break;
                }
                if(retVal.Length % 5 == 0)
                {
                    retVal = retVal + retVal.Substring(0, 1);
                }
                return retVal.ToUpper();
            }
        }

        public string CreateLicenseKey()
        {
            Tuple<Dictionary<int, List<char>>, Dictionary<int, List<int>>>  dict = PrepareDictionary();
            return GetKey(dict.Item1, dict.Item2);
        }

        public bool ValidateLicenseKey(string key)
        {
            Tuple<Dictionary<int, List<char>>, Dictionary<int, List<int>>> dict = PrepareDictionary();
            var alphabets = new String(key.Where(Char.IsLetter).ToArray());
            var sections = key.Split('-');
            if (alphabets.Length != 8 || sections.Count() != 5)
                return false;
            var alphaRule = new AlphabetRule1(dict.Item1, CustomerName);
            if(alphaRule.Validate(alphabets) && ValidSectionNumbers(sections, CustomerName))
            {
                return true;
            }
            return false;
        }

        private bool ValidSectionNumbers(string[] sections, string customerName)
        {
            bool retVal = false;
            var sec1Rule = new Section1Rule(customerName, 2);
            var sec2Rule = new Section2Rule(customerName, 2);
            var sec3Rule = new Section3Rule(customerName, 3);
            var sec4Rule = new Section4Rule(customerName, 2);
            var sec5Rule = new Section5Rule(customerName, 3);
            if(sec1Rule.Validate(customerName, new String(sections[0].Where(Char.IsDigit).ToArray()))  &&
                sec2Rule.Validate(customerName, new String(sections[1].Where(Char.IsDigit).ToArray())) &&
                sec3Rule.Validate(customerName, new String(sections[2].Where(Char.IsDigit).ToArray())) &&
                sec4Rule.Validate(customerName, new String(sections[3].Where(Char.IsDigit).ToArray())) &&
                sec5Rule.Validate(customerName, new String(sections[4].Where(Char.IsDigit).ToArray())))
            {
                retVal = true;
            }
            return retVal;
        }

        private Tuple<Dictionary<int, List<char>>, Dictionary<int, List<int>>> PrepareDictionary()
        {
            var probableAlphabets = new Dictionary<int, List<char>>();
            var probableDigits = new Dictionary<int, List<int>>();
            int alphaCount = Constants.Pattern.Count(i => i == '#');

            for (int i = 1; i <= alphaCount; i++)
            {
                AlphabetRule objAlphaRule1 = new AlphabetRule1(CustomerName, CustomerName.Length, i);
                AddOrUpdateAlphaDictionary(probableAlphabets, Helper.GetCharPositionIndexIndex(i), objAlphaRule1.GenerateList());
            }

            AddOrUpdateDigitDictionary(probableDigits, 1, new Section1Rule(CustomerName, 2).GenerateList());
            AddOrUpdateDigitDictionary(probableDigits, 2, new Section2Rule(CustomerName, 2).GenerateList());
            AddOrUpdateDigitDictionary(probableDigits, 3, new Section3Rule(CustomerName, 3).GenerateList());
            AddOrUpdateDigitDictionary(probableDigits, 4, new Section4Rule(CustomerName, 2).GenerateList());
            AddOrUpdateDigitDictionary(probableDigits, 5, new Section5Rule(CustomerName, 3).GenerateList());

            return new Tuple<Dictionary<int, List<char>>, Dictionary<int, List<int>>>(probableAlphabets, probableDigits);

        }

        private void AddOrUpdateAlphaDictionary(Dictionary<int, List<char>> probableAlphabets, int i, List<char> list)
        {
            if (probableAlphabets.ContainsKey(i))
            {
                probableAlphabets[i] = list;
            }
            else
            {
                probableAlphabets.Add(i, list);
            }
        }

        private void AddOrUpdateDigitDictionary(Dictionary<int, List<int>> probableDigits, int i, List<int> list)
        {
            if (probableDigits.ContainsKey(i))
            {
                probableDigits[i] = list;
            }
            else
            {
                probableDigits.Add(i, list);
            }
        }
        private string GetKey(Dictionary<int, List<char>> probableAlphabets, Dictionary<int, List<int>> probableDigits)
        {
            StringBuilder builder = new StringBuilder();
            int alphaIndex = 1;
            int sectionIndex = 1;
            foreach (string item in Constants.Pattern.Split('-'))
            {
                int numCounter = 0;
                foreach (char c in item.ToCharArray())
                {
                    switch (c)
                    {
                        case '#':
                            builder.Append(probableAlphabets[Helper.GetCharPositionIndexIndex(alphaIndex)].ElementAt(new Random().Next(0, 2)));
                            alphaIndex++;
                            break;
                        case '%':
                            builder.Append(probableDigits[sectionIndex].ElementAt(numCounter));
                            numCounter++;
                            break;
                    }
                }
                sectionIndex++;
                builder.Append('-');
            }
            return builder.ToString().TrimEnd('-');
        }

    }

    public static class Constants
    {
        public const string Pattern = "%#%#-#%#%-%%%#-%##%-#%%%";
        // public static char[] AlphabetsArrey = new char[] { 'A', 'Q', 'P', 'S', 'L', 'W', 'O', 'M', 'N', 'B', 'C', 'V', 'X', 'D', 'F', 'G', 'I', 'U', 'Y', 'H', 'J', 'K', 'T', 'R', 'E', 'Z' };
        public static char[] AlphabetsArray = new char[] { 'A', 'Q', 'P', 'S', 'L', 'W', 'O', 'C', 'M', 'N', 'B', 'V', 'X', 'D', 'F', 'G', 'I', 'U', 'Y', 'H', 'J', 'K', 'T', 'R', 'E', 'Z' };
        public const int RemainderBy = 5;
    }


    public enum SectionWiseNumRule
    {
        SumMustBy5AndRemainder,
        SumMustDivideBy7,
        SumMust15,
        SumMust9,
        SumMustBeDivideBy1stChar
    }

    internal abstract class NumRule
    {
        protected int NumCount { get; set; }
        public abstract SectionWiseNumRule Rule { get; }
        public NumRule(string customerName, int numLength)
        {
            CustomerName = customerName;
            NumCount = numLength;
        }
        protected string CustomerName { get; set; }

        public abstract bool Validate(string value,string Enteredkey);

        public abstract List<int> GenerateList();
    }

    //SumMustBy5AndRemainder
    internal class Section1Rule : NumRule
      {

        public Section1Rule(string customerName, int numCount) : base(customerName, numCount) { }

        public Dictionary<int, List<int>> ProbableDigits = new Dictionary<int, List<int>>();


        public override SectionWiseNumRule Rule
        {
            get
            {
                return SectionWiseNumRule.SumMustBy5AndRemainder;
            }
        }
        public override List<int> GenerateList()
        {          
            int SumValue = (CustomerName.Length % Constants.RemainderBy) + 5;
            var random = new Random();
            int num1 = random.Next(0, SumValue);
            int num2 = SumValue - num1;
            return new List<int> { num1, num2 };
        }

        public override bool Validate(string value, string Enteredkey)
        {
            base.CustomerName = value;
            bool validateFlg = false;            
            int SumValue = (CustomerName.Length % Constants.RemainderBy) + 5;
            var digits = new String(Enteredkey.Where(Char.IsDigit).ToArray());
            if (digits.Length == NumCount)
            {
                int total = 0;
                for (int i = 0; i < digits.Length; i++)
                {
                    total += Convert.ToInt32(digits.Substring(i, 1));
                }
                if (SumValue == total)
                {
                    validateFlg = true;
                }
            }
            return validateFlg;
        }
    }

    //SumMustDivideBy7
    internal class Section2Rule : NumRule
    {
        public Section2Rule(string customerName, int numCount) : base(customerName, numCount) { }

        public override SectionWiseNumRule Rule
        {
            get
            {
                return SectionWiseNumRule.SumMustDivideBy7;
            }
        }
        public override List<int> GenerateList()
        {
            string[] NumArrey = new string[] { "07", "16", "25", "34", "43", "52", "61", "70", "59", "68", "77", "95", "86" };
            var random = new Random();
            int index = random.Next(0, NumArrey.Count()-1);
            string finalNum = NumArrey[index];
            return new List<int> { Convert.ToInt32(finalNum.Substring(0, 1)), Convert.ToInt32(finalNum.Substring(1)) };
        }

        public override bool Validate(string value, string Enteredkey)
        {
            base.CustomerName = value;
            bool validateFlg = false;

            var digits = new String(Enteredkey.Where(Char.IsDigit).ToArray());
            if (digits.Length == NumCount)
            {
                int total = 0;
                for (int i = 0; i < digits.Length; i++)
                {
                    total += Convert.ToInt32(digits.Substring(i, 1));
                }
                if (total % 7 == 0)
                {
                    validateFlg = true;
                }
            }
            return validateFlg;
        }
    }

    //SumMust15
    internal class Section3Rule : NumRule
    {
        public Section3Rule(string customerName, int numCount) : base(customerName, numCount) { }

        public override SectionWiseNumRule Rule
        {
            get
            {
                return SectionWiseNumRule.SumMust15;
            }
        }
        public override List<int> GenerateList()
        {         
            var random = new Random();
            int num1 = random.Next(2, 9);
            int num2 = 15 / num1;
            int num3 = 15 - (num1 + num2);
            return new List<int> { num1, num2,num3 };
        }

        public override bool Validate(string value, string Enteredkey)
        {
            base.CustomerName = value;
            bool validateFlg = false;

            var digits = new String(Enteredkey.Where(Char.IsDigit).ToArray());
            if (digits.Length == NumCount)
            {
                int total = 0;
                for (int i = 0; i < digits.Length; i++)
                {
                    total += Convert.ToInt32(digits.Substring(i, 1));
                }
                if (total == 15)
                {
                    validateFlg = true;
                }
            }

            return validateFlg;
        }
    }

    //SumMust9
    internal class Section4Rule : NumRule
    {
        public Section4Rule(string customerName, int numCount) : base(customerName, numCount) { }

        public override SectionWiseNumRule Rule
        {
            get
            {
                return SectionWiseNumRule.SumMust9;
            }
        }
        public override List<int> GenerateList()
        {
            var random = new Random();
            int num1 = random.Next(0, 9);
            int num2 = 9-num1;           
            return new List<int> { num1, num2 };
        }

        public override bool Validate(string value, string Enteredkey)
        {
            base.CustomerName = value;
            bool validateFlg = false;

            var digits = new String(Enteredkey.Where(Char.IsDigit).ToArray());
            if (digits.Length == NumCount)
            {
                int total = 0;
                for (int i = 0; i < digits.Length; i++)
                {
                    total += Convert.ToInt32(digits.Substring(i, 1));
                }
                if (total == 9)
                {
                    validateFlg = true;
                }
            }

            return validateFlg;
        }
    }

    //SumMustBeDivideBy1stChar
    internal class Section5Rule : NumRule
    {
        public Section5Rule(string customerName, int numCount) : base(customerName, numCount) { }

        public override SectionWiseNumRule Rule
        {
            get
            {
                return SectionWiseNumRule.SumMustBeDivideBy1stChar;
            }
        }
        public override List<int> GenerateList()
        {
            var r1 = new Random();
            var n1 = r1.Next(2, 4);
            var n2 = r1.Next(0, 9);
            var n3 = n1 - (n1 + n2) % n1;
            return new List<int> { n1, n2, n3 };
        }

        public override bool Validate(string value, string Enteredkey)
        {
            base.CustomerName = value;
            bool validateFlg = false;

            var digits = new String(Enteredkey.Where(Char.IsDigit).ToArray());
            if (digits.Length == NumCount)
            {
                int total = 0;
                for (int i = 0; i < digits.Length; i++)
                {
                    total += Convert.ToInt32(digits.Substring(i, 1));
                }
                if (total % Convert.ToInt32(digits.Substring(0, 1)) == 0)
                {
                    validateFlg = true;
                }
            }
            return validateFlg;
        }
    }

}
