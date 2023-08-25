using static System.Net.Mime.MediaTypeNames;

namespace CsharpAquarium.dos
{
    public class Seaweed
    {
        // Property to represent the life of the seaweed
        public int life;

        // Flag to track whether a new seaweed instance was added
        bool newSeaweedAdded = false;

        // Constructor for creating a new Seaweed instance
        public Seaweed()
        {
            life = 10;
            newSeaweedAdded = true;

            // Increment the seaweed count when a new instance is created
            if (newSeaweedAdded == true)
            {
                Aqua.seaweedCount++;
                newSeaweedAdded = false;
            }

            // Remove the seaweed if its life reaches 0
            if (life == 0)
            {
                Aqua.seaweeds.Remove(this);
            }
        }

        // Method to simulate a fish damaging the seaweed and gaining life
        public static void TakeDamage(Fish fish, Seaweed seaweed)
        {
            if (fish.diet == "Lettuce eater" && seaweed.life > 0)
            {
                seaweed.life -= fish.damage;
                fish.life += 3;
            }
            if (seaweed.life <= 0)
            {
                Aqua.seaweeds.Remove(seaweed);
                Console.BackgroundColor = ConsoleColor.Blue; Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("The Seaweed has been completely eaten.");
                Console.ResetColor();
                Console.WriteLine();
            }
        }
    }
}
