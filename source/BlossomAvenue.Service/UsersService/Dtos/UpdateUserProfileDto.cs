using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BlossomAvenue.Core.Users;
using BlossomAvenue.Core.ValueTypes;

namespace BlossomAvenue.Service.UsersService.Dtos
{
    public class UpdateUserProfileDto
    {
        public Guid UserId { get; set; }
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public ICollection<UpdateUserContactNumber>? UserContactNumbers { get; set; }

        public ICollection<UpdateUserAddress>? UserAddresses { get; set; }

        public User UpdateUser(User user)
        {
            if (FirstName != null) user.FirstName = FirstName;
            if (LastName != null) user.LastName = LastName;
            // updating contact numbers
            if (UserContactNumbers != null)
            {
                // temporary contact number list
                List<UserContactNumber> newContactNumberList = [];
                foreach (var ucn in UserContactNumbers)
                {
                    if (ucn.ContactNumberId == null)
                    {
                        newContactNumberList.Add(new UserContactNumber { ContactNumber = ucn.ContactNumber });
                    }
                    else
                    {
                        var contactNumber = user.UserContactNumbers.FirstOrDefault(cn => cn.ContactNumberId == ucn.ContactNumberId);
                        if (contactNumber != null)
                        {
                            contactNumber.ContactNumber = ucn.ContactNumber;
                            // adding to temporary list
                            newContactNumberList.Add(contactNumber);
                        }
                        else throw new ArgumentException("The contact number id does not match");
                    }
                }
                // updating UserContactNumbers list from temporary list
                user.UserContactNumbers = newContactNumberList;
            }
            // updating user addresses
            if (UserAddresses != null)
            {
                // temporary address list
                List<UserAddress> newUserAddressList = [];
                foreach (var address in UserAddresses)
                {
                    // this address currently does not exist
                    // will be create for the user
                    if (address.UserAddressId == null)
                    {
                        newUserAddressList.Add(new UserAddress
                        {
                            DefaultAddress = address.DefaultAddress,
                            AddressDetail = new AddressDetail
                            {
                                FullName = address.Address.FullName,
                                AddressLine1 = address.Address.AddressLine1,
                                AddressLine2 = address.Address.AddressLine2,
                                PostCode = address.Address.PostCode,
                                City = address.Address.City,
                                Country = address.Address.Country
                            }

                        });
                    }
                    else
                    {
                        // address already exist
                        // updating values
                        var addressToUpdate = user.UserAddresses.FirstOrDefault(u => u.UserAddressId == address.UserAddressId);
                        if (addressToUpdate != null)
                        {
                            addressToUpdate.DefaultAddress = address.DefaultAddress;
                            addressToUpdate.AddressDetail.FullName = address.Address.FullName;
                            addressToUpdate.AddressDetail.AddressLine1 = address.Address.AddressLine1;
                            addressToUpdate.AddressDetail.AddressLine2 = address.Address.AddressLine2;
                            addressToUpdate.AddressDetail.PostCode = address.Address.PostCode;
                            addressToUpdate.AddressDetail.City = address.Address.City;
                            addressToUpdate.AddressDetail.Country = address.Address.Country;
                            // adding to temporary list
                            newUserAddressList.Add(addressToUpdate);
                        }
                    }
                }
                // finally updating actual list from temp list
                user.UserAddresses = newUserAddressList;


            }

            return user;

        }
    }

    public class UpdateUserContactNumber
    {
        public Guid? ContactNumberId { get; set; }
        public string ContactNumber { get; set; }
    }

    public class UpdateUserAddress
    {
        public Guid? UserAddressId { get; set; }

        public bool? DefaultAddress { get; set; }

        public UpdateAddressDetail Address { get; set; }

    }

    public class UpdateAddressDetail
    {
        public Guid? AddressDetailId { get; set; }
        [Required]
        public string FullName { get; set; } = null!;
        [Required]
        public string AddressLine1 { get; set; } = null!;
        public string? AddressLine2 { get; set; }
        [Required, StringLength(5, ErrorMessage = "Invalid formate, a valid postcode has five digits.")]
        public string PostCode { get; set; } = null!;
        [Required]
        public string City { get; set; } = null!;
        [Required]
        public Country Country { get; set; }
    }



}