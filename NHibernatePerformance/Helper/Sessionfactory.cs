namespace NHibernatePerformance.Helper
{
  using System;
  using System.Reflection;

  using NHibernate;
  using NHibernate.Cfg;
  using NHibernate.Cfg.MappingSchema;
  using NHibernate.Mapping.ByCode;
  using NHibernate.Tool.hbm2ddl;

  using NHibernatePerformance.BusinessEntities.Mappings;

  /// <summary>
  /// The session factory.
  /// </summary>
  public sealed class SessionFactory : IDisposable
  {
    /// <summary>
    /// The session factory.
    /// </summary>
    private static SessionFactory sessionFactory;

    /// <summary>
    /// The factory.
    /// </summary>
    private ISessionFactory factory;

    /// <summary>
    /// Gets the instance.
    /// </summary>
    public static SessionFactory Instance
    {
      get
      {
        return sessionFactory ?? (sessionFactory = new SessionFactory());
      }
    }

    /// <summary>
    /// The create session factory.
    /// </summary>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public bool CreateSessionFactory()
    {
      var config = CreateConfiguration(); 
      factory = config.BuildSessionFactory();
      return factory != null;
    }

    /// <summary>
    /// The create schema from mapping files.
    /// </summary>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public bool CreateSchemaFromMappingFiles()
    {
      var config = CreateConfiguration();
      var schema = new SchemaExport(config);
      schema.Create(false, true);
      return true;
    }

    /// <summary>
    /// The crete session.
    /// </summary>
    /// <returns>
    /// The <see cref="ISession"/>.
    /// </returns>
    public ISession CreateSession()
    {
      return factory == null ? null : factory.OpenSession();
    }

    /// <summary>
    /// The dispose.
    /// </summary>
    public void Dispose()
    {
      if (factory == null)
      {
        return;
      }
      factory.Dispose();
      factory = null;
    }

    /// <summary>
    /// The create configuration.
    /// </summary>
    /// <returns>
    /// The <see cref="Configuration"/>.
    /// </returns>
    private Configuration CreateConfiguration()
    {
      if (factory != null)
      {
        factory.Dispose();
        factory = null;
      }

      var config = new Configuration();
      config.Configure();

      var mapping = GetMappings();
      config.AddDeserializedMapping(mapping, "NHibernatePerformaceTest");
      SchemaMetadataUpdater.QuoteTableAndColumns(config);
      return config;
    }

    /// <summary>
    /// The get mappings.
    /// </summary>
    /// <returns>
    /// The <see cref="HbmMapping"/>.
    /// </returns>
    private HbmMapping GetMappings()
    {
      var mapper = new ModelMapper();
      mapper.AddMappings(Assembly.GetAssembly(typeof(ProductMap)).GetExportedTypes());
      var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
      return mapping;
    }
  }
}
