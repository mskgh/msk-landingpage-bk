using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using AutoMapper;
using main.src.Entities;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text.Json;

namespace main.src.Repositories.DynamoUserDB
{
    public class DynamoDBUserRepository : IDynamoDBUserRepository
    {
        private readonly IAmazonDynamoDB dynamoDB;
        private readonly IOptions<DbSettings> dbSettings;
        IMapper mapper;
        public DynamoDBUserRepository(IAmazonDynamoDB dynamoDB ,IOptions<DbSettings> dbSettings,IMapper mapper)
        {
            this.dbSettings = dbSettings;
            this.dynamoDB = dynamoDB;
            this.mapper = mapper;
        }
        public async Task<bool> AddUser(User user)
        {
            var userJsonString = JsonSerializer.Serialize(user);
            var userDocument = Document.FromJson(userJsonString);
            var userAttribute = userDocument.ToAttributeMap();

            var putRequest = new PutItemRequest()
            {
                TableName = dbSettings.Value.DemoUsersTable,
                Item = userAttribute
            };

            var response = await dynamoDB.PutItemAsync(putRequest);
            if(response.HttpStatusCode == HttpStatusCode.OK)
            {
                return true;
            }

            return false;

        }

        public async Task<bool> CheckIfUserExists(Guid id, Guid ternantId)
        {
            var request = new GetItemRequest()
            {
                TableName = dbSettings.Value.DemoUsersTable,
                Key = new Dictionary<string, AttributeValue>()
                {
                    {"Id", new AttributeValue(id.ToString())},
                    {"TernantId", new AttributeValue(ternantId.ToString())},
                }
            };

            var response = await dynamoDB.GetItemAsync(request);

            if (response.HttpStatusCode != HttpStatusCode.OK)
                return false;

            if (response.Item.Count <= 0)
                return false;

            if (response.Item.Count > 1)
                return true;
            return true;


        }

        public async Task<bool> DeleteUser(Guid id,Guid ternantId)
        {
            var deleRequest = new DeleteItemRequest()
            {
                TableName = dbSettings.Value.DemoUsersTable,
                Key = new Dictionary<string, AttributeValue>()
                {
                    {"Id", new AttributeValue(id.ToString())},
                    {"TernantId", new AttributeValue(ternantId.ToString())},
                }
            };

            var response = await dynamoDB.DeleteItemAsync(deleRequest);

            return response.HttpStatusCode == HttpStatusCode.OK;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = new List<User>();
            var request = new ScanRequest
            {
                TableName = dbSettings.Value.DemoUsersTable,
                
            };
            var response = await dynamoDB.ScanAsync(request);

            if (response != null && response.Items != null) 
            {
                foreach (var item in response.Items) 
                {
                    item.TryGetValue("Id", out var Id);
                    item.TryGetValue("TernantId",out var TernantId);
                    item.TryGetValue("FirstName", out var FirstName);
                    item.TryGetValue("LastName", out var LastName);
                    item.TryGetValue("OtherNames", out var OtherNames);
                    item.TryGetValue("Email",out var Email);
                    item.TryGetValue("MobileNumber",out var MobileNumber);

                    users.Add(new User { 
                    
                        Id = Guid.Parse(Id.S),
                        TernantId = Guid.Parse(TernantId.S),
                        FirstName = FirstName.S,
                        LastName = LastName.S,
                        OtherNames = OtherNames.S,
                        Email = Email.S,
                        MobileNumber = MobileNumber.S,
                    }) ;
                }
            }
                 
            return users;
        }

        public async Task<Entities.User> GetUserById(Guid id, Guid ternantId)
        {
            var request = new GetItemRequest()
            {
                TableName = dbSettings.Value.DemoUsersTable,
                Key = new Dictionary<string, AttributeValue>() 
                {
                    {"Id", new AttributeValue(id.ToString())},
                    {"TernantId", new AttributeValue(ternantId.ToString())},
                }
            };

            var response = await dynamoDB.GetItemAsync(request);

            if(response.HttpStatusCode != HttpStatusCode.OK)
                return null;

            if (response.Item.Count <= 0)
                return null;

            var userDocument = Document.FromAttributeMap(response.Item);
            var user = JsonSerializer.Deserialize<Entities.User>(userDocument.ToJson());
            return user;

        }

        public async Task<bool> UpdateUser(Guid id, Guid ternantId,Dtos.UpdateUserDto updateUserDto)
        {
            var myUser = mapper.Map<Entities.UserWithoutPassword>(updateUserDto);
            myUser.Id = id;
            myUser.TernantId = ternantId;
            var userJsonString = JsonSerializer.Serialize(myUser);
            var userDocument = Document.FromJson(userJsonString);
            var userAttribute = userDocument.ToAttributeMap();

            var putRequest = new PutItemRequest()
            {
                TableName = dbSettings.Value.DemoUsersTable,
                Item = userAttribute
            };

            var response = await dynamoDB.PutItemAsync(putRequest);
            return response.HttpStatusCode == HttpStatusCode.OK;
        }
    }
}
