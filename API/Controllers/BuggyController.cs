namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        [HttpGet("not-found")]
        public ActionResult GetNotFound()
        {
            return NotFound();
        }

        [HttpGet("bad-request")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ProblemDetails{Title="This is a bad request"});
        }

        [HttpGet("unauthorised")]
        public ActionResult GetUnauthorised()
        {
            return Unauthorized();
        }

        [HttpGet("validation-error")]
        public ActionResult GetValidationError()
        {
            ModelState.AddModelError("Propblem1", "This is the first error");
            ModelState.AddModelError("Propblem2", "This is the second error");
            return ValidationProblem();
        }
        
        [HttpGet("server-error")]
        public ActionResult GetServerError()
        {
            throw new Exception("This is server error");
        }
    }
}