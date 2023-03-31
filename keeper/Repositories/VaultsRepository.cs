namespace keeper.Repositories
{
  public class VaultsRepository
    {
        private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Vault CreateVault(Vault vaultData)
    {
        string sql = @"
        INSERT INTO vaults
        (name, description, img, isPrivate, creatorId)
        VALUES
        (@name, @description, @img, @isPrivate, @creatorId);
        SELECT LAST_INSERT_ID()
        ;";

        int id = _db.ExecuteScalar<int>(sql, vaultData);
        vaultData.Id = id;
        return vaultData;
    }

    internal Vault GetVaultById(int id)
    {
        string sql = @"
        SELECT
        vaults.*,
        creator.*
        FROM 
        vaults
        JOIN accounts creator ON vaults.creatorId = creator.id
        WHERE vaults.id = @id;
        ";

        Vault vault = _db.Query<Vault, Account, Vault>(sql, (vault, creator)=>{
            vault.Creator = creator;
            return vault;
        }, new{id}).FirstOrDefault();
        return vault;
    }
    }
}