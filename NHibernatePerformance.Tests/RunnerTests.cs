using NHibernatePerformance.Helper;

namespace NHibernatePerformance.Tests
{
  public class RunnerTests
  {
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestCreateAndFillDatabase()
    {
      var testObj = new Runner();
      testObj.CreateAndFillDatabase(1000);
      Assert.True(File.Exists("performancetest.db"));
    }
  }
}