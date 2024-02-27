namespace main.src.Repositories
{
    public class DbSettings
    {
        public const string SectionName = "DynamoDbTables";
        public string DemoUsersTable { get; set; } = string.Empty;
    }
}
