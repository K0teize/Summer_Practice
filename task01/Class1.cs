namespace task01;

public static class StringExtension
{
    public static bool IsPalindrome(this string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return false;
        }
        var cleanInput = new string(input.ToLower().Where(c => !char.IsWhiteSpace(c) && !char.IsPunctuation(c)).ToArray());
        var revClean = new string(cleanInput.Reverse().ToArray());
        return cleanInput == revClean;
    }
}
