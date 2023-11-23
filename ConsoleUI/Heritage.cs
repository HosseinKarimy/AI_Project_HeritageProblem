namespace ConsoleUI;

public class Heritage
{
    public static List<double> AllItems;
    private List<double> BigBrother;
    private List<double> SmallBrother;
    private List<double> Sister;
    public double Value { get; init; }
    public Heritage(List<double> BigBrother, List<double> SmallBrother, List<double> Sister)
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
        return new Heritage(AllItems.GetRange(0, BigBrotherItemsCount), AllItems.GetRange(BigBrotherItemsCount, SmallBrotherItemsCount), AllItems.GetRange(BigBrotherItemsCount + SmallBrotherItemsCount, SisterItemsCount));
    }

    public Heritage CopyOf()
    {
        return new Heritage(new List<double>(BigBrother!), new List<double>(SmallBrother!), new List<double>(Sister!));
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
            double tempValue = current.BigBrother[0];
            current.BigBrother.RemoveAt(0);
            //give to small brother
            current.SmallBrother.Add(tempValue);
            Temp = new(current.BigBrother, current.SmallBrother, current.Sister);

            if (bestDivide.Value > Temp.Value)
                bestDivide = Temp;


            current.SmallBrother.Remove(tempValue);
            // give to sister
            current.Sister.Add(tempValue);
            Temp = new(current.BigBrother, current.SmallBrother, current.Sister);

            if (bestDivide.Value > Temp.Value)
                bestDivide = Temp;

            //back to big brother
            current.Sister.Remove(tempValue);
            current.BigBrother.Add(tempValue);
        }

        //for SmallBrother
        for (int i = 0; i < current.SmallBrother.Count; i++)
        {
            double tempValue = current.SmallBrother[0];
            current.SmallBrother.RemoveAt(0);

            //give to Big brother
            current.BigBrother.Add(tempValue);
            Temp = new(current.BigBrother, current.SmallBrother, current.Sister);

            if (bestDivide.Value > Temp.Value)
                bestDivide = Temp;


            current.BigBrother.Remove(tempValue);
            // give to sister
            current.Sister.Add(tempValue);
            Temp = new(current.BigBrother, current.SmallBrother, current.Sister);

            if (bestDivide.Value > Temp.Value)
                bestDivide = Temp;

            //back to Small brother
            current.Sister.Remove(tempValue);
            current.SmallBrother.Add(tempValue);
        }

        //for Sister
        for (int i = 0; i < current.Sister.Count; i++)
        {
            double tempValue = current.Sister[0];
            current.Sister.RemoveAt(0);
            //give to Big brother
            current.BigBrother.Add(tempValue);
            Temp = new(current.BigBrother, current.SmallBrother, current.Sister);

            if (bestDivide.Value > Temp.Value)
                bestDivide = Temp;


            current.BigBrother.Remove(tempValue);
            // give to SmallBrother
            current.SmallBrother.Add(tempValue);
            Temp = new(current.BigBrother, current.SmallBrother, current.Sister);

            if (bestDivide.Value > Temp.Value)
                bestDivide = Temp;

            //back to Small brother
            current.SmallBrother.Remove(tempValue);
            current.Sister.Add(tempValue);
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
