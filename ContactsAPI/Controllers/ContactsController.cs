using ContactsAPI.Data;
using ContactsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> GetContacts()
        {

            return Ok(await dbContext.Contacts.ToListAsync());
        }

      

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest) 
        {
            var contact = new Contact
            {
                Id = Guid.NewGuid(),
                Name = addContactRequest.Name,
                Email = addContactRequest.Email,
                Phone = addContactRequest.Phone
            };
            await dbContext.Contacts.AddAsync(contact);
             await dbContext.SaveChangesAsync();

           return Ok(contact);


        }


    }
}
