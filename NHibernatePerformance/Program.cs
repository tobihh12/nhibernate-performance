using System.Diagnostics;

namespace NHibernatePerformance
{
  using System;
  using NHibernatePerformance.Helper;

  /// <summary>
  /// The program.
  /// </summary>
  public class Program
  {
    /// <summary>
    /// The main.
    /// </summary>
    /// <param name="args">
    /// The args.
    /// </param>
    public static void Main(string[] args)
    {
      var runner = new Runner();
      runner.CreateAndFillDatabase(1000000);

      var stopWatch = Stopwatch.StartNew();
      runner.QueryAll();
      Console.WriteLine(stopWatch.Elapsed);
      Console.WriteLine("Press Enter to terminate...");
      Console.ReadLine();
    }
  }
}
