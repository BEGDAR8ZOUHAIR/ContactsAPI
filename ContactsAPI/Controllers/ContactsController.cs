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

        [HttpGet]
        [Route("{id:guid}")]

        public async Task<IActionResult> GetContact(Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
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

        [HttpPut]
        [Route ("{id:guid}")]

        public async Task<IActionResult> UpdateContact(Guid id, UpdateContactRequest updateContactRequest)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            contact.Name = updateContactRequest.Name;
            contact.Email = updateContactRequest.Email;
            contact.Phone = updateContactRequest.Phone;
            await dbContext.SaveChangesAsync();
            return Ok(contact);
        }


        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteContact(Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            dbContext.Contacts.Remove(contact);
            await dbContext.SaveChangesAsync();
            return Ok();
        }








    }
}
