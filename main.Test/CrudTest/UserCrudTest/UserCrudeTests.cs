﻿using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using main.src.Controllers;
using main.src.Repositories;
using main.src.Repositories.MyInMemoryDB;
using main.src.Services.User;
using main.Test.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace main.Test.CrudTest.UserCrudTest
{
    public class UserCrudeTests
    {
      
        [Fact]
        public void Create_User() 
        {

            InMemoryDBHelper db = new ();
        
            //Act
            var result = db.CreateUsersAutoMatically(1).GetUsers();

            //Assert 
            
            result.Should().HaveCount(1);
            
        }

        [Fact]
        public void Read_User()
        {
            InMemoryDBHelper db = new();

            var result = db.CreateUsersManually().GetUsers();

            result.Should().HaveCount(2);

            result.Should().Contain(user => user.FirstName == "Test2");
            result.Should().Contain(user => user.LastName == "Test2");
            result.Should().Contain(user => user.Email == "Test@e.com2");

        }


        [Fact]
        public void Update_User()
        {
            MyInMemoryDB db = new MyInMemoryDB();
            //Arrange
            var user1 = new src.Entities.User();
            user1.FirstName = "Test";
            user1.OtherNames = "Test";
            user1.LastName = "Test";
            user1.Email = "Test@e.com";
            user1.Password = "password";
            user1.MobileNumber = "1234567890";
            var Id = new Guid();
            user1.Id = Id;
            //Act
            db.AddUser(user1);
            

            //Assert 
            var result = db.GetUsers();
            result.Should().HaveCount(1);
            result.Should().Contain(user=> user.Email == "Test@e.com");

            var user2 = new src.Dtos.WriteUserDto();
            user2.FirstName = "Test";
            user2.OtherNames = "Test";
            user2.LastName = "Test";
            user2.Email = "Test@e.co";
            user2.Password = "password";
            user2.MobileNumber = "1234567890";

            db.UpdateUser(Id, user2);
            var result2 = db.GetUsers();
            result2.Should().HaveCount(1);
            result2.Should().NotContain(user => user.Email == "Test@e.com");
            result2.Should().Contain(user => user.Email == "Test@e.co");

        }
        [Fact]
        public void Delete_User()
        {
            MyInMemoryDB db = new MyInMemoryDB();
            //Arrange
            var user1 = new src.Entities.User();
            user1.FirstName = "Test";
            user1.OtherNames = "Test";
            user1.LastName = "Test";
            user1.Email = "Test@e.com";
            user1.Password = "password";
            user1.MobileNumber = "1234567890";
            var Id = new Guid();
            user1.Id = Id;
            
            db.AddUser(user1);
 
            var result = db.GetUsers();
            result.Should().HaveCount(1);
            result.Should().Contain(user1);

            db.DeleteUser(Id);
            var result2 = db.GetUsers();
            result2.Should().HaveCount(0);
            result2.Should().NotContain(user1);

        }
    }
}
