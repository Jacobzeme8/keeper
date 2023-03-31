namespace keeper.Controllers
{
  using Microsoft.AspNetCore.Mvc;

  [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _profilesService;
        private readonly Auth0Provider _auth;
        private readonly KeepsService _keepsService;
        private readonly VaultsService _vaultsService;

    public ProfilesController(ProfilesService profilesService, Auth0Provider auth, KeepsService keepsService, VaultsService vaultsService)
    {
      _profilesService = profilesService;
      _auth = auth;
      _keepsService = keepsService;
      _vaultsService = vaultsService;
    }

    [HttpGet("{id}")]
    public ActionResult<Profile> GetProfileById(string id){
        try 
        {
            Profile profile = _profilesService.GetProfileById(id);
            return profile;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}/keeps")]
    public ActionResult<List<Keep>> GetKeepsByProfile(string id){
        try 
        {
            List<Keep> keeps = _keepsService.GetKeepsByProfile(id);
            return keeps;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}/vaults")]
    public async Task<ActionResult<List<Vault>>> GetVaultsByProfile(string id){
        try 
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            List<Vault> vaults = _vaultsService.GetVaultsByProfile(id, userInfo);
            return vaults;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    

    }
}