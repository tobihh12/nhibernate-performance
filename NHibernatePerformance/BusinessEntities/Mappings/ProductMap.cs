namespace NHibernatePerformance.BusinessEntities.Mappings
{
  using NHibernate.Mapping.ByCode;
  using NHibernate.Mapping.ByCode.Conformist;

  /// <summary>
  /// The product map.
  /// </summary>
  public class ProductMap : ClassMapping<Procuct>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ProductMap"/> class.
    /// </summary>
    public ProductMap()
    {
      Id(p => p.Id, map => map.Generator(Generators.Assigned));
      Property(
        p => p.Name,
        map =>
          {
            map.NotNullable(true);
            map.Length(80);
          });
      Property(p => p.Price, map => map.NotNullable(true));
    }
  }
}
