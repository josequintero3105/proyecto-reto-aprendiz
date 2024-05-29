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
    [Description("Not Controller Exception")]
    NotControlerException = 400,

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

    [Description("Customer Id Is Not Valid")]
    CustomerIdIsNotValid = 410,
}