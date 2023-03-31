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



  }
}