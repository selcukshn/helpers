using System.Text.RegularExpressions;
using System.Text.Unicode;

namespace Mvc.Helpers
{
    public class StringOperation : IDisposable
    {
        private string Text { get; set; }
        private bool _disposed = false;
        private char[] TurkishChars = new char[] { 'ç', 'Ç', 'ğ', 'Ğ', 'ı', 'İ', 'ö', 'Ö', 'ş', 'Ş', 'ü', 'Ü' };
        private Dictionary<char, char> TrToEng = new Dictionary<char, char>{
            {'ç','c'},{'Ç','C'},{'ğ','g'},{'Ğ','G'},{'ı','i'},{'İ','I'},{'ö','o'},{'Ö','O'},{'ş','s'},{'Ş','S'},{'ü','u'},{'Ü','U'}
        };
        public StringOperation(string text)
        {
            this.Text = text;
        }
        public StringOperation Trim()
        {
            this.Text = this.Text.Trim();
            return this;
        }
        public StringOperation ReplaceSpace(char replaceChar = '-')
        {
            this.Text = this.Text.Replace(' ', replaceChar);
            return this;
        }
        public StringOperation TurkishToEnglish()
        {
            foreach (var et in TrToEng)
            {
                this.Text = this.Text.Replace(et.Key, et.Value);
            }
            return this;
        }
        public StringOperation RemoveMultipleSpaces()
        {
            RemoveMultipleSpacesRecursive();
            return this;
        }
        private void RemoveMultipleSpacesRecursive(int i = 0, int spaceCount = 0, int spaceStartIndex = 0, int spaceEndIndex = 0)
        {
            if (i >= this.Text.Length)
                return;
            bool spaceCountMoreThanZero = spaceCount > 0;
            bool charIsWhiteSpace = char.IsWhiteSpace(this.Text[i]);

            if (charIsWhiteSpace)
                spaceCount++;

            if (spaceCount == 1 && charIsWhiteSpace)
                spaceStartIndex = i;

            if (spaceCountMoreThanZero && !charIsWhiteSpace)
                spaceEndIndex = i;

            if (spaceCountMoreThanZero && spaceStartIndex != 0 && spaceEndIndex != 0)
            {
                if (spaceCount >= 2)
                {
                    string spaces = this.Text[spaceStartIndex..spaceEndIndex];
                    spaces = " ";
                    this.Text = $"{this.Text[0..spaceStartIndex]}{spaces}{this.Text[spaceEndIndex..this.Text.Length]}";
                    i = spaceStartIndex + 1;
                }
                spaceStartIndex = 0;
                spaceEndIndex = 0;
                spaceCount = 0;
            }

            RemoveMultipleSpacesRecursive(i + 1, spaceCount, spaceStartIndex, spaceEndIndex);

        }
        public StringOperation ToLower()
        {
            this.Text = this.Text.ToLower();
            return this;
        }
        public StringOperation AppendGuid(char bond = '-')
        {
            this.Text = $"{this.Text}{bond}{Guid.NewGuid().ToString("N")}";
            return this;
        }
        public StringOperation ReplaceSpecialChars(string replaceChar = "")
        {
            var regexItem = new Regex(@$"[^a-zA-Z0-9\- ]");
            this.Text = regexItem.Replace(this.Text, replaceChar);
            return this;
        }

        public override string ToString() => this.Text;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            this.Text = string.Empty;
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~StringOperation() => this.Dispose(false);
    }
}