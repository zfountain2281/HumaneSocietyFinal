using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public static class Query
    {
        internal static int? GetBreed()
        {
            HumaneSocietyDataContext db  = new HumaneSocietyDataContext();
            int category = UserInterface.GetIntegerData("species", "the animal's");
            string breed = UserInterface.GetStringData("breed", "the animal's");
            string pattern = UserInterface.GetStringData("pattern", "the animal's");
            //try catch catch needs to add input to the database
            try
            {
                var query = (from breedName in db.Breeds
                             where breedName.breed1 == breed
                             select breedName.ID).First();
                return query;
                
            } 
            catch
            {
                Breed newBreed = new Breed
                {
                    breed1 = breed,
                    catagory = category,
                    pattern = pattern
                };
                db.Breeds.InsertOnSubmit(newBreed);
                return newBreed.ID;
            }
            
        }

        internal static int? GetDiet()
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            string diet = UserInterface.GetStringData("diet", "the animal's");
            int amount = UserInterface.GetIntegerData("amount", "the animal's");

            try
            {
                var query = (from dietPlan in db.DietPlans
                            where dietPlan.food == diet
                            select dietPlan.ID).First();
                return query;

            }

            catch
            {
                DietPlan newDP = new DietPlan
                {
                    food = diet,
                    amount = amount
                };
                return newDP.ID;
            }
        }

        internal static int? GetLocation()
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            string location = UserInterface.GetStringData("location", "the animal's");

            var query = (from place in db.Rooms 
                        where place.name == location
                        select place.ID).First();
            return query;
            //room db
            //name column
            //building column
        }

        internal static void AddAnimal(Animal animal)
        {
            //needs to add the newly user created animal to the DB
            throw new NotImplementedException();
        }
    }
}
