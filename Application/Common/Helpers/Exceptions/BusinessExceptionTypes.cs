using System.ComponentModel;

namespace Common.Helpers.Exceptions;
public enum BusinessExceptionTypes
{
    NotControlledException,
    InvalidNoteId,
    InvalidNoteListId,
    RecordNotFound
}
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
    [Description("transaction id cannot be empty")]
    NotControlledException = 555,

    /// <summary>
    ///     Transaction not found on mongo
    /// </summary>
    [Description("Transaction not found on mongo")]
    TransactionNotFound = 556,

    /// <summary>
    ///     Transaction not found in payment method
    /// </summary>
    [Description("Transaction not found in payment method")]
    TransactionNotFoundInPaymentMethod = 557,

    /// <summary>
    ///     TransactionIdCanNotBeNull
    /// </summary>
    [Description("TransactionIdCanNotBeNull")]
    TransactionIdCanNotBeNull = 900,

    /// <summary>
    ///     TransactionIdCanNotBeEmpty
    /// </summary>
    [Description("TransactionIdCanNotBeEmpty")]
    TransactionIdCanNotBeEmpty = 901,

    /// <summary>
    ///     Not allow Special Characters
    /// </summary>
    [Description("Not allow Special Characters")]
    NotAllowSpecialCharacters = 902,

    /// <summary>
    ///     Payment Method Cannot Be Empty
    /// </summary>
    [Description("Payment Method Cannot Be Empty")]
    PaymentMethodCannotBeEmpty = 903,

    /// <summary>
    ///     Payment Method Cannot Be Null
    /// </summary>
    [Description("Payment Method Cannot Be Null")]
    PaymentMethodCannotBeNull = 904,

    /// <summary>
    ///     Data Cannot Be Null
    /// </summary>
    [Description("Data Cannot Be Empty")]
    DataCannotBeEmpty = 905,

    /// <summary>
    ///     Data Cannot Be Null
    /// </summary>
    [Description("Data Cannot Be Null")]
    DataCannotBeNull = 906,

    /// <summary>
    ///   ErrorGettingTracsactionToPaymentMethod
    /// </summary>
    [Description("ErrorGettingTracsactionToPaymentMethod")]
    ErrorGettingTracsactionToPaymentMethod = 907,

    /// <summary>
    ///   PhoneNumberMaximumLengthIs10
    /// </summary>
    [Description("PhoneNumberMaximumLengthIs10")]
    PhoneNumberMaximumLengthIs10 = 908,

    /// <summary>
    ///   JustNumberAllowInPhone
    /// </summary>
    [Description("JustNumberAllowInPhone")]
    JustNumberAllowInPhone = 909,

    /// <summary>
    ///   EmailFormatIsWrong
    /// </summary>
    [Description("EmailFormatIsWrong")]
    EmailFormatIsWrong = 910,

    /// <summary>
    ///   EmailFormatIsWrong
    /// </summary>
    [Description("EmailFormatIsWrong")]
    EmailIsRequired = 911,

    /// <summary>
    ///   CurrencyIsRequired
    /// </summary>
    [Description("CurrencyIsRequired")]
    CurrencyIsRequired = 912,

    /// <summary>
    ///   CurrencyIsNotValid
    /// </summary>
    [Description("CurrencyIsNotValid")]
    CurrencyIsNotValid = 913,

    /// <summary>
    ///   ReferenceIsRequired
    /// </summary>
    [Description("ReferenceIsRequired")]
    ReferenceIsRequired = 914,

    /// <summary>
    ///   ReferenceIsNotValid
    /// </summary>
    [Description("ReferenceIsNotValid")]
    ReferenceIsNotValid = 915,

    /// <summary>
    ///   AmountIsRequired
    /// </summary>
    [Description("AmountIsRequired")]
    AmountIsRequired = 916,

    /// <summary>
    ///   EventsKeyIsRequired
    /// </summary>
    [Description("EventsKeyIsRequired")]
    EventsKeyIsRequired = 917,

    /// <summary>
    ///   PrivateKeyIsRequired
    /// </summary>
    [Description("PrivateKeyIsRequired")]
    PrivateKeyIsRequired = 918,

    /// <summary>
    ///   IntegrationKeyIsRequired
    /// </summary>
    [Description("IntegrationKeyIsRequired")]
    IntegrationKeyIsRequired = 919,

    /// <summary>
    ///   PublicKeyIsRequired
    /// </summary>
    [Description("PublicKeyIsRequired")]
    PublicKeyIsRequired = 920,

    /// <summary>
    ///   ErrorByPaymenthMethod
    /// </summary>
    [Description("ErrorCreateTransactionToPaymenthMethod")]
    ErrorCreateTransactionToPaymenthMethod = 921,

    /// <summary>
    ///   SignHashException
    /// </summary>
    [Description("SignHashException")]
    SignHashException = 922,

    /// <summary>
    ///   ErrorUpdatingEvent
    /// </summary>
    [Description("ErrorUpdatingEvent")]
    ErrorUpdatingEvent = 923,

    /// <summary>
    /// AuthorizationCodeEmpty
    /// </summary>
    [Description("Not allow AuthorizationCode empty")]
    AuthorizationCodeEmpty = 924,

    /// <summary>
    ///   AmountIsExclusiveBetween0And250000000
    /// </summary>
    [Description("AmountIsExclusiveBetween1And250000000")]
    AmountIsExclusiveBetween0And250000000 = 925,

    /// <summary>
    ///   DescriptionIsRequired
    /// </summary>
    [Description("DescriptionIsRequired")]
    DescriptionIsRequired = 926,

    /// <summary>
    ///   DescriptionMaxLength64
    /// </summary>
    [Description("DescriptionMaxLength64")]
    DescriptionMaxLength64 = 927,

    /// <summary>
    ///   PaymentMethodIdIsRequired
    /// </summary>
    [Description("PaymentMethodIdIsRequired")]
    PaymentMethodIdIsRequired = 928,
    /// <summary>
    ///   PaymentMethodIdJustNumberAreAllow
    /// </summary>
    [Description("PaymentMethodIdJustNumberAreAllow")]
    PaymentMethodIdJustNumberAreAllow = 929,

    /// <summary>
    ///   PaymentMethodIdMaxLength999999999
    /// </summary>
    [Description("PaymentMethodIdMaxLength999999999")]
    PaymentMethodIdMaxLength999999999 = 930,

    /// <summary>
    ///   TheConsultRoutineDoesNotExist
    /// </summary>
    [Description("TheConsultRoutineDoesNotExist")]
    TheConsultRoutineDoesNotExist = 931,

    /// <summary>
    ///   DescriptionIsNotValid
    /// </summary>
    [Description("DescriptionIsNotValid")]
    DescriptionIsNotValid = 932

}