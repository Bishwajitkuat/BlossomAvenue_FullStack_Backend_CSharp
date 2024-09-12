using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BlossomAvenue.Core.Carts;
using BlossomAvenue.Core.Products;
using BlossomAvenue.Core.Users;
using BlossomAvenue.Core.ValueTypes;
using BlossomAvenue.Service.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace BlossomAvenue.Infrastructure.Database
{
    public static class SeedDataUsers
    {
        private static byte[] _salt { get; set; } = Encoding.UTF8.GetBytes("sljfldsfjkeklskjflksjekjisdflj34");
        public static List<User> Users { get; } = [
        new User
        {
            UserId = new Guid("70209ee3-03c0-436e-bc65-8b4e1f27929e"),
            FirstName = "admin1",
            LastName = "admin1",
            Email = "admin1@test.com",
            UserRole = UserRole.Admin,
            IsUserActive = true,
        },
        new User
        {
            UserId = new Guid("7b572d48-db2e-4c08-aa63-15e71e42e9fa"),
            FirstName = "cus1",
            LastName = "cus1",
            Email = "cus1@test.com",
            UserRole = UserRole.Customer,
            IsUserActive = true,
        },
        new User
        {
            UserId = new Guid("693fd321-dc49-4a6d-91aa-85c5fa8fdc5c"),
            FirstName = "emp1",
            LastName = "emp1",
            Email = "emp1@test.com",
            UserRole = UserRole.Employee,
            IsUserActive = true,
        }
        ];

        public static List<UserCredential> UserCredentials { get; } = [

        new UserCredential
        {
            UserId = new Guid("70209ee3-03c0-436e-bc65-8b4e1f27929e"),
            UserName = "admin1@test.com",
            Salt = _salt,
            Password = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: "Abcd1234!",
                    salt: _salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 3000,
                    numBytesRequested: 32
                )
            ),
        },

        new UserCredential
        {
            UserId = new Guid("7b572d48-db2e-4c08-aa63-15e71e42e9fa"),
            UserName = "cus1@test.com",
            Salt = _salt,
            Password = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: "Abcd1234!",
                    salt: _salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 3000,
                    numBytesRequested: 32
                )
            ),
        },
        new UserCredential
        {
            UserId = new Guid("693fd321-dc49-4a6d-91aa-85c5fa8fdc5c"),
            UserName = "emp1@test.com",
            Salt = _salt,
            Password = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: "Abcd1234!",
                    salt: _salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 3000,
                    numBytesRequested: 32
                )
            ),
        }
        ];


        public static List<UserContactNumber> UserContactNumbers { get; } = [
            new UserContactNumber
            {
                ContactNumberId = new Guid("8c90cc18-c18d-4dc3-819e-eae7885626e2"),
                UserId = new Guid("70209ee3-03c0-436e-bc65-8b4e1f27929e"),
                ContactNumber = "1234567891"
            },
            new UserContactNumber
            {
                ContactNumberId = new Guid("a58f9e66-2022-44d3-93f1-68f429650882"),
                UserId = new Guid("7b572d48-db2e-4c08-aa63-15e71e42e9fa"),
                ContactNumber = "1234567891"
            },
            new UserContactNumber
            {
                ContactNumberId = new Guid("f771d76b-55b0-43d7-b302-cb91d27dfc17"),
                UserId = new Guid("693fd321-dc49-4a6d-91aa-85c5fa8fdc5c"),
                ContactNumber = "1234567891"
            }
        ];

        public static List<UserAddress> UserAddresses { get; } = [
                new UserAddress
                {
                    UserAddressId = new Guid("dba1cf84-d0c5-481e-ac7c-f315d72c6944"),
                    UserId = new Guid("70209ee3-03c0-436e-bc65-8b4e1f27929e"),
                    AddressDetailId = new Guid("68d62bf0-7dfa-41b6-a940-4f55376f00e4"),
                    DefaultAddress = true,
                },
                new UserAddress
                {
                    UserAddressId = new Guid("0bc6bdd9-8b1e-4127-9af2-ea16915cf5a6"),
                    UserId = new Guid("7b572d48-db2e-4c08-aa63-15e71e42e9fa"),
                    AddressDetailId = new Guid("697e00f2-150a-4ffe-ae0c-53553ba5b5e9"),
                    DefaultAddress = true,
                },
                new UserAddress
                {
                    UserAddressId = new Guid("bd311d77-8517-4b2d-a9f1-808339b1f9e9"),
                    UserId = new Guid("693fd321-dc49-4a6d-91aa-85c5fa8fdc5c"),
                    AddressDetailId = new Guid("c4d7ec84-7b58-4c6b-aac8-fdde7263cc7d"),
                    DefaultAddress = true,
                }
];

        public static List<AddressDetail> AddressDetails { get; } = [

            new AddressDetail
                {
                    AddressDetailId = new Guid("68d62bf0-7dfa-41b6-a940-4f55376f00e4"),
                    FullName = "admin1 admin 1",
                    AddressLine1 = "Talonpojantie 17 as 403",
                    AddressLine2 = "",
                    PostCode = "00790",
                    City = "Helsinki",
                    Country = Country.Finland
                },

                new AddressDetail
                {
                    AddressDetailId = new Guid("697e00f2-150a-4ffe-ae0c-53553ba5b5e9"),
                    FullName = "cus1 cus1",
                    AddressLine1 = "Talonpojantie 13 as 209",
                    PostCode = "00790",
                    City = "Helsinki",
                    Country = Country.Finland
                },
                new AddressDetail
                {
                    AddressDetailId = new Guid("c4d7ec84-7b58-4c6b-aac8-fdde7263cc7d"),
                    FullName = "emp1 emp1",
                    AddressLine1 = "Talonpojantie 9 as 305",
                    PostCode = "00790",
                    City = "Helsinki",
                    Country = Country.Finland
                }
        ];


        public static List<Cart> Carts { get; } = [
            new Cart
                {
                    CartId = new Guid("7f212adc-7061-450f-9f7e-fbfa39e9de5a"),
                    UserId = new Guid("70209ee3-03c0-436e-bc65-8b4e1f27929e")
                },
            new Cart
                {
                    CartId = new Guid("499907c4-690b-40a7-b792-843d7f01d4d5"),
                    UserId = new Guid("7b572d48-db2e-4c08-aa63-15e71e42e9fa")
                },
            new Cart
                {
                    CartId = new Guid("b2e78177-88b6-4923-8b28-a18fda359e5d"),
                    UserId = new Guid("693fd321-dc49-4a6d-91aa-85c5fa8fdc5c"),
                }
        ];


    }
}