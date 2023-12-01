using HillClimbing;

Heritage.Initialize();
var result = await HillClimbing_Steepest.TryClimbing(1000, 4);
//await HillClimbing_Lazy.TryClimbing(TimeSpan.FromSeconds(10), 4);
//await HillClimbing_Stochastic.TryClimbing(TimeSpan.FromSeconds(100),2, result);

return 0;


