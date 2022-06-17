using Microsoft.AspNetCore.Mvc;
using blzrwasm_d.Server.Services;
using blzrwasm_d.Shared;
using System.Net.Mime;

namespace blzrwasm_d.Server.Controllers
{
    // For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
    // https://editor.swagger.io/#/
    // https://code-maze.com/swagger-ui-asp-net-core-web-api/
    // C:\Users\SeanGlover\source\repos\blzrwasm_d\blzrwasm_d\Server\bin\Debug\net6.0\blzrwasm_d.Server.xml

    namespace blzrwasm_d.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]

        public class UsersController : ControllerBase
        {
            private readonly IUserService userService;
            public UsersController(IUserService service) { userService = service; }

            [HttpGet]
            public ActionResult<List<User>> Get() { return userService.Get(); }

            [HttpGet("{id}")]
            public ActionResult<User?> Get(string id)
            {
                User? user = userService.Get(id);
                return user == null ? NotFound($"User with Id = {id} not found") : user;
            }

            [HttpPost]
            public ActionResult<User> Post([FromBody] User User)
            {
                userService.Create(User);
                return CreatedAtAction(nameof(Get), new { id = User.Id }, User);
            }

            [HttpPut("{id}")]
            public ActionResult<User> Put(string id, [FromBody] User user)
            {
                if (id != user.Id)
                    return BadRequest("Employee ID mismatch");

                User? userToUpdate = userService.Get(id);
                if (userToUpdate == null) { return NotFound($"Employee with Id = {id} not found"); }
                userService.Update(id, user);

                return NoContent();
            }

            [HttpDelete("{id}")]
            public ActionResult<User> Delete(string id)
            {
                var existingUser = userService.Get(id);
                if (existingUser == null) { return NotFound($"User with Id = {id} not found"); }
                else { userService.Remove(id); return Ok($"User with Id = {id} deleted"); }
            }
        }

        #region"doesnt work"
        //[Produces(MediaTypeNames.Application.Json)]
        //[Consumes(MediaTypeNames.Application.Json)]
        //public class UsersController : ControllerBase
        //{
        //    private readonly IUserService userService;
        //    public UsersController(IUserService service) { userService = service; }
        //    private static User? GetUser(IUserService service, string id)
        //    {
        //        bool isNumber = int.TryParse(id, out int value);
        //        if (isNumber) { return service.Get(value); }
        //        else { return service.Get(id); }
        //    }

        //    /// <summary>
        //    /// Gets the list of all Users.
        //    /// </summary>
        //    /// <returns>The list of Users.</returns>
        //    // GET: api/User
        //    [HttpGet]
        //    [ProducesResponseType(StatusCodes.Status200OK)]
        //    public ActionResult<List<User>> Get() { return userService.Get(); }

        //    // GET api/values
        //    /// <summary>
        //    /// Returns an User searching by id as an integer or string
        //    /// </summary>
        //    /// <remarks>
        //    /// Sample request:
        //    /// 
        //    ///     GET api/User
        //    ///     int     --> searches the UserId
        //    ///     string  --> searches the Id(MongoDb)
        //    /// </remarks>
        //    /// <param name="id" example="id">The integration property ID. Events related to presentations related to this ID will be returned</param>
        //    /// <returns></returns>
        //    [HttpGet("{id}")]
        //    [ProducesResponseType(StatusCodes.Status200OK)]
        //    [ProducesResponseType(StatusCodes.Status404NotFound)]
        //    public ActionResult<User> Get(string id)
        //    {
        //        User? user = userService.Get(id);
        //        return user == null ? NotFound($"User with Id = {id} not found") : user;
        //    }

        //    /// <summary>
        //    /// Creates an User.
        //    /// </summary>
        //    /// <remarks>
        //    /// Sample request:
        //    /// 
        //    ///     POST api/User
        //    ///     {        
        //    ///       "firstName": "Mike",
        //    ///       "lastName": "Andrew",
        //    ///       "email": "Mike.Andrew@gmail.com"        
        //    ///     }
        //    /// </remarks>
        //    /// <param name="User"></param>
        //    /// <returns>A newly created User</returns>
        //    /// <response code="201">Returns the newly created item</response>
        //    /// <response code="400">If the item is null</response> 
        //    [HttpPost]
        //    public ActionResult<User> Post([FromBody] User User)
        //    {
        //        userService.Create(User);
        //        return CreatedAtAction(nameof(Get), new { id = User.Id }, User);
        //    }

        //    // below Patch method is just an example but doesn't make sense to use since the json field and value must be provided to update
        //    //[ApiExplorerSettings(IgnoreApi = true)]
        //    [HttpPatch("{id}")]
        //    public ActionResult<User> Patch(string id, string fieldName, object fieldValue)
        //    {
        //        User? UserById = GetUser(userService, id);
        //        if (UserById == null) { return NotFound($"User with Id = {id} not found"); }
        //        else
        //        {
        //            //UserService.Update(UserById.Id, fieldName, fieldValue);
        //            return NoContent();
        //        }
        //    }

        //    [HttpPut("{id}")]
        //    public ActionResult<User> Put(string id, [FromBody] User user)
        //    {
        //        if (id != user.Id)
        //            return BadRequest("Employee ID mismatch");

        //        User? userToUpdate = userService.Get(id);
        //        if (userToUpdate == null) { return NotFound($"Employee with Id = {id} not found"); }
        //        userService.Update(id, user);

        //        return NoContent();
        //    }

        //    [HttpDelete("{id}")]
        //    public ActionResult<User> Delete(string id)
        //    {
        //        var existingUser = userService.Get(id);
        //        if (existingUser == null) { return NotFound($"User with Id = {id} not found"); }
        //        else { userService.Remove(id); return Ok($"User with Id = {id} deleted"); }
        //    }
        //}
        #endregion
    }
}
