using Newtonsoft.Json;
using System.Collections.Generic;

namespace DLEA_Lib.Shared.Wardrobe
{
    public enum CategoryType
    {
        Default, PoliceDepartment, FireDepartment, Military, Civil, Custom
    }

    public class Outfit
    {
        public static readonly string FemalePed = "mp_f_freemode_01";

        public static readonly string MalePed = "mp_m_freemode_01";

        public Outfit(Outfit_Category Category, bool Male, string Name)
        {
            this.Category = Category;
            this.IsMale = Male;
            this.Name = Name;
        }

        public Outfit()
        { }

        public Outfit_Category Category { get; set; }
        public Component Comp01 { get; set; }
        public Component Comp03 { get; set; }
        public Component Comp04 { get; set; }
        public Component Comp05 { get; set; }
        public Component Comp06 { get; set; }
        public Component Comp07 { get; set; }
        public Component Comp08 { get; set; }
        public Component Comp09 { get; set; }
        public Component Comp10 { get; set; }
        public Component Comp11 { get; set; }

        [JsonIgnore]
        public List<Component> Components
        {
            get => new List<Component>() { Comp01, Comp03, Comp04, Comp05, Comp06, Comp07, Comp08, Comp09, Comp10, Comp11 };
        }

        public string Gender { get; set; }

        [JsonIgnore]
        public bool IsMale { get => Gender == "Male"; set => Gender = value ? "Male" : "Female"; }

        public string Name { get; set; }

        [JsonIgnore]
        public string Ped { get => IsMale ? MalePed : FemalePed; }

        public Prop Prop01 { get; set; }
        public Prop Prop02 { get; set; }
        public Prop Prop03 { get; set; }
        public Prop Prop04 { get; set; }

        [JsonIgnore]
        public List<Prop> Props
        {
            get => new List<Prop>() { Prop01, Prop02, Prop03, Prop04 };
        }
    }

    public class Outfit_Category
    {
        public Outfit_Category(int ID, string ShortName, string LongName, CategoryType Type = CategoryType.Default)
        {
            this.ShortName = ShortName;
            this.LongName = LongName;
            this.ID = ID;
            this.Type = Type;
        }

        public int ID { get; set; }
        public string LongName { get; set; }
        public string ShortName { get; set; }
        public CategoryType Type { get; set; }
    }
}