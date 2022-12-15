using System.Text.RegularExpressions;

var input = File.ReadAllText($"Resources\\Example1.txt");

var score = Part1(input, 2000000);
Console.WriteLine($"Result Part1: {score}");

score = Part2(input, new Range(0, 4000000));
Console.WriteLine($"Result Part2: {score}");

// In the row where y=2000000, how many positions cannot contain a beacon?
object Part1(string input, int yToScan)
{
    var scans = input.Split(Environment.NewLine)
                     .Select(SelectScanInfo)
                     .ToList();

    var minX = int.MaxValue;
    var maxX = int.MinValue;
    var beacons = new HashSet<string>();
    foreach (var scan in scans)
    {
        var (success, startX, endX, beaconKey) = scan.ScanFields(yToScan);
        if (!success)
            continue;

        minX = Math.Min(minX, startX);
        maxX = Math.Max(maxX, endX);
        if (beaconKey is not null && !beacons.Contains(beaconKey))
            beacons.Add(beaconKey);
    }

    var score = maxX - minX - beacons.Count + 1;
    return score;
}

//What is its tuning frequency?
object Part2(string input, Range rangeSearch)
{
    const ulong ratioFrequency = 4000000;

    var scans = input.Split(Environment.NewLine)
                     .Select(SelectScanInfo)
                     .ToList();

    foreach (var y in Enumerable.Range(rangeSearch.Start, rangeSearch.End)) 
    {
        var ranges = new Ranges();
        foreach (var scan in scans)
        {
            var (success, startX, endX, _) = scan.ScanFields(y);
            if (!success)
                continue;

            ranges.AddRange(new Range(startX, endX));
        }

        var x = ranges.GetFirstNumberOutsideRange(rangeSearch);
        if (x.HasValue)
            return (ulong)x.Value * ratioFrequency + (ulong)y;
    }

    return -1;
}

ScanInfo SelectScanInfo(string input)
{
    var regex = new Regex(@"\-?[0-9]+");
    var matches = regex.Matches(input).Select(x => int.Parse(x.Value)).ToArray();

    var sensorPosition = new Point(matches[0], matches[1]);
    var beaconPosition = new Point(matches[2], matches[3]);
    var scanInfo = new ScanInfo(sensorPosition, beaconPosition);

    return scanInfo;
}

class ScanInfo
{
    public readonly int Range;

    public Point SensorPosition { get; set; }
    public Point BeaconPosition { get; set; }

    public ScanInfo(Point sensorPosition, Point beaconPosition)
    {
        SensorPosition = sensorPosition;
        BeaconPosition = beaconPosition;
        Range = GetRange();
    }

    public int GetRange() => Math.Abs(BeaconPosition.X - SensorPosition.X) + Math.Abs(BeaconPosition.Y - SensorPosition.Y);

    public (bool Success, int StartX, int EndX, string? BeaconKey) ScanFields(int y)
    {
        var minY = SensorPosition.Y - Range;
        var maxY = SensorPosition.Y + Range;
        var deltaY = Math.Abs(SensorPosition.Y - y);

        if (y < minY || y > maxY)
            return (false, 0, 0, null);

        var startX = (SensorPosition.X - Range) + deltaY;
        var endX = (SensorPosition.X + Range) - deltaY;
        if (BeaconPosition.Y == y)
            return (true, startX, endX, BeaconPosition.ToString());

        return (true, startX, endX, null);
    }
}

public class Point
{
    public int X { get; set; }
    public int Y { get; set; }
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
    public override string ToString() => $"X:{X},Y:{Y}";
}

public class Ranges
{
    public List<Range> Items = new List<Range>();

    //Add and merge with exist ranges
    public void AddRange(Range range)
    {
        var rangesBefore = Items.Where(x => x.Start <= range.End);
        var rangesAfter = Items.Where(x => x.End >= range.Start);

        var ranges = rangesBefore.Intersect(rangesAfter).ToList();
        foreach (var nextRange in ranges)
            Items.Remove(nextRange);

        ranges.Add(range);

        var start = ranges.Min(x => x.Start);
        var end = ranges.Max(x => x.End);

        Items.Add(new Range(start, end));
        Items = Items.OrderBy(x => x.Start).ToList();
    }

    //Required to Part2
    //Get First number from param of range, which is not contained in this
    public int? GetFirstNumberOutsideRange(Range range)
    {
        if (!Items.Any())
            return null;

        if (Items.Count == 1)
        {
            var single = Items.Single();
            if (range.Start < single.Start)
                return range.Start;
            if (range.End > single.End)
                return single.End;

            return null;
        }

        return Items.First().End + 1;
    }
}

public class Range
{
    public int Start { get; set; }
    public int End { get; set; }

    public Range(int start, int end)
    {
        Start = start;
        End = end;
    }
}