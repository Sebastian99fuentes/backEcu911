﻿using MatrizPlanificacion.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("api/AlertaDSPPP")]
    public class ProcedimientoContratacionController : ControllerBase
    {

        private readonly DatabaseContext context;

        public ProcedimientoContratacionController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<ProcedimientoContratacion>>> Get()
        {
            var procedimientos = await context.ProcedimientoContrataciones.ToListAsync();
            if (!procedimientos.Any())
                return NotFound();
            return procedimientos;
        }

        [HttpGet("id")]
        public async Task<ActionResult<ICollection<ProcedimientoContratacion>>> GetProcedimiento(Guid id)
        {
            var procedimiento = await context.ProcedimientoContrataciones.Where(e => e.ProcedimientoContratacionId.Equals(id)).FirstOrDefaultAsync();
            if (procedimiento == null)
                return NotFound();
            return Ok(procedimiento);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Post(ProcedimientoContratacion procedimientoContratacion)
        {
            var created = context.ProcedimientoContrataciones.Add(procedimientoContratacion);
            await context.SaveChangesAsync();
            return CreatedAtAction("GetProcedimiento", new { id = procedimientoContratacion.ProcedimientoContratacionId }, created.Entity);

        }

        [HttpPut("id")]
        public async Task<ActionResult> Put(Guid id, ProcedimientoContratacion procedimientoContratacion)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            context.ProcedimientoContrataciones.Update(procedimientoContratacion);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            var procedimiento = await context.ProcedimientoContrataciones.FindAsync(id);
            context.ProcedimientoContrataciones.Remove(procedimiento);
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> Existe(Guid id)
        {
            return await context.ProcedimientoContrataciones.AnyAsync(p => p.ProcedimientoContratacionId == id);
        }
    }
}
