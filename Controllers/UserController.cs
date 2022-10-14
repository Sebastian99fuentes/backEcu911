﻿using MatrizPlanificacion.Modelos;
using MatrizPlanificacion.ResponseModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private DatabaseContext context;
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> userManager;


        public UserController(DatabaseContext context, Microsoft.AspNetCore.Identity.UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<User>>> Get()
        {
            var users = await context.Users.Include(a => a.Area).ToListAsync();
            if (!users.Any())
                return NotFound();
            return users;

        }

        [HttpGet("id")]
        public async Task<ActionResult<ICollection<User>>> GetUser(string id)
        {
            var user = await context.Users.Where(e => e.Id.Equals(id)).Include(a => a.Area).FirstOrDefaultAsync();
            if (user == null)
                return NotFound();
            return Ok(user);
        }


        [HttpDelete("id")]
        public async Task<ActionResult> Delete(string id)
        {
            var existe = await Existe(id);

            if (!existe)
                return NotFound();

            var user = await context.Users.FindAsync(id);
            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> Existe(string id)
        {
            return await context.Users.AnyAsync(p => p.Id == id);
        }
    }
}
