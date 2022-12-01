
namespace Day04_GiantSquid.Models
{
    internal class BingoGrid
    {
        private readonly Dictionary<int, List<BingoValue>> _cache = new();
        private int _length { get; set; }

        public BingoValue[,] Values { get; set; }

        public BingoGrid(string valueStr)
        {
            var rows = valueStr.Split(Environment.NewLine);
            Values = new BingoValue[rows.Length, rows.Length];
            for (int i = 0; i < rows.Length; i++)
            {
                var a = rows[i].Split(" ");
                var cells = rows[i].Split(" ").Where(x => !string.IsNullOrEmpty(x)).Select(x => int.Parse(x)).ToArray();
                for (int j = 0; j < cells.Length; j++)
                {
                    var value = new BingoValue(cells[j], false);
                    Values[i, j] = value;
                    if (_cache.ContainsKey(value.Value))
                    {
                        _cache[value.Value].Add(value);
                        continue;
                    }

                    _cache.Add(value.Value, new List<BingoValue> { value });    
                }
            }
            _length = rows.Length;
        }

        public bool SelectNumber(int number)
        {
            if (!_cache.TryGetValue(number, out var result))
                return false;

            foreach (var item in result)
                item.IsSelected = true;

            return true;
        }

        public bool HasBingo()
        {
            for (int i = 0; i < _length; i++)
            {
                var tempCountX = 0;
                var tempCountY = 0;
                for (int j = 0; j < _length; j++)
                {
                    Globals.Test2++;
                    tempCountX += Values[i, j].IsSelected == true ? 1 : 0;
                    tempCountY += Values[j, i].IsSelected == true ? 1 : 0;
                }

                if (tempCountX == _length || tempCountY == _length)
                    return true;
            }

            return false;
        }

        public int GetScore()
        {
            var score = 0;
            for (int i = 0; i < _length; i++)
                for (int j = 0; j < _length; j++)
                    score += Values[i, j].IsSelected == false ? Values[i, j].Value : 0;

            return score;
        }

    }

    internal class BingoValue
    {
        public int Value { get; set; }
        public bool IsSelected { get; set; }

        public BingoValue(int value, bool isSelected)
        {
            Value = value;
            IsSelected = isSelected;
        }
    }
}
