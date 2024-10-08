﻿using System.Diagnostics;
using HillClimbing;

namespace Algorithms.HillClimbing.Steepest;

public class HillClimbing_Steepest
{
    public static async Task<IHeritage> TryClimbing(int TryNumber = 10, int TryTasks = 2)
    {
        Heritage_Steepest Best = HillClimbing();

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
                    temp = HillClimbing2(temp);
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
        string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
        Console.WriteLine("RunTime: " + elapsedTime);

        Best.Print();
        return Best;
    }

    static Heritage_Steepest HillClimbing()
    {
        var Current = Heritage_Steepest.FromRandom();
        while (true)
        {
            Heritage_Steepest bestNeighbor = Current.BestNeighbor();
            if (Current.Value <= bestNeighbor.Value)
                return Current;
            Current = bestNeighbor;
        }
    }
    static Heritage_Steepest HillClimbing2(Heritage_Steepest start)
    {
        var Current = start ?? Heritage_Steepest.FromRandom();
        while (true)
        {
            Heritage_Steepest bestNeighbor = Current.BestNeighbor2();
            if (Current.Value <= bestNeighbor.Value)
                return Current;
            Current = bestNeighbor;
        }

    }

    static Heritage_Steepest HillClimbing3(Heritage_Steepest? heritage = null)
    {
        var Current = heritage ??= Heritage_Steepest.FromRandom();
        int timeoutCounter = 0;
        while (true)
        {
            if (timeoutCounter == 15)
                return Current;
            Heritage_Steepest bestNeighbor = Current.GetNeighbor();
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
