using System.Runtime.CompilerServices;

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

    public override Heritage_Steepest GetNeighbor()
    {
        return BestNeighbor2();
    }

    public Heritage_Steepest BestNeighbor() //O(3n^2)
    {
        var current = CopyOf();
        Heritage_Steepest bestDivide = current;
        double Temp;
        double SmallBrotherAmountTemp;
        double SisterAmountTemp;
        double BigBrotherAmountTemp;
        //for BigBrother
        for (int i = 0; i < current.BigBrother.Count; i++)
        {
            double tempValue = current.BigBrother.GetAndRemoveFirst();
            //give to small brother
            current.SmallBrother.AddLast(tempValue);
            BigBrotherAmountTemp = current.BigBrotherAmount!.Value - tempValue;
            SmallBrotherAmountTemp = current.SmallBrotherAmount!.Value + tempValue;
            SisterAmountTemp = current.SisterAmount!.Value;
            Temp = CalculateValue(BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);

            if (bestDivide.Value >= Temp)
                bestDivide = new(current.BigBrother.CreateCopy(), current.SmallBrother.CreateCopy(), current.Sister.CreateCopy(), BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);


            current.SmallBrother.RemoveLast();
            // give to sister
            current.Sister.AddLast(tempValue);
            BigBrotherAmountTemp = current.BigBrotherAmount!.Value - tempValue;
            SmallBrotherAmountTemp = current.SmallBrotherAmount!.Value;
            SisterAmountTemp = current.SisterAmount!.Value + tempValue;
            Temp = CalculateValue(BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);

            if (bestDivide.Value >= Temp)
                bestDivide = new(current.BigBrother.CreateCopy(), current.SmallBrother.CreateCopy(), current.Sister.CreateCopy(), BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);

            //back to big brother
            current.Sister.RemoveLast();
            current.BigBrother.AddLast(tempValue);
        }

        //for SmallBrother
        for (int i = 0; i < current.SmallBrother.Count; i++)
        {
            double tempValue = current.SmallBrother.GetAndRemoveFirst();

            //give to Big brother
            current.BigBrother.AddLast(tempValue);
            BigBrotherAmountTemp = current.BigBrotherAmount!.Value + tempValue;
            SmallBrotherAmountTemp = current.SmallBrotherAmount!.Value - tempValue;
            SisterAmountTemp = current.SisterAmount!.Value;
            Temp = CalculateValue(BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);

            if (bestDivide.Value >= Temp)
                bestDivide = new(current.BigBrother.CreateCopy(), current.SmallBrother.CreateCopy(), current.Sister.CreateCopy(), BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);


            current.BigBrother.RemoveLast();
            // give to sister
            current.Sister.AddLast(tempValue);
            BigBrotherAmountTemp = current.BigBrotherAmount!.Value;
            SmallBrotherAmountTemp = current.SmallBrotherAmount!.Value - tempValue;
            SisterAmountTemp = current.SisterAmount!.Value + tempValue;
            Temp = CalculateValue(BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);

            if (bestDivide.Value >= Temp)
                bestDivide = new(current.BigBrother.CreateCopy(), current.SmallBrother.CreateCopy(), current.Sister.CreateCopy(), BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);

            //back to Small brother
            current.Sister.RemoveLast();
            current.SmallBrother.AddLast(tempValue);
        }

        //for Sister
        for (int i = 0; i < current.Sister.Count; i++)
        {
            double tempValue = current.Sister.GetAndRemoveFirst();
            //give to Big brother
            current.BigBrother.AddLast(tempValue);
            BigBrotherAmountTemp = current.BigBrotherAmount!.Value + tempValue;
            SmallBrotherAmountTemp = current.SmallBrotherAmount!.Value;
            SisterAmountTemp = current.SisterAmount!.Value - tempValue;
            Temp = CalculateValue(BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);


            if (bestDivide.Value >= Temp)
                bestDivide = new(current.BigBrother.CreateCopy(), current.SmallBrother.CreateCopy(), current.Sister.CreateCopy(), BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);



            current.BigBrother.RemoveLast();
            // give to SmallBrother
            current.SmallBrother.AddLast(tempValue);
            BigBrotherAmountTemp = current.BigBrotherAmount!.Value;
            SmallBrotherAmountTemp = current.SmallBrotherAmount!.Value + tempValue;
            SisterAmountTemp = current.SisterAmount!.Value - tempValue;
            Temp = CalculateValue(BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);


            if (bestDivide.Value >= Temp)
                bestDivide = new(current.BigBrother.CreateCopy(), current.SmallBrother.CreateCopy(), current.Sister.CreateCopy(), BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);


            //back to Sister
            current.SmallBrother.RemoveLast();
            current.Sister.AddLast(tempValue);
        }

        return bestDivide;
    }

    public Heritage_Steepest BestNeighbor2()  //O(3n^3)
    {
        var current = CopyOf();
        Heritage_Steepest bestDivide = current;
        double Temp;
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
                Temp = CalculateValue(BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);

                if (bestDivide.Value >= Temp)
                    bestDivide = new(current.BigBrother.CreateCopy(), current.SmallBrother.CreateCopy(), current.Sister.CreateCopy(), BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);
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
                SmallBrotherAmountTemp = current.SmallBrotherAmount!.Value ;
                SisterAmountTemp = current.SisterAmount!.Value + bigBrotherItem - sisterItem;
                Temp = CalculateValue(BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);

                if (bestDivide.Value >= Temp)
                    bestDivide = new(current.BigBrother.CreateCopy(), current.SmallBrother.CreateCopy(), current.Sister.CreateCopy(), BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);
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
                SisterAmountTemp = current.SisterAmount!.Value ;
                Temp = CalculateValue(BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);

                if (bestDivide.Value >= Temp)
                    bestDivide = new(current.BigBrother.CreateCopy(), current.SmallBrother.CreateCopy(), current.Sister.CreateCopy(), BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);
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
                BigBrotherAmountTemp = current.BigBrotherAmount!.Value ;
                SmallBrotherAmountTemp = current.SmallBrotherAmount!.Value - smallBrotherItem + sisterItem;
                SisterAmountTemp = current.SisterAmount!.Value + smallBrotherItem - sisterItem;
                Temp = CalculateValue(BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);

                if (bestDivide.Value >= Temp)
                    bestDivide = new(current.BigBrother.CreateCopy(), current.SmallBrother.CreateCopy(), current.Sister.CreateCopy(), BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);
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
                Temp = CalculateValue(BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);

                if (bestDivide.Value >= Temp)
                    bestDivide = new(current.BigBrother.CreateCopy(), current.SmallBrother.CreateCopy(), current.Sister.CreateCopy(), BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);
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
                Temp = CalculateValue(BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);

                if (bestDivide.Value >= Temp)
                    bestDivide = new(current.BigBrother.CreateCopy(), current.SmallBrother.CreateCopy(), current.Sister.CreateCopy(), BigBrotherAmountTemp, SmallBrotherAmountTemp, SisterAmountTemp);
                current.Sister.RemoveFirst();
                current.SmallBrother.RemoveFirst();
                current.SmallBrother.AddLast(smallBrotherItem);
            }

            //back to Sister
            current.Sister.AddLast(sisterItem);
        }

        return bestDivide;
    }
}
