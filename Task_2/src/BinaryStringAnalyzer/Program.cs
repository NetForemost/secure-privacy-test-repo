public class BinaryStringAnalyzer
{
    public static bool IsGoodBinaryString(string binaryString)
    {
        int countZeros = 0;
        int countOnes = 0;

        foreach (char bit in binaryString)
        {
            if (bit == '0')
                countZeros++;
            else if (bit == '1')
                countOnes++;
            else
                throw new ArgumentException("The input string should only contain '0' or '1'.");

            if (countZeros > countOnes)
                return false;
        }

        return countZeros == countOnes;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter a binary string:");
        string input = Console.ReadLine();
        bool result = BinaryStringAnalyzer.IsGoodBinaryString(input);

        if (result)
            Console.WriteLine("The binary string is good.");
        else
            Console.WriteLine("The binary string is not good.");
    }
}
