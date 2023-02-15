﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.Application.Users.Dto;
using Nucleus.Core.Roles;
using Nucleus.Core.Users;
using Nucleus.EntityFramework;
using Nucleus.Utilities.Collections;
using Nucleus.Utilities.Extensions.PrimitiveTypes;
using Xunit;

namespace Nucleus.Tests.Web.Api.Controllers
{
    public class UsersControllerTests : ApiTestBase
    {
        private readonly string _token;

        public UsersControllerTests()
        {
            _token = LoginAsAdminUserAndGetTokenAsync().Result;
        }

        [Fact]
        public async Task Should_Get_Users()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/users");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var responseGetUsers = await TestServer.CreateClient().SendAsync(requestMessage);
            Assert.Equal(HttpStatusCode.OK, responseGetUsers.StatusCode);

            var users = await responseGetUsers.Content.ReadAsAsync<PagedList<UserListOutput>>();
            Assert.True(users.Items.Any());
        }

        [Fact]
        public async Task Should_Get_User_For_Create()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/users/" + Guid.Empty);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var responseGetUsers = await TestServer.CreateClient().SendAsync(requestMessage);
            Assert.Equal(HttpStatusCode.OK, responseGetUsers.StatusCode);

            var user = await responseGetUsers.Content.ReadAsAsync<GetUserForCreateOrUpdateOutput>();
            Assert.True(string.IsNullOrEmpty(user.User.UserName));
        }

        [Fact]
        public async Task Should_Get_User_For_Update()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/users/" + DefaultUsers.Member.Id);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var responseGetUsers = await TestServer.CreateClient().SendAsync(requestMessage);
            Assert.Equal(HttpStatusCode.OK, responseGetUsers.StatusCode);

            var user = await responseGetUsers.Content.ReadAsAsync<GetUserForCreateOrUpdateOutput>();
            Assert.False(string.IsNullOrEmpty(user.User.UserName));
        }

        [Fact]
        public async Task Should_Create_User()
        {
            var input = new CreateOrUpdateUserInput
            {
                User = new UserDto
                {
                    UserName  = "TestUserName_" + Guid.NewGuid(),
                    Email  = "TestUserEmail_" + Guid.NewGuid(),
                    Password = "aA!121212"
                },
                GrantedRoleIds = new List<Guid> { DefaultRoles.Member.Id }
            };
            
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/users");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            requestMessage.Content = input.ToStringContent(Encoding.UTF8, "application/json");
            var responseAddUser = await TestServer.CreateClient().SendAsync(requestMessage);
            Assert.Equal(HttpStatusCode.Created, responseAddUser.StatusCode);

            var insertedUser = await DbContext.Users.FirstAsync(u => u.UserName == input.User.UserName);
            Assert.NotNull(insertedUser);
        }

        [Fact]
        public async Task Should_Update_User()
        {
            var testUser = await CreateAndGetTestUserAsync();

            var input = new CreateOrUpdateUserInput
            {
                User = new UserDto
                {
                    Id = testUser.Id,
                    UserName = "TestUserName_Edited_" + Guid.NewGuid(),
                    Email = testUser.Email
                },
                GrantedRoleIds = new List<Guid> { DefaultRoles.Member.Id }
            };
            
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "/api/users");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            requestMessage.Content = input.ToStringContent(Encoding.UTF8, "application/json");
            var responseAddUser = await TestServer.CreateClient().SendAsync(requestMessage);
            Assert.Equal(HttpStatusCode.OK, responseAddUser.StatusCode);

            var dbContextFromAnotherScope = GetNewScopeServiceProvider().GetService<NucleusDbContext>(); 
            var editedTestUser = await dbContextFromAnotherScope.Users.FindAsync(testUser.Id);
            Assert.Contains(editedTestUser.UserRoles, ur => ur.RoleId == DefaultRoles.Member.Id);
        }

        [Fact]
        public async Task Should_Delete_User()
        {
            var testUser = await CreateAndGetTestUserAsync();
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/api/users?id=" + testUser.Id);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var responseAddUser = await TestServer.CreateClient().SendAsync(requestMessage);
            Assert.Equal(HttpStatusCode.NoContent, responseAddUser.StatusCode);
        }
    }
}
