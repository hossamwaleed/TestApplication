namespace TestApplication.Contracts.ShopInfo;

public record ShopInfoResponse
(
    string Name,
    string Street,
    string City,
   double Lat,
   double Long
    );