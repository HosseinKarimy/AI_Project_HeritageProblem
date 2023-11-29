namespace HillClimbing;

public class Heritage_Lazy : Heritage
{
    private readonly double[] BigBrother;
    private readonly double[] SmallBrother;
    private readonly double[] Sister;

    public Heritage_Lazy(IEnumerable<double> BigBrotherItems, IEnumerable<double> SmallBrotherItems, IEnumerable<double> SisterItems)
        : base(BigBrotherItems, SmallBrotherItems, SisterItems)
    {
        BigBrother = BigBrotherItems.ToArray();
        SmallBrother = SmallBrotherItems.ToArray();
        Sister = SisterItems.ToArray();
    }

    public Heritage_Lazy(double[] BigBrotherItems, double[] SmallBrotherItems, double[] SisterItems)
        : base(BigBrotherItems, SmallBrotherItems, SisterItems)
    {
        BigBrother = new double[BigBrotherItems.Length];
        Array.Copy(BigBrotherItems, BigBrother, BigBrotherItems.Length);

        SmallBrother = new double[SmallBrotherItems.Length];
        Array.Copy(SmallBrotherItems, SmallBrother, SmallBrotherItems.Length);

        Sister = new double[SisterItems.Length];
        Array.Copy(SisterItems, Sister, SisterItems.Length);
    }

    public static Heritage_Lazy FromRandom()
    {
        var lists = GetRandom();
        return new Heritage_Lazy(lists.BigBrotherItems, lists.SmallBrotherItems, lists.SisterItems);
    }

    public Heritage_Lazy RandomNeighbor()
    {
        Heritage_Lazy randomNeighbor;

        var random = new Random();
        int fromChild = random.Next(0, 3);

        // from Big Brother
        if (fromChild == 0)
        {
            int SelectedItemIndex = random.Next(0, BigBrother.Length);
            var selectedItem = BigBrother.ElementAt(SelectedItemIndex);
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

        return randomNeighbor;
    }
}
