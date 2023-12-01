using System.Diagnostics;

namespace HillClimbing;

public class HillClimbing_Lazy
{
    public static async Task TryClimbing(TimeSpan time, int TryTasks = 2)
    {
        Heritage_Lazy Best = HillClimbing(TimeSpan.FromSeconds(1));

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
        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
        Console.WriteLine("RunTime: " + elapsedTime);

        Best.Print();
    }

    static Heritage_Lazy HillClimbing(TimeSpan time)
    {
        int counter = 0;
        Stopwatch stopwatch = Stopwatch.StartNew();
        var Current = Heritage_Lazy.FromRandom();
        while (stopwatch.Elapsed < time)
        {
            counter++;
            var randomNeighbor = Current.RandomNeighbor2();
            if (Current.Value > randomNeighbor.Value)
                Current = randomNeighbor;
        }
        Console.WriteLine(counter);
        return Current;
    }

    static Heritage_Lazy HillClimbing2(Heritage_Lazy? heritage = null)
    {
        var Current = heritage ??= Heritage_Lazy.FromRandom();
        int timeoutCounter = 0;
        while (true)
        {
            if (timeoutCounter == 15)
                return Current;
            var bestNeighbor = Current.RandomNeighbor();
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
