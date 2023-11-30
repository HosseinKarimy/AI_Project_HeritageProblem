using HillClimbing;

Heritage.Initialize();
await HillClimbing_Steepest.TryClimbing(1000, 4);
//await HillClimbing_Lazy.TryClimbing(TimeSpan.FromSeconds(10), 4);

return 0;


