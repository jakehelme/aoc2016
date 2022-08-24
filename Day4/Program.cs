using System.Text;
using System.Text.RegularExpressions;

Console.WriteLine("Day 4");

string raw;
Regex pattern = new Regex(@"([\D-]+)(\d+)\[(\w+)\]");

using (var sr = new StreamReader("input.txt"))
{
    raw = sr.ReadToEnd();
}

var roomCodes = raw.Split("\n");

bool IsValidRoom((string name, int sectorId, string checkSum) room)
{
    var chars = new Dictionary<char, int>();
    foreach (var c in room.name)
    {
        if (c == '-') continue;
        if (chars.ContainsKey(c)) chars[c]++;
        else chars.Add(c, 1);
    }
    var sumToCheck = chars.OrderByDescending(x => x.Value).ThenBy(x => x.Key).Take(5).Aggregate("", (acc, x) => acc + x.Key);

    return sumToCheck.Equals(room.checkSum);
}

int Part1()
{
    var sumValidSectorIds = 0;
    foreach (var roomCode in roomCodes)
    {
        var match = pattern.Match(roomCode);
        var room = (name: match.Groups[1].Value, sectorId: int.Parse(match.Groups[2].Value), checkSum: match.Groups[3].Value);
        if (IsValidRoom(room))
        {
            sumValidSectorIds += room.sectorId;
        }
    }
    return sumValidSectorIds;
}

string Part2()
{
    var validRooms = new List<(string name, int sectorId, string checkSum)>();
    foreach (var roomCode in roomCodes)
    {
        var match = pattern.Match(roomCode);
        var room = (name: match.Groups[1].Value, sectorId: int.Parse(match.Groups[2].Value), checkSum: match.Groups[3].Value);
        if (IsValidRoom(room))
        {
            validRooms.Add(room);
        }
    }
    var decryptedNames = new List<string>();
    foreach (var room in validRooms)
    {
        var decryptedName = $"{room.sectorId}:";
        foreach (var c in room.name)
        {
            if(c == '-')
            {
                decryptedName += ' ';
                continue;
            }
            var startingValue = (int)c;
            var minimumValue = 97;
            var offset = room.sectorId;
            var modulus = 26; 

            var newChar = (startingValue - minimumValue + (offset % modulus) + modulus) % modulus + minimumValue;
            decryptedName += (char)newChar;
        }
        decryptedNames.Add(decryptedName);
    }

    var northPoleRoom = decryptedNames.FirstOrDefault(x => x.Contains("north"));
    if (northPoleRoom != null){
        return northPoleRoom.Substring(0,3);
    }
    
    return "404: Not found";
}

Console.WriteLine($"Part 1: The sectorId sum of the valid rooms is: {Part1()}");
Console.WriteLine($"Part 2: The sectorId of the room where North Pole objects are stored is: {Part2()}");