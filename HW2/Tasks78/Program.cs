using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Task78
{
    class Program
{
    //Funkcija prima cijeli broj n, a vraća obećanje da će vratiti sumu znamenaka broja n!
    private static Task<int> FactorialDigitSum(int n)
    {
        int faktorijel = 1, suma = 0;
        for (int i = 2; i <= n; ++i)
        {
            faktorijel *= i;
        }

        while (faktorijel > 0)
        {
            suma += faktorijel % 10;
            faktorijel = faktorijel / 10;
        }
        return Task.FromResult(suma);
    }


    static void Main(string[] args)
    {
        // Main method is the only method that
        // can ’t be marked with async .
        // What we are doing here is just a way for us to simulate
        // async - friendly environment you usually have with
        // other .NET application types ( like web apps , win apps etc .)
        // Ignore main method , you can just focus on LetsSayUserClickedAButtonOnGuiMethod() as a
        // first method in call hierarchy .
        var t = Task.Run(() => LetsSayUserClickedAButtonOnGuiMethod());
        Console.Read();
    }

    private static void LetsSayUserClickedAButtonOnGuiMethod()
    {
        var result = GetTheMagicNumber().Result;
        Console.WriteLine(result);
    }

    private async static Task<int> GetTheMagicNumber()
    {
        var r = await IKnowIGuyWhoKnowsAGuy();
        return r;
    }

    private async static Task<int> IKnowIGuyWhoKnowsAGuy()
    {
        var r1 = await IKnowWhoKnowsThis(10);
        var r2 = await IKnowWhoKnowsThis(5);
        return r1 + r2;
    }

    private async static Task<int> IKnowWhoKnowsThis(int n)
    {
        var r = await FactorialDigitSum(n);
        return r;
    }
}
}
