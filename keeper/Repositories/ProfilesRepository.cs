namespace keeper.Repositories
{
  public class ProfilesRepository
    {
        private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Profile GetProfileById(string id)
    {
        string sql = @"
        SELECT
        *
        FROM accounts
        WHERE id = @id;
        ";

        Profile profile = _db.Query<Profile>(sql, new{id}).FirstOrDefault();
        return profile;
    }
    }
}