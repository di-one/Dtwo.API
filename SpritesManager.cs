using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtwo.API
{
    public static class SpritesManager
    {
        public static string GetCharacterIconPath(int breed, bool sex)
        {
            string str = $@"images/vignettingMiniCharacter/mini_{breed}_{(sex ? 0 : 1)}.png";

            Console.WriteLine(str);

            return str;
        }
    }
}
