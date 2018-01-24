using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fullPlate.Data;
using fullPlate.Data.Models;
using fullPlate.DataContracts.Responses;
using fullPlate.Services.Interfaces;

namespace fullPlate.Services
{
    public class UsersService : IUsersService 
    {
        private readonly FullPlateContext _dbContext;

        public UsersService(FullPlateContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        public List<UserInfoResponse> GetAllUsers()
        {
            List<UserInfoResponse> users = _dbContext.Users
                .Where(x => x.Disabled == false && x.UserName != "admin")
                .Select(x => new UserInfoResponse
                {
                    id = x.Id,
                    email = x.Email,
                    firstName = x.FirstName,
                    lastName = x.FirstName,
                    username = x.UserName
                })
                .ToList();

            return users;
        }
    }
}
