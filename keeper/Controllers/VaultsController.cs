namespace keeper.Controllers
{
  using Microsoft.AspNetCore.Mvc;

  [ApiController]
    [Route("api/[controller]")]
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vaultsService;
        private readonly Auth0Provider _auth;

    public VaultsController(VaultsService vaultsService, Auth0Provider auth)
    {
      _vaultsService = vaultsService;
      _auth = auth;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault VaultData){
        try 
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            Vault vault = _vaultsService.CreateVault(userInfo, VaultData);
            return vault;
        }
        catch (Exception e)
        {
          return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Vault>> GetVaultById(int id){
        try 
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            Vault vault = _vaultsService.GetVaultById(id, userInfo?.Id);
            return vault;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> EditVault(int id, [FromBody] Vault updata){
        try 
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            Vault vault = _vaultsService.EditVault(id, updata, userInfo);
            return vault;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> DeleteVault(int id){
        try 
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            Vault vault = _vaultsService.DeleteVault(id, userInfo);
            return vault;
        }
        catch (Exception e)
        {
          return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}/keeps")]
    public async Task<ActionResult<List<KeepInVault>>> GetKeepsInVault(int id){
        Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
        List<KeepInVault> keeps = _vaultsService.GetKeepsInVault(id, userInfo?.Id);
        return keeps;
    }



  }
}