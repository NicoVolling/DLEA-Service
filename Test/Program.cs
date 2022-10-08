using DLEA_Lib.Shared.Wardrobe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Outfits.InitializeOutfits();
            foreach (var ewaslö in Outfits._OutfitList.Concat(Outfits.CustomOutfitList).Select(r => r.Category).Distinct().ToList())
            {
                if (ewaslö.Type == null) { throw new Exception(); }
                if (ewaslö.ID == null) { throw new Exception(); }
                if (ewaslö.ShortName == null) { throw new Exception(); }
            }
            List<Outfit_Category> irgendwas = Outfits.CategorieList.OrderBy(o => (int)o.Type).ThenBy(o => o.ID).ToList();

            Console.Read();
        }
    }
}