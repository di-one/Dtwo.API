using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtwo.API
{
    public class Character
    {
        public double Id { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }

        public int Breed { get; set; }
        public bool Sex { get; set; }

        public Character(double id, int level, string name, int breed, bool sex)
        {
            Console.WriteLine($"Create character with id {id} level {level} name {name} breed {breed} sex {sex}");

            Id = id;
            Level = level;
            Name = name;
            Breed = breed;
            Sex = sex;
        }
    }
}
