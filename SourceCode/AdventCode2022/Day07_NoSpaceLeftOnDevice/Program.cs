
var input = File.ReadAllText($"Resources\\Example1.txt");

var score = Part1(input);
Console.WriteLine($"Result Part1: {score}");

score = Part2(input);
Console.WriteLine($"Result Part2: {score}");

//Part 1
//Sum total size all catalogs with space less than 100 000
object Part1(string input)
{
    var root = CreateTree(input);

    var score = root.GetFlatChildren(true)
                    .Select(x => x.GetTotalSize(true))
                    .Where(x => x <= 100000)
                    .Sum();

    return score;
}

//Part 2
//Find min size of folder to delete for update system
//Required is 30 000 000 space
//Total space is 70 000 000
object Part2(string input)
{
    var root = CreateTree(input);

    var usedSpace = root.GetTotalSize(true);
    var freeSpace = 70000000 - usedSpace;
    var requiredSpace = 30000000 - freeSpace;

    var score = root.GetFlatChildren(true)
                    .Select(x => x.GetTotalSize(true))
                    .Where(x => x >= requiredSpace)
                    .Min();

    return score;
}

//Build directories tree
TreeNode CreateTree(string input)
{
    var data = input.Split(Environment.NewLine).Skip(2); //I can skip 2, because in examples always we are starting from root - it is my base assumption. 

    var root = new TreeNode(null, "Root");
    var cursorNode = root;
    foreach (var line in data)
    {
        //Nothing to do with this lines
        if (line.StartsWith("$ ls") || line.StartsWith("dir "))
            continue;

        //Add directory to tree [Assumption: we can not go twice time to the same]
        if (line.StartsWith("$ cd "))
        {
            var param = line[5..^0];
            if (param != "..")
            {
                cursorNode = new TreeNode(cursorNode, param);
                continue;
            }

            if (cursorNode.Parent is null)
                throw new Exception("Parent can be not null for Operation $cd ..");

            cursorNode = cursorNode.Parent;
            continue;

        }

        //Stayed only lines representing files with size
        var parts = line.Split(" ");
        cursorNode.AddFile(parts[1], int.Parse(parts[0]));
    }

    return root;
}

public class TreeNode
{
    public TreeNode? Parent { get; set; }
    public List<TreeNode> Children { get; set; } = new List<TreeNode>();
    public string CatalogName { get; set; }
    public Dictionary<string, int> Files { get; set; } = new Dictionary<string, int>(); //Name + Size

    public TreeNode(TreeNode? parent, string catalogName)
    {
        CatalogName = catalogName;
        if (parent is null)
            return;

        Parent = parent;
        Parent.Children.Add(this);
    }

    public void AddFile(string fileName, int size) => Files.Add(fileName, size);

    int totalSizeCache = -1;
    public int GetTotalSize(bool useCache) //Required refresh if added/removed file or directory
    {
        if (totalSizeCache >= 0 && useCache) 
            return totalSizeCache;

        totalSizeCache = Files.Sum(x => x.Value);
        totalSizeCache += Children.Sum(x => x.GetTotalSize(useCache));
        return totalSizeCache;
    }

    public IEnumerable<TreeNode> GetFlatChildren(bool withThis)
    {
        var startElements = withThis ? new[] { this }.ToList() : Children;

        var queue = new Queue<TreeNode>(startElements);
        while (queue.Count > 0)
        {
            var item = queue.Dequeue();
                foreach (var child in item.Children)
                    queue.Enqueue(child);

            yield return item;
        }
    }
}