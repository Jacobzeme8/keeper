namespace keeper.Repositories
{
  public class VaultKeepsRepository
    {
        private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
        string sql = @"
        INSERT INTO vaultkeeps
        (creatorId, vaultId, keepId)
        VALUES
        (@creatorId, @vaultId, @keepId);
        SELECT LAST_INSERT_ID();
        ";

        int id = _db.ExecuteScalar<int>(sql, vaultKeepData);
        vaultKeepData.Id = id;
        return vaultKeepData;


    }

    internal int DeleteVaultKeep(int id)
    {
        string sql = @"
        DELETE FROM vaultkeeps
        WHERE id = @id;
        ";

        int rows = _db.Execute(sql, new{id});
        return rows;
    }

    internal VaultKeep FindOne(int id)
    {
        string sql = @"
        SELECT * FROM vaultkeeps
        WHERE id = @id;
        ";

        VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new{id}).FirstOrDefault();
        return vaultKeep;
    }

    
    }
}