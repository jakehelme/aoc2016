Console.WriteLine("Day 6");

string raw;

using (var sr = new StreamReader("input.txt"))
{
    raw = sr.ReadToEnd();
}

var messages = raw.Split('\n');

var dictionaries = new List<Dictionary<char, int>>();
for (int i = 0; i < messages[0].Length; i++)
{
    dictionaries.Add(new Dictionary<char, int>());
}

foreach (var message in messages)
{
    for (var i = 0; i < message.Length; i++)
    {
        var c = message[i];
        if (dictionaries[i].ContainsKey(c)) dictionaries[i][c]++;
        else dictionaries[i].Add(c, 1);
    }
}

string Part1()
{
    var finalMessage = "";
    foreach (var dict in dictionaries)
    {
        finalMessage += dict.OrderByDescending(x => x.Value).First().Key;
    }
    return finalMessage;
}

string Part2()
{
    var finalMessage = "";
    foreach (var dict in dictionaries)
    {
        finalMessage += dict.OrderBy(x => x.Value).First().Key;
    }
    return finalMessage;
}


Console.WriteLine($"Part 1: The message is '{Part1()}'");
Console.WriteLine($"Part 2: The message is '{Part2()}'");