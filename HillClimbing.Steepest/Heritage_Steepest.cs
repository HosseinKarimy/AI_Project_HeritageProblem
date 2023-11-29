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
    public Heritage_Steepest BestNeighbor()
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
            double tempValue = current.BigBrother.First!.Value;
            current.BigBrother.RemoveFirst();
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
            double tempValue = current.SmallBrother.First!.Value;
            current.SmallBrother.RemoveFirst();

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
            double tempValue = current.Sister.First!.Value;
            current.Sister.RemoveFirst();
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

}
