namespace keeper.Services
{
  public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;
        private readonly VaultsService _vaultsService;

    public VaultKeepsService(VaultKeepsRepository repo, VaultsService vaultsService)
    {
      _repo = repo;
      _vaultsService = vaultsService;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData, Account userInfo)
    {
        vaultKeepData.creatorId = userInfo.Id;
        Vault vault = _vaultsService.GetVaultById(vaultKeepData.vaultId, vaultKeepData.creatorId);
        if(vault.CreatorId != userInfo.Id) throw new Exception("Not your vault to edit");
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


    }
}