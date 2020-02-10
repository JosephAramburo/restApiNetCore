using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class LoginResponseDTO
    {
        public LoginDTO User { get; set; }
        public string Token { get; set; }
    }
}
