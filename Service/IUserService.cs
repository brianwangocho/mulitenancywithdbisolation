using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MultiTenancy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenancy.Service
{
    public interface IUserService
    {
        Task<UserManagerResponse> RegisterUserAsync(RegisterRequest registerRequest);

        Task<UserManagerResponse> CreateRole(string name);

        Task<UserManagerResponse> AssignUserRole(string roleId, string userId);


        Task<LoginResponse> LoginUserAsync(LoginRequest loginRequest);
    }

    public class UserSerivce : IUserService
    {
        public HostDbContext _hostDbContext;
        public MydbContext _mydbContext;
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        public UserSerivce(HostDbContext hostDbContext,
            MydbContext mydbContext,
             RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager)
        {
            _hostDbContext = hostDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _mydbContext = mydbContext;
        }
        public Task<UserManagerResponse> AssignUserRole(string roleId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<UserManagerResponse> CreateRole(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResponse> LoginUserAsync(LoginRequest loginRequest)
        {
            IdentityUser user = await _userManager.FindByEmailAsync(loginRequest.Email);
            throw new NotImplementedException();
        }

        public Task<UserManagerResponse> RegisterUserAsync(RegisterRequest registerRequest)
        {
            throw new NotImplementedException();
        }
    }
}
