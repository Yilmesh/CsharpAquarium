using System;
using System.Collections.Generic;
using System.Linq;

namespace CsharpAquarium.dos
{
    public class Aqua
    {
        public static int turnCount;
        public static int seaweedCount;
        public static int fishCount;

        public static Dictionary<string, Fish> fishes = new Dictionary<string, Fish>();
        public static List<Seaweed> seaweeds = new List<Seaweed>();

        private List<string> babyFishNames = new List<string>
        {
            "Baby1", "Baby2", "Baby3", "Baby4", "Baby5", "Baby6", "Baby7", "Baby8", "Baby9", "Baby10",
            "Baby11", "Baby12", "Baby13", "Baby14", "Baby15", "Baby16"
        };

        public void AddFish(string species, string name, string gender, string diet)
        {
            Fish newFish = new Fish(species, name, gender, diet);
            fishes[name] = newFish;
        }

        public void AddSeaweed()
        {
            Seaweed newSeaweed = new Seaweed();
            seaweeds.Add(newSeaweed);
        }

        private string GetRandomBabyFishName()
        {
            Random random = new Random();
            int randomIndex = random.Next(0, babyFishNames.Count);
            return babyFishNames[randomIndex];
        }

        public void PassTime()
        {
            foreach (var kvp in fishes.ToList())
            {
                Fish fish = kvp.Value;
                Fish.TimeLimit(fish, fishes);
                fish.IsLife(fish, fishes);
                fish.AgeHerma(fish);
            }

            Console.WriteLine("---- Aquarium Status ----");
            Console.WriteLine("Number of turns: " + turnCount);
            Console.WriteLine("Number of Fish: " + fishes.Count);
            Console.WriteLine("Number of Seaweed: " + seaweeds.Count);
            Console.WriteLine("-------------------------");

            List<Fish> fishToReproduce = new List<Fish>(); // List to store fish that can reproduce

            foreach (var kvp in fishes)
            {
                Fish fish = kvp.Value;
                fish.life--;

                Random random = new Random();
                int randomIndex = random.Next(0, fishes.Values.Count);
                Fish otherFish = fishes.Values.ElementAt(randomIndex);

                fish.OppoHerma(fish, otherFish);
                fish.Repro(fish, otherFish); // Call Repro method

                if (fish.canBirth)
                {
                    fishToReproduce.Add(fish); // Add fish to the list of fish that can reproduce
                }
            }

            // Now apply the changes to the fishes dictionary for those that can reproduce
            foreach (Fish reproducingFish in fishToReproduce)
            {
                string newGender = (new Random().Next(2) == 0) ? "Sir" : "Lady";
                Fish babyFish = new Fish(reproducingFish.species, GetRandomBabyFishName(), newGender, reproducingFish.diet);
                AddFish(babyFish.species, babyFish.name, babyFish.gender, babyFish.diet); // Add the new fish to the dictionary
                Console.BackgroundColor = ConsoleColor.Yellow; Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine($"{reproducingFish.name} has given birth to {babyFish.name}!");
                Console.ResetColor();
                Console.WriteLine();
                reproducingFish.canBirth = false; // Reset canBirth flag after using it
            }

            foreach (Seaweed seaweed in seaweeds)
            {
                seaweed.life++;
            }

            foreach (var fish in fishes.Values)
            {
                fish.isEaten = false;
                if (!fish.isEaten && fish.life <= 5)
                {
                    if (fish.diet == "Lettuce eater")
                    {
                        if (seaweeds.Count > 0)
                        {
                            fish.Eat(seaweeds[0]);
                            Seaweed.TakeDamage(fish, seaweeds[0]);
                            Fish.canReachFood = true;
                        }
                        else if (seaweeds.Count == 0)
                        {
                            Fish.canReachFood = false;
                            Console.ResetColor();
                            Console.BackgroundColor = ConsoleColor.White; Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine(fish.name + " hasn't found anything to eat");
                            Console.ResetColor();
                            Console.WriteLine();
                        }
                    }
                    else if (fish.diet == "My Boy")
                    {
                        if (fishes.Values.Count > 1)
                        {
                            Random random = new Random();
                            int randomIndex = random.Next(0, fishes.Values.Count);
                            Fish preyFish = fishes.Values.ElementAt(randomIndex);

                            if (preyFish.species == fish.species)
                            {
                                randomIndex++;
                            }

                            fish.Eat(fish, preyFish);
                        }
                    }
                }
            }

            foreach (var kvp in fishes)
            {
                string fishName = kvp.Key;
                Fish fish = kvp.Value;

                Console.WriteLine($"Fish Name: {fishName}");
                Console.WriteLine($"Species: {fish.species}");
                Console.WriteLine($"Gender: {fish.gender}");
                Console.WriteLine($"Diet: {fish.diet}");
                Console.WriteLine($"Life: {fish.life}");
                Console.WriteLine("-------------------------");
            }

            foreach (Seaweed seaweed in seaweeds)
            {
                seaweed.life++;
                Console.WriteLine($"Seaweed Life: {seaweed.life}");
                Console.ResetColor();
                Console.WriteLine();
            }

            turnCount++;
        }
    }
}
