using TestApplication.ApplicationLayer.abstractions;

namespace TestApplication.errors;

public class OrderError
{
    public static Error OrderEmpty = new Error("Order.Empty", "there is no item in this order");

    public static Error quantityValueWrong = new Error("Quantity.ValueWrong", "quantity Value Must be greater than 0");
}
