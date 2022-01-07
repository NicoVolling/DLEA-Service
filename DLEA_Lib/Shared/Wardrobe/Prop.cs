using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLEA_Lib.Shared.Wardrobe
{
    public class Prop
    {
        public Prop(int ComponentId, int DrawableId, int TextureId)
        {
            this.ComponentId = ComponentId;
            this.DrawableId = DrawableId;
            this.TextureId = TextureId;
        }

        public int ComponentId { get; set; }
        public int DrawableId { get; set; }
        public int TextureId { get; set; }
    }
}
