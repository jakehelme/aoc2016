using System.Text.RegularExpressions;

Console.WriteLine("Day 3");

string raw;

using (var sr = new StreamReader("input.txt"))
{
    raw = sr.ReadToEnd();
}

bool IsValidTriangle(IList<int> triangle)
{
    if (
        triangle[0] + triangle[1] > triangle[2] &&
        triangle[0] + triangle[2] > triangle[1] &&
        triangle[1] + triangle[2] > triangle[0]
    )
        return true;
    return false;
}

int CountValidTrianglesVertically(string raw)
{
    var whiteSpaceRegex = new Regex(@"\s+");
    var triangles = raw.Split("\n").Select(x => whiteSpaceRegex.Split(x.Trim()).Select(y => int.Parse(y)).ToList());
    var validTriangles = 0;
    foreach (var sides in triangles)
    {
        if (IsValidTriangle(sides)) validTriangles++;
    }
    return validTriangles;
}

int CountValidTrianglesHorizontally(string raw)
{
    var whiteSpaceRegex = new Regex(@"\s+");
    var horizontalTriangles = raw.Split("\n").Select(x => whiteSpaceRegex.Split(x.Trim()).Select(y => int.Parse(y)).ToList()).ToList();
    var triangles = new List<List<int>>();
    for (var i = 0; i < horizontalTriangles.Count() - 2; i += 3)
    {
        var t1 = new List<int> { horizontalTriangles[i][0], horizontalTriangles[i + 1][0], horizontalTriangles[i + 2][0] };
        var t2 = new List<int> { horizontalTriangles[i][1], horizontalTriangles[i + 1][1], horizontalTriangles[i + 2][1] };
        var t3 = new List<int> { horizontalTriangles[i][2], horizontalTriangles[i + 1][2], horizontalTriangles[i + 2][2] };
        triangles.Add(t1);
        triangles.Add(t2);
        triangles.Add(t3);
    }
    var validTriangles = 0;
    foreach (var sides in triangles)
    {
        if (IsValidTriangle(sides)) validTriangles++;
    }
    return validTriangles;
}



Console.WriteLine($"Part 1: {CountValidTrianglesVertically(raw)} valid triangles");
Console.WriteLine($"Part 2: {CountValidTrianglesHorizontally(raw)} valid triangles");
