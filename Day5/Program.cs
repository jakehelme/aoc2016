Console.WriteLine("Day 5");

var doorId = "cxdnnyjw";

string CreateMD5(string input)
{
    using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
    {
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hashBytes = md5.ComputeHash(inputBytes);

        return Convert.ToHexString(hashBytes);
    }
}

string Part1()
{
    var password = "";
    var index = 0;
    while (password.Length != 8)
    {
        var test = CreateMD5(doorId + index.ToString());
        if (test.Substring(0,5) == "00000")
        {
            password += test[5];
        }
        index++;
    }
    return password.ToLower();
}

string Part2()
{
    var password = new List<char>( new char[8] );
    var passwordLength = 0;
    var index = 0;
    while (password.Contains('\0'))
    {
        var test = CreateMD5(doorId + index.ToString());
        if (test.Substring(0,5) == "00000")
        {
            int pos;
            if(int.TryParse(test[5].ToString(), out pos))
            {
                var newChar = test[6];
                if(pos >= 0 && pos <= 7 && password[pos] == '\0')
                {
                    password[pos] = newChar;
                    passwordLength++;
                }
            }
        }
        index++;
    }
    return password.Aggregate("", (acc, x) => acc += x).ToLower();
}

Console.WriteLine($"Part 1: The password is {Part1()}");
Console.WriteLine($"Part 2: The password is {Part2()}");