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

    internal Vault GetVaultById(int id, string accountId)
    {
        Vault vault = _repo.GetVaultById(id);
        if(vault.CreatorId != accountId && vault.IsPrivate == true) throw new Exception("That vaults private man stop looking at it");
        return vault;
    }
    }
}