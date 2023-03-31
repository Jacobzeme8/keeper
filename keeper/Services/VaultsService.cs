namespace keeper.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _repo;
        private readonly VaultKeepsService _vaultKeepsService;

    public VaultsService(VaultsRepository repo, VaultKeepsService vaultKeepsService)
    {
      _repo = repo;
      _vaultKeepsService = vaultKeepsService;
    }

    internal Vault CreateVault(Account userInfo, Vault vaultData)
    {
        vaultData.CreatorId = userInfo.Id;
        vaultData.Creator = userInfo;
        Vault vault = _repo.CreateVault(vaultData);
        return vault;
    }

    internal Vault DeleteVault(int id, Account userInfo)
    {
        Vault vault = this.GetVaultById(id, userInfo.Id);
        if(vault.CreatorId != userInfo.Id) throw new Exception("Not your vault to delete man, how did you even get here? nerd");
        int rows = _repo.DeleteVault(id);
        if(rows != 1) throw new Exception($"something went wrong man {rows} vaults were deleted check your db");
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

    internal List<KeepInVault> GetKeepsInVault(int id, string userId)
    {
        Vault vault = this.GetVaultById(id, userId);
        List<KeepInVault> keeps = _vaultKeepsService.GetKeepsInVault(vault.Id);
        return keeps;

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