namespace DLEA_Lib.Shared.Wardrobe
{
    public class Component
    {
        public Component(int ComponentId, int DrawableId, int TextureId, int PaletteID = 0)
        {
            this.ComponentId = ComponentId;
            this.DrawableId = DrawableId;
            this.TextureId = TextureId;
            this.PaletteId = PaletteId;
        }

        public int ComponentId { get; set; }
        public int DrawableId { get; set; }
        public int PaletteId { get; set; }
        public int TextureId { get; set; }
    }
}