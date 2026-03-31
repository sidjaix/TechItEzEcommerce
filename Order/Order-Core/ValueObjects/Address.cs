namespace OrderCore.ValueObjects;

// A Record is perfect for a Value Object: it is immutable and equality is based on its values.
public record Address(string Street, string City, string State, string Country, string ZipCode);
