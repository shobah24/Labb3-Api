using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webb_API.Data;
using webb_API.Models;


namespace webb_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public PersonController(ApiDbContext context)
        {
            _context = context;
        }

        // Hämta alla personer i systemet
        [HttpGet(Name = "GetPerson")]
        public async Task<ActionResult<ICollection<Person>>> GetPerson()
        {
            var people = await _context.People.ToListAsync();
            if (people == null || people.Count == 0)
            {
                return NotFound(new { errorMessage = "Inga personer hittades i databasen!" });
            }
            return Ok(people);
        }

        // Hämta alla intressen kopplade till en specifik person
        [HttpGet("{id}/interests", Name = "GetInterestsByPersonId")]
        public async Task<ActionResult<ICollection<Person>>> GetPersonInterestsById(int id)
        {
            var person = await _context.People
                .Where(p => p.Id == id)
                .Select(p => new
                {
                    p.FirstName,
                    p.LastName,
                    p.Age,
                    Interests = p.PersonInterests.Select(pi => new
                    {
                        pi.Interest.Name,
                        pi.Interest.Description
                    }).ToList()
                }).FirstOrDefaultAsync();
            if (person == null)
            {
                return NotFound(new { errorMessage = $"Person med idt {id} finns inte, försök med annat id!" });
            }
            return Ok(person);
        }

        // Hämta alla länkar kopplade till en specifik person
        [HttpGet("{id}/links", Name = "GetLinksByPersonId")]
        public async Task<ActionResult<ICollection<Link>>> GetPersonLinksById(int id)
        {
            var link = await _context.People
                .Where(p => p.Id == id)
                .Select(p => new
                {
                    p.FirstName,
                    p.LastName,
                    p.Age,
                    Links = p.PersonInterests.SelectMany(pi => pi.Links).Select(l => new
                    {
                        l.Url
                    }).ToList()
                }).FirstOrDefaultAsync();
            if (link == null)
            {
                return NotFound(new { errorMessage = $"Länk med idt {id} finns inte!" });
            }
            return Ok(link);
        }

        //Koppla en person till ett nytt intresse
        [HttpPost("{personId}/interests/{interestId}", Name = "AddPersonToInterest")]
        public async Task<ActionResult> AddPersonToInterest(int personId, int interestId)
        {
            var person = await _context.People.Where(p => p.Id == personId).FirstOrDefaultAsync();
            if (person == null)
            {
                return NotFound(new { errorMessage = $"Person med idt {personId} finns inte!" });
            }

            var interest = await _context.Interests.Where(i => i.Id == interestId).FirstOrDefaultAsync();
            if (interest == null)
            {
                return NotFound(new { errorMessage = $"Intresse med idt {interestId} finns inte!" });
            }

            // Kontrollera om personen redan är kopplad till intresset
            var personInterest = await _context.PersonInterests
                .FirstOrDefaultAsync(pi => pi.PersonId == personId && pi.InterestId == interestId);
            if (personInterest != null)
            {
                return BadRequest(new { errorMessage = $"Person med idt {personId} är redan kopplad till intresse med idt {interestId}!" });
            }

            // Om personen inte är kopplad till intresset, skapa en ny koppling
            var personAndInterest = new PersonInterest
            {
                PersonId = person.Id,
                InterestId = interest.Id
            };

            _context.PersonInterests.Add(personAndInterest);
            await _context.SaveChangesAsync();
            return Ok(new { message = $"Person med idt {personId} är nu kopplad till intresse med idt {interestId}!" });

        }

        //Lägga till nya länkar för en specifik person och ett specifikt intresse
        [HttpPost("{personId}/interests/{interestId}/links", Name = "AddLinkToPersonInterest")]
        public async Task<ActionResult> AddLinkToPersonInterest(int personId, int interestId, [FromBody] Link link)
        {

            var person = await _context.People.FirstOrDefaultAsync(p => p.Id == personId);

            if (person == null)
            {
                return NotFound(new { errorMessage = $"Person med idt {personId} finns inte!" });
            }

            var interest = await _context.Interests.FirstOrDefaultAsync(i => i.Id == interestId);
            if (interest == null)
            {
                return NotFound(new { errorMessage = $"Intresse med idt {interestId} finns inte!" });
            }
            var addLink = new Link
            {
                Url = link.Url,
                PersonId = person.Id,
                InterestId = interest.Id
            };

            _context.Links.Add(addLink);
            await _context.SaveChangesAsync();
            return Ok(addLink);
            //return CreatedAtAction(nameof(GetPersonLinksById), new { id = personId }, link);
        }

    }
}
