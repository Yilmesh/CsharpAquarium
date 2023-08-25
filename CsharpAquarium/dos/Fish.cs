using CsharpAquarium.dos;

namespace CsharpAquarium.dos
{
    public class Fish
    {
        // Properties of the Fish class
        public string species;
        public string name;
        public string gender;
        public string diet;
        public int life;
        public int damage;
        public static int turnLimit;

        // Flags and properties for fish behavior
        public bool newFishAdded = false;
        public bool isEaten { get; set; }
        public static bool canReachFood = false;
        public bool canBirth;

        // Constructor for creating a new Fish instance
        public Fish(string species, string name, string gender, string diet)
        {
            newFishAdded = true;
            this.species = species;
            this.name = name;
            this.gender = gender;
            this.diet = diet;
            isEaten = false;
            canReachFood = true;
            life = 10;
            damage = 2;
            turnLimit = 20 + Aqua.turnCount;
            canBirth = false;
        }

        // Method to simulate eating a seaweed
        public void Eat(Seaweed seaweed)
        {
            if (diet == "Lettuce eater")
            {
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.DarkGreen; Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{name} eats a Seaweed.");
                Console.ResetColor();
                Console.WriteLine();
                isEaten = true;
            }
        }

        // Method to check and remove a fish if its turn limit is reached
        public static void TimeLimit(Fish fish, Dictionary<string, Fish> fishes)
        {
            if (turnLimit == Aqua.turnCount)
            {
                fishes.Remove(fish.name);
            }
        }

        // Method to simulate one fish eating another fish
        public void Eat(Fish fish, Fish otherFish)
        {
            if (diet == "My Boy" && otherFish.life > 0)
            {
                otherFish.life -= (damage * 2);
                fish.life += 5;
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.DarkRed; Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{fish.name} bit, dealing {fish.damage * 2} damage(s). {otherFish.name} has {otherFish.life} life remaining.");
                Console.ResetColor();
                Console.WriteLine();
            }
            if (otherFish.life <= 0)
            {
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black; Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{otherFish.name} is dead.");
                Console.ResetColor();
                Console.WriteLine();
            }
            otherFish.isEaten = true;
        }

        // Method to check if a fish's life is 0 or below and remove it
        public void IsLife(Fish fish, Dictionary<string, Fish> fishes)
        {
            if (fish.life <= 0)
            {
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.Black; Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{fish.name} is dead.");
                Console.ResetColor();
                Console.WriteLine();
                fishes.Remove(fish.name);
            }
        }

        // Method to check if two fish can reproduce and set canBirth flag
        public bool Repro(Fish fish, Fish otherFish)
        {
            if (fish.species == otherFish.species && fish.gender != otherFish.gender)
            {
                fish.canBirth = true;
                Console.BackgroundColor = ConsoleColor.Yellow; Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Partner found!");
                Console.ResetColor();
                Console.WriteLine();
            }
            return fish.canBirth; // Return the result of the reproduction check
        }

        // Method to age hermaphroditic fish after a certain turn count
        public void AgeHerma(Fish fish)
        {
            if (Aqua.turnCount > 10)
            {
                if ((fish.name == "Bar" || fish.name == "Mérou") && fish.gender == "Sir")
                {
                    fish.gender = "Lady";
                }
                else if ((fish.name == "Bar" || fish.name == "Mérou") && fish.gender == "Lady")
                {
                    fish.gender = "Sir";
                }
            }
        }

        // Method to change the gender of specific fish based on their names
        public void OppoHerma(Fish fish, Fish otherFish)
        {
            if ((fish.name == "Sole" || fish.name == "Poisson-clown") && fish.gender != otherFish.gender)
            {
                fish.gender = "Lady";
            }
            else if ((fish.name == "Sole" || fish.name == "Poisson-clown") && fish.gender != otherFish.gender)
            {
                fish.gender = "Sir";
            }
        }
    }
}
