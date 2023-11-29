using System.Diagnostics;

namespace HillClimbing.Steepest;

public class HillClimbing_Steepest
{
    public HillClimbing_Steepest(List<double> items)
    {
        Heritage.AllItems = new LinkedList<double>(items);
    }
    public async Task TryClimbing(int TryNumber = 10, int TryTasks = 2)
    {
        Heritage Best = HillClimbing();

        object s = new();

        Stopwatch sw = new();
        sw.Start();

        List<Task> tasks = new();
        for (int i = 0; i < TryTasks; i++)
        {
            var task = Task.Run(() =>
            {
                for (int i = 0; i < TryNumber; i++)
                {
                    var temp = HillClimbing();
                    if (temp.Value < Best.Value)
                        lock (s)
                        {
                            if (temp.Value < Best.Value)
                                Best = temp;
                        }
                }
            });
            tasks.Add(task);
        }

        await Task.WhenAll(tasks);

        TimeSpan ts = sw.Elapsed;
        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
        Console.WriteLine("RunTime: " + elapsedTime);

        Best.Print();
    }

    static Heritage HillClimbing()
    {
        var Current = Heritage.FromRandom();
        while (true)
        {
            var bestNeighbor = Heritage.BestNeighbor(Current.CopyOf());
            if (Current.Value <= bestNeighbor.Value)
                return Current;
            Current = bestNeighbor;
        }
    }

    static Heritage HillClimbing2(Heritage? heritage = null)
    {
        var Current = heritage ??= Heritage.FromRandom();
        int timeoutCounter = 0;
        while (true)
        {
            if (timeoutCounter == 15)
                return Current;
            var bestNeighbor = Heritage.BestNeighbor(Current.CopyOf());
            if (Current.Value == bestNeighbor.Value)
            {
                Current = bestNeighbor;
                timeoutCounter++;
            } else if (Current.Value < bestNeighbor.Value)
            {
                return Current;
            } else
            {
                timeoutCounter = 0;
                Current = bestNeighbor;
            }
        }
    }

}
