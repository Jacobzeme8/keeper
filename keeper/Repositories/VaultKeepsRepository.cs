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

    internal List<KeepInVault> GetKeepsInVault(int vaultId)
    {
        string sql = @"
        SELECT
        vaultkeeps.*,
        keeps.*,
        accounts.*
        FROM
        vaultkeeps
        JOIN keeps ON vaultkeeps.keepId = keeps.id
        JOIN accounts ON keeps.creatorId = accounts.id
        WHERE vaultkeeps.vaultId = @vaultId;
        ";

        List<KeepInVault> keeps = _db.Query<VaultKeep, KeepInVault, Account, KeepInVault>(sql, (vaultKeep, keepInVault, account) => {
            keepInVault.vaultKeepId = vaultKeep.Id;
            keepInVault.Creator = account;
            return keepInVault;
        }, new{vaultId}).ToList();
        return keeps;


    }
    }
}