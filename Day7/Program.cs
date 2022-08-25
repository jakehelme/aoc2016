using System.Text.RegularExpressions;

Console.WriteLine("Day 7");

string raw;

using (var sr = new StreamReader("input.txt"))
{
    raw = sr.ReadToEnd();
}

var ips = raw.Split('\n');

bool IsAbba(string seq)
{
    return seq[0] == seq[3] && seq[1] == seq[2] && seq[0] != seq[1];
}

bool IsAba(string seq)
{
    return seq[0] == seq[2] && seq[0] != seq[1];
}

bool SeqHasAbba(string fullSeq)
{
    for (int i = 0; i < fullSeq.Length - 3; i++)
    {
        if (IsAbba(fullSeq.Substring(i, 4)))
        {
            return true;
        }
    }
    return false;
}

List<string> SeqAbas(string fullSeq)
{
    var abas = new List<string>();
    for (int i = 0; i < fullSeq.Length - 2; i++)
    {
        var candidate = fullSeq.Substring(i, 3);
        if (IsAba(candidate))
        {
            abas.Add(candidate);
        }
    }
    return abas;
}

bool HasBab(string aba, List<string> hypernets)
{
    var bab = $"{aba[1]}{aba[0]}{aba[1]}";
    foreach (var hypernet in hypernets)
    {
        if (hypernet.Contains(bab)) return true;
    }
    return false;
}

int Part1()
{
    var supportCount = 0;
    foreach (var ip in ips)
    {
        var pattern = new Regex(@"\w+");
        var matches = pattern.Matches(ip);
        var regSequences = new List<string>();
        var hypernetSequences = new List<string>();
        for (int i = 0; i < matches.Count; i++)
        {
            if (i % 2 == 0) regSequences.Add(matches[i].Value);
            else hypernetSequences.Add(matches[i].Value);
        }

        var supportsTls = false;
        foreach (var seq in regSequences)
        {
            if (SeqHasAbba(seq)) supportsTls = true;
        }
        foreach (var seq in hypernetSequences)
        {
            if (SeqHasAbba(seq)) supportsTls = false;
        }

        if (supportsTls) supportCount++;
    }
    return supportCount;
}

int Part2()
{
    var supportCount = 0;
    foreach (var ip in ips)
    {
        var pattern = new Regex(@"\w+");
        var matches = pattern.Matches(ip);
        var regSequences = new List<string>();
        var hypernetSequences = new List<string>();
        for (int i = 0; i < matches.Count; i++)
        {
            if (i % 2 == 0) regSequences.Add(matches[i].Value);
            else hypernetSequences.Add(matches[i].Value);
        }

        var abas = new List<string>();
        foreach (var seq in regSequences)
        {
            abas.AddRange(SeqAbas(seq));
        }
        
        foreach (var aba in abas)
        {
            if (HasBab(aba, hypernetSequences)) 
            {
                supportCount++;
                break;
            }
        }
    }
    return supportCount;
}

Console.WriteLine($"Part 1: There are {Part1()} IPs that support TLS");
Console.WriteLine($"Part 2: There are {Part2()} IPs that support SSL");