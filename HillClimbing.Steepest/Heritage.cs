﻿namespace HillClimbing;

public abstract class Heritage : IHeritage
{
    //    private static readonly List<double> AllItems = new List<double>() {
    //797, 398, 499, 492, 167, 951, 104, 846, 997, 901
    //};
    private static readonly List<double> AllItems = new()
    {
    1444685023.0,
    147143653.0,
    3553542816.0,
    296924024.0,
    2287143902.0,
    3393614635.0,
    4277729237.0,
    2742856284.0,
    3922118837.0,
    1334251687.0,
    1927764466.0,
    3653303069.0,
    4189953905.0,
    905797322.0,
    2048978342.0,
    1976012864.0,
    3780023666.0,
    72593689.0,
    3309147370.0,
    2026598387.0,
    1192920608.0,
    2429354053.0,
    2391167062.0,
    2226593888.0,
    489035206.0,
    547245318.0,
    2027952409.0,
    1465626752.0,
    3912271583.0,
    1387505169.0,
    3192017200.0,
    755847237.0,
    3669537464.0,
    2506042949.0,
    1030012019.0,
    2142922365.0,
    4107888203.0,
    3012864664.0,
    4086009460.0,
    245558715.0,
    545534357.0,
    244236176.0,
    634906628.0,
    3752918838.0,
    3546792543.0,
    552427608.0,
    3970438204.0,
    785031284.0,
    3831267936.0,
    1118505600.0,
    2833124253.0,
    3305871370.0,
    1955325325.0,
    3617541657.0,
    2149603724.0,
    2187261515.0,
    1324034615.0,
    2387593042.0,
    977866143.0,
    623867858.0,
    4212413696.0,
    2745928654.0,
    3217677041.0,
    3213529958.0,
    2428790239.0,
    1107949140.0,
    728885317.0,
    2170790102.0,
    2584491919.0,
    2531144770.0,
    2788449507.0,
    3379213673.0,
    3948496054.0,
    2955853479.0,
    3821232074.0,
    1104466351.0,
    4225208446.0,
    2298292067.0,
    3148968554.0,
    4272714002.0,
    104717507.0,
    486843667.0,
    1553709427.0,
    3655311003.0,
    1759685834.0,
    906228413.0,
    572712225.0,
    1161296095.0,
    3458485926.0,
    805746663.0,
    2456903478.0,
    4058822183.0,
    2321337986.0,
    2115250876.0,
    2106466203.0,
    2866489240.0,
    539240199.0,
    565575138.0,
    2452567575.0,
    1025393815.0
};
    private static readonly double AllAmount = AllItems.Sum();
    public IEnumerable<double> BigBrotherItems { get; init; }
    public IEnumerable<double> SmallBrotherItems { get; init; }
    public IEnumerable<double> SisterItems { get; init; }
    public double Value { get; init; }
    protected double? BigBrotherAmount;
    protected double? SmallBrotherAmount;
    protected double? SisterAmount;

    public static void Initialize()
    {
        AllItems.Sort();
    }

    public Heritage(IEnumerable<double> BigBrotherItems, IEnumerable<double> SmallBrotherItems, IEnumerable<double> SisterItems)
    {
        this.BigBrotherItems = BigBrotherItems;
        this.SmallBrotherItems = SmallBrotherItems;
        this.SisterItems = SisterItems;
        Value = CalculateValue();
    }

    protected static (IEnumerable<double> BigBrotherItems, IEnumerable<double> SmallBrotherItems, IEnumerable<double> SisterItems) GetRandom()
    {
        return RandomMethod1();
    }

    private static (IEnumerable<double> BigBrotherItems, IEnumerable<double> SmallBrotherItems, IEnumerable<double> SisterItems) RandomMethod1()
    {
        var allItemTemp = new Stack<double>(AllItems);
        var bigBrother = new List<double>();
        var smallBrother = new List<double>();
        var sister = new List<double>();

        var r = new Random();

        while (allItemTemp.Count > 0)
        {
            int childIndex = r.Next(0, 5);
            var item = allItemTemp.Pop();
            switch (childIndex)
            {
                case 0:
                case 1:
                    bigBrother.Add(item);
                    break;
                case 2:
                case 3:
                    smallBrother.Add(item);
                    break;
                case 4:
                    sister.Add(item);
                    break;
            }
        }

        return (bigBrother, smallBrother, sister);
    }


    private static (IEnumerable<double> BigBrotherItems, IEnumerable<double> SmallBrotherItems, IEnumerable<double> SisterItems) RandomMethod2()
    {
        var allItemTemp = new Stack<double>(AllItems);
        var bigBrother = new List<double>();
        var smallBrother = new List<double>();
        var sister = new List<double>();

        var r = new Random();

        while (allItemTemp.Count > 0)
        {
            int childIndex = r.Next(0, 3);
            var item = allItemTemp.Pop();
            switch (childIndex)
            {
                case 0:
                    bigBrother.Add(item);
                    break;
                case 1:
                    smallBrother.Add(item);
                    break;
                case 2:
                    sister.Add(item);
                    break;
            }
        }

        return (bigBrother, smallBrother, sister);
    }
    
    private static (IEnumerable<double> BigBrotherItems, IEnumerable<double> SmallBrotherItems, IEnumerable<double> SisterItems) RandomMethod3()
    {
        var tempList = new List<double>(AllItems);
        tempList.Shuffle();
        int allItemsCount = tempList.Count;
        int BigBrotherItemsCount = new Random().Next(allItemsCount);
        int SmallBrotherItemsCount = new Random().Next(allItemsCount - BigBrotherItemsCount);
        int SisterItemsCount = allItemsCount - BigBrotherItemsCount - SmallBrotherItemsCount;
        return (tempList.GetRange(0, BigBrotherItemsCount),
            tempList.GetRange(BigBrotherItemsCount, SmallBrotherItemsCount),
            tempList.GetRange(BigBrotherItemsCount + SmallBrotherItemsCount, SisterItemsCount));

    }
    private double CalculateValue()
    {
        BigBrotherAmount ??= BigBrotherItems.Sum();
        SmallBrotherAmount ??= SmallBrotherItems.Sum();
        SisterAmount ??= SisterItems.Sum();
        return Math.Abs(BigBrotherAmount.Value - 0.4 * AllAmount) + Math.Abs(SmallBrotherAmount.Value - 0.4 * AllAmount) + Math.Abs(SisterAmount.Value - 0.2 * AllAmount);
    }

    public static double CalculateValue(double BigBrotherAmount, double SmallBrotherAmount, double SisterAmount)
    {
        return Math.Abs(BigBrotherAmount - 0.4 * AllAmount) + Math.Abs(SmallBrotherAmount - 0.4 * AllAmount) + Math.Abs(SisterAmount - 0.2 * AllAmount);
    }

    public void Print()
    {
        Console.WriteLine($"Value: {Value}");
        Console.WriteLine($"BigBrother ItemsCount: {BigBrotherItems.Count()}");
        Console.WriteLine($"BigBrother Amount: {BigBrotherAmount}");
        Console.WriteLine($"SmallBrother ItemsCount: {SmallBrotherItems.Count()}");
        Console.WriteLine($"SmallBrother Amount: {SmallBrotherAmount}");
        Console.WriteLine($"Sister ItemsCount: {SisterItems.Count()}");
        Console.WriteLine($"Sister Amount: {SisterAmount}");

        Console.Write("BigBrother Items:");
        BigBrotherItems.Print();
        Console.Write("SmallBrother Items:");
        SmallBrotherItems.Print();
        Console.Write("Sister Items:");
        SisterItems.Print();
    }

    public abstract Heritage GetNeighbor();
}

public static class Extensions
{
    // Shuffle the list using Fisher-Yates algorithm
    public static void Shuffle(this List<double> list)  //۱۶,۱۰۱/۱۵۶۲ ms
    {
        int threadId = Environment.CurrentManagedThreadId;
        long tickCount = DateTime.Now.Ticks;
        int seed = unchecked((int)(threadId ^ tickCount));
        var random = new Random(seed);

        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            (list[j], list[i]) = (list[i], list[j]);
        }
    }

    public static LinkedList<double> CreateCopy(this LinkedList<double> main)
    {
        return new LinkedList<double>(main);
    }

    public static double GetAndRemoveFirst(this LinkedList<double> node)
    {
        var first = node.First;
        node.RemoveFirst();
        return first!.Value;
    }

    public static void Print(this IEnumerable<double> list)
    {
        foreach (var item in list)
        {
            Console.Write(item + " , ");
        }
        Console.WriteLine();
        Console.WriteLine("_______________");
        Console.WriteLine();
    }

}