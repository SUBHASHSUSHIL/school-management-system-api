using SMS.API.Data;
using SMS.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AuthService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
    }
}
