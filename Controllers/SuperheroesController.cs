// Necessary namespaces for the controller to handle HTTP requests, work with models, and manage lists of superheroes
using Microsoft.AspNetCore.Mvc;
using SuperheroesAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace SuperheroesAPI.Controllers
{

    [ApiController] // Marks this class as an API controller and defines the base route for all endpoints in this controller.
    [Route("api/[controller]")]
    // A static list to simulate a database. Contains a list of Superhero objects.
    public class SuperheroesController : ControllerBase
    {
        private static List<Superhero> heroes = new List<Superhero>
        {
            new Superhero { Id = 1, Name = "Spider-Man", Power = "Wall-Crawling", Universe = "Marvel" },
            new Superhero { Id = 2, Name = "Batman", Power = "Super Intelligence", Universe = "DC" },
        };

        // HTTP GET endpoint to retrieve all superheroes in the list.
        // This method returns an Ok result (HTTP 200) with the list of superheroes.
        [HttpGet]
        public ActionResult<IEnumerable<Superhero>> GetAll()
        {
            return Ok(heroes);
        }

        // HTTP GET endpoint to retrieve a superhero by their Id.
        // The {id} in the route is a placeholder for the superhero's Id.
        // If the superhero is found, it returns the superhero with an Ok result; if not, it returns a NotFound result.
        [HttpGet("{id}")]
        public ActionResult<Superhero> GetById(int id)
        {
            var hero = heroes.FirstOrDefault(h => h.Id == id);
            if (hero == null) return NotFound(); // If no hero with the given Id exists, return a 404 Not Found.
            return Ok(hero); // If the hero is found, return a 200 Ok with the hero data.
        }

        // HTTP POST endpoint to add a new superhero to the list.
        // The superhero's Id is auto-incremented by finding the max Id in the current list and adding 1.
        // After adding, it returns a CreatedAtAction response, which includes the route to access the newly created superhero.
        [HttpPost]
        public ActionResult AddHero(Superhero hero)
        {
            hero.Id = heroes.Max(h => h.Id) + 1;
            heroes.Add(hero);
            return CreatedAtAction(nameof(GetById), new { id = hero.Id }, hero);
        }

        // HTTP PUT endpoint to update an existing superhero.
        // It takes an Id to identify the superhero and an updatedHero object with the new data.
        // If the superhero exists, their Name, Power, and Universe are updated. If not, it returns a NotFound result.
        [HttpPut("{id}")]
        public ActionResult UpdateHero(int id, Superhero updatedHero)
        {
            var hero = heroes.FirstOrDefault(h => h.Id == id);
            if (hero == null) return NotFound();

            // Update the hero's properties with the values from updatedHero.
            hero.Name = updatedHero.Name;
            hero.Power = updatedHero.Power;
            hero.Universe = updatedHero.Universe;

            // Return a 204 No Content response to indicate the update was successful, with no content returned.
            return NoContent();
        }

        // HTTP DELETE endpoint to remove a superhero by their Id.
        // If the superhero exists, it is removed from the list. Otherwise, a NotFound result is returned.
        [HttpDelete("{id}")]
        public ActionResult DeleteHero(int id)
        {
            var hero = heroes.FirstOrDefault(h => h.Id == id);
            if (hero == null) return NotFound();

            heroes.Remove(hero); // Remove the hero from the list.
            return NoContent();
        }
    }
}
