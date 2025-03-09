using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemDeFacturation_Server.Data;
using SystemDeFacturation_Server.Models;

namespace SystemDeFacturation_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcheteursController : ControllerBase
    {
        private readonly FacturationContext _context;

        public AcheteursController(FacturationContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Acheteur>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Acheteurs
                .ToListAsync());
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Acheteur), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var acheteur = await _context.Acheteurs
                .SingleOrDefaultAsync(a => a.AcheteurID == id);

            return acheteur == null
                ? NotFound()
                : Ok(acheteur);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Acheteur), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] Acheteur acheteur)
        {
            await _context.Acheteurs.AddAsync(acheteur);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = acheteur.AcheteurID }, acheteur);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(Acheteur), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Acheteur acheteur)
        {
            var existingAcheteur = await _context.Acheteurs.FindAsync(id);

            if (existingAcheteur is null)
                return NotFound();

            existingAcheteur.Nom = acheteur.Nom;
            existingAcheteur.Prenom = acheteur.Prenom;
            existingAcheteur.Adresse = acheteur.Adresse;
            existingAcheteur.Telephone = acheteur.Telephone;
            existingAcheteur.Email = acheteur.Email;

            await _context.SaveChangesAsync();

            return Ok(existingAcheteur);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove([FromRoute] int id)
        {
            var existingAcheteur = await _context.Acheteurs.FindAsync(id);

            if (existingAcheteur is null)
                return NotFound();

            _context.Acheteurs.Remove(existingAcheteur);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
