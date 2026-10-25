using SQLite;

namespace RSVPApp.Data
{
    public static class DatabaseService
    {
        private static SQLiteAsyncConnection? _database;

        public static async Task<SQLiteAsyncConnection> GetDatabaseAsync()
        {
            if (_database != null)
            {
                return _database;
            }

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "rsvpapp.db3");
            _database = new SQLiteAsyncConnection(dbPath);

            await InitializeAsync(_database);

            return _database;
        }

        private static async Task InitializeAsync(SQLiteAsyncConnection database)
        {
            await database.CreateTableAsync<Models.User>();
            await database.CreateTableAsync<Models.Event>();
            await database.CreateTableAsync<Models.Rsvp>();
        }
    }
}
