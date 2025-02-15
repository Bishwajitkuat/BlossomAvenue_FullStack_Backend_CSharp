﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlossomAvenue.Core.Authentication;
using BlossomAvenue.Core.ValueTypes;

namespace BlossomAvenue.Service.AuthenticationService
{
    public class AuthenticationResultDto
    {
        public LoginResponseDto LoginResponseDto { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}
