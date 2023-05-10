using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
namespace CRUD_Entity_.NET_6._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heroes = new List<SuperHero>
        {
             new SuperHero
                {
                    Id = 1,
                    Name = "Spider-Man",
                    FirstName = "Peter",
                    LastName = "Parker",
                    Place = "New York City"
                },
             new SuperHero
                {
                    Id = 2,
                    Name = "Visão",
                    FirstName = "Jarvis",
                    LastName = "CPU",
                    Place = "Internet"
                }
        };


        //public SuperHero FindHero(int id)
        //{
        //    SuperHero hero = heroes.Find(h => h.Id == id);

        //    return hero;
        //}
        private readonly DataContext _context;
        public SuperHeroController(DataContext context)
        {
            _context = context;
        }
        
        [HttpGet]

        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
        } 

        [HttpGet("{id}")]

        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            //SuperHero hero = FindHero(id);
            var hero = await _context.SuperHeroes.FindAsync(id);

            if (hero == null)
            {
                return BadRequest("Hero not found");
            }
            else
            {
                return Ok(hero);
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            
            return Ok(await _context.SuperHeroes.ToListAsync()); 
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> EditHero(SuperHero request)
        {


            //SuperHero hero = FindHero(request.Id);
            var dbHero = await _context.SuperHeroes.FindAsync(request.Id);

            if (dbHero == null)
            {
                return BadRequest("Hero not found");
            }
            else
            {
                dbHero.Name = request.Name;
                dbHero.FirstName = request.FirstName;
                dbHero.LastName = request.LastName;
                dbHero.Place = request.Place;

                await _context.SaveChangesAsync();

                return Ok(await _context.SuperHeroes.ToListAsync());
            }
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            
            var hero = await _context.SuperHeroes.FindAsync(id);


            if (hero == null)
                return BadRequest("Hero not found");

            _context.SuperHeroes.Remove(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }
}
