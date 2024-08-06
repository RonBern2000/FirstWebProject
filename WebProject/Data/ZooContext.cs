using Microsoft.EntityFrameworkCore;
using WebProject.Models;

namespace WebProject.Data
{
    public class ZooContext : DbContext
    {
        public ZooContext(DbContextOptions<ZooContext> options) : base(options)
        {
        }
        public DbSet<Animal>? Animals { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Comment>? Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>().HasData(
                new Animal { AnimalId = 1, Name = "Peacock", Age = 5, PictureName = "/images/peacock.jpg", Description = "The peacock is a large, colorful bird known for its striking plumage. Males, called peacocks, have iridescent blue and green feathers and an impressive tail, or train, that they fan out in a display to attract females, known as peahens. Peahens are less colorful, with muted brown and gray feathers. Peacocks belong to the pheasant family and are native to South Asia, though they have been introduced to many other parts of the world. They are known for their loud calls and majestic courtship displays.", CategoryId = 1 },
                new Animal { AnimalId = 2, Name = "Hyena", Age = 13, PictureName = "/images/hyena.jpg", Description = "The hyena is a carnivorous mammal known for its scavenging habits and distinctive vocalizations, which often resemble laughter. Hyenas belong to the family Hyaenidae and are native to Africa and parts of Asia. There are four species: the spotted hyena, the brown hyena, the striped hyena, and the aardwolf. Spotted hyenas are the largest and most social, living in clans led by females. They have powerful jaws and are effective hunters as well as scavengers. Hyenas play a crucial role in their ecosystems by consuming carrion and keeping animal populations in check.", CategoryId = 2 },
                new Animal { AnimalId = 3, Name = "Cheetah", Age = 22, PictureName = "/images/Cheetah.jpeg", Description = "The cheetah is a large feline known for being the fastest land animal, capable of reaching speeds up to 60-70 miles per hour in short bursts covering distances up to 500 meters. They have slender bodies, long legs, and a distinctive coat with black spots on a tan background. Cheetahs have small heads with high-set eyes, aiding in spotting prey. They primarily hunt during the day, relying on their speed to catch antelopes and other small to medium-sized ungulates. Cheetahs are native to Africa, with a small population in Iran.", CategoryId = 2 },
                new Animal { AnimalId = 4, Name = "Giraffe", Age = 34, PictureName = "/images/giraffe.jpg", Description = "The giraffe is the tallest land animal, distinguished by its long neck and legs. Native to Africa, giraffes have a distinctive coat pattern of irregular brown patches separated by lighter-colored lines. Adult giraffes can reach heights of up to 18 feet (5.5 meters) and weigh between 1,600 to 3,000 pounds (725 to 1,360 kilograms). Their long necks and legs help them access foliage high in trees, particularly acacias, which are their main diet. Giraffes have a unique walking pattern, moving their legs on one side of the body and then the other. They are social animals, often found in loose herds. Despite their size, giraffes are gentle and have relatively few natural predators.", CategoryId = 2 },
                new Animal { AnimalId = 5, Name = "Python", Age = 8, PictureName = "/images/phython.jpeg", Description = "Pythons are large, non-venomous snakes belonging to the family Pythonidae. They are native to Africa, Asia, and Australia. Pythons are known for their impressive size, with some species, like the reticulated python, reaching lengths of over 20 feet (6 meters). They are constrictors, meaning they kill their prey by wrapping around it and squeezing until it suffocates. Pythons have heat-sensing pits along their jaws to detect warm-blooded prey and can consume animals larger than their head due to their highly flexible jaws. They are solitary creatures and vary in color and pattern depending on the species.", CategoryId = 3 },
                new Animal { AnimalId = 6, Name = "Shark", Age = 19, PictureName = "/images/shark.jpg", Description = "Sharks are a diverse group of predatory fish known for their streamlined bodies and sharp teeth. They belong to the class Chondrichthyes, which means their skeletons are made of cartilage rather than bone. Sharks have been around for hundreds of millions of years and come in various sizes, from the small dwarf lantern shark to the massive whale shark. They are found in oceans worldwide, from shallow coastal areas to deep sea environments. Sharks play a crucial role in marine ecosystems as top predators, helping to maintain the balance of marine life by controlling prey populations. Their keen senses, including excellent hearing and a specialized sense called electroreception, aid in hunting.", CategoryId = 4 }
                );
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Birds" },
                new Category { CategoryId = 2, Name = "Mammals" },
                new Category { CategoryId = 3, Name = "Reptiles" },
                new Category { CategoryId = 4, Name = "Fish" }
                );
            modelBuilder.Entity<Comment>().HasData(
                new Comment { CommentId = 1, CommentText = "He is beatiful!", AnimalId = 1 },
                new Comment { CommentId = 2, CommentText = "He is ugly!", AnimalId = 2 },
                new Comment { CommentId = 3, CommentText = "He is very cute!", AnimalId = 2 },
                new Comment { CommentId = 4, CommentText = "He is lean!", AnimalId = 3 },
                new Comment { CommentId = 5, CommentText = "He is tall!", AnimalId = 4 },
                new Comment { CommentId = 6, CommentText = "He is strong!", AnimalId = 5 },
                new Comment { CommentId = 7, CommentText = "He is quiet!", AnimalId = 6 },
                new Comment { CommentId = 8, CommentText = "He is shy!", AnimalId = 6 }
                );
        }
    }
}
