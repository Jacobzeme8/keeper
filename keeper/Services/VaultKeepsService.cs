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

    internal VaultKeep DeleteVaultKeep(int id, Account userInfo)
    {
        VaultKeep vaultKeep = _repo.FindOne(id);
        if(vaultKeep == null) throw new Exception("No Keep in that vault idiot ha");
        if(vaultKeep.creatorId != userInfo.Id)throw new Exception("Not your vault to edit the keeps idiot");
        int rows = _repo.DeleteVaultKeep(id);
        if(rows != 1) throw new Exception($"something went really wrong man {rows} keeps deleted from vault check your db");
        return vaultKeep;
    }

    internal List<KeepInVault> GetKeepsInVault(int vaultId)
    {
        List<KeepInVault> keeps = _repo.GetKeepsInVault(vaultId);
        return keeps;
    }

    }
}