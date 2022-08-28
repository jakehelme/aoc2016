using System.Text.RegularExpressions;

Console.WriteLine("Day 8");

string raw;
int gridLength = 50;
int gridHeight = 6;
char fill = '█';
var grid = CreateGrid();

using (var sr = new StreamReader("input.txt"))
{
    raw = sr.ReadToEnd();
}

var rawInstructions = raw.Split('\n');
var pattern = new Regex(@"(rect|column|row).+?(\d+).+?(\d+)");
var instructions = new List<(string type, int a, int b)>();

foreach (var rawInstruction in rawInstructions)
{
    var match = pattern.Match(rawInstruction);
    instructions.Add((match.Groups[1].Value, int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value)));
}

char[,] CreateGrid()
{
    var grid = new char[gridHeight, gridLength];
    for (int row = 0; row < gridHeight; row++)
    {
        for (int col = 0; col < gridLength; col++)
        {
            grid[row, col] = ' ';
        }
    }
    return grid;
}

void PrintGrid()
{
    for (int row = 0; row < grid.GetLength(0); row++)
    {
        var rowBuf = "";
        for (int col = 0; col < grid.GetLength(1); col++)
        {
            rowBuf += grid[row, col];
        }
        Console.WriteLine(rowBuf);
    }
    Console.WriteLine("");
}

void DrawRect(int a, int b)
{
    for (int row = 0; row < b; row++)
    {
        for (int col = 0; col < a; col++)
        {
            grid[row, col] = fill;
        }
    }
}

void RightShiftArray(char[] arr, int shift)
{
    shift = shift % arr.Length;
    char[] buffer = new char[shift];
    Array.Copy(arr, arr.Length - shift, buffer, 0, shift);
    Array.Copy(arr, 0, arr, shift, arr.Length - shift);
    Array.Copy(buffer, arr, shift);
}

void RotateCol(int a, int b)
{
    var currentCol = new char[gridHeight];
    for (int row = 0; row < gridHeight; row++)
    {
        currentCol[row] = grid[row, a];
    }
    RightShiftArray(currentCol, b);
    for (int row = 0; row < gridHeight; row++)
    {
        grid[row, a] = currentCol[row];
    }
}

void RotateRow(int a, int b)
{
    var rowCopy = new char[gridLength];
    for (int col = 0; col < gridLength; col++)
    {
        rowCopy[col] = grid[a, col];
    }
    RightShiftArray(rowCopy, b);
    for (int col = 0; col < gridLength; col++)
    {
        grid[a, col] = rowCopy[col];
    }
}


PrintGrid();

foreach (var instruction in instructions)
{
    // Console.WriteLine(instruction);
    switch (instruction.type)
    {
        case "rect":
            DrawRect(instruction.a, instruction.b);
            break;
        case "column":
            RotateCol(instruction.a, instruction.b);
            break;
        case "row":
            RotateRow(instruction.a, instruction.b);
            break;
        default:
            break;
    }
    // PrintGrid();
    // Console.WriteLine();
}

var litPixels = 0;

for (var row = 0; row < grid.GetLength(0); row++)
{
    for (int col = 0; col < grid.GetLength(1); col++)
    {
        if(grid[row,col] == fill) litPixels++;
    }
}

Console.WriteLine($"Part 1: Number of lit pixels is {litPixels}");
Console.WriteLine("Part 2: The code is:");
PrintGrid();

