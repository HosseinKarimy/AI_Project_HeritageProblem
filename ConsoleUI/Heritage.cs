namespace ConsoleUI;

public class Heritage
{
    public static List<double> AllItems;
    private LinkedList<double> BigBrother;
    private LinkedList<double> SmallBrother;
    private LinkedList<double> Sister;
    public double Value { get; init; }
    public Heritage(LinkedList<double> BigBrother, LinkedList<double> SmallBrother, LinkedList<double> Sister)
    {
        this.BigBrother = BigBrother;
        this.SmallBrother = SmallBrother;
        this.Sister = Sister;
        Value = CalculateValue();
    }

    public static Heritage FromRandom()
    {
        AllItems.Shuffle();
        int allItemsCount = AllItems.Count;
        int BigBrotherItemsCount = new Random().Next(allItemsCount);
        int SmallBrotherItemsCount = new Random().Next(allItemsCount - BigBrotherItemsCount);
        int SisterItemsCount = allItemsCount - BigBrotherItemsCount - SmallBrotherItemsCount;
        return new Heritage(new LinkedList<double>(AllItems.GetRange(0, BigBrotherItemsCount)),
            new LinkedList<double>(AllItems.GetRange(BigBrotherItemsCount, SmallBrotherItemsCount)),
            new LinkedList<double>(AllItems.GetRange(BigBrotherItemsCount + SmallBrotherItemsCount, SisterItemsCount)));
    }

    public Heritage CopyOf()
    {
        return new Heritage(new LinkedList<double>(BigBrother!), new LinkedList<double>(SmallBrother!), new LinkedList<double>(Sister!));
    }

    private double CalculateValue()
    {
        double bigBrother = BigBrother.Sum();
        double smallBrother = SmallBrother.Sum();
        double sister = Sister.Sum();
        double sum = AllItems.Sum();
        return Math.Abs(bigBrother - 0.4 * sum) + Math.Abs(smallBrother - 0.4 * sum) + Math.Abs(sister - 0.2 * sum);
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
            Temp = new(current.BigBrother, current.SmallBrother, current.Sister);

            if (bestDivide.Value > Temp.Value)
                bestDivide = Temp;


            current.SmallBrother.RemoveLast();
            // give to sister
            current.Sister.AddLast(tempValue);
            Temp = new(current.BigBrother, current.SmallBrother, current.Sister);

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
            Temp = new(current.BigBrother, current.SmallBrother, current.Sister);

            if (bestDivide.Value > Temp.Value)
                bestDivide = Temp;


            current.BigBrother.RemoveLast();
            // give to sister
            current.Sister.AddLast(tempValue);
            Temp = new(current.BigBrother, current.SmallBrother, current.Sister);

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
            Temp = new(current.BigBrother, current.SmallBrother, current.Sister);

            if (bestDivide.Value > Temp.Value)
                bestDivide = Temp;


            current.BigBrother.RemoveLast();
            // give to SmallBrother
            current.SmallBrother.AddLast(tempValue);
            Temp = new(current.BigBrother, current.SmallBrother, current.Sister);

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

}
