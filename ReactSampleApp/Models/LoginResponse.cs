using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactSampleApp.Models
{
    public class LoginResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public long UserId { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }

        public string Token { get; set; }
    }
}
