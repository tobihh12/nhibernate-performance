namespace NHibernatePerformance.Helper
{
  using NHibernatePerformance.BusinessEntities;
  using System.Collections.Generic;
  using System.Data.SQLite;
  using System;

  /// <summary>
  /// Run the performance test
  /// </summary>
  public class Runner
  {
    
    /// <summary>
    /// Query everything once
    /// </summary>
    public void QueryAll()
    {
      SessionFactory.Instance.CreateSessionFactory();
      using (var session = SessionFactory.Instance.CreateSession())
      {
        var list = session.QueryOver<Procuct>().List<Procuct>();
      }
    }

    /// <summary>
    /// The create and fill database.
    /// </summary>
    /// <param name="insertedValues">
    /// The inserted Values.
    /// </param>
    public void CreateAndFillDatabase(long insertedValues)
    {
      SQLiteConnection.CreateFile("performancetest.db");
      SessionFactory.Instance.CreateSchemaFromMappingFiles();


      var products = new List<Procuct>();
      const string ProduktName = "Produkt";
      var random = new Random();

      for (int index = 0; index < insertedValues; index++)
      {
        var p = new Procuct { Id = Guid.NewGuid(), Name = ProduktName + index, Price = random.NextDouble() * 7 };
        products.Add(p);
      }

      SessionFactory.Instance.CreateSessionFactory();
      using (var session = SessionFactory.Instance.CreateSession())
      {
        using (var tx = session.BeginTransaction())
        {
          try
          {
            foreach (var product in products)
            {
              session.Save(product);
            }
            tx.Commit();
          }
          catch (Exception)
          {
            tx.Rollback();
            throw;
          }
        }
      }
    }

  }
}