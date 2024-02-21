using FakeItEasy;
using main.src.Repositories.MyInMemoryDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace main.Test.Helpers
{
    public class InMemoryDBHelper
    {
        public MyInMemoryDB CreateUsersAutoMatically(int numberOfUsers)
        {
            MyInMemoryDB db = new MyInMemoryDB();

            for (int i = 0; i < numberOfUsers; i++) 
            {
                var user = A.Fake<src.Entities.User>();
                db.AddUser(user);
            }
            return db;
        }

        public MyInMemoryDB CreateUsersManually()
        {
            MyInMemoryDB db = new MyInMemoryDB();
            
            var user1 = new src.Entities.User();
            user1.FirstName = "Test";
            user1.OtherNames = "Test";
            user1.LastName = "Test";
            user1.Email = "Test@e.com";
            user1.Password = "password";
            user1.MobileNumber = "1234567890";
            db.AddUser(user1);

            var user2 = new src.Entities.User();
            user2.FirstName = "Test2";
            user2.OtherNames = "Test2";
            user2.LastName = "Test2";
            user2.Email = "Test@e.com2";
            user2.Password = "password2";
            user2.MobileNumber = "12345678902";
            user2.Id = Guid.NewGuid();

            db.AddUser(user2);
            
            return db;
        }

    }
}
