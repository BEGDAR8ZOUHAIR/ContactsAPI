using ContactsAPI.Data;
using Microsoft.AspNetCore.Mvc;


namespace ContactsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class ContactsController : Controller
    {
        private readonly ContactAPIDbContext dbContext;

        public ContactsController(ContactAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult GetContacts()
        {

            return Ok(dbContext.Contacts.ToList());
        }
    }
}
