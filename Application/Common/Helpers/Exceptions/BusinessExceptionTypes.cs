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

    [Description("Customer Email Cannot Be Empty")]
    CustomerEmailCannotBeEmpty = 426,

    [Description("Customer Email Not Valid")]
    CustomerEmailNotValid = 427,

    [Description("Customer Phone Cannot Be Empty")]
    CustomerPhoneCannotBeEmpty = 428,

    [Description("Customer Phone Is Not Valid")]
    CustomerPhoneIsNotValid = 429,

    [Description("Customer Phone Cannot Be Very Long")]
    CustomerPhoneCannotBeVeryLong = 430,

    [Description("The Object Cannot Be Empty")]
    ObjectCannotBeEmpty = 431,

    [Description("The List of Products Cannot Be Null")]
    ProductListCannotBeNull = 432,

    [Description("Product Quantity or Price Invalid")]
    ProductQuantityOrPriceInvalid = 433,

    [Description("Customer Not Found")]
    CustomerNotExists = 434,

    [Description("Not Products In Cart")]
    NotProductsInCart = 435,

    [Description("Cart And Customer Invalid")]
    CartAndCustomerInvalid = 436,

    [Description("Customer Id Cannot Be Null")]
    CustomerIdCannotBeNull = 437,

    [Description("Products Unavailable")]
    ProductsUnavailable = 438,

    [Description("Product Id Cannot Be Null")]
    ProductIdCannotBeNull = 439,

    [Description("Product Not Exists In The Cart")]
    ProductNotExistsInTheCart = 440,

    [Description("Invoice Id Cannot Be Null")]
    InvoiceIdCannotBeNull = 441,

    [Description("Invoice Id Is Invalid")]
    InvoiceIdIsNotValid = 442,

    [Description("Invoice Id Not Found")]
    InvoiceIdNotFound = 443,

    [Description("Pagination Parameters Cannot Be Null")]
    PaginationParametersCannotBeNull = 444,

    [Description("Pagination Parameters Not Valid")]
    PaginationParametersNotValid = 445,

    [Description("Not Allow Special Characters")]
    NotAllowSpecialCharacters = 446,

    [Description("Characters Lenght")]
    CharactersLenghtNotValid = 447,
}