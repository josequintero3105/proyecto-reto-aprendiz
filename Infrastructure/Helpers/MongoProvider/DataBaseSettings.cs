using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helpers.MongoProvider;

/// <summary>
///     BusinessSettings
/// </summary>
[ExcludeFromCodeCoverage]
public class DataBaseSettings
{
    public string? ProductCollectionName {  get; set; }
    public string? ShoppingCartCollectionName { get; set; }
    public string? InvoiceCollectionName { get; set; }
    public string? CustomerCollectioName { get; set; }
    public string? TransactionCollectionName { get; set; }
}
