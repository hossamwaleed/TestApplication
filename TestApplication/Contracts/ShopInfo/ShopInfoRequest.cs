namespace TestApplication.Contracts.ShopInfo;

public record ShopInfoRequest
(
    string Name,
    string Street,
    string City,
     ShopLocationRequest Location

    );