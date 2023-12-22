using HillClimbing;

namespace Algorithms.HillClimbing.Stochastic;

public class Heritage_Stochastic : Heritage
{

    private readonly LinkedList<double> BigBrother;
    private readonly LinkedList<double> SmallBrother;
    private readonly LinkedList<double> Sister;

    public Heritage_Stochastic(IEnumerable<double> BigBrother, IEnumerable<double> SmallBrother, IEnumerable<double> Sister)
        : base(BigBrother, SmallBrother, Sister)
    {
        this.BigBrother = new LinkedList<double>(BigBrother);
        this.SmallBrother = new LinkedList<double>(SmallBrother);
        this.Sister = new LinkedList<double>(Sister);
    }
    public Heritage_Stochastic(LinkedList<double> BigBrother, LinkedList<double> SmallBrother, LinkedList<double> Sister)
        : base(BigBrother, SmallBrother, Sister)
    {
        this.BigBrother = BigBrother;
        this.SmallBrother = SmallBrother;
        this.Sister = Sister;
    }
    public Heritage_Stochastic(LinkedList<double> BigBrother, LinkedList<double> SmallBrother, LinkedList<double> Sister, double? BigBrotherAmount, double? SmallBrotherAmount, double? SisterAmount)
        : base(BigBrother, SmallBrother, Sister)
    {
        this.BigBrother = BigBrother;
        this.SmallBrother = SmallBrother;
        this.Sister = Sister;
        this.BigBrotherAmount = BigBrotherAmount;
        this.SmallBrotherAmount = SmallBrotherAmount;
        this.SisterAmount = SisterAmount;
    }

    public static Heritage_Stochastic FromRandom()
    {
        var lists = GetRandom();
        return new Heritage_Stochastic(lists.BigBrotherItems, lists.SmallBrotherItems, lists.SisterItems);
    }

    public Heritage_Stochastic CopyOf()
    {
        return new Heritage_Stochastic(new LinkedList<double>(BigBrother!), new LinkedList<double>(SmallBrother!), new LinkedList<double>(Sister!));
    }
    public override Heritage_Stochastic GetNeighbor()
    {
        return BestNeighbor2();
    }

    public Heritage_Stochastic BestNeighbor2()  //O(3n^3)
    {
        HashSet<Heritage_Stochastic> AllNeighbors = new();
        var current = CopyOf();
        double SmallBrotherAmountTemp;
        double SisterAmountTemp;
        double BigBrotherAmountTemp;
        //for BigBrother
        for (int i = 0; i < current.BigBrother.Count; i++)
        {
            double bigBrotherItem = current.BigBrother.GetAndRemoveFirst();
            //Swap with small brother
            for (int j = 0; j < current.SmallBrother.Count; j++)
            {
                var smallBrotherItem = current.SmallBrother.GetAndRemoveFirst();
                current.SmallBrother.AddFirst(bigBrotherItem);
                current.BigBrother.AddFirst(smallBrotherItem);
                BigBrotherAmountTemp = current.BigBrotherAmount!.Value - bigBrotherItem + smallBrotherItem;
                SmallBrotherAmountTemp = current.SmallBrotherAmount!.Value + bigBrotherItem - smallBrotherItem;
                SisterAmountTemp = current.SisterAmount!.Value;
                //var value = CalculateValue(BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);
                var item = new Heritage_Stochastic(current.BigBrother.CreateCopy(), current.SmallBrother.CreateCopy(), current.Sister.CreateCopy(), BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);
                AllNeighbors.Add(item);
                current.BigBrother.RemoveFirst();
                current.SmallBrother.RemoveFirst();
                current.SmallBrother.AddLast(smallBrotherItem);
            }

            // swap with sister
            for (int j = 0; j < current.Sister.Count; j++)
            {
                var sisterItem = current.Sister.GetAndRemoveFirst();
                current.Sister.AddFirst(bigBrotherItem);
                current.BigBrother.AddFirst(sisterItem);
                BigBrotherAmountTemp = current.BigBrotherAmount!.Value - bigBrotherItem + sisterItem;
                SmallBrotherAmountTemp = current.SmallBrotherAmount!.Value;
                SisterAmountTemp = current.SisterAmount!.Value + bigBrotherItem - sisterItem;
                //var value = CalculateValue(BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);
                var item = new Heritage_Stochastic(current.BigBrother.CreateCopy(), current.SmallBrother.CreateCopy(), current.Sister.CreateCopy(), BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);
                AllNeighbors.Add(item);
                current.BigBrother.RemoveFirst();
                current.Sister.RemoveFirst();
                current.Sister.AddLast(sisterItem);
            }

            //back to big brother
            current.BigBrother.AddLast(bigBrotherItem);
        }

        //for SmallBrother
        for (int i = 0; i < current.SmallBrother.Count; i++)
        {
            double smallBrotherItem = current.SmallBrother.GetAndRemoveFirst();

            //Swap with Big brother
            for (int j = 0; j < current.BigBrother.Count; j++)
            {
                var bigBrotherItem = current.BigBrother.GetAndRemoveFirst();
                current.SmallBrother.AddFirst(bigBrotherItem);
                current.BigBrother.AddFirst(smallBrotherItem);
                BigBrotherAmountTemp = current.BigBrotherAmount!.Value + smallBrotherItem - bigBrotherItem;
                SmallBrotherAmountTemp = current.SmallBrotherAmount!.Value - smallBrotherItem + bigBrotherItem;
                SisterAmountTemp = current.SisterAmount!.Value;
                //var value = CalculateValue(BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);
                var item = new Heritage_Stochastic(current.BigBrother.CreateCopy(), current.SmallBrother.CreateCopy(), current.Sister.CreateCopy(), BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);
                AllNeighbors.Add(item);
                current.BigBrother.RemoveFirst();
                current.SmallBrother.RemoveFirst();
                current.BigBrother.AddLast(bigBrotherItem);
            }

            // swap with sister
            for (int j = 0; j < current.Sister.Count; j++)
            {
                var sisterItem = current.Sister.GetAndRemoveFirst();
                current.Sister.AddFirst(smallBrotherItem);
                current.SmallBrother.AddFirst(sisterItem);
                BigBrotherAmountTemp = current.BigBrotherAmount!.Value;
                SmallBrotherAmountTemp = current.SmallBrotherAmount!.Value - smallBrotherItem + sisterItem;
                SisterAmountTemp = current.SisterAmount!.Value + smallBrotherItem - sisterItem;
                //var value = CalculateValue(BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);
                var item = new Heritage_Stochastic(current.BigBrother.CreateCopy(), current.SmallBrother.CreateCopy(), current.Sister.CreateCopy(), BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);
                AllNeighbors.Add(item);
                current.SmallBrother.RemoveFirst();
                current.Sister.RemoveFirst();
                current.Sister.AddLast(sisterItem);
            }

            //back to Small brother
            current.SmallBrother.AddLast(smallBrotherItem);
        }

        //for Sister
        for (int i = 0; i < current.Sister.Count; i++)
        {
            double sisterItem = current.Sister.GetAndRemoveFirst();
            //Swap with Big brother
            for (int j = 0; j < current.BigBrother.Count; j++)
            {
                var bigBrotherItem = current.BigBrother.GetAndRemoveFirst();
                current.Sister.AddFirst(bigBrotherItem);
                current.BigBrother.AddFirst(sisterItem);
                BigBrotherAmountTemp = current.BigBrotherAmount!.Value - bigBrotherItem + sisterItem;
                SmallBrotherAmountTemp = current.SmallBrotherAmount!.Value;
                SisterAmountTemp = current.SisterAmount!.Value + bigBrotherItem - sisterItem;
                //var value = CalculateValue(BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);
                var item = new Heritage_Stochastic(current.BigBrother.CreateCopy(), current.SmallBrother.CreateCopy(), current.Sister.CreateCopy(), BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);
                AllNeighbors.Add(item);
                current.Sister.RemoveFirst();
                current.BigBrother.RemoveFirst();
                current.BigBrother.AddLast(bigBrotherItem);
            }

            //Swap with small brother
            for (int j = 0; j < current.SmallBrother.Count; j++)
            {
                var smallBrotherItem = current.SmallBrother.GetAndRemoveFirst();
                current.SmallBrother.AddFirst(sisterItem);
                current.Sister.AddFirst(smallBrotherItem);
                BigBrotherAmountTemp = current.BigBrotherAmount!.Value;
                SmallBrotherAmountTemp = current.SmallBrotherAmount!.Value + sisterItem - smallBrotherItem;
                SisterAmountTemp = current.SisterAmount!.Value - sisterItem + smallBrotherItem;
                //var value = CalculateValue(BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);
                var item = new Heritage_Stochastic(current.BigBrother.CreateCopy(), current.SmallBrother.CreateCopy(), current.Sister.CreateCopy(), BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);
                AllNeighbors.Add(item);
                current.Sister.RemoveFirst();
                current.SmallBrother.RemoveFirst();
                current.SmallBrother.AddLast(smallBrotherItem);
            }

            //back to Sister
            current.Sister.AddLast(sisterItem);
        }


        double sum = 0;
        foreach (Heritage_Stochastic h in AllNeighbors)
        {
            sum += 1 / h.Value;
        }

        // Generate a random number between 0 and the sum
        Random random = new Random();
        double r = random.NextDouble() * sum;

        // Loop through the heritages in the set
        foreach (Heritage_Stochastic h in AllNeighbors)
        {
            // Subtract the inverse value of the current heritage from the random number
            r -= 1 / h.Value;

            // If the random number is negative or zero, return the current heritage and exit the loop
            if (r <= 0)
            {
                return h;
            }
        }

        //var SumOfValues = AllNeighbors.Sum(heritage =>heritage.Value);
        //double s = 0;
        //var random = new Random();
        //var r = random.NextDouble() * SumOfValues;

        //foreach (var item in AllNeighbors)
        //{
        //    s += item.Value;
        //    if (s >= r)
        //    {
        //        return item;
        //    }
        //}

        return AllNeighbors.Last();
    }
}
