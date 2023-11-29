namespace HillClimbing.Steepest;

public class Heritage
{
    public static LinkedList<double> AllItems;
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
        var tempList = new LinkedList<double>(AllItems);
        Random r = new Random();
        var bigBrother = new LinkedList<double>();
        var smallBrother = new LinkedList<double>();
        var sister = new LinkedList<double>();




        //while (tempList.Count > 0)
        //{
        //    int childIndex = r.Next(0, 5);
        //    switch (childIndex)
        //    {
        //        case 0:
        //        case 1:
        //            bigBrother.AddLast(tempList.Last!.Value);
        //            break;
        //        case 2:
        //        case 3:
        //            smallBrother.AddLast(tempList.Last!.Value);
        //            break;
        //        case 4:
        //            sister.AddLast(tempList.Last!.Value);
        //            break;
        //    }
        //    tempList.RemoveLast();
        //}
        //return new Heritage(bigBrother, smallBrother, sister);



        while (tempList.Count > 0)
        {
            int childIndex = r.Next(0, 3);
            switch (childIndex)
            {
                case 0:
                    bigBrother.AddLast(tempList.Last!.Value);
                    break;
                case 1:
                    smallBrother.AddLast(tempList.Last!.Value);
                    break;
                case 2:
                    sister.AddLast(tempList.Last!.Value);
                    break;
            }
            tempList.RemoveLast();
        }
        return new Heritage(bigBrother, smallBrother, sister);



        //tempList.Shuffle();
        //int allItemsCount = tempList.Count;
        //int BigBrotherItemsCount = new Random().Next(allItemsCount);
        //int SmallBrotherItemsCount = new Random().Next(allItemsCount - BigBrotherItemsCount);
        //int SisterItemsCount = allItemsCount - BigBrotherItemsCount - SmallBrotherItemsCount;
        //return new Heritage(new LinkedList<double>(tempList.GetRange(0, BigBrotherItemsCount)),
        //    new LinkedList<double>(tempList.GetRange(BigBrotherItemsCount, SmallBrotherItemsCount)),
        //    new LinkedList<double>(tempList.GetRange(BigBrotherItemsCount + SmallBrotherItemsCount, SisterItemsCount)));
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

    private static double CalculateValue(double BigBrotherAmount, double SmallBrotherAmount, double SisterAmount)
    {
        return Math.Abs(BigBrotherAmount - 0.4 * AllAmount) + Math.Abs(SmallBrotherAmount - 0.4 * AllAmount) + Math.Abs(SisterAmount - 0.2 * AllAmount);
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
        double Temp;
        double SmallBrotherAmountTemp;
        double SisterAmountTemp;
        double BigBrotherAmountTemp;
        //for BigBrother
        for (int i = 0; i < current.BigBrother.Count; i++)
        {
            double tempValue = current.BigBrother.First!.Value;
            current.BigBrother.RemoveFirst();
            //give to small brother
            current.SmallBrother.AddLast(tempValue);
            BigBrotherAmountTemp = current.BigBrotherAmount!.Value - tempValue;
            SmallBrotherAmountTemp = current.SmallBrotherAmount!.Value + tempValue;
            SisterAmountTemp = current.SisterAmount!.Value;
            Temp = CalculateValue(BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);

            if (bestDivide.Value > Temp)
                bestDivide = new(current.BigBrother.CreateCopy(), current.SmallBrother.CreateCopy(), current.Sister.CreateCopy(), BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);


            current.SmallBrother.RemoveLast();
            // give to sister
            current.Sister.AddLast(tempValue);
            BigBrotherAmountTemp = current.BigBrotherAmount!.Value - tempValue;
            SmallBrotherAmountTemp = current.SmallBrotherAmount!.Value;
            SisterAmountTemp = current.SisterAmount!.Value + tempValue;
            Temp = CalculateValue(BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);

            if (bestDivide.Value > Temp)
                bestDivide = new(current.BigBrother.CreateCopy(), current.SmallBrother.CreateCopy(), current.Sister.CreateCopy(), BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);

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
            BigBrotherAmountTemp = current.BigBrotherAmount!.Value + tempValue;
            SmallBrotherAmountTemp = current.SmallBrotherAmount!.Value - tempValue;
            SisterAmountTemp = current.SisterAmount!.Value;
            Temp = CalculateValue(BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);

            if (bestDivide.Value > Temp)
                bestDivide = new(current.BigBrother.CreateCopy(), current.SmallBrother.CreateCopy(), current.Sister.CreateCopy(), BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);


            current.BigBrother.RemoveLast();
            // give to sister
            current.Sister.AddLast(tempValue);
            BigBrotherAmountTemp = current.BigBrotherAmount!.Value;
            SmallBrotherAmountTemp = current.SmallBrotherAmount!.Value - tempValue;
            SisterAmountTemp = current.SisterAmount!.Value + tempValue;
            Temp = CalculateValue(BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);

            if (bestDivide.Value > Temp)
                bestDivide = new(current.BigBrother.CreateCopy(), current.SmallBrother.CreateCopy(), current.Sister.CreateCopy(), BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);

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
            BigBrotherAmountTemp = current.BigBrotherAmount!.Value + tempValue;
            SmallBrotherAmountTemp = current.SmallBrotherAmount!.Value;
            SisterAmountTemp = current.SisterAmount!.Value - tempValue;
            Temp = CalculateValue(BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);


            if (bestDivide.Value > Temp)
                bestDivide = new(current.BigBrother.CreateCopy(), current.SmallBrother.CreateCopy(), current.Sister.CreateCopy(), BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);



            current.BigBrother.RemoveLast();
            // give to SmallBrother
            current.SmallBrother.AddLast(tempValue);
            BigBrotherAmountTemp = current.BigBrotherAmount!.Value;
            SmallBrotherAmountTemp = current.SmallBrotherAmount!.Value + tempValue;
            SisterAmountTemp = current.SisterAmount!.Value - tempValue;
            Temp = CalculateValue(BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);


            if (bestDivide.Value > Temp)
                bestDivide = new(current.BigBrother.CreateCopy(), current.SmallBrother.CreateCopy(), current.Sister.CreateCopy(), BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);


            //back to Sister
            current.SmallBrother.RemoveLast();
            current.Sister.AddLast(tempValue);
        }

        return bestDivide;
    }

    public static Heritage RandomNeighbor(Heritage current)
    {
        Heritage bestDivide = current;

        var random = new Random();
        int fromChild = random.Next(0, 3);

        // from Big Brother
        if (fromChild == 0)
        {
            int toChild = random.Next(0, 2);

            //to small Brother
            if (toChild == 0)
            {

            }

            //to sister
            if (toChild == 1)
            {

            }
        }

        // from Small Brother
        if (fromChild == 1)
        {
            int toChild = random.Next(0, 2);

            //to Big Brother
            if (toChild == 0)
            {

            }

            //to sister
            if (toChild == 1)
            {

            }
        }

        // from Sister
        if (fromChild == 2)
        {
            int toChild = random.Next(0, 2);

            //to Big Brother
            if (toChild == 0)
            {

            }

            //to Small Brother
            if (toChild == 1)
            {

            }
        }

        return bestDivide;
    }

}

public static class Extensions
{
    // Shuffle the list using Fisher-Yates algorithm
    public static void Shuffle(this List<double> list)  //۱۶,۱۰۱/۱۵۶۲ ms
    {
        //int threadId = Environment.CurrentManagedThreadId;
        //long tickCount = DateTime.Now.Ticks;
        //int seed = unchecked((int)(threadId ^ tickCount));
        var random = new Random();

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
