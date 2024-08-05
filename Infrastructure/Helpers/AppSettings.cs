using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helpers;

/// <summary>
///     AppSettings
/// </summary>
[ExcludeFromCodeCoverage]
public class AppSettings
{
    /// <summary>
    ///     Name of de mongo data base
    /// </summary>
    public string? DatabaseName { get; set; }

    /// <summary>
    ///     Name of de mongo collection
    /// </summary>
    public string? CollectionName { get; set; }
}
