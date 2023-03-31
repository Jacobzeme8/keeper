namespace keeper.Controllers
{
  using Microsoft.AspNetCore.Mvc;

  [ApiController]
    [Route("api/[controller]")]
    public class KeepsController : ControllerBase
    {
        private readonly KeepsService _keepsService;
        private readonly Auth0Provider _auth;

    public KeepsController(KeepsService keepsService, Auth0Provider auth)
    {
      _keepsService = keepsService;
      _auth = auth;
    }

    [HttpPost]
    [Authorize]

    public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep keepData){
        try 
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            Keep keep = _keepsService.CreateKeep(keepData, userInfo);
            return keep;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public ActionResult<List<Keep>> GetAllKeeps(){
        try 
        {
            List<Keep> keeps = _keepsService.GetAllKeeps();
            return keeps;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}")]
    
    public ActionResult<Keep> GetOneKeep(int id){
        try 
        {
            Keep keep = _keepsService.GetOneKeep(id);
            return keep;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Keep>> EditKeep(int id, [FromBody] Keep updata){
        try 
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            Keep keep = _keepsService.EditKeep(id, updata, userInfo);
            return keep;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<String>> DeleteKeep(int id){
        try 
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            string message = _keepsService.DeleteKeep(id, userInfo);
            return message;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    }
}