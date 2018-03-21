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
                        select place.ID).First();

            return query;
        }

        internal static void AddAnimal(Animal animal)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            db.Animals.InsertOnSubmit(animal);
        }

        internal static void RemoveAnimal(Animal animal)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            db.Animals.DeleteOnSubmit(animal);
        }

        internal static Employee EmployeeLogin(string userName, string password)
        {
            //retrieves and returns matching employee at given userName and password
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            var query = (from employee in db.Employees
                         where employee.userName == userName
                         where employee.pass == password
                         select employee).First();
            return query;
        }

        internal static Employee RetrieveEmployeeUser(string email, int employeeNumber)
        {
            //returns a given user from the DB
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            var query = (from employee in db.Employees
                         where employee.email == email
                         where employee.employeeNumber == employeeNumber
                         select employee).First();
            return query;
        }

        internal static void AddUsernameAndPassword(Employee employee)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            employee.userName = employee.userName;
            employee.pass = employee.pass;
        
        }

        internal static bool CheckEmployeeUserNameExist(string username)
        {
            //needs to search DB for userName match 
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            var query = (from employee in db.Employees
                         where employee.userName == username
                         select employee).First();
            if(query != null)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }



        internal static void EnterUpdate(Animal animal, Dictionary<int, string> updates)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            foreach (int entry in updates.Keys)
            {
                switch(entry)
                {
                    case 1:
                        UpdateCategory(animal, updates[1]);
                        break;
                    case 2:
                        UpdateBreed(animal, updates[2]);
                        break;
                    case 3:
                        UpdateName(animal, updates[3]);
                        break;
                    case 4:
                        UpdateAge(animal, updates[4]);
                        break;
                    case 5:
                        UpdateDemeanor(animal, updates[5]);
                        break;
                    case 6:
                        UpdateKidFriendly(animal, updates[6]);
                        break;
                    case 7:
                        UpdatePetFriendly(animal, updates[7]);
                        break;
                    case 8:
                        UpdateWeight(animal, updates[8]);
                        break;
                }


            }

        }

        internal static void UpdateCategory(Animal animal, string value)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            Breed dbBreed = (from row in db.Breeds
                             where row.ID == animal.ID
                             select row).First();

            int category = 0;
            var isValidCategory = int.TryParse(value, out category);
            if (isValidCategory)
            {
                dbBreed.catagory = category;
                db.SubmitChanges();
            }

        }

        internal static void UpdateBreed(Animal animal, string value)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            Breed dbBreed = (from row in db.Breeds
                             where row.ID == animal.ID
                             select row).First();
            dbBreed.breed1 = value;
            db.SubmitChanges();

        }

        internal static void UpdateName(Animal animal, string value)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            
            Animal dbAnimal = (from row in db.Animals
                               where row.ID == animal.ID
                               select row).First();

            dbAnimal.name = value;
            db.SubmitChanges();
        }

        internal static void UpdateAge(Animal animal, string value)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            Animal dbAnimal = (from row in db.Animals
                               where row.ID == animal.ID
                               select row).First();

            dbAnimal.age = Int32.Parse(value);
            db.SubmitChanges();
        }
        
        internal static void UpdateDemeanor(Animal animal, string value)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            Animal dbAnimal = (from row in db.Animals
                               where row.ID == animal.ID
                               select row).First();

            dbAnimal.demeanor = value;
            db.SubmitChanges();
        }

        internal static void UpdateKidFriendly(Animal animal, string value)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            Animal dbAnimal = (from row in db.Animals
                               where row.ID == animal.ID
                               select row).First();

            dbAnimal.kidFriendly = UserInterface.GetBitData(value);
            db.SubmitChanges();
        }

        internal static void UpdatePetFriendly(Animal animal, string value)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            Animal dbAnimal = (from row in db.Animals
                               where row.ID == animal.ID
                               select row).First();

            dbAnimal.petFriendly = UserInterface.GetBitData(value);
            db.SubmitChanges();
        }

        internal static void UpdateWeight(Animal animal, string value)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();

            Animal dbAnimal = (from row in db.Animals
                               where row.ID == animal.ID
                               select row).First();
            
            dbAnimal.weight = Int32.Parse(value);
            db.SubmitChanges();
        }



        internal static void UpdateShot(string v, Animal animal)
        {
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

