using HillClimbing;

Heritage.Initialize();
await HillClimbing_Steepest.TryClimbing(100000, 4);
//await HillClimbing_Lazy.TryClimbing(TimeSpan.FromSeconds(20), 4);

return 0;


