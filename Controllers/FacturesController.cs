using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemDeFacturation_Server.Data;
using SystemDeFacturation_Server.Models;

namespace SystemDeFacturation_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturesController : ControllerBase
    {
        private readonly FacturationContext _context;

        public FacturesController(FacturationContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Facture>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Factures.ToListAsync());
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Facture), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var facture = await _context.Factures
                .SingleOrDefaultAsync(a => a.AcheteurID == id);

            return facture == null
                ? NotFound()
                : Ok(facture);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Acheteur), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] Facture facture)
        {
            await _context.Factures.AddAsync(facture);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = facture.FactureID }, facture);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(Acheteur), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Facture facture)
        {
            var existingFacture = await _context.Factures.FindAsync(id);

            if (existingFacture is null)
                return NotFound();

            existingFacture.NumFacture = facture.NumFacture;
            existingFacture.DateDeEcheance = facture.DateDeEcheance;
            existingFacture.MontantTotal = facture.MontantTotal;
            existingFacture.MontantRestantDue = facture.MontantRestantDue;
            existingFacture.AcheteurID = facture.AcheteurID;

            await _context.SaveChangesAsync();

            return Ok(existingFacture);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove([FromRoute] int id)
        {
            var existingFacture = await _context.Factures.FindAsync(id);

            if (existingFacture is null)
                return NotFound();

            _context.Factures.Remove(existingFacture);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
