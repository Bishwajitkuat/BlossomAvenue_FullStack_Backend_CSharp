using System;
using System.Collections.Generic;
using System.Linq;
using BlossomAvenue.Core.Users;

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
                    if (address.AddressId == null)
                    {
                        newUserAddressList.Add(new UserAddress
                        {
                            DefaultAddress = address.DefaultAddress,
                            Address = new AddressDetail
                            {
                                AddressLine1 = address.Address.AddressLine1,
                                AddressLine2 = address.Address.AddressLine2,
                                CityId = address.Address.CityId,

                            }

                        });
                    }
                    else
                    {
                        var addressToUpdate = user.UserAddresses.FirstOrDefault(u => u.AddressId == address.AddressId);
                        if (addressToUpdate != null)
                        {
                            addressToUpdate.DefaultAddress = address.DefaultAddress;
                            addressToUpdate.Address.AddressLine1 = address.Address.AddressLine1;
                            addressToUpdate.Address.AddressLine2 = address.Address.AddressLine2;
                            addressToUpdate.Address.CityId = address.Address.CityId;
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
        public Guid? AddressId { get; set; }

        public bool? DefaultAddress { get; set; }

        public UpdateAddressDetail Address { get; set; }

    }

    public class UpdateAddressDetail
    {
        public string AddressLine1 { get; set; }

        public string? AddressLine2 { get; set; }

        public Guid CityId { get; set; }
    }



}