Console.WriteLine("Day 2");

string raw;

using (var sr = new StreamReader("input.txt"))
{
    raw = sr.ReadToEnd();
}

var instructionSets = raw.Split("\n").Select(x => x.ToCharArray());

string ProcessInstructionsPart1(IEnumerable<char[]> instructions)
{
    int[,] grid = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
    var position = (x: 1, y: 1);
    var code = "";

    foreach (var set in instructions)
    {
        foreach (var move in set)
        {
            switch (move)
            {
                case 'U':
                    if (position.x > 0) position.x -= 1;
                    break;
                case 'D':
                    if (position.x < grid.GetLength(0) - 1) position.x += 1;
                    break;
                case 'L':
                    if (position.y > 0) position.y -= 1;
                    break;
                case 'R':
                    if (position.y < grid.GetLength(1) - 1) position.y += 1;
                    break;
                default:
                    throw new Exception("nope");
            }
        }
        code += grid[position.x, position.y].ToString();
    }
    return code;
}

string ProcessInstructionsPart2(IEnumerable<char[]> instructions)
{
    string[,] grid = { { "", "", "1", "", "" }, { "", "2", "3", "4", "" }, { "5", "6", "7", "8", "9" }, { "", "A", "B", "C", "" }, { "", "", "D", "", "" } };
    var position = (x: 2, y: 0);
    var code = "";

    foreach (var set in instructions)
    {
        foreach (var move in set)
        {
            switch (move)
            {
                case 'U':
                    if (position.x > 0 && grid[position.x - 1, position.y] != "") position.x -= 1;
                    break;
                case 'D':
                    if (position.x < grid.GetLength(0) - 1 && grid[position.x + 1, position.y] != "") position.x += 1;
                    break;
                case 'L':
                    if (position.y > 0 && grid[position.x, position.y - 1] != "") position.y -= 1;
                    break;
                case 'R':
                    if (position.y < grid.GetLength(1) - 1 && grid[position.x, position.y + 1] != "") position.y += 1;
                    break;
                default:
                    throw new Exception("nope");
            }
        }
        code += grid[position.x, position.y];
    }
    return code;
}

Console.WriteLine($"Part 1: The code is {ProcessInstructionsPart1(instructionSets)}");
Console.WriteLine($"Part 2: The code is {ProcessInstructionsPart2(instructionSets)}");
