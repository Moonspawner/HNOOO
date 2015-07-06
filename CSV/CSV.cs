using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSV
{
    public class CSVDocument : IEnumerable<Dictionary<object, string>>
    {
        private string[] titles; //null if no titles are given
        private string[][] data; //because working with a 2D array isn't too pleasant in this case, although it would map better on representing a table

        public string[] this[int index] => data[index];
        public string[] this[string index] => data[Array.IndexOf(titles, index)];

        public CSVDocument(string file, bool heading = true, char columnSeperator = ';', char rowSeperator = '\n')
        {
            var table = file.Split(rowSeperator).Select(row => row.Split(columnSeperator).ToArray()).ToArray();

            if (heading) {
                titles = table[0];
                data = table.Skip(1).ToArray();
            } else {
                titles = null;
                data = table;
            }
        }

        public IEnumerator<Dictionary<object, string>> GetEnumerator()
        {
            for (var row = 0; row < data.Length; row++)
            {
                var dic = new Dictionary<object, string>(this[row].Length);
                for (var columnNumber = 0; columnNumber < this[row].Length; columnNumber++)
                {
                    dic.Add(titles != null ? (object)titles[columnNumber] : (object)columnNumber, this[row][columnNumber]);
                }

                yield return dic;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
