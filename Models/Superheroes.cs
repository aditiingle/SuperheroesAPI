namespace SuperheroesAPI.Models
   {

        //Class that defines the a model for the superhero entity in the API
        // It contains four properties that represent key attributes of a superhero.
       public class Superhero
       {
           public int Id { get; set; }
           public string Name { get; set; }
           public string Power { get; set; }
           public string Universe { get; set; }
       }
   }
