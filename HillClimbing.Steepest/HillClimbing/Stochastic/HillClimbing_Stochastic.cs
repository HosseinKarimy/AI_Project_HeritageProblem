using System.Diagnostics;
using HillClimbing;

namespace Algorithms.HillClimbing.Stochastic;

public class HillClimbing_Stochastic
{
    public static async Task<IHeritage> TryClimbing(TimeSpan time, int TryTasks = 2, IHeritage? start = null)
    {
        Heritage_Stochastic Best = HillClimbing(TimeSpan.FromSeconds(1), start);

        object s = new();

        Stopwatch sw = new();
        sw.Start();

        List<Task> tasks = new();
        for (int i = 0; i < TryTasks; i++)
        {
            var task = Task.Run(() =>
            {
                var temp = HillClimbing(time);
                if (temp.Value < Best.Value)
                    lock (s)
                    {
                        if (temp.Value < Best.Value)
                            Best = temp;
                    }
            });
            tasks.Add(task);
        }

        await Task.WhenAll(tasks);

        TimeSpan ts = sw.Elapsed;
        string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
        Console.WriteLine("RunTime: " + elapsedTime);

        Best.Print();

        return Best;
    }

    static Heritage_Stochastic HillClimbing(TimeSpan time, IHeritage? start = null)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        var Current = start is null ? Heritage_Stochastic.FromRandom() : new Heritage_Stochastic(start.BigBrotherItems, start.SmallBrotherItems, start.SisterItems);
        while (stopwatch.Elapsed < time)
        {
            Current = Current.GetNeighbor();
        }
        return Current;
    }

    static Heritage_Stochastic HillClimbing2(Heritage_Stochastic? heritage = null)
    {
        var Current = heritage ??= Heritage_Stochastic.FromRandom();
        int timeoutCounter = 0;
        while (true)
        {
            if (timeoutCounter == 15)
                return Current;
            var bestNeighbor = Current.GetNeighbor();
            if (Current.Value == bestNeighbor.Value)
            {
                Current = bestNeighbor;
                timeoutCounter++;
            }
            else if (Current.Value < bestNeighbor.Value)
            {
                return Current;
            }
            else
            {
                timeoutCounter = 0;
                Current = bestNeighbor;
            }
        }
    }
}
