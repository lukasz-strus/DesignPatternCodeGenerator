

Console.WriteLine("");

/*
 * [Container(ObjectLifeTime.Singleton, "AddViewModels", new {"ITest123"})] - 1. nazwa kontenera; 2. interfejsy wykluczone
 * class Test : ITest1, ITest12, ITest123
 * 
 * 
 * generated:
 * 
 * public static partial class AddViewModelHostBuildersExtension
 * {
 *      public satatic partial IHostBuilder AddViewModels(this IHostBuilder host)
 *      {
 *          host.ConfigureServices(services =>
 *          {
 *              services.AddSingleton<ITest1, Test>();
 *              services.AddSingleton<ITest12, Test>();
 *          }
 *      }
 * }
 * 
 * 
 * 
 */


