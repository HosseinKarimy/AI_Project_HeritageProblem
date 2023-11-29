namespace HillClimbing;

public class Heritage_Steepest : Heritage
{
    private readonly LinkedList<double> BigBrother;
    private readonly LinkedList<double> SmallBrother;
    private readonly LinkedList<double> Sister;

    public Heritage_Steepest(IEnumerable<double> BigBrother, IEnumerable<double> SmallBrother, IEnumerable<double> Sister)
        : base(BigBrother, SmallBrother, Sister)
    {
        this.BigBrother = new LinkedList<double>(BigBrother);
        this.SmallBrother = new LinkedList<double>(SmallBrother);
        this.Sister = new LinkedList<double>(Sister);
    }
    public Heritage_Steepest(LinkedList<double> BigBrother, LinkedList<double> SmallBrother, LinkedList<double> Sister)
        : base(BigBrother, SmallBrother, Sister)
    {
        this.BigBrother = BigBrother;
        this.SmallBrother = SmallBrother;
        this.Sister = Sister;
    }
    public Heritage_Steepest(LinkedList<double> BigBrother, LinkedList<double> SmallBrother, LinkedList<double> Sister, double? BigBrotherAmount, double? SmallBrotherAmount, double? SisterAmount)
        : base(BigBrother, SmallBrother, Sister)
    {
        this.BigBrother = BigBrother;
        this.SmallBrother = SmallBrother;
        this.Sister = Sister;
        this.BigBrotherAmount = BigBrotherAmount;
        this.SmallBrotherAmount = SmallBrotherAmount;
        this.SisterAmount = SisterAmount;
    }

    public static Heritage_Steepest FromRandom()
    {
        var lists = GetRandom();
        return new Heritage_Steepest(lists.BigBrotherItems, lists.SmallBrotherItems, lists.SisterItems);
    }

    public Heritage_Steepest CopyOf()
    {
        return new Heritage_Steepest(new LinkedList<double>(BigBrother!), new LinkedList<double>(SmallBrother!), new LinkedList<double>(Sister!));
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
    public static Heritage_Steepest BestNeighbor(Heritage_Steepest current)
    {
        Heritage_Steepest bestDivide = current;
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

    public static Heritage_Steepest RandomNeighbor(Heritage_Steepest current)
    {
        Heritage_Steepest bestDivide = current;

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
