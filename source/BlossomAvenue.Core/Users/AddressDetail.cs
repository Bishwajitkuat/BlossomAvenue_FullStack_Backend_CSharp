using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BlossomAvenue.Core.Orders;
using BlossomAvenue.Core.ValueTypes;

namespace BlossomAvenue.Core.Users;

public partial class AddressDetail
{
    public Guid AddressDetailId { get; set; }
    public string FullName { get; set; } = null!;
    public string AddressLine1 { get; set; } = null!;

    public string? AddressLine2 { get; set; }

    public string PostCode { get; set; } = null!;

    public string City { get; set; } = null!;

    public Country Country { get; set; } = Country.Finland;

}
