namespace keeper.Controllers
{
  using Microsoft.AspNetCore.Mvc;

  [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _profilesService;
        private readonly Auth0Provider _auth;

    public ProfilesController(ProfilesService profilesService, Auth0Provider auth)
    {
        _profilesService = profilesService;
        _auth = auth;
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


    }
}