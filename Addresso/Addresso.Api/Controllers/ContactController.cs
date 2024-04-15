using Addresso.Api.Filters.Contacts;
using Addresso.Api.Repositories;
using Addresso.UI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Addresso.Api.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class ContactController(IContactRepository repository) : ControllerBase
{
    [HttpPost]
    [TypeFilter(typeof(Contact_ValidateContactToCreateFilterAttribute))]
    public IActionResult Add([FromBody] Contact contact)
    {
        repository.Add(contact);

        return CreatedAtAction(
            nameof(GetById),
            new { id = contact.Id },
            contact
        );
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        List<Contact> contacts = repository.Get();

        return Ok(contacts);
    }

    [HttpGet("{id}")]
    [TypeFilter(typeof(Contact_ValidateContactIdFilterAttribute))]
    public IActionResult GetById(int id)
    {
        Contact contact = repository.GetById(id);

        return Ok(contact);
    }
    
    [HttpPut]
    [TypeFilter(typeof(Contact_ValidateContactToUpdateFilterAttribute))]
    public IActionResult Update([FromBody] Contact contact)
    {
        repository.Update(contact);
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    [TypeFilter(typeof(Contact_ValidateContactIdFilterAttribute))]
    public IActionResult Delete(int id)
    {
        Contact removedContact = repository.Delete(id);

        return Ok(removedContact);
    }
}