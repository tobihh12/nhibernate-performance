namespace NHibernatePerformance.BusinessEntities
{
  using System;

  /// <summary>
  /// The procuct.
  /// </summary>
  public class Procuct
  {
    /// <summary>
    /// Gets or sets the id.
    /// </summary>
    public virtual Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public virtual string Name { get; set; }

    /// <summary>
    /// Gets or sets the price.
    /// </summary>
    public virtual double Price { get; set; }
  }
}
