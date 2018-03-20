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
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
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
                         //orderby breedName
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


        public static Client GetClient(string userName, string password)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var client = (from user in context.Clients where user.userName == userName && user.pass == password select user).ToList();
            return (Client)client[0];

        }

        public static void AddNewClient(string firstName, string lastName, string username, string password, string email, string streetAddress, int zipCode, int stateNumber)
        {
            Client client = new Client();
            int address = GetClientAddressKey(streetAddress, zipCode, stateNumber);
            client.email = email;
            client.firstName = firstName;
            client.lastName = lastName;
            client.userName = username;
            client.pass = password;
            client.userAddress = address;
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            context.Clients.InsertOnSubmit(client);
            context.SubmitChanges();
        }

        public static int GetClientAddressKey(string streetAddress, int zipCode, object stateNumber)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            int addressNumber;
            var addressObject = from address in context.UserAddresses where address.addessLine1 == streetAddress && address.zipcode == zipCode && address.usState == stateNumber select address.ID;
            if (addressObject.ToList().Count > 0)
            {
                addressNumber = addressObject.ToList()[0];
            }
            else
            {
                UserAddress address = new UserAddress();
                address.zipcode = zipCode;
                address.addessLine1 = streetAddress;
                address.usState = stateNumber;
                context.UserAddresses.InsertOnSubmit(address);
                context.SubmitChanges();
                var addressKey = from location in context.UserAddresses where location.addessLine1 == streetAddress && location.zipcode == zipCode && location.usState == stateNumber select address.ID;
                addressNumber = addressKey.ToList()[0];
            }
            return addressNumber;
        }

        public static void UpdateFirstName(Client client)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var clientData = from entry in context.Clients where entry.ID == client.ID select entry;
            clientData.First().firstName = client.firstName;
            context.SubmitChanges();
        }
        public static void UpdateLastName(Client client)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var clientData = from entry in context.Clients where entry.ID == client.ID select entry;
            clientData.First().lastName = client.lastName;
            context.SubmitChanges();
        }
        public static void UpdateAddress(Client client)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var clientData = from entry in context.Clients where entry.ID == client.ID select entry;
            clientData.First().UserAddress1.zipcode = client.UserAddress1.zipcode;
            clientData.First().UserAddress1.addessLine1 = client.UserAddress1.addessLine1;
            clientData.First().UserAddress1.usState = client.UserAddress1.usState;
            context.SubmitChanges();
        }
        public static void UpdateEmail(Client client)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var clientData = from entry in context.Clients where entry.ID == client.ID select entry;
            clientData.First().email = client.email;
            context.SubmitChanges();
        }
        public static void UpdateUsername(Client client)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var clientData = from entry in context.Clients where entry.ID == client.ID select entry;
            clientData.First().userName = client.userName;
            context.SubmitChanges();
        }
        public static IQueryable<Client> RetrieveClients()
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var clients = from r in context.Clients select r;
            return clients;
        }
        public static IQueryable<USState> GetStates()
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var states = from r in context.USStates select r;
            return states;
        }

        public static void updateClient(Client client)
        {

        }
        internal static IQueryable<ClientAnimalJunction> GetUserAdoptionStatus(Client client)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var adoptions = from data in context.ClientAnimalJunctions where data.client == client.ID select data;
            return adoptions;
        }
        internal static IQueryable<ClientAnimalJunction> GetPendingAdoptions()
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var animals = from data in context.ClientAnimalJunctions where data.approvalStatus == "pending" select data;
            return animals;
        }
        internal static void UpdateAdoption(bool isApproved, ClientAnimalJunction junction)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var currentJunction = (from data in context.ClientAnimalJunctions where data.animal == junction.animal && data.client == junction.client select data).First();
            var currentAnimal = (from data in context.Animals where data.ID == junction.animal select data).First();
            if (isApproved)
            {
                currentJunction.approvalStatus = "approved";
                currentAnimal.adoptionStatus = "adopted";
                UserInterface.DisplayUserOptions("transferring adoption fee from adopter");
            }
            else
            {
                currentJunction.approvalStatus = "denied";
            }
            context.SubmitChanges();
        }
        public static IQueryable<AnimalShotJunction> GetShots(Animal animal)
        {
            HumaneSocietyDataContext context = new HumaneSocietyDataContext();
            var shots = (from data in context.AnimalShotJunctions where data.Animal_ID == animal.ID select data);
            return shots;
        }

    }
}
