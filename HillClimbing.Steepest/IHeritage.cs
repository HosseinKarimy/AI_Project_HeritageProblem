namespace HillClimbing;

public interface IHeritage
{
    public IEnumerable<double> BigBrotherItems { get; init; }
    public IEnumerable<double> SmallBrotherItems { get; init; }
    public IEnumerable<double> SisterItems { get; init; }
    public double Value { get; init; }

    public IHeritage GetNeighbor();
}
