namespace Voky.app.product.api.DTOs;

public record SupplierCostDto(
    int CurrencyNr,
    decimal CostPriceAddon,
    long MarketMargin
);
