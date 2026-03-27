using UserAccess.Core.Entities;
using UserAccess.Application.Dtos;

namespace UserAccess.Application.Mappers;

public static class AddressMapper
{
    public static Address MapToEntity(this AddressModel addressModel)
    {
        return new Address
        {
            AddressId = addressModel.AddressId,
            FirstName = addressModel.FirstName,
            LastName = addressModel.LastName,
            CountryId = 99, // India from Country Table
            UnitNumber = addressModel.UnitNumber,
            AreaOrStreet = addressModel.AreaOrStreet,
            Landmark = addressModel.Landmark,
            TownOrCity = addressModel.TownOrCity,
            State = addressModel.State,
            Pincode = addressModel.Pincode,
            IsDefaultAddress = addressModel.IsDefaultAddress
        };
    }

    public static AddressModel MapToDto(this Address address)
    {
        return new AddressModel
        {
            AddressId = address.AddressId,
            FirstName = address.FirstName,
            LastName = address.LastName,
            UnitNumber = address.UnitNumber,
            AreaOrStreet = address.AreaOrStreet,
            Landmark = address.Landmark,
            TownOrCity = address.TownOrCity,
            State = address.State,
            Pincode = address.Pincode,
            IsDefaultAddress = address.IsDefaultAddress
        };
    }
}
