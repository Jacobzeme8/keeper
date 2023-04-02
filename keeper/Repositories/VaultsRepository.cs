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

    internal int DeleteVault(int id)
    {
        string sql = @"
        DELETE FROM vaults
        WHERE id = @id;
        ";

        int rows = _db.Execute(sql, new{id});
        return rows;
    }

    internal int EditVault(Vault original)
    {
        string sql = @"
        UPDATE vaults
        SET
        description = @description,
        name = @name,
        Img = @Img

        WHERE id = @id;
        ";

        int rows = _db.Execute(sql, original);
        return rows;
    }

    internal List<Vault> GetAccountVaults(string id)
    {
        string sql = @"
        SELECT
        *
        FROM vaults
        WHERE
        vaults.creatorId = @id;
        ";

        List<Vault> vaults = _db.Query<Vault>(sql, new{id}).ToList();
        return vaults;

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

    internal List<Vault> GetVaultsByProfile(string creatorId)
    {
        string sql = @"
        SELECT
        *
        FROM vaults
        WHERE creatorId = @creatorId;
        ";

        List<Vault> vaults = _db.Query<Vault>(sql, new{creatorId}).ToList();
        return vaults;
    }
    }
}