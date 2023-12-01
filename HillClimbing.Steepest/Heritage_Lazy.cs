namespace HillClimbing;

public class Heritage_Lazy : Heritage
{
    private readonly List<double> BigBrother;
    private readonly List<double> SmallBrother;
    private readonly List<double> Sister;

    public Heritage_Lazy(IEnumerable<double> BigBrotherItems, IEnumerable<double> SmallBrotherItems, IEnumerable<double> SisterItems)
        : base(BigBrotherItems, SmallBrotherItems, SisterItems)
    {
        BigBrother = new List<double>(BigBrotherItems);
        SmallBrother = new List<double>(SmallBrotherItems);
        Sister = new List<double>(SisterItems);
    }

    public Heritage_Lazy(List<double> BigBrother, List<double> SmallBrother, List<double> Sister)
        : base(BigBrother, SmallBrother, Sister)
    {
        this.BigBrother = BigBrother;
        this.SmallBrother = SmallBrother;
        this.Sister = Sister;
    }

    public static Heritage_Lazy FromRandom()
    {
        var lists = GetRandom();
        return new Heritage_Lazy(lists.BigBrotherItems, lists.SmallBrotherItems, lists.SisterItems);
    }

    public Heritage_Lazy CopyOf()
    {
        return new Heritage_Lazy(new List<double>(BigBrother!), new List<double>(SmallBrother!), new List<double>(Sister!));
    }

    public Heritage_Lazy RandomNeighbor()
    {
        var thisHeritageTemp = CopyOf();
        var BigBrother = thisHeritageTemp.BigBrother;
        var SmallBrother = thisHeritageTemp.SmallBrother;
        var Sister = thisHeritageTemp.Sister;

        var random = new Random();
        int fromChild = random.Next(0, 3);
        int toChild = random.Next(0, 2);

        // from Big Brother
        if (fromChild == 0)
        {
            int SelectedItemIndex = random.Next(0, BigBrother.Count);
            var selectedItem = BigBrother.ElementAt(SelectedItemIndex);
                BigBrother.RemoveAt(SelectedItemIndex);

            //to small Brother
            if (toChild == 0)
            {
                SmallBrother.Add(selectedItem);
                return new Heritage_Lazy(BigBrother, SmallBrother, Sister);
            }

            //to sister
            if (toChild == 1)
            {
                Sister.Add(selectedItem);
                return new Heritage_Lazy(BigBrother, SmallBrother, Sister);
            }
        }

        // from Small Brother
        if (fromChild == 1)
        {
            int SelectedItemIndex = random.Next(0, SmallBrother.Count);
            var selectedItem = SmallBrother.ElementAt(SelectedItemIndex);
                SmallBrother.RemoveAt(SelectedItemIndex);

            //to Big Brother
            if (toChild == 0)
            {
                BigBrother.Add(selectedItem);
                return new Heritage_Lazy(BigBrother, SmallBrother, Sister);
            }

            //to sister
            if (toChild == 1)
            {
                Sister.Add(selectedItem);
                return new Heritage_Lazy(BigBrother, SmallBrother, Sister);
            }
        }

        // from Sister
        if (fromChild == 2)
        {
            int SelectedItemIndex = random.Next(0, Sister.Count);
            var selectedItem = Sister.ElementAt(SelectedItemIndex);
                Sister.RemoveAt(SelectedItemIndex);

            //to Big Brother
            if (toChild == 0)
            {
                BigBrother.Add(selectedItem);
                return new Heritage_Lazy(BigBrother, SmallBrother, Sister);
            }

            //to Small Brother
            if (toChild == 1)
            {
                SmallBrother.Add(selectedItem);
                return new Heritage_Lazy(BigBrother, SmallBrother, Sister);
            }
        }

        return null;
    }

    public Heritage_Lazy RandomNeighbor2()
    {
        var thisHeritageTemp = CopyOf();
        var BigBrother = thisHeritageTemp.BigBrother;
        var SmallBrother = thisHeritageTemp.SmallBrother;
        var Sister = thisHeritageTemp.Sister;

        var random = new Random();
        int fromChild = random.Next(0, 3);
        int toChild = random.Next(0, 2);

        // from Big Brother
        if (fromChild == 0)
        {
            int bigBrotherSelectedItemIndex = random.Next(0, BigBrother.Count);
            var bigBrotherItem = BigBrother.ElementAt(bigBrotherSelectedItemIndex);
            BigBrother.RemoveAt(bigBrotherSelectedItemIndex);

            //to small Brother
            if (toChild == 0)
            {
                SmallBrother.Add(bigBrotherItem);
                int smallBrotherSelectedItemIndex = random.Next(0, SmallBrother.Count);
                var smallBrotherItem = SmallBrother.ElementAt(smallBrotherSelectedItemIndex);
                BigBrother.Add(smallBrotherItem);
                return new Heritage_Lazy(BigBrother, SmallBrother, Sister);
            }

            //to sister
            if (toChild == 1)
            {
                Sister.Add(bigBrotherItem);
                int sisterSelectedItemIndex = random.Next(0, Sister.Count);
                var sisterItem = Sister.ElementAt(sisterSelectedItemIndex);
                BigBrother.Add(sisterItem);
                return new Heritage_Lazy(BigBrother, SmallBrother, Sister);
            }
        }

        // from Small Brother
        if (fromChild == 1)
        {
            int smallBrotherSelectedItemIndex = random.Next(0, SmallBrother.Count);
            var smallBrotherItem = SmallBrother.ElementAt(smallBrotherSelectedItemIndex);
            SmallBrother.RemoveAt(smallBrotherSelectedItemIndex);

            //to Big Brother
            if (toChild == 0)
            {
                BigBrother.Add(smallBrotherItem);
                int bigBrotherSelectedItemIndex = random.Next(0, BigBrother.Count);
                var bigBrotherItem = BigBrother.ElementAt(bigBrotherSelectedItemIndex);
                SmallBrother.Add(bigBrotherItem);
                return new Heritage_Lazy(BigBrother, SmallBrother, Sister);
            }

            //to sister
            if (toChild == 1)
            {
                Sister.Add(smallBrotherItem);
                int sisterSelectedItemIndex = random.Next(0, Sister.Count);
                var sisterItem = Sister.ElementAt(sisterSelectedItemIndex);
                BigBrother.Add(sisterItem);
                return new Heritage_Lazy(BigBrother, SmallBrother, Sister);
            }
        }

        // from Sister
        if (fromChild == 2)
        {
            int siterSelectedItemIndex = random.Next(0, Sister.Count);
            var sisterItem = Sister.ElementAt(siterSelectedItemIndex);
            Sister.RemoveAt(siterSelectedItemIndex);

            //to Big Brother
            if (toChild == 0)
            {
                BigBrother.Add(sisterItem);
                int bigBrotherSelectedItemIndex = random.Next(0, BigBrother.Count);
                var bigBrotherItem = BigBrother.ElementAt(bigBrotherSelectedItemIndex);
                Sister.Add(bigBrotherItem);
                return new Heritage_Lazy(BigBrother, SmallBrother, Sister);
            }

            //to Small Brother
            if (toChild == 1)
            {
                SmallBrother.Add(sisterItem);
                int smallBrotherSelectedItemIndex = random.Next(0, SmallBrother.Count);
                var smallBrotherItem = SmallBrother.ElementAt(smallBrotherSelectedItemIndex);
                Sister.Add(smallBrotherItem);
                return new Heritage_Lazy(BigBrother, SmallBrother, Sister);
            }
        }

        return null;
    }
}
