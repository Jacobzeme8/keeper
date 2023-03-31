namespace keeper.Services
{
  public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;

    public VaultKeepsService(VaultKeepsRepository repo)
    {
        _repo = repo;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData, Account userInfo)
    {
        vaultKeepData.creatorId = userInfo.Id;
        VaultKeep vaultKeep = _repo.CreateVaultKeep(vaultKeepData);
        return vaultKeep;
    }

    internal List<KeepInVault> GetKeepsInVault(int vaultId)
    {
        List<KeepInVault> keeps = _repo.GetKeepsInVault(vaultId);
        return keeps;
    }
    }
}