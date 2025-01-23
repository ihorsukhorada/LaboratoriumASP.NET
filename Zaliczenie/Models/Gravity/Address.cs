using System;
using System.Collections.Generic;

namespace Zaliczenie.Models.Gravity;

public partial class Address
{
    public int AddressId { get; set; }

    public string? StreetNumber { get; set; }

    public string? StreetName { get; set; }

    public string? City { get; set; }

    public int? CountryId { get; set; }

    public virtual Country? Country { get; set; }

    public virtual ICollection<CustOrder> CustOrders { get; set; } = new List<CustOrder>();

    public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; } = new List<CustomerAddress>();
}
