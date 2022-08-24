Console.WriteLine("Day 1");

string raw = "";
using (var sr = new StreamReader("input.txt"))
{
    var tryRaw = sr.ReadLine();
    if (string.IsNullOrEmpty(tryRaw)) throw new Exception("Nope");
    raw = tryRaw;
}
var instructions = raw.Split(',').Select(x => x.Trim());

void ProcessInstruction(string instruction, ref (int x, int y) pos, ref int dir)
{
    var direction = instruction[0];
    var distance = int.Parse(instruction[1..instruction.Length]);

    switch (instruction[0])
    {
        case 'R':
            dir = (dir + 90) % 360;
            break;
        case 'L':
            dir = dir - 90 < 0 ? dir + 270 : dir - 90;
            break;
        default:
            throw new Exception("nope");
    }

    switch (dir)
    {
        case 0:
            pos.y += distance;
            break;
        case 90:
            pos.x += distance;
            break;
        case 180:
            pos.y -= distance;
            break;
        case 270:
            pos.x -= distance;
            break;
        default:
            throw new Exception("nope");
    }
}

bool Record(Dictionary<(int, int), int> visited, (int, int) pos)
{
    if (visited.ContainsKey(pos))
    {
        visited[pos]++;
        return true;
    }
    else
    {
        visited.Add(pos, 1);
    }
    return false;
}

bool ProcessInstructionAndTrack(string instruction, ref (int x, int y) pos, ref int dir, Dictionary<(int, int), int> visitedLocations)
{
    var direction = instruction[0];
    var distance = int.Parse(instruction[1..instruction.Length]);

    switch (instruction[0])
    {
        case 'R':
            dir = (dir + 90) % 360;
            break;
        case 'L':
            dir = dir - 90 < 0 ? dir + 270 : dir - 90;
            break;
        default:
            throw new Exception("nope");
    }

    switch (dir)
    {
        case 0:
            for (int i = 0; i < distance; i++)
            {
                pos.y++;
                if (Record(visitedLocations, pos)) return true;
            }
            break;
        case 90:
            for (int i = 0; i < distance; i++)
            {
                pos.x++;
                if (Record(visitedLocations, pos)) return true;
            }
            break;
        case 180:
            for (int i = 0; i < distance; i++)
            {
                pos.y--;
                if (Record(visitedLocations, pos)) return true;
            }
            break;
        case 270:
            for (int i = 0; i < distance; i++)
            {
                pos.x--;
                if (Record(visitedLocations, pos)) return true;
            }
            break;
        default:
            throw new Exception("nope");
    }
    return false;
}

void Part1()
{
    var pos = (x: 0, y: 0);
    var dir = 0;

    foreach (var instruction in instructions)
    {
        ProcessInstruction(instruction, ref pos, ref dir);
    }
    Console.WriteLine($"Part 1: Distance from start: {Math.Abs(pos.x) + Math.Abs(pos.y)}");
}

void Part2()
{
    var pos = (x: 0, y: 0);
    var dir = 0;
    var locationsVisited = new Dictionary<(int, int), int>();

    foreach (var instruction in instructions)
    {
        if(ProcessInstructionAndTrack(instruction, ref pos, ref dir, locationsVisited)){
            Console.WriteLine($"Part 2: First position visited twice is {pos} and it is {Math.Abs(pos.x) + Math.Abs(pos.y)} blocks away from start");
            break;
        }
        
    }
}

Part1();
Part2();
