using System.ComponentModel;

namespace Common.Helpers.Exceptions;

public enum CommonExceptionTypes
{
    [Description("Excepción no controlada.")]
    NotControlledException,

    [Description("Ambiente errado.")]
    WrongEnviromentValue,

    [Description("Header {0} es requerido.")]
    HeaderIsRequired,
}

public enum DriverBusinessException
{
    /// <summary>
    ///     A not Controlled Exception
    /// </summary>
    [Description("Product id cannot be empty")]
    NotControlledException = 555,

    /// <summary>
    ///     Product not found on mongo
    /// </summary>
    [Description("Product not found on mongo")]
    TransactionNotFound = 556,

    /// <summary>
    ///     TransactionIdCanNotBeNull
    /// </summary>
    [Description("ProductIdCanNotBeNull")]
    TransactionIdCanNotBeNull = 900,

    /// <summary>
    ///     TransactionIdCanNotBeEmpty
    /// </summary>
    [Description("ProductIdCanNotBeEmpty")]
    TransactionIdCanNotBeEmpty = 901,

    /// <summary>
    ///     Not allow Special Characters
    /// </summary>
    [Description("Not allow Special Characters")]
    NotAllowSpecialCharacters = 902,
}