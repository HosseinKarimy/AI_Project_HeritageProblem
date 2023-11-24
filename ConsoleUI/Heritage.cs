namespace ConsoleUI;

public class Heritage
{
    public static List<double> AllItems;
    private static double AllAmount;
    private readonly LinkedList<double> BigBrother;
    private double? BigBrotherAmount;
    private readonly LinkedList<double> SmallBrother;
    private double? SmallBrotherAmount;
    private readonly LinkedList<double> Sister;
    private double? SisterAmount;
    public double Value { get; init; }

    public Heritage(LinkedList<double> BigBrother, LinkedList<double> SmallBrother, LinkedList<double> Sister)
    {
        this.BigBrother = BigBrother;
        this.SmallBrother = SmallBrother;
        this.Sister = Sister;
        Value = CalculateValue();
    }
    public Heritage(LinkedList<double> BigBrother, LinkedList<double> SmallBrother, LinkedList<double> Sister, double? BigBrotherAmount, double? SmallBrotherAmount, double? SisterAmount)
    {
        this.BigBrother = BigBrother;
        this.SmallBrother = SmallBrother;
        this.Sister = Sister;
        this.BigBrotherAmount = BigBrotherAmount;
        this.SmallBrotherAmount = SmallBrotherAmount;
        this.SisterAmount = SisterAmount;
        Value = CalculateValue();
    }

    public static Heritage FromRandom()
    {
        var tempList = new List<double>(AllItems);
        tempList.Shuffle();
        int allItemsCount = tempList.Count;
        int BigBrotherItemsCount = new Random().Next(allItemsCount);
        int SmallBrotherItemsCount = new Random().Next(allItemsCount - BigBrotherItemsCount);
        int SisterItemsCount = allItemsCount - BigBrotherItemsCount - SmallBrotherItemsCount;
        return new Heritage(new LinkedList<double>(tempList.GetRange(0, BigBrotherItemsCount)),
            new LinkedList<double>(tempList.GetRange(BigBrotherItemsCount, SmallBrotherItemsCount)),
            new LinkedList<double>(tempList.GetRange(BigBrotherItemsCount + SmallBrotherItemsCount, SisterItemsCount)));

        //var tempList = new List<double>(AllItems);
        //tempList.Sort();

        //LinkedList<double> list1 = new();
        //LinkedList<double> list2 = new();
        //LinkedList<double> list3 = new();

        //while (true)
        //{
        //    list1.AddFirst(tempList[0]);
        //    tempList.RemoveAt(0);
        //    if (tempList.Count == 0) break;

        //    list2.AddFirst(tempList[0]);
        //    tempList.RemoveAt(0);
        //    if (tempList.Count == 0) break;

        //    list3.AddFirst(tempList[0]);
        //    tempList.RemoveAt(0);
        //    if (tempList.Count == 0) break;
        //}
        //return new Heritage(list1, list2, list3);
    }

    public Heritage CopyOf()
    {
        return new Heritage(new LinkedList<double>(BigBrother!), new LinkedList<double>(SmallBrother!), new LinkedList<double>(Sister!));
    }

    private double CalculateValue()
    {
        AllAmount = AllItems!.Sum();
        BigBrotherAmount ??= BigBrother.Sum();
        SmallBrotherAmount ??= SmallBrother.Sum();
        SisterAmount ??= Sister.Sum();
        return Math.Abs(BigBrotherAmount.Value - 0.4 * AllAmount) + Math.Abs(SmallBrotherAmount.Value - 0.4 * AllAmount) + Math.Abs(SisterAmount.Value - 0.2 * AllAmount);
    }

    public void Print()
    {
        Console.WriteLine($"Value: {Value}");
        Console.WriteLine($"BigBrother ItemsCount: {BigBrother.Count}");
        Console.WriteLine($"BigBrother Amount: {BigBrotherAmount}");
        Console.WriteLine($"SmallBrother ItemsCount: {SmallBrother.Count}");
        Console.WriteLine($"SmallBrother Amount: {SmallBrotherAmount}");
        Console.WriteLine($"Sister ItemsCount: {Sister.Count}");
        Console.WriteLine($"Sister Amount: {SisterAmount}");

        Console.Write("BigBrother Items:");
        BigBrother.Print();
        Console.Write("SmallBrother Items:");
        SmallBrother.Print();
        Console.Write("Sister Items:");
        Sister.Print();
    }

    //pls send copy of the heritage (the heritage that send to this method will be modified)
    public static Heritage BestNeighbor(Heritage current)
    {
        Heritage bestDivide = current;
        Heritage Temp;
        //for BigBrother
        for (int i = 0; i < current.BigBrother.Count; i++)
        {
            double tempValue = current.BigBrother.First!.Value;
            current.BigBrother.RemoveFirst();
            //give to small brother
            current.SmallBrother.AddLast(tempValue);
            Temp = new(current.BigBrother, current.SmallBrother, current.Sister,current.BigBrotherAmount - tempValue,current.SmallBrotherAmount + tempValue,current.SisterAmount);

            if (bestDivide.Value > Temp.Value)
                bestDivide = Temp;


            current.SmallBrother.RemoveLast();
            // give to sister
            current.Sister.AddLast(tempValue);
            Temp = new(current.BigBrother, current.SmallBrother, current.Sister , current.BigBrotherAmount - tempValue, current.SmallBrotherAmount , current.SisterAmount + tempValue );

            if (bestDivide.Value > Temp.Value)
                bestDivide = Temp;

            //back to big brother
            current.Sister.RemoveLast();
            current.BigBrother.AddLast(tempValue);
        }

        //for SmallBrother
        for (int i = 0; i < current.SmallBrother.Count; i++)
        {
            double tempValue = current.SmallBrother.First!.Value;
            current.SmallBrother.RemoveFirst();

            //give to Big brother
            current.BigBrother.AddLast(tempValue);
            Temp = new(current.BigBrother, current.SmallBrother, current.Sister , current.BigBrotherAmount + tempValue , current.SmallBrotherAmount - tempValue , current.SisterAmount);

            if (bestDivide.Value > Temp.Value)
                bestDivide = Temp;


            current.BigBrother.RemoveLast();
            // give to sister
            current.Sister.AddLast(tempValue);
            Temp = new(current.BigBrother, current.SmallBrother, current.Sister, current.BigBrotherAmount , current.SmallBrotherAmount - tempValue , current.SisterAmount + tempValue);

            if (bestDivide.Value > Temp.Value)
                bestDivide = Temp;

            //back to Small brother
            current.Sister.RemoveLast();
            current.SmallBrother.AddLast(tempValue);
        }

        //for Sister
        for (int i = 0; i < current.Sister.Count; i++)
        {
            double tempValue = current.Sister.First!.Value;
            current.Sister.RemoveFirst();
            //give to Big brother
            current.BigBrother.AddLast(tempValue);
            Temp = new(current.BigBrother, current.SmallBrother, current.Sister , current.BigBrotherAmount + tempValue , current.SmallBrotherAmount, current.SisterAmount - tempValue);

            if (bestDivide.Value > Temp.Value)
                bestDivide = Temp;


            current.BigBrother.RemoveLast();
            // give to SmallBrother
            current.SmallBrother.AddLast(tempValue);
            Temp = new(current.BigBrother, current.SmallBrother, current.Sister , current.BigBrotherAmount , current.SmallBrotherAmount + tempValue , current.SisterAmount - tempValue);

            if (bestDivide.Value > Temp.Value)
                bestDivide = Temp;

            //back to Sister
            current.SmallBrother.RemoveLast();
            current.Sister.AddLast(tempValue);
        }

        return bestDivide;
    }

}

public static class Extensions
{
    // Shuffle the list using Fisher-Yates algorithm
    public static void Shuffle(this List<double> list)
    {
        var random = new Random();
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            (list[j], list[i]) = (list[i], list[j]);
        }
    }

    public static void Print(this LinkedList<double> list)
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
