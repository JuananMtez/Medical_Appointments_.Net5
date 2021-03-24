using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnet5.Entities;
using dotnet5.Repositories;
using dotnet5.Services;
using dotnet5.DTOs;
using CitasMedicas.DTOs;
using CitasMedicas.Services;

namespace dotnet5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosticosController : ControllerBase
    {

        private readonly IDiagnosticoService diagnosticoService;

        public DiagnosticosController(IDiagnosticoService diagnosticoService)
        {
            this.diagnosticoService = diagnosticoService;
        }

        // GET: api/Diagnosticoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiagnosticoDTOResponse>>> GetDiagnosticos()
        {
            return await diagnosticoService.GetAll();
        }

        // GET: api/Diagnosticoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DiagnosticoDTOResponse>> GetDiagnostico(int id)
        {
            var diagnostico = await diagnosticoService.Get(id);
            if (diagnostico == null)
            {
                return NotFound();
            }
            return diagnostico;
        }

        // PUT: api/Diagnosticoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiagnostico(int id, DiagnosticoDTOPut diagnosticoDTO)
        {

            switch (await diagnosticoService.Put(id, diagnosticoDTO))
            {
                case 0:
                    return BadRequest();

                case 1:
                    return NotFound();

                case 2: return NoContent();

            }
            return null;
        }

        // POST: api/Diagnosticoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DiagnosticoDTOResponse>> PostDiagnostico(DiagnosticoDTOPost diagnostico)
        {

            DiagnosticoDTOResponse response = await diagnosticoService.Post(diagnostico);
            return CreatedAtAction("GetDiagnostico", new { id = response.DiagnosticoId }, response);
        }

        // DELETE: api/Diagnosticoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiagnostico(int id)
        {
            if (await diagnosticoService.Delete(id))
            {
                return NoContent();
            }

            return NotFound();
        }

    }
}
