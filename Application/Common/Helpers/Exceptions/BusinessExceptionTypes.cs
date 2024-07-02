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

    [Description("Product Name Cannot Be Empty")]
    ProductNameCannotBeEmpty = 402,

    [Description("Product Price Cannot Be Null")]
    ProductPriceCannotBeNull = 403,

    [Description("Product Quantity Cannot Be Null")]
    ProductQuantityCannotBeNull = 404,

    [Description("Product Description Cannot Be Empty")]
    ProductDescriptionCannotBeEmpty = 405,

    [Description("Product Category Cannot Be Empty")]
    ProductCategoryCannotBeEmpty = 406,

    [Description("Not Allow Special Characters")]
    NotAllowSpecialCharacters = 407,

    [Description("Characters Lenght")]
    CharactersLenghtNotValid = 408,

    [Description("ShoppingCart Id Is Not Valid")]
    ShoppingCartIdIsNotValid = 409,

    [Description("Product Count Cannot Be Less")]
    ProductCountNotValid = 410,

    [Description("Customer Id Is Not Valid")]
    CustomerIdIsNotValid = 411,

    [Description("Customer DocumentType Cannot Be Empty")]
    CustomerDocumentTypeCannotBeEmpty = 412,

    [Description("Customer Document Cannot Be Empty")]
    CustomerDocumentCannotBeEmpty = 413,

    [Description("Customer Name Cannot Be Empty")]
    CustomerNameCannotBeEmpty = 414,

    [Description("Customer Email Cannot Be Empty")]
    CustomerEmailCannotBeEmpty = 415,

    [Description("Customer Phone Cannot Be Empty")]
    CustomerPhoneCannotBeEmpty = 416,

    [Description("The Object Cannot Be Empty")]
    ObjectCannotBeEmpty = 417,

    [Description("The List of Products Cannot Be Null")]
    ProductListCannotBeNull = 418,

    [Description("Product Quantity or Price Invalid")]
    ProductQuantityOrPriceInvalid = 419,

    [Description("Customer Not Exists")]
    CustomerNotExists = 420,

    [Description("ShoppingCart Not Exists")]
    ShoppingCartNotExists = 421,

    [Description("Not Products In Cart")]
    NotProductsInCart = 422,

    [Description("Cart And Customer Invalid")]
    CartAndCustomerInvalid = 423,

    [Description("Customer Id Cannot Be Null")]
    CustomerIdCannotBeNull = 424,

    [Description("Customer DocumentType Is Invalid")]
    CustomerDocumentTypeIsInvalid = 425,

    [Description("Customer Document Cannot Be Very Long")]
    CustomerDocumentCannotBeVeryLong = 426,

    [Description("Customer Name Cannot Be Very Long")]
    CustomerNameCannotBeVeryLong = 427,

    [Description("Customer Phone Cannot Be Very Long")]
    CustomerPhoneCannotBeVeryLong = 428,

    [Description("Products Unavailable")]
    ProductsUnavailable = 429,

    [Description("Pagination Parameters Not Valid")]
    PaginationParametersNotValid = 430,

    [Description("Customer Email Not Valid")]
    CustomerEmailNotValid = 431,

    [Description("Product Id Cannot Be Null")]
    ProductIdCannotBeNull = 432,

    [Description("ShoppingCart Id Cannot Be Null")]
    ShoppingCartIdCannotBeNull = 433,

    [Description("Product Not Exists In The Cart")]
    ProductNotExistsInTheCart = 434,

    [Description("Invoice Id Cannot Be Null")]
    InvoiceIdCannotBeNull = 435,

    [Description("Invoice Id Is Invalid")]
    InvoiceIdIsNotValid = 435,
}