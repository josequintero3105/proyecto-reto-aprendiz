using System.ComponentModel;

namespace Common.Helpers.Exceptions;

public enum BusinessExceptionTypes
{
    [Description("Not Controller Exception.")]
    NotControlledException,

    [Description("Wrong Enviroment Value.")]
    WrongEnviromentValue,

    [Description("Header {0} Is Required.")]
    HeaderIsRequired,
}

public enum GateWayBusinessException
{
    [Description("Not Controlled Exception")]
    NotControlledException = 400,

    [Description("Product Id Is Not Valid")]
    ProductIdIsNotValid = 401,

    [Description("Product Id Not Found")]
    ProductIdNotFound = 402,

    [Description("Product Name Cannot Be Empty")]
    ProductNameCannotBeEmpty = 403,

    [Description("Product Name Is Not Valid")]
    ProductNameIsNotValid = 404,

    [Description("Product Price Cannot Be Null")]
    ProductPriceCannotBeNull = 405,

    [Description("Product Quantity Cannot Be Null")]
    ProductQuantityCannotBeNull = 406,

    [Description("Product Description Cannot Be Empty")]
    ProductDescriptionCannotBeEmpty = 407,

    [Description("Product Description Is Not Valid")]
    ProductDescriptionIsNotValid = 408,

    [Description("Product Category Cannot Be Empty")]
    ProductCategoryCannotBeEmpty = 409,

    [Description("Product Category Is Not Valid")]
    ProductCategoryIsNotValid = 410,

    [Description("ShoppingCart Id Is Not Valid")]
    ShoppingCartIdIsNotValid = 411,

    [Description("ShoppingCart Id Cannot Be Null")]
    ShoppingCartIdCannotBeNull = 412,

    [Description("ShoppingCart Not Found")]
    ShoppingCartNotExists = 413,

    [Description("ShoppingCart Id Not Found")]
    ShoppingCartIdNotFound = 414,

    [Description("Product Count Cannot Be Less")]
    ProductCountNotValid = 415,

    [Description("Customer Id Is Not Valid")]
    CustomerIdIsNotValid = 416,

    [Description("Customer Id Not Found")]
    CustomerIdNotFound = 417,

    [Description("Customer DocumentType Cannot Be Empty")]
    CustomerDocumentTypeCannotBeEmpty = 418,

    [Description("Customer DocumentType Not valid")]
    CustomerDocumentTypeIsInvalid = 419,

    [Description("Customer Document Cannot Be Empty")]
    CustomerDocumentCannotBeEmpty = 420,

    [Description("Customer Document Is Not Valid")]
    CustomerDocumentIsNotValid = 421,

    [Description("Customer Document Cannot Be Very Long")]
    CustomerDocumentCannotBeVeryLong = 422,

    [Description("Customer Name Cannot Be Empty")]
    CustomerNameCannotBeEmpty = 423,

    [Description("Customer Name Is Not Valid")]
    CustomerNameIsNotValid = 424,

    [Description("Customer Name Cannot Be Very Long")]
    CustomerNameCannotBeVeryLong = 425,

    [Description("Customer Last Name Cannot Be Empty")]
    CustomerLastNameCannotBeEmpty = 426,

    [Description("Customer Last Name Is Not Valid")]
    CustomerLastNameIsNotValid = 427,

    [Description("Customer Last Name Cannot Be Very Lon")]
    CustomerLastNameCannotBeVeryLong = 428,

    [Description("Customer Email Cannot Be Empty")]
    CustomerEmailCannotBeEmpty = 429,

    [Description("Customer Email Not Valid")]
    CustomerEmailNotValid = 430,

    [Description("IndCountry Cannot Be Null")]
    IndCountryCannotBeNull = 431,

    [Description("IndCountry Is Not Valid")]
    IndCountryIsNotValid = 432,

    [Description("IndCountry Cannot Be Very Long")]
    IndCountryCannotBeVeryLong = 433,

    [Description("Customer Phone Cannot Be Empty")]
    CustomerPhoneCannotBeEmpty = 434,

    [Description("Customer Phone Is Not Valid")]
    CustomerPhoneIsNotValid = 435,

    [Description("Customer Phone Cannot Be Very Long")]
    CustomerPhoneCannotBeVeryLong = 436,

    [Description("Country Cannot Be Null")]
    CountryCannotBeNull = 437,

    [Description("Country Is Not Valid")]
    CountryIsNotValid = 438,

    [Description("Country Cannot Be Very Long")]
    CountryCannotBeVeryLong = 439,

    [Description("City Cannot Be Null")]
    CityCannotBeNull = 440,

    [Description("City Is Not Valid")]
    CityIsNotValid = 441,

    [Description("City Cannot Be Very Long")]
    CityCannotBeVeryLong = 442,

    [Description("Customer Address Cannot Be Null")]
    CustomerAddressCannotBeNull = 443,

    [Description("Customer Address Is Not Valid")]
    CustomerAddressIsNotValid = 444,

    [Description("Customer Address Cannot Be Very Long")]
    CustomerAddressCannotBeVeryLong = 445,

    [Description("Customer Ip Address Cannot Be Null")]
    CustomerIpAddressCannotBeNull = 446,

    [Description("Customer Ip Address Is Not Valid")]
    CustomerIpAddressIsNotValid = 447,

    [Description("Customer Ip Address Cannot Be Very Long")]
    CustomerIpAddressCannotBeVeryLong = 448,

    [Description("The Object Cannot Be Empty")]
    ObjectCannotBeEmpty = 449,

    [Description("The List of Products Cannot Be Null")]
    ProductListCannotBeNull = 450,

    [Description("Product Quantity or Price Invalid")]
    ProductQuantityOrPriceInvalid = 451,

    [Description("Customer Not Found")]
    CustomerNotExists = 452,

    [Description("ShoppingCart Is Empty")]
    ShoppingCartIsEmpty = 453,

    [Description("Cart And Customer Invalid")]
    CartAndCustomerInvalid = 454,

    [Description("Customer Id Cannot Be Null")]
    CustomerIdCannotBeNull = 455,

    [Description("Products Unavailable")]
    ProductsUnavailable = 456,

    [Description("Product Id Cannot Be Null")]
    ProductIdCannotBeNull = 457,

    [Description("Product Not Exists In The Cart")]
    ProductNotExistsInTheCart = 458,

    [Description("Invoice Id Cannot Be Null")]
    InvoiceIdCannotBeNull = 459,

    [Description("Invoice Id Is Invalid")]
    InvoiceIdIsNotValid = 460,

    [Description("Invoice Id Not Found")]
    InvoiceIdNotFound = 461,

    [Description("Pagination Parameters Cannot Be Null")]
    PaginationParametersCannotBeNull = 462,

    [Description("Pagination Parameters Not Valid")]
    PaginationParametersNotValid = 463,

    [Description("Not Allow Special Characters")]
    NotAllowSpecialCharacters = 464,

    [Description("Characters Lenght")]
    CharactersLenghtNotValid = 465,

    [Description("Transaction Attempt Failed")]
    TransactionAttemptFailed = 466,

    [Description("Internal Server Error")]
    InternalServerError = 500,
}