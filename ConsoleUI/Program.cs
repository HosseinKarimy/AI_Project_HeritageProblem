﻿
using ConsoleUI;
using System.Diagnostics;
using System.Text;

var AllItems = new List<double>
{
    1444685023.0,
    147143653.0,
    3553542816.0,
    296924024.0,
    2287143902.0,
    3393614635.0,
    4277729237.0,
    2742856284.0,
    3922118837.0,
    1334251687.0,
    1927764466.0,
    3653303069.0,
    4189953905.0,
    905797322.0,
    2048978342.0,
    1976012864.0,
    3780023666.0,
    72593689.0,
    3309147370.0,
    2026598387.0,
    1192920608.0,
    2429354053.0,
    2391167062.0,
    2226593888.0,
    489035206.0,
    547245318.0,
    2027952409.0,
    1465626752.0,
    3912271583.0,
    1387505169.0,
    3192017200.0,
    755847237.0,
    3669537464.0,
    2506042949.0,
    1030012019.0,
    2142922365.0,
    4107888203.0,
    3012864664.0,
    4086009460.0,
    245558715.0,
    545534357.0,
    244236176.0,
    634906628.0,
    3752918838.0,
    3546792543.0,
    552427608.0,
    3970438204.0,
    785031284.0,
    3831267936.0,
    1118505600.0,
    2833124253.0,
    3305871370.0,
    1955325325.0,
    3617541657.0,
    2149603724.0,
    2187261515.0,
    1324034615.0,
    2387593042.0,
    977866143.0,
    623867858.0,
    4212413696.0,
    2745928654.0,
    3217677041.0,
    3213529958.0,
    2428790239.0,
    1107949140.0,
    728885317.0,
    2170790102.0,
    2584491919.0,
    2531144770.0,
    2788449507.0,
    3379213673.0,
    3948496054.0,
    2955853479.0,
    3821232074.0,
    1104466351.0,
    4225208446.0,
    2298292067.0,
    3148968554.0,
    4272714002.0,
    104717507.0,
    486843667.0,
    1553709427.0,
    3655311003.0,
    1759685834.0,
    906228413.0,
    572712225.0,
    1161296095.0,
    3458485926.0,
    805746663.0,
    2456903478.0,
    4058822183.0,
    2321337986.0,
    2115250876.0,
    2106466203.0,
    2866489240.0,
    539240199.0,
    565575138.0,
    2452567575.0,
    1025393815.0
};
AllItems.Sort();

Heritage.AllItems = new LinkedList<double>(AllItems);

Heritage Best = HillClimbing();

object s = new();
object l = new();

Stopwatch sw = new();
sw.Start();

List<Task> tasks = new();
for (int i = 0; i < 5; i++)
{
    var task = Task.Run(() =>
    {
        for (int i = 0; i < 1000000; i++)
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
//_ = Task.Run(() =>
//{
//    for (int i = 0; i < 100; i++)
//    {
//        var temp = HillClimbing2(Best);

//        if (temp.Value < Best.Value)
//            Best = temp;
//    }
//});


//Task.Delay(30000).Wait();

await Task.WhenAll(tasks);

TimeSpan ts = sw.Elapsed;
string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
Console.WriteLine("RunTime: " + elapsedTime);

Best.Print();
return 0;


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
        if (timeoutCounter == 100)
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
