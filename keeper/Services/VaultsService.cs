namespace keeper.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _repo;

    public VaultsService(VaultsRepository repo)
    {
        _repo = repo;
    }

    internal Vault CreateVault(Account userInfo, Vault vaultData)
    {
        vaultData.CreatorId = userInfo.Id;
        vaultData.Creator = userInfo;
        Vault vault = _repo.CreateVault(vaultData);
        return vault;
    }

    internal Vault EditVault(int id, Vault updata, Account userInfo)
    {
        Vault original = this.GetVaultById(id, userInfo.Id);
        if(original.CreatorId != userInfo.Id) throw new Exception("Not your vault to edit man find your own");
        original.description = updata.description != null ? updata.description : original.description;
        original.name = updata.name != null ? updata.name : original.name;
        original.Img = updata.Img != null ? updata.Img : original.Img;
        original.IsPrivate = updata.IsPrivate != null ? updata.IsPrivate : original.IsPrivate;
        int rows = _repo.EditVault(original);
        if(rows != 1) throw new Exception($"somethings wacky man {rows} vaults were eddited check your db");
        return original;
    }

    internal Vault GetVaultById(int id, string accountId)
    {
        Vault vault = _repo.GetVaultById(id);
        if(vault.CreatorId != accountId && vault.IsPrivate == true) throw new Exception("That vaults private man stop looking at it");
        if(vault == null) throw new Exception("No vault there man figure it out");
        return vault;
    }
    }
}