using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DLEA_Lib.Shared.Wardrobe
{
    public static class Outfits
    {
        public static List<Outfit> _OutfitList;

        public static List<Outfit> CustomOutfitList;

        public static List<Outfit_Category> CategorieList
        {
            get
            {
                return Outfits._OutfitList.Concat(CustomOutfitList).Select(r => r.Category).Distinct().OrderBy(x => x.ShortName ?? "").ToList();
            }
        }

        public static List<Outfit> OutfitList
        {
            get
            {
                return Outfits._OutfitList.Concat(CustomOutfitList).OrderBy(x => x.Name ?? "").ToList();
            }
        }

        public static List<Outfit> Outfits_Female
        {
            get
            {
                return Outfits._OutfitList.Concat(CustomOutfitList).Where(r => !r.IsMale).OrderBy(x => x.Name ?? "").ToList();
            }
        }

        public static List<Outfit> Outfits_Male
        {
            get
            {
                return Outfits._OutfitList.Concat(CustomOutfitList).Where(r => r.IsMale).OrderBy(x => x.Name ?? "").ToList();
            }
        }

        public static int Count(int CategoryID = -1)
        {
            if (CategoryID == -1)
            {
                return Outfits._OutfitList.Concat(CustomOutfitList).Count();
            }
            return Outfits._OutfitList.Concat(CustomOutfitList).Where(o => o.Category.ID == CategoryID).Count();
        }

        public static int CountFemale(int CategoryID = -1)
        {
            if (CategoryID == -1)
            {
                return Outfits._OutfitList.Concat(CustomOutfitList).Where(o => !o.IsMale).Count();
            }
            return Outfits._OutfitList.Concat(CustomOutfitList).Where(o => !o.IsMale && o.Category.ID == CategoryID).Count();
        }

        public static int CountMale(int CategoryID = -1)
        {
            return 0;
            if (CategoryID == -1)
            {
                return Outfits._OutfitList.Concat(CustomOutfitList ?? new List<Outfit>()).Where(o => o.IsMale).Count();
            }
            return Outfits._OutfitList.Concat(CustomOutfitList ?? new List<Outfit>()).Where(o => o.IsMale && o.Category.ID == CategoryID).Count();
        }

        public static void InitializeOutfits()
        {
            int _ID = -1;
            Categories.Alle = new Outfit_Category(_ID++, "Alle", "Alle Kategorien", CategoryType.Default);
            Categories.Army = new Outfit_Category(_ID++, "US Army", "Bundeswehr", CategoryType.Military);
            Categories.BauTechnik = new Outfit_Category(_ID++, "Bau und Technik", "Bau und Technik", CategoryType.Civil);
            Categories.BCFD = new Outfit_Category(_ID++, "BCFD", "Blaine County Fire Department", CategoryType.FireDepartment);
            Categories.BCSO = new Outfit_Category(_ID++, "BCSO", "Blaine County Sheriff's Office", CategoryType.PoliceDepartment);
            Categories.Custom = new Outfit_Category(_ID++, "Gespeichert", "Gespeicherte Outfits", CategoryType.Civil);
            Categories.FBI = new Outfit_Category(_ID++, "FBI", "Federal Bureau of Investigation", CategoryType.PoliceDepartment);
            Categories.DEA = new Outfit_Category(_ID++, "DEA", "Drug Enforcement Administration ", CategoryType.PoliceDepartment);
            Categories.LG = new Outfit_Category(_ID++, "Lifeguard", "Wasserwacht / Lebensrettung", CategoryType.FireDepartment);
            Categories.LSCoFD = new Outfit_Category(_ID++, "LSCoFD", "Los Santos County Fire Department", CategoryType.FireDepartment);
            Categories.LSFD = new Outfit_Category(_ID++, "LSFD", "Los Santos Fire Department", CategoryType.FireDepartment);
            Categories.LSIA = new Outfit_Category(_ID++, "LSIA", "Los Santos International Airport", CategoryType.PoliceDepartment);
            Categories.LSPA = new Outfit_Category(_ID++, "LSPA", "Los Santos Port Authority", CategoryType.PoliceDepartment);
            Categories.LSPD = new Outfit_Category(_ID++, "LSPD", "Los Santos Police Department", CategoryType.PoliceDepartment);
            Categories.LSSD = new Outfit_Category(_ID++, "LSSD", "Los Santos County Sheriff's Office", CategoryType.PoliceDepartment);
            Categories.MW = new Outfit_Category(_ID++, "Merryweather", "Merryweather", CategoryType.Civil);
            Categories.NOOSE = new Outfit_Category(_ID++, "N.O.O.S.E.", "National Office of Security Enforcement", CategoryType.PoliceDepartment);
            Categories.SADCR = new Outfit_Category(_ID++, "SADCR", "San Andreas Department of Corrections", CategoryType.PoliceDepartment);
            Categories.SAHP = new Outfit_Category(_ID++, "SAHP", "San Andreas Highway Patrol", CategoryType.PoliceDepartment);
            Categories.SAMS = new Outfit_Category(_ID++, "SAMS", "San Andreas Medical Services", CategoryType.FireDepartment);
            Categories.SASP = new Outfit_Category(_ID++, "SASP", "San Andreas State Park Ranger", CategoryType.PoliceDepartment);
            Categories.Sec = new Outfit_Category(_ID++, "Sicherheit", "Sicherheit", CategoryType.Civil);
            Categories.USAF = new Outfit_Category(_ID++, "USAF", "US Air Force", CategoryType.Military);
            Categories.USCG = new Outfit_Category(_ID++, "USCG", "US Coast Guard", CategoryType.PoliceDepartment);
            Categories.Wartung = new Outfit_Category(_ID++, "Wartung", "Wartung", CategoryType.Civil);

            CustomOutfitList = new List<Outfit>();

            _OutfitList = new List<Outfit>()
            {
                      #region LSPD

                new Outfit (Categories.LSPD, true, "LSPD Uniform (Langärmlig)")
                      {
                          Prop01 = new Prop            ( 0, 47, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 5, 1 ),
                          Comp04 = new Component       ( 4, 36, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 58, 1 ),
                          Comp09 = new Component       ( 9, 15, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 144, 1 )
                      },

                      new Outfit (Categories.LSPD, true, "LSPD Uniform (Kurzärmlig)")
                      {
                          Prop01 = new Prop            ( 0, 47, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 4, 1 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 1, 1 ),
                          Comp04 = new Component       ( 4, 36, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 59, 1 ),
                          Comp09 = new Component       ( 9, 14, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 150, 1 )
                      },

                      new Outfit (Categories.LSPD, false, "LSPD Uniform (Kurzärmlig)")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 15, 1 ),
                          Comp04 = new Component       ( 4, 35, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 53, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 36, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 147, 1 )
                      },

                      new Outfit (Categories.LSPD, true, "LSPD Jacke")
                      {
                          Prop01 = new Prop            ( 0, 47, 1 ),
                          Prop02 = new Prop            ( 1, 1, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 5, 1 ),
                          Comp04 = new Component       ( 4, 36, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 52, 1 ),
                          Comp09 = new Component       ( 9, 3, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 40, 1 )
                      },

                      new Outfit (Categories.LSPD, false, "LSPD Jacke")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 15, 1 ),
                          Comp04 = new Component       ( 4, 35, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 53, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 54, 1 ),
                          Comp09 = new Component       ( 9, 3, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 173, 1 )
                      },

                      new Outfit (Categories.LSPD, true, "LSPD Verkehrsuniform")
                      {
                          Prop01 = new Prop            ( 0, 18, 2 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 20, 1 ),
                          Comp04 = new Component       ( 4, 33, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 31, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 58, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 19, 1 )
                      },

                      new Outfit (Categories.LSPD, true, "LSPD Motorraduniform")
                      {
                          Prop01 = new Prop            ( 0, 18, 2 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 23, 1 ),
                          Comp04 = new Component       ( 4, 33, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 31, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 58, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 53, 1 )
                      },

                      new Outfit (Categories.LSPD, false, "LSPD Motorraduniform")
                      {
                          Prop01 = new Prop            ( 0, 18, 2 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 15, 1 ),
                          Comp04 = new Component       ( 4, 32, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 10, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 35, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 37, 1 )
                      },

                      new Outfit (Categories.LSPD, true, "LSPD Anzug")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 7, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 5, 1 ),
                          Comp04 = new Component       ( 4, 11, 5 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 11, 1 ),
                          Comp07 = new Component       ( 7, 39, 8 ),
                          Comp08 = new Component       ( 8, 11, 1 ),
                          Comp09 = new Component       ( 9, 25, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 5, 5 )
                      },

                      new Outfit (Categories.LSPD, false, "LSPD Anzug")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 8, 1 ),
                          Comp04 = new Component       ( 4, 4, 4 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 30, 1 ),
                          Comp07 = new Component       ( 7, 1, 1 ),
                          Comp08 = new Component       ( 8, 65, 2 ),
                          Comp09 = new Component       ( 9, 24, 2 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 25, 2 )
                      },

                      new Outfit (Categories.LSPD, true, "LSPD Ermittler")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 7, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 5, 1 ),
                          Comp04 = new Component       ( 4, 11, 5 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 11, 1 ),
                          Comp07 = new Component       ( 7, 7, 1 ),
                          Comp08 = new Component       ( 8, 81, 8 ),
                          Comp09 = new Component       ( 9, 25, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 4, 1 )
                      },

                      new Outfit (Categories.LSPD, true, "LSPD Ermittler (Weste)")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 7, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 5, 1 ),
                          Comp04 = new Component       ( 4, 11, 5 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 11, 1 ),
                          Comp07 = new Component       ( 7, 7, 1 ),
                          Comp08 = new Component       ( 8, 42, 2 ),
                          Comp09 = new Component       ( 9, 25, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 4, 1 )
                      },

                      new Outfit (Categories.LSPD, false, "LSPD Ermittler")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 1, 1 ),
                          Comp04 = new Component       ( 4, 4, 4 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 30, 1 ),
                          Comp07 = new Component       ( 7, 7, 1 ),
                          Comp08 = new Component       ( 8, 25, 6 ),
                          Comp09 = new Component       ( 9, 27, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 29, 3 )
                      },

                      new Outfit (Categories.LSPD, true, "LSPD Windjacke")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 13, 1 ),
                          Comp04 = new Component       ( 4, 11, 5 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 11, 1 ),
                          Comp07 = new Component       ( 7, 7, 1 ),
                          Comp08 = new Component       ( 8, 12, 1 ),
                          Comp09 = new Component       ( 9, 21, 2 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 36, 2 )
                      },

                      new Outfit (Categories.LSPD, true, "LSPD K-9 Uniform")
                      {
                          Prop01 = new Prop            ( 0, 11, 7 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 1, 1 ),
                          Comp04 = new Component       ( 4, 53, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 25, 1 ),
                          Comp07 = new Component       ( 7, 2, 1 ),
                          Comp08 = new Component       ( 8, 38, 1 ),
                          Comp09 = new Component       ( 9, 15, 1 ),
                          Comp10 = new Component       ( 10, 9, 1 ),
                          Comp11 = new Component       ( 11, 103, 1 )
                      },

                      new Outfit (Categories.LSPD, false, "LSPD K-9 Uniform")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 15, 1 ),
                          Comp04 = new Component       ( 4, 55, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 26, 1 ),
                          Comp07 = new Component       ( 7, 2, 1 ),
                          Comp08 = new Component       ( 8, 3, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 8, 1 ),
                          Comp11 = new Component       ( 11, 94, 1 )
                      },

                      new Outfit (Categories.LSPD, true, "LSPD Winteruniform")
                      {
                          Prop01 = new Prop            ( 0, 47, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 39, 1 ),
                          Comp04 = new Component       ( 4, 36, 1 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 58, 1 ),
                          Comp09 = new Component       ( 9, 27, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 155, 1 )
                      },

                      new Outfit (Categories.LSPD, false, "LSPD Winteruniform")
                      {
                          Prop01 = new Prop            ( 0, 46, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 37, 1 ),
                          Comp04 = new Component       ( 4, 35, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 53, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 35, 1 ),
                          Comp09 = new Component       ( 9, 29, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 152, 1 )
                      },

                      new Outfit (Categories.LSPD, true, "LSPD Fahrraduniform")
                      {
                          Prop01 = new Prop            ( 0, 50, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 20, 1 ),
                          Comp04 = new Component       ( 4, 13, 3 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 3, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 38, 1 ),
                          Comp09 = new Component       ( 9, 15, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 94, 3 )
                      },

                      new Outfit (Categories.LSPD, false, "LSPD Fahrraduniform")
                      {
                          Prop01 = new Prop            ( 0, 48, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 32, 1 ),
                          Comp04 = new Component       ( 4, 15, 3 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 11, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 3, 1 ),
                          Comp09 = new Component       ( 9, 2, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 85, 3 )
                      },

                      new Outfit (Categories.LSPD, true, "LSPD Pilotenuniform")
                      {
                          Prop01 = new Prop            ( 0, 80, 2 ),
                          Prop02 = new Prop            ( 1, 1, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 17, 1 ),
                          Comp04 = new Component       ( 4, 39, 3 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 25, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 68, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 16, 1 ),
                          Comp11 = new Component       ( 11, 66, 3 )
                      },

                      new Outfit (Categories.LSPD, false, "LSPD Pilotenuniform")
                      {
                          Prop01 = new Prop            ( 0, 79, 2 ),
                          Prop02 = new Prop            ( 1, 14, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 18, 1 ),
                          Comp04 = new Component       ( 4, 39, 3 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 25, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 50, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 15, 1 ),
                          Comp11 = new Component       ( 11, 60, 3 )
                      },

                      new Outfit (Categories.LSPD, true, "LSPD SWAT Uniform")
                      {
                          Prop01 = new Prop            ( 0, 76, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 97, 1 ),
                          Comp04 = new Component       ( 4, 32, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 26, 1 ),
                          Comp07 = new Component       ( 7, 111, 1 ),
                          Comp08 = new Component       ( 8, 16, 1 ),
                          Comp09 = new Component       ( 9, 16, 1 ),
                          Comp10 = new Component       ( 10, 4, 1 ),
                          Comp11 = new Component       ( 11, 50, 1 )
                      },

                      new Outfit (Categories.LSPD, false, "LSPD SWAT Uniform")
                      {
                          Prop01 = new Prop            ( 0, 75, 1 ),
                          Prop02 = new Prop            ( 1, 26, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 112, 1 ),
                          Comp04 = new Component       ( 4, 31, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 26, 1 ),
                          Comp07 = new Component       ( 7, 82, 1 ),
                          Comp08 = new Component       ( 8, 16, 1 ),
                          Comp09 = new Component       ( 9, 18, 1 ),
                          Comp10 = new Component       ( 10, 12, 1 ),
                          Comp11 = new Component       ( 11, 43, 1 )
                      },

                      new Outfit (Categories.LSPD, true, "LSPD Aufstandsuniform")
                      {
                          Prop01 = new Prop            ( 0, 82, 2 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 31, 1 ),
                          Comp04 = new Component       ( 4, 36, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 58, 1 ),
                          Comp09 = new Component       ( 9, 13, 4 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 150, 1 )
                      },

                      new Outfit (Categories.LSPD, true, "LSPD Uniform (Weste)")
                      {
                          Prop01 = new Prop            ( 0, 47, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 31, 1 ),
                          Comp04 = new Component       ( 4, 36, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 58, 1 ),
                          Comp09 = new Component       ( 9, 13, 4 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 150, 1 )
                      },

                   #endregion LSPD

                      #region LSSD

                new Outfit (Categories.LSSD, true, "LSSD Uniform (Kurzärmlig)")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 1, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 4, 1 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 1, 1 ),
                          Comp04 = new Component       ( 4, 26, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 45, 2 ),
                          Comp09 = new Component       ( 9, 14, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 76, 1 )
                      },

                      new Outfit (Categories.LSSD, false, "LSSD Uniform")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 15, 1 ),
                          Comp04 = new Component       ( 4, 42, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 53, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 53, 2 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 141, 1 )
                      },

                      new Outfit (Categories.LSSD, true, "LSSD Jacke")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 1, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 5, 1 ),
                          Comp04 = new Component       ( 4, 26, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 66, 1 ),
                          Comp09 = new Component       ( 9, 3, 3 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 157, 5 )
                      },

                      new Outfit (Categories.LSSD, false, "LSSD Jacke")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 4, 1 ),
                          Comp04 = new Component       ( 4, 42, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 53, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 46, 1 ),
                          Comp09 = new Component       ( 9, 3, 3 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 154, 5 )
                      },

                      new Outfit (Categories.LSSD, true, "LSSD Jacke Wanrschutzkleidung")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 1, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 5, 1 ),
                          Comp04 = new Component       ( 4, 26, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 66, 1 ),
                          Comp09 = new Component       ( 9, 3, 3 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 157, 3 )
                      },

                      new Outfit (Categories.LSSD, true, "LSSD Motorraduniform")
                      {
                          Prop01 = new Prop            ( 0, 18, 3 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 23, 1 ),
                          Comp04 = new Component       ( 4, 33, 3 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 31, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 39, 2 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 151, 1 )
                      },

                      new Outfit (Categories.LSSD, false, "LSSD Motorraduniform")
                      {
                          Prop01 = new Prop            ( 0, 18, 3 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 15, 1 ),
                          Comp04 = new Component       ( 4, 32, 3 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 10, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 52, 2 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 141, 1 )
                      },

                      new Outfit (Categories.LSSD, true, "LSSD Anzug")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 7, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 5, 1 ),
                          Comp04 = new Component       ( 4, 11, 1 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 11, 1 ),
                          Comp07 = new Component       ( 7, 39, 5 ),
                          Comp08 = new Component       ( 8, 11, 8 ),
                          Comp09 = new Component       ( 9, 24, 2 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 5, 1 )
                      },

                      new Outfit (Categories.LSSD, false, "LSSD Anzug")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 8, 1 ),
                          Comp04 = new Component       ( 4, 4, 2 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 30, 1 ),
                          Comp07 = new Component       ( 7, 1, 1 ),
                          Comp08 = new Component       ( 8, 40, 6 ),
                          Comp09 = new Component       ( 9, 21, 2 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 25, 4 )
                      },

                      new Outfit (Categories.LSSD, true, "LSSD Ermittler")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 7, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 5, 1 ),
                          Comp04 = new Component       ( 4, 11, 1 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 11, 1 ),
                          Comp07 = new Component       ( 7, 7, 1 ),
                          Comp08 = new Component       ( 8, 81, 5 ),
                          Comp09 = new Component       ( 9, 24, 2 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 4, 8 )
                      },

                      new Outfit (Categories.LSSD, true, "LSSD Ermittler (Weste)")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 7, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 5, 1 ),
                          Comp04 = new Component       ( 4, 11, 1 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 11, 1 ),
                          Comp07 = new Component       ( 7, 7, 1 ),
                          Comp08 = new Component       ( 8, 42, 3 ),
                          Comp09 = new Component       ( 9, 24, 2 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 4, 8 )
                      },

                      new Outfit (Categories.LSSD, false, "LSSD Ermittler")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 6, 1 ),
                          Comp04 = new Component       ( 4, 4, 5 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 30, 1 ),
                          Comp07 = new Component       ( 7, 7, 1 ),
                          Comp08 = new Component       ( 8, 5, 1 ),
                          Comp09 = new Component       ( 9, 26, 2 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 9, 8 )
                      },

                      new Outfit (Categories.LSSD, true, "LSSD Windjacke")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 13, 1 ),
                          Comp04 = new Component       ( 4, 11, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 11, 1 ),
                          Comp07 = new Component       ( 7, 7, 1 ),
                          Comp08 = new Component       ( 8, 12, 8 ),
                          Comp09 = new Component       ( 9, 22, 2 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 36, 5 )
                      },

                      new Outfit (Categories.LSSD, true, "LSSD K-9 Uniform")
                      {
                          Prop01 = new Prop            ( 0, 11, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 1, 1 ),
                          Comp04 = new Component       ( 4, 60, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 25, 1 ),
                          Comp07 = new Component       ( 7, 2, 1 ),
                          Comp08 = new Component       ( 8, 54, 1 ),
                          Comp09 = new Component       ( 9, 15, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 101, 1 )
                      },

                      new Outfit (Categories.LSSD, false, "LSSD K-9 Uniform")
                      {
                          Prop01 = new Prop            ( 0, 11, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 15, 1 ),
                          Comp04 = new Component       ( 4, 62, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 25, 1 ),
                          Comp07 = new Component       ( 7, 2, 1 ),
                          Comp08 = new Component       ( 8, 32, 2 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 91, 1 )
                      },

                      new Outfit (Categories.LSSD, true, "LSSD Uniform (Langärmlig)")
                      {
                          Prop01 = new Prop            ( 0, 14, 1 ),
                          Prop02 = new Prop            ( 1, 1, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 5, 1 ),
                          Comp04 = new Component       ( 4, 26, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 39, 2 ),
                          Comp09 = new Component       ( 9, 15, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 151, 1 )
                      },

                      new Outfit (Categories.LSSD, true, "LSSD Winteruniform")
                      {
                          Prop01 = new Prop            ( 0, 3, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 34, 1 ),
                          Comp04 = new Component       ( 4, 26, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 39, 2 ),
                          Comp09 = new Component       ( 9, 29, 2 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 18, 2 )
                      },

                      new Outfit (Categories.LSSD, false, "LSSD Winteruniform")
                      {
                          Prop01 = new Prop            ( 0, 6, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 37, 1 ),
                          Comp04 = new Component       ( 4, 42, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 53, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 52, 2 ),
                          Comp09 = new Component       ( 9, 29, 2 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 22, 2 )
                      },

                      new Outfit (Categories.LSSD, true, "LSSD SWAT Uniform")
                      {
                          Prop01 = new Prop            ( 0, 60, 2 ),
                          Prop02 = new Prop            ( 1, 24, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 97, 1 ),
                          Comp04 = new Component       ( 4, 32, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 36, 1 ),
                          Comp07 = new Component       ( 7, 111, 1 ),
                          Comp08 = new Component       ( 8, 16, 1 ),
                          Comp09 = new Component       ( 9, 28, 2 ),
                          Comp10 = new Component       ( 10, 5, 1 ),
                          Comp11 = new Component       ( 11, 50, 2 )
                      },

                      new Outfit (Categories.LSSD, false, "LSSD SWAT Uniform")
                      {
                          Prop01 = new Prop            ( 0, 60, 2 ),
                          Prop02 = new Prop            ( 1, 26, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 112, 1 ),
                          Comp04 = new Component       ( 4, 31, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 37, 1 ),
                          Comp07 = new Component       ( 7, 82, 1 ),
                          Comp08 = new Component       ( 8, 16, 1 ),
                          Comp09 = new Component       ( 9, 30, 2 ),
                          Comp10 = new Component       ( 10, 12, 2 ),
                          Comp11 = new Component       ( 11, 43, 2 )
                      },

                      new Outfit (Categories.LSSD, true, "LSSD Pilotenuniform")
                      {
                          Prop01 = new Prop            ( 0, 80, 1 ),
                          Prop02 = new Prop            ( 1, 1, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 97, 1 ),
                          Comp04 = new Component       ( 4, 39, 3 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 25, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 44, 2 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 16, 2 ),
                          Comp11 = new Component       ( 11, 66, 3 )
                      },

                      new Outfit (Categories.LSSD, false, "LSSD Pilotenuniform")
                      {
                          Prop01 = new Prop            ( 0, 79, 1 ),
                          Prop02 = new Prop            ( 1, 14, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 37, 1 ),
                          Comp04 = new Component       ( 4, 39, 3 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 25, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 31, 2 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 15, 2 ),
                          Comp11 = new Component       ( 11, 60, 3 )
                      },

                      new Outfit (Categories.LSSD, true, "LSSD Fahrraduniform")
                      {
                          Prop01 = new Prop            ( 0, 50, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 20, 1 ),
                          Comp04 = new Component       ( 4, 13, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 3, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 38, 1 ),
                          Comp09 = new Component       ( 9, 15, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 95, 3 )
                      },

                      new Outfit (Categories.LSSD, false, "LSSD Fahrraduniform")
                      {
                          Prop01 = new Prop            ( 0, 48, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 32, 1 ),
                          Comp04 = new Component       ( 4, 15, 4 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 11, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 3, 1 ),
                          Comp09 = new Component       ( 9, 2, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 86, 3 )
                      },

                      new Outfit (Categories.LSSD, true, "LSSD Aufstandsuniform")
                      {
                          Prop01 = new Prop            ( 0, 82, 3 ),
                          Prop02 = new Prop            ( 1, 1, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 31, 1 ),
                          Comp04 = new Component       ( 4, 60, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 25, 1 ),
                          Comp07 = new Component       ( 7, 2, 1 ),
                          Comp08 = new Component       ( 8, 39, 2 ),
                          Comp09 = new Component       ( 9, 13, 3 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 76, 1 )
                      },

                   #endregion LSSD

                      #region SAHP

                new Outfit (Categories.SAHP, true, "SAHP Uniform (Langärmlig)")
                      {
                          Prop01 = new Prop            ( 0, 14, 3 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 5, 1 ),
                          Comp04 = new Component       ( 4, 26, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 16, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 54, 1 ),
                          Comp09 = new Component       ( 9, 15, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 27, 1 )
                      },

                      new Outfit (Categories.SAHP, true, "SAHP Uniform (Kurzärmlig)")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 4, 1 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 1, 1 ),
                          Comp04 = new Component       ( 4, 26, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 16, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 54, 1 ),
                          Comp09 = new Component       ( 9, 14, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 119, 1 )
                      },

                      new Outfit (Categories.SAHP, false, "SAHP Uniform (Kurzärmlig)")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 15, 1 ),
                          Comp04 = new Component       ( 4, 42, 3 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 53, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 32, 2 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 26, 1 )
                      },

                      new Outfit (Categories.SAHP, true, "SAHP Jacke")
                      {
                          Prop01 = new Prop            ( 0, 45, 1 ),
                          Prop02 = new Prop            ( 1, 1, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 5, 1 ),
                          Comp04 = new Component       ( 4, 26, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 16, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 66, 9 ),
                          Comp09 = new Component       ( 9, 2, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 104, 1 )
                      },

                      new Outfit (Categories.SAHP, false, "SAHP Jacke")
                      {
                          Prop01 = new Prop            ( 0, 44, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 4, 1 ),
                          Comp04 = new Component       ( 4, 42, 3 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 53, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 46, 9 ),
                          Comp09 = new Component       ( 9, 2, 4 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 95, 1 )
                      },

                      new Outfit (Categories.SAHP, true, "SAHP Motorraduniform")
                      {
                          Prop01 = new Prop            ( 0, 18, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 5, 1 ),
                          Comp04 = new Component       ( 4, 33, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 31, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 54, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 27, 1 )
                      },

                      new Outfit (Categories.SAHP, false, "SAHP Motorraduniform")
                      {
                          Prop01 = new Prop            ( 0, 18, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 15, 1 ),
                          Comp04 = new Component       ( 4, 32, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 10, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 32, 2 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 26, 1 )
                      },

                      new Outfit (Categories.SAHP, true, "SAHP Motorradjacke (Leder)")
                      {
                          Prop01 = new Prop            ( 0, 18, 1 ),
                          Prop02 = new Prop            ( 1, 1, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 25, 1 ),
                          Comp04 = new Component       ( 4, 33, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 14, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 66, 9 ),
                          Comp09 = new Component       ( 9, 2, 4 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 65, 1 )
                      },

                      new Outfit (Categories.SAHP, false, "SAHP Motorradjacke (Leder)")
                      {
                          Prop01 = new Prop            ( 0, 18, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 24, 1 ),
                          Comp04 = new Component       ( 4, 32, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 10, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 46, 9 ),
                          Comp09 = new Component       ( 9, 2, 4 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 35, 1 )
                      },

                      new Outfit (Categories.SAHP, true, "SAHP K-9 Uniform")
                      {
                          Prop01 = new Prop            ( 0, 45, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 1, 1 ),
                          Comp04 = new Component       ( 4, 48, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 55, 1 ),
                          Comp07 = new Component       ( 7, 2, 1 ),
                          Comp08 = new Component       ( 8, 54, 1 ),
                          Comp09 = new Component       ( 9, 15, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 32, 5 )
                      },

                      new Outfit (Categories.SAHP, false, "SAHP K-9 Uniform")
                      {
                          Prop01 = new Prop            ( 0, 44, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 15, 1 ),
                          Comp04 = new Component       ( 4, 50, 1 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 53, 1 ),
                          Comp07 = new Component       ( 7, 2, 1 ),
                          Comp08 = new Component       ( 8, 32, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 34, 5 )
                      },

                      new Outfit (Categories.SAHP, true, "SAHP Ermittler")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 7, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 5, 1 ),
                          Comp04 = new Component       ( 4, 11, 1 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 11, 1 ),
                          Comp07 = new Component       ( 7, 7, 1 ),
                          Comp08 = new Component       ( 8, 89, 1 ),
                          Comp09 = new Component       ( 9, 24, 3 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 4, 13 )
                      },

                      new Outfit (Categories.SAHP, true, "SAHP Ermittler (Weste)")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 7, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 5, 1 ),
                          Comp04 = new Component       ( 4, 11, 1 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 11, 1 ),
                          Comp07 = new Component       ( 7, 7, 1 ),
                          Comp08 = new Component       ( 8, 42, 2 ),
                          Comp09 = new Component       ( 9, 24, 3 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 4, 13 )
                      },

                      new Outfit (Categories.SAHP, false, "SAHP Ermittler")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 1, 1 ),
                          Comp04 = new Component       ( 4, 4, 5 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 30, 1 ),
                          Comp07 = new Component       ( 7, 7, 1 ),
                          Comp08 = new Component       ( 8, 8, 1 ),
                          Comp09 = new Component       ( 9, 26, 3 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 28, 5 )
                      },

                      new Outfit (Categories.SAHP, true, "SAHP Windjacke")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 13, 1 ),
                          Comp04 = new Component       ( 4, 11, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 11, 1 ),
                          Comp07 = new Component       ( 7, 7, 1 ),
                          Comp08 = new Component       ( 8, 12, 13 ),
                          Comp09 = new Component       ( 9, 22, 8 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 36, 6 )
                      },

                      new Outfit (Categories.SAHP, true, "SAHP Winteruniform")
                      {
                          Prop01 = new Prop            ( 0, 9, 3 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 39, 1 ),
                          Comp04 = new Component       ( 4, 26, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 16, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 54, 1 ),
                          Comp09 = new Component       ( 9, 27, 3 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 18, 4 )
                      },

                      new Outfit (Categories.SAHP, false, "SAHP Winteruniform")
                      {
                          Prop01 = new Prop            ( 0, 9, 3 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 37, 1 ),
                          Comp04 = new Component       ( 4, 42, 3 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 53, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 32, 2 ),
                          Comp09 = new Component       ( 9, 29, 3 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 22, 4 )
                      },

                      new Outfit (Categories.SAHP, true, "SAHP Pilotenuniform")
                      {
                          Prop01 = new Prop            ( 0, 80, 1 ),
                          Prop02 = new Prop            ( 1, 1, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 97, 1 ),
                          Comp04 = new Component       ( 4, 39, 4 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 25, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 44, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 16, 3 ),
                          Comp11 = new Component       ( 11, 66, 4 )
                      },

                      new Outfit (Categories.SAHP, false, "SAHP Pilotenuniform")
                      {
                          Prop01 = new Prop            ( 0, 79, 1 ),
                          Prop02 = new Prop            ( 1, 14, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 37, 1 ),
                          Comp04 = new Component       ( 4, 39, 4 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 25, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 31, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 15, 3 ),
                          Comp11 = new Component       ( 11, 60, 4 )
                      },

                      new Outfit (Categories.SAHP, true, "SAHP SWAT Uniform")
                      {
                          Prop01 = new Prop            ( 0, 40, 2 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 17, 1 ),
                          Comp04 = new Component       ( 4, 38, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 26, 1 ),
                          Comp07 = new Component       ( 7, 111, 1 ),
                          Comp08 = new Component       ( 8, 16, 1 ),
                          Comp09 = new Component       ( 9, 28, 3 ),
                          Comp10 = new Component       ( 10, 10, 1 ),
                          Comp11 = new Component       ( 11, 54, 2 )
                      },

                      new Outfit (Categories.SAHP, false, "SAHP SWAT Uniform")
                      {
                          Prop01 = new Prop            ( 0, 39, 2 ),
                          Prop02 = new Prop            ( 1, 26, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 18, 1 ),
                          Comp04 = new Component       ( 4, 37, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 26, 1 ),
                          Comp07 = new Component       ( 7, 82, 1 ),
                          Comp08 = new Component       ( 8, 16, 1 ),
                          Comp09 = new Component       ( 9, 30, 3 ),
                          Comp10 = new Component       ( 10, 12, 5 ),
                          Comp11 = new Component       ( 11, 47, 2 )
                      },

                      new Outfit (Categories.SAHP, true, "SAHP Aufstandsuniform")
                      {
                          Prop01 = new Prop            ( 0, 82, 1 ),
                          Prop02 = new Prop            ( 1, 24, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 31, 1 ),
                          Comp04 = new Component       ( 4, 48, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 55, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 54, 1 ),
                          Comp09 = new Component       ( 9, 13, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 32, 1 )
                      },

                   #endregion SAHP

                      #region SASP

                new Outfit (Categories.SASP, true, "SASP Rettungsschwimmer 2 (Langärmlig)")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 5, 1 ),
                          Comp04 = new Component       ( 4, 48, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 54, 2 ),
                          Comp09 = new Component       ( 9, 15, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 81, 2 )
                      },

                      new Outfit (Categories.SASP, true, "SASP Ranger (Langärmlig)")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 5, 1 ),
                          Comp04 = new Component       ( 4, 48, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 54, 2 ),
                          Comp09 = new Component       ( 9, 15, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 81, 1 )
                      },

                      new Outfit (Categories.SASP, true, "SASP Rettungsschwimmer 2 (Kurzärmlig)")
                      {
                          Prop01 = new Prop            ( 0, 11, 4 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 1, 1 ),
                          Comp04 = new Component       ( 4, 48, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 54, 2 ),
                          Comp09 = new Component       ( 9, 14, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 82, 2 )
                      },

                      new Outfit (Categories.SASP, false, "SASP Rettungsschwimmer 2 (Kurzärmlig)")
                      {
                          Prop01 = new Prop            ( 0, 11, 7 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 15, 1 ),
                          Comp04 = new Component       ( 4, 50, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 53, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 32, 1 ),
                          Comp09 = new Component       ( 9, 15, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 41, 2 )
                      },

                      new Outfit (Categories.SASP, true, "SASP Ranger (Kurzärmlig)")
                      {
                          Prop01 = new Prop            ( 0, 11, 2 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 4, 1 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 1, 1 ),
                          Comp04 = new Component       ( 4, 48, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 54, 2 ),
                          Comp09 = new Component       ( 9, 14, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 82, 1 )
                      },

                      new Outfit (Categories.SASP, false, "SASP Ranger (Kurzärmlig)")
                      {
                          Prop01 = new Prop            ( 0, 11, 2 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 15, 1 ),
                          Comp04 = new Component       ( 4, 50, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 53, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 32, 1 ),
                          Comp09 = new Component       ( 9, 15, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 41, 1 )
                      },

                      new Outfit (Categories.SASP, true, "SASP Jacke")
                      {
                          Prop01 = new Prop            ( 0, 11, 2 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 5, 1 ),
                          Comp04 = new Component       ( 4, 48, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 66, 17 ),
                          Comp09 = new Component       ( 9, 2, 3 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 157, 2 )
                      },

                      new Outfit (Categories.SASP, false, "SASP Jacke")
                      {
                          Prop01 = new Prop            ( 0, 11, 2 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 4, 1 ),
                          Comp04 = new Component       ( 4, 50, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 53, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 46, 17 ),
                          Comp09 = new Component       ( 9, 2, 3 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 154, 2 )
                      },

                      new Outfit (Categories.SASP, true, "SASP Ranger Winteruniform")
                      {
                          Prop01 = new Prop            ( 0, 9, 4 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 39, 1 ),
                          Comp04 = new Component       ( 4, 48, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 54, 1 ),
                          Comp09 = new Component       ( 9, 27, 4 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 18, 5 )
                      },

                      new Outfit (Categories.SASP, false, "SASP Ranger Winteruniform")
                      {
                          Prop01 = new Prop            ( 0, 9, 4 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 37, 1 ),
                          Comp04 = new Component       ( 4, 50, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 53, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 32, 1 ),
                          Comp09 = new Component       ( 9, 29, 4 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 22, 5 )
                      },

                      new Outfit (Categories.SASP, true, "SASP Rettungsschwimmer 2 Winteruniform")
                      {
                          Prop01 = new Prop            ( 0, 3, 2 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 39, 1 ),
                          Comp04 = new Component       ( 4, 48, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 54, 1 ),
                          Comp09 = new Component       ( 9, 27, 5 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 18, 6 )
                      },

                      new Outfit (Categories.SASP, false, "SASP Rettungsschwimmer 2 Winteruniform")
                      {
                          Prop01 = new Prop            ( 0, 6, 2 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 37, 1 ),
                          Comp04 = new Component       ( 4, 50, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 53, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 32, 1 ),
                          Comp09 = new Component       ( 9, 29, 5 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 22, 6 )
                      },

                   #endregion SASP

                      #region BCSO

                new Outfit (Categories.BCSO, true, "BCSO Uniform (Langärmlig)")
                      {
                          Prop01 = new Prop            ( 0, 14, 2 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 5, 1 ),
                          Comp04 = new Component       ( 4, 26, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 39, 1 ),
                          Comp09 = new Component       ( 9, 15, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 25, 1 )
                      },

                      new Outfit (Categories.BCSO, true, "BCSO Uniform (Kurzärmlig)")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 1, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 4, 1 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 1, 1 ),
                          Comp04 = new Component       ( 4, 26, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 45, 1 ),
                          Comp09 = new Component       ( 9, 14, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 75, 1 )
                      },

                      new Outfit (Categories.BCSO, false, "BCSO Uniform")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 15, 1 ),
                          Comp04 = new Component       ( 4, 42, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 53, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 53, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 111, 1 )
                      },

                      new Outfit (Categories.BCSO, true, "BCSO Jacke Wanrschutzkleidung")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 1, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 5, 1 ),
                          Comp04 = new Component       ( 4, 26, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 66, 1 ),
                          Comp09 = new Component       ( 9, 3, 2 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 157, 1 )
                      },

                      new Outfit (Categories.BCSO, true, "BCSO Jacke")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 1, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 5, 1 ),
                          Comp04 = new Component       ( 4, 26, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 66, 1 ),
                          Comp09 = new Component       ( 9, 3, 2 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 157, 4 )
                      },

                      new Outfit (Categories.BCSO, false, "BCSO Jacke")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 4, 1 ),
                          Comp04 = new Component       ( 4, 42, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 53, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 46, 1 ),
                          Comp09 = new Component       ( 9, 3, 2 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 154, 4 )
                      },

                      new Outfit (Categories.BCSO, true, "BCSO Motorraduniform")
                      {
                          Prop01 = new Prop            ( 0, 18, 4 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 23, 1 ),
                          Comp04 = new Component       ( 4, 33, 3 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 31, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 39, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 25, 1 )
                      },

                      new Outfit (Categories.BCSO, false, "BCSO Motorraduniform")
                      {
                          Prop01 = new Prop            ( 0, 18, 4 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 15, 1 ),
                          Comp04 = new Component       ( 4, 32, 3 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 10, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 52, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 111, 1 )
                      },

                      new Outfit (Categories.BCSO, true, "BCSO K-9 Uniform")
                      {
                          Prop01 = new Prop            ( 0, 11, 8 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 1, 1 ),
                          Comp04 = new Component       ( 4, 60, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 25, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 39, 1 ),
                          Comp09 = new Component       ( 9, 15, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 184, 1 )
                      },

                      new Outfit (Categories.BCSO, false, "BCSO K-9 Uniform")
                      {
                          Prop01 = new Prop            ( 0, 11, 8 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 15, 1 ),
                          Comp04 = new Component       ( 4, 55, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 25, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 52, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 92, 1 )
                      },

                      new Outfit (Categories.BCSO, true, "BCSO Anzug")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 13, 1 ),
                          Comp04 = new Component       ( 4, 11, 4 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 11, 1 ),
                          Comp07 = new Component       ( 7, 39, 9 ),
                          Comp08 = new Component       ( 8, 11, 4 ),
                          Comp09 = new Component       ( 9, 24, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 5, 2 )
                      },

                      new Outfit (Categories.BCSO, false, "BCSO Anzug")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 8, 1 ),
                          Comp04 = new Component       ( 4, 4, 9 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 30, 1 ),
                          Comp07 = new Component       ( 7, 1, 1 ),
                          Comp08 = new Component       ( 8, 65, 2 ),
                          Comp09 = new Component       ( 9, 26, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 25, 3 )
                      },

                      new Outfit (Categories.BCSO, true, "BCSO Ermittler")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 7, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 5, 1 ),
                          Comp04 = new Component       ( 4, 11, 4 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 11, 1 ),
                          Comp07 = new Component       ( 7, 7, 1 ),
                          Comp08 = new Component       ( 8, 81, 9 ),
                          Comp09 = new Component       ( 9, 24, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 4, 4 )
                      },

                      new Outfit (Categories.BCSO, true, "BCSO Ermittler (Weste)")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 7, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 5, 1 ),
                          Comp04 = new Component       ( 4, 11, 4 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 11, 1 ),
                          Comp07 = new Component       ( 7, 7, 1 ),
                          Comp08 = new Component       ( 8, 42, 3 ),
                          Comp09 = new Component       ( 9, 24, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 4, 4 )
                      },

                      new Outfit (Categories.BCSO, false, "BCSO Ermittler")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 15, 1 ),
                          Comp04 = new Component       ( 4, 4, 6 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 30, 1 ),
                          Comp07 = new Component       ( 7, 7, 1 ),
                          Comp08 = new Component       ( 8, 8, 1 ),
                          Comp09 = new Component       ( 9, 26, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 15, 16 )
                      },

                      new Outfit (Categories.BCSO, true, "BCSO Stellvertretender Sheriff")
                      {
                          Prop01 = new Prop            ( 0, 31, 2 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 1, 1 ),
                          Comp04 = new Component       ( 4, 1, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 39, 4 ),
                          Comp07 = new Component       ( 7, 7, 1 ),
                          Comp08 = new Component       ( 8, 89, 1 ),
                          Comp09 = new Component       ( 9, 24, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 134, 1 )
                      },

                      new Outfit (Categories.BCSO, true, "BCSO Windjacke")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 5, 1 ),
                          Comp04 = new Component       ( 4, 1, 2 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 11, 1 ),
                          Comp07 = new Component       ( 7, 7, 1 ),
                          Comp08 = new Component       ( 8, 66, 1 ),
                          Comp09 = new Component       ( 9, 24, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 36, 5 )
                      },

                      new Outfit (Categories.BCSO, true, "BCSO Winteruniform")
                      {
                          Prop01 = new Prop            ( 0, 9, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 39, 1 ),
                          Comp04 = new Component       ( 4, 26, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 45, 1 ),
                          Comp09 = new Component       ( 9, 27, 2 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 18, 3 )
                      },

                      new Outfit (Categories.BCSO, false, "BCSO Winteruniform")
                      {
                          Prop01 = new Prop            ( 0, 9, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 37, 1 ),
                          Comp04 = new Component       ( 4, 42, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 53, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 52, 1 ),
                          Comp09 = new Component       ( 9, 29, 2 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 22, 3 )
                      },

                      new Outfit (Categories.BCSO, true, "BCSO Fahrraduniform")
                      {
                          Prop01 = new Prop            ( 0, 50, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 20, 1 ),
                          Comp04 = new Component       ( 4, 13, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 3, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 38, 1 ),
                          Comp09 = new Component       ( 9, 15, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 95, 2 )
                      },

                      new Outfit (Categories.BCSO, false, "BCSO Fahrraduniform")
                      {
                          Prop01 = new Prop            ( 0, 48, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 32, 1 ),
                          Comp04 = new Component       ( 4, 15, 4 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 11, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 3, 1 ),
                          Comp09 = new Component       ( 9, 2, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 86, 2 )
                      },

                      new Outfit (Categories.BCSO, true, "BCSO Pilotenuniform")
                      {
                          Prop01 = new Prop            ( 0, 80, 3 ),
                          Prop02 = new Prop            ( 1, 1, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 97, 1 ),
                          Comp04 = new Component       ( 4, 39, 3 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 25, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 44, 3 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 16, 4 ),
                          Comp11 = new Component       ( 11, 66, 3 )
                      },

                      new Outfit (Categories.BCSO, false, "BCSO Pilotenuniform")
                      {
                          Prop01 = new Prop            ( 0, 79, 3 ),
                          Prop02 = new Prop            ( 1, 14, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 37, 1 ),
                          Comp04 = new Component       ( 4, 39, 3 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 25, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 31, 3 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 15, 4 ),
                          Comp11 = new Component       ( 11, 60, 3 )
                      },

                      new Outfit (Categories.BCSO, true, "BCSO SWAT Uniform")
                      {
                          Prop01 = new Prop            ( 0, 76, 2 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 97, 1 ),
                          Comp04 = new Component       ( 4, 32, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 26, 1 ),
                          Comp07 = new Component       ( 7, 111, 1 ),
                          Comp08 = new Component       ( 8, 16, 1 ),
                          Comp09 = new Component       ( 9, 28, 1 ),
                          Comp10 = new Component       ( 10, 7, 1 ),
                          Comp11 = new Component       ( 11, 50, 2 )
                      },

                      new Outfit (Categories.BCSO, false, "BCSO SWAT Uniform")
                      {
                          Prop01 = new Prop            ( 0, 75, 2 ),
                          Prop02 = new Prop            ( 1, 26, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 112, 1 ),
                          Comp04 = new Component       ( 4, 31, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 26, 1 ),
                          Comp07 = new Component       ( 7, 82, 1 ),
                          Comp08 = new Component       ( 8, 16, 1 ),
                          Comp09 = new Component       ( 9, 30, 1 ),
                          Comp10 = new Component       ( 10, 12, 4 ),
                          Comp11 = new Component       ( 11, 43, 2 )
                      },

                      new Outfit (Categories.BCSO, true, "BCSO Aufstandsuniform")
                      {
                          Prop01 = new Prop            ( 0, 82, 3 ),
                          Prop02 = new Prop            ( 1, 1, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 31, 1 ),
                          Comp04 = new Component       ( 4, 60, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 25, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 39, 1 ),
                          Comp09 = new Component       ( 9, 13, 2 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 75, 1 )
                      },

                   #endregion BCSO

                      #region FBI

                new Outfit (Categories.FBI, true, "FBI Anzug")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 13, 1 ),
                          Comp04 = new Component       ( 4, 11, 1 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 11, 1 ),
                          Comp07 = new Component       ( 7, 11, 1 ),
                          Comp08 = new Component       ( 8, 11, 1 ),
                          Comp09 = new Component       ( 9, 23, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 5, 1 )
                      },

                      new Outfit (Categories.FBI, false, "FBI Anzug")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 8, 1 ),
                          Comp04 = new Component       ( 4, 4, 1 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 30, 1 ),
                          Comp07 = new Component       ( 7, 1, 1 ),
                          Comp08 = new Component       ( 8, 65, 3 ),
                          Comp09 = new Component       ( 9, 25, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 25, 1 )
                      },

                      new Outfit (Categories.FBI, true, "FBI Agent")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 7, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 5, 1 ),
                          Comp04 = new Component       ( 4, 11, 5 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 11, 1 ),
                          Comp07 = new Component       ( 7, 13, 13 ),
                          Comp08 = new Component       ( 8, 17, 1 ),
                          Comp09 = new Component       ( 9, 23, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 4, 4 )
                      },

                      new Outfit (Categories.FBI, false, "FBI Agent")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 1, 1 ),
                          Comp04 = new Component       ( 4, 4, 1 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 30, 1 ),
                          Comp07 = new Component       ( 7, 1, 1 ),
                          Comp08 = new Component       ( 8, 10, 1 ),
                          Comp09 = new Component       ( 9, 25, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 28, 1 )
                      },

                      new Outfit (Categories.FBI, true, "Regierungsanzug")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 13, 1 ),
                          Comp04 = new Component       ( 4, 11, 1 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 11, 1 ),
                          Comp07 = new Component       ( 7, 116, 1 ),
                          Comp08 = new Component       ( 8, 11, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 11, 1 )
                      },

                      new Outfit (Categories.FBI, false, "Regierungsanzug")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 8, 1 ),
                          Comp04 = new Component       ( 4, 38, 1 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 30, 1 ),
                          Comp07 = new Component       ( 7, 87, 1 ),
                          Comp08 = new Component       ( 8, 39, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 59, 1 )
                      },

                      new Outfit (Categories.FBI, true, "FBI Zugriff")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 5, 1 ),
                          Comp04 = new Component       ( 4, 11, 5 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 11, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 42, 4 ),
                          Comp09 = new Component       ( 9, 20, 4 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 4, 4 )
                      },

                      new Outfit (Categories.FBI, true, "FBI Windjacke")
                      {
                          Prop01 = new Prop            ( 0, 11, 5 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 13, 1 ),
                          Comp04 = new Component       ( 4, 11, 5 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 21, 1 ),
                          Comp07 = new Component       ( 7, 7, 1 ),
                          Comp08 = new Component       ( 8, 12, 4 ),
                          Comp09 = new Component       ( 9, 21, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 36, 1 )
                      },

                      new Outfit (Categories.FBI, true, "FBI Polizeiuniform")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 1, 1 ),
                          Comp04 = new Component       ( 4, 48, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 55, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 38, 1 ),
                          Comp09 = new Component       ( 9, 14, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 33, 1 )
                      },

                      new Outfit (Categories.FBI, true, "FBI ATU Uniform")
                      {
                          Prop01 = new Prop            ( 0, 40, 1 ),
                          Prop02 = new Prop            ( 1, 24, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 17, 1 ),
                          Comp04 = new Component       ( 4, 32, 5 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 36, 1 ),
                          Comp07 = new Component       ( 7, 43, 1 ),
                          Comp08 = new Component       ( 8, 16, 1 ),
                          Comp09 = new Component       ( 9, 28, 4 ),
                          Comp10 = new Component       ( 10, 11, 1 ),
                          Comp11 = new Component       ( 11, 50, 2 )
                      },

                      new Outfit (Categories.FBI, false, "FBI ATU Uniform")
                      {
                          Prop01 = new Prop            ( 0, 39, 1 ),
                          Prop02 = new Prop            ( 1, 26, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 18, 1 ),
                          Comp04 = new Component       ( 4, 31, 5 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 37, 2 ),
                          Comp07 = new Component       ( 7, 30, 1 ),
                          Comp08 = new Component       ( 8, 16, 1 ),
                          Comp09 = new Component       ( 9, 30, 4 ),
                          Comp10 = new Component       ( 10, 13, 1 ),
                          Comp11 = new Component       ( 11, 43, 2 )
                      },

                   #endregion FBI

                      #region DEA

                new Outfit (Categories.DEA, true, "DEA Anzug")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 5, 1 ),
                          Comp04 = new Component       ( 4, 11, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 11, 1 ),
                          Comp07 = new Component       ( 7, 39, 9 ),
                          Comp08 = new Component       ( 8, 11, 2 ),
                          Comp09 = new Component       ( 9, 23, 3 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 5, 1 )
                      },

                      new Outfit (Categories.DEA, true, "DEA Agent")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 5, 1 ),
                          Comp04 = new Component       ( 4, 11, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 11, 1 ),
                          Comp07 = new Component       ( 7, 7, 1 ),
                          Comp08 = new Component       ( 8, 81, 9 ),
                          Comp09 = new Component       ( 9, 23, 3 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 4, 2 )
                      },

                      new Outfit (Categories.DEA, false, "DEA Agent")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 4, 1 ),
                          Comp04 = new Component       ( 4, 4, 3 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 30, 1 ),
                          Comp07 = new Component       ( 7, 1, 1 ),
                          Comp08 = new Component       ( 8, 40, 1 ),
                          Comp09 = new Component       ( 9, 25, 3 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 25, 5 )
                      },

                      new Outfit (Categories.DEA, true, "DEA Zugriff")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 1, 1 ),
                          Comp04 = new Component       ( 4, 50, 4 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 26, 1 ),
                          Comp07 = new Component       ( 7, 2, 1 ),
                          Comp08 = new Component       ( 8, 88, 1 ),
                          Comp09 = new Component       ( 9, 8, 5 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 3, 4 )
                      },

                      new Outfit (Categories.DEA, false, "DEA Operativ")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 15, 1 ),
                          Comp04 = new Component       ( 4, 52, 5 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 51, 1 ),
                          Comp07 = new Component       ( 7, 7, 1 ),
                          Comp08 = new Component       ( 8, 8, 1 ),
                          Comp09 = new Component       ( 9, 24, 4 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 15, 1 )
                      },

                      new Outfit (Categories.DEA, true, "DEA Windjacke")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 13, 1 ),
                          Comp04 = new Component       ( 4, 11, 1 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 11, 1 ),
                          Comp07 = new Component       ( 7, 7, 1 ),
                          Comp08 = new Component       ( 8, 12, 2 ),
                          Comp09 = new Component       ( 9, 21, 4 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 36, 3 )
                      },

                      new Outfit (Categories.DEA, true, "DEA SRT Uniform")
                      {
                          Prop01 = new Prop            ( 0, 89, 1 ),
                          Prop02 = new Prop            ( 1, 23, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 97, 1 ),
                          Comp04 = new Component       ( 4, 32, 3 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 26, 1 ),
                          Comp07 = new Component       ( 7, 111, 1 ),
                          Comp08 = new Component       ( 8, 16, 1 ),
                          Comp09 = new Component       ( 9, 28, 5 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 50, 3 )
                      },

                      new Outfit (Categories.DEA, false, "DEA SRT Uniform")
                      {
                          Prop01 = new Prop            ( 0, 88, 1 ),
                          Prop02 = new Prop            ( 1, 24, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 112, 1 ),
                          Comp04 = new Component       ( 4, 31, 3 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 26, 1 ),
                          Comp07 = new Component       ( 7, 82, 1 ),
                          Comp08 = new Component       ( 8, 16, 1 ),
                          Comp09 = new Component       ( 9, 30, 5 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 43, 3 )
                      },

                   #endregion DEA

                      #region NOOSE

                new Outfit (Categories.NOOSE, true, "PIA Spezialagent")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 13, 1 ),
                          Comp04 = new Component       ( 4, 11, 5 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 11, 1 ),
                          Comp07 = new Component       ( 7, 14, 1 ),
                          Comp08 = new Component       ( 8, 12, 1 ),
                          Comp09 = new Component       ( 9, 23, 2 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 5, 5 )
                      },

                      new Outfit (Categories.NOOSE, false, "PIA Spezialagent")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 4, 1 ),
                          Comp04 = new Component       ( 4, 4, 5 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 30, 1 ),
                          Comp07 = new Component       ( 7, 1, 1 ),
                          Comp08 = new Component       ( 8, 40, 3 ),
                          Comp09 = new Component       ( 9, 21, 6 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 25, 4 )
                      },

                      new Outfit (Categories.NOOSE, true, "PIA Polizei")
                      {
                          Prop01 = new Prop            ( 0, 11, 6 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 1, 1 ),
                          Comp04 = new Component       ( 4, 53, 4 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 26, 1 ),
                          Comp07 = new Component       ( 7, 6, 1 ),
                          Comp08 = new Component       ( 8, 89, 1 ),
                          Comp09 = new Component       ( 9, 23, 2 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 52, 3 )
                      },

                      new Outfit (Categories.NOOSE, false, "PIA Polizei")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 15, 1 ),
                          Comp04 = new Component       ( 4, 55, 4 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 25, 1 ),
                          Comp07 = new Component       ( 7, 7, 1 ),
                          Comp08 = new Component       ( 8, 8, 1 ),
                          Comp09 = new Component       ( 9, 25, 2 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 74, 1 )
                      },

                      new Outfit (Categories.NOOSE, true, "PIA Zugriff")
                      {
                          Prop01 = new Prop            ( 0, 11, 6 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 1, 1 ),
                          Comp04 = new Component       ( 4, 50, 4 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 2, 1 ),
                          Comp08 = new Component       ( 8, 38, 1 ),
                          Comp09 = new Component       ( 9, 13, 5 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 52, 3 )
                      },

                      new Outfit (Categories.NOOSE, true, "PIA Windjacke")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 13, 1 ),
                          Comp04 = new Component       ( 4, 50, 4 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 55, 1 ),
                          Comp07 = new Component       ( 7, 7, 1 ),
                          Comp08 = new Component       ( 8, 29, 4 ),
                          Comp09 = new Component       ( 9, 21, 3 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 36, 4 )
                      },

                      new Outfit (Categories.NOOSE, false, "PIA Zugriff")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 15, 1 ),
                          Comp04 = new Component       ( 4, 5, 10 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 26, 1 ),
                          Comp07 = new Component       ( 7, 2, 1 ),
                          Comp08 = new Component       ( 8, 3, 1 ),
                          Comp09 = new Component       ( 9, 8, 4 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 74, 1 )
                      },

                      new Outfit (Categories.NOOSE, true, "PIA Uniform")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 1, 1 ),
                          Comp04 = new Component       ( 4, 47, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 25, 1 ),
                          Comp07 = new Component       ( 7, 2, 1 ),
                          Comp08 = new Component       ( 8, 38, 1 ),
                          Comp09 = new Component       ( 9, 15, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 99, 1 )
                      },

                      new Outfit (Categories.NOOSE, false, "PIA Uniform")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 15, 1 ),
                          Comp04 = new Component       ( 4, 49, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 26, 1 ),
                          Comp07 = new Component       ( 7, 2, 1 ),
                          Comp08 = new Component       ( 8, 3, 1 ),
                          Comp09 = new Component       ( 9, 15, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 90, 1 )
                      },

                      new Outfit (Categories.NOOSE, true, "Grenzschutz")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 1, 1 ),
                          Comp04 = new Component       ( 4, 53, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 28, 1 ),
                          Comp07 = new Component       ( 7, 2, 1 ),
                          Comp08 = new Component       ( 8, 38, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 98, 1 )
                      },

                      new Outfit (Categories.NOOSE, false, "Grenzschutz")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 15, 1 ),
                          Comp04 = new Component       ( 4, 55, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 26, 1 ),
                          Comp07 = new Component       ( 7, 2, 1 ),
                          Comp08 = new Component       ( 8, 3, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 89, 1 )
                      },

                      new Outfit (Categories.NOOSE, true, "SEP Uniform")
                      {
                          Prop01 = new Prop            ( 0, 11, 6 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 5, 1 ),
                          Comp04 = new Component       ( 4, 11, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 21, 2 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 20, 1 )
                      },

                      new Outfit (Categories.NOOSE, true, "NOOSE TRU Uniform")
                      {
                          Prop01 = new Prop            ( 0, 76, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 97, 1 ),
                          Comp04 = new Component       ( 4, 38, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 26, 1 ),
                          Comp07 = new Component       ( 7, 111, 1 ),
                          Comp08 = new Component       ( 8, 16, 1 ),
                          Comp09 = new Component       ( 9, 17, 3 ),
                          Comp10 = new Component       ( 10, 6, 1 ),
                          Comp11 = new Component       ( 11, 54, 1 )
                      },

                      new Outfit (Categories.NOOSE, false, "NOOSE TRU Uniform")
                      {
                          Prop01 = new Prop            ( 0, 75, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 112, 1 ),
                          Comp04 = new Component       ( 4, 37, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 26, 1 ),
                          Comp07 = new Component       ( 7, 82, 1 ),
                          Comp08 = new Component       ( 8, 16, 1 ),
                          Comp09 = new Component       ( 9, 19, 3 ),
                          Comp10 = new Component       ( 10, 12, 3 ),
                          Comp11 = new Component       ( 11, 47, 1 )
                      },

                      new Outfit (Categories.NOOSE, true, "Juggernaut")
                      {
                          Prop01 = new Prop            ( 0, 94, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 111, 4 ),
                          Comp04 = new Component       ( 4, 85, 1 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 14, 1 ),
                          Comp07 = new Component       ( 7, 1, 1 ),
                          Comp08 = new Component       ( 8, 98, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 187, 1 )
                      },

                      new Outfit (Categories.NOOSE, false, "Juggernaut")
                      {
                          Prop01 = new Prop            ( 0, 93, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 128, 4 ),
                          Comp04 = new Component       ( 4, 87, 1 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 35, 1 ),
                          Comp07 = new Component       ( 7, 1, 1 ),
                          Comp08 = new Component       ( 8, 106, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 189, 1 )
                      },

                   #endregion NOOSE

                      #region USAF

                new Outfit (Categories.USAF, true, "USAF Polizei")
                      {
                          Prop01 = new Prop            ( 0, 29, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 1, 1 ),
                          Comp04 = new Component       ( 4, 60, 4 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 36, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 56, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 22, 1 ),
                          Comp11 = new Component       ( 11, 64, 1 )
                      },

                      new Outfit (Categories.USAF, false, "USAF Polizei")
                      {
                          Prop01 = new Prop            ( 0, 29, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 15, 1 ),
                          Comp04 = new Component       ( 4, 62, 4 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 37, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 33, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 21, 1 ),
                          Comp11 = new Component       ( 11, 57, 1 )
                      },

                      new Outfit (Categories.USAF, true, "USAF Uniform")
                      {
                          Prop01 = new Prop            ( 0, 29, 3 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 1, 1 ),
                          Comp04 = new Component       ( 4, 60, 4 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 36, 1 ),
                          Comp07 = new Component       ( 7, 1, 1 ),
                          Comp08 = new Component       ( 8, 3, 2 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 24, 1 ),
                          Comp11 = new Component       ( 11, 64, 1 )
                      },

                      new Outfit (Categories.USAF, false, "USAF Uniform")
                      {
                          Prop01 = new Prop            ( 0, 29, 3 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 15, 1 ),
                          Comp04 = new Component       ( 4, 62, 4 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 37, 1 ),
                          Comp07 = new Component       ( 7, 1, 1 ),
                          Comp08 = new Component       ( 8, 15, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 23, 1 ),
                          Comp11 = new Component       ( 11, 57, 1 )
                      },

                      new Outfit (Categories.USAF, true, "USAF Pilotenuniform")
                      {
                          Prop01 = new Prop            ( 0, 39, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 17, 1 ),
                          Comp04 = new Component       ( 4, 31, 1 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 25, 1 ),
                          Comp07 = new Component       ( 7, 34, 1 ),
                          Comp08 = new Component       ( 8, 16, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 49, 1 )
                      },

                      new Outfit (Categories.USAF, false, "USAF Pilotenuniform")
                      {
                          Prop01 = new Prop            ( 0, 38, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 18, 1 ),
                          Comp04 = new Component       ( 4, 30, 1 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 25, 1 ),
                          Comp07 = new Component       ( 7, 17, 1 ),
                          Comp08 = new Component       ( 8, 16, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 42, 1 )
                      },

                   #endregion USAF

                      #region SADCR

                new Outfit (Categories.SADCR, true, "SADCR Uniform")
                      {
                          Prop01 = new Prop            ( 0, 45, 2 ),
                          Prop02 = new Prop            ( 1, 1, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 1, 1 ),
                          Comp04 = new Component       ( 4, 26, 3 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 39, 2 ),
                          Comp09 = new Component       ( 9, 14, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 102, 1 )
                      },

                      new Outfit (Categories.SADCR, true, "SADCR Kaki Uniform")
                      {
                          Prop01 = new Prop            ( 0, 82, 3 ),
                          Prop02 = new Prop            ( 1, 1, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 1, 1 ),
                          Comp04 = new Component       ( 4, 53, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 25, 1 ),
                          Comp07 = new Component       ( 7, 2, 1 ),
                          Comp08 = new Component       ( 8, 38, 1 ),
                          Comp09 = new Component       ( 9, 14, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 102, 2 )
                      },

                      new Outfit (Categories.SADCR, false, "SADCR Uniform")
                      {
                          Prop01 = new Prop            ( 0, 44, 2 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 15, 1 ),
                          Comp04 = new Component       ( 4, 42, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 53, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 53, 2 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 39, 1 )
                      },

                      new Outfit (Categories.SADCR, false, "SADCR Kaki Uniform")
                      {
                          Prop01 = new Prop            ( 0, 80, 3 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 15, 1 ),
                          Comp04 = new Component       ( 4, 55, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 25, 1 ),
                          Comp07 = new Component       ( 7, 2, 1 ),
                          Comp08 = new Component       ( 8, 3, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component      ( 11, 39, 2 )
                      },

                   #endregion SADCR

                      #region LSIA

                new Outfit (Categories.LSIA, true, "LSIA Uniform")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 4, 1 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 1, 1 ),
                          Comp04 = new Component       ( 4, 36, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 58, 1 ),
                          Comp09 = new Component       ( 9, 14, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 31, 1 )
                      },

                      new Outfit (Categories.LSIA, false, "LSIA Uniform")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 15, 1 ),
                          Comp04 = new Component       ( 4, 35, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 53, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 36, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 17, 7 )
                      },

                      new Outfit (Categories.LSIA, true, "LSIA Winteruniform")
                      {
                          Prop01 = new Prop            ( 0, 3, 2 ),
                          Prop02 = new Prop            ( 1, 1, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 39, 1 ),
                          Comp04 = new Component       ( 4, 36, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 58, 1 ),
                          Comp09 = new Component       ( 9, 27, 6 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 124, 1 )
                      },

                      new Outfit (Categories.LSIA, false, "LSIA Winteruniform")
                      {
                          Prop01 = new Prop            ( 0, 6, 2 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 37, 1 ),
                          Comp04 = new Component       ( 4, 35, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 53, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 35, 1 ),
                          Comp09 = new Component       ( 9, 29, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 120, 1 )
                      },

                   #endregion LSIA

                      #region LSPA

                new Outfit (Categories.LSPA, true, "Hafenbehörde Uniform")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 1, 1 ),
                          Comp04 = new Component       ( 4, 36, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 59, 1 ),
                          Comp09 = new Component       ( 9, 14, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 30, 1 )
                      },

                      new Outfit (Categories.LSPA, false, "Hafenbehörde Uniform")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 15, 1 ),
                          Comp04 = new Component       ( 4, 35, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 53, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 36, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 32, 1 )
                      },

                      new Outfit (Categories.LSPA, true, "PAP Motorraduniform")
                      {
                          Prop01 = new Prop            ( 0, 49, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 20, 1 ),
                          Comp04 = new Component       ( 4, 33, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 14, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 58, 1 ),
                          Comp09 = new Component       ( 9, 14, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 30, 1 )
                      },

                      new Outfit (Categories.LSPA, false, "PAP Motorraduniform")
                      {
                          Prop01 = new Prop            ( 0, 49, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 15, 1 ),
                          Comp04 = new Component       ( 4, 32, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 10, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 35, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 32, 1 )
                      },

                      new Outfit (Categories.LSPA, true, "PAP Marineuniform")
                      {
                          Prop01 = new Prop            ( 0, 45, 8 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 1, 1 ),
                          Comp04 = new Component       ( 4, 47, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 25, 1 ),
                          Comp07 = new Component       ( 7, 2, 1 ),
                          Comp08 = new Component       ( 8, 88, 1 ),
                          Comp09 = new Component       ( 9, 26, 2 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 30, 1 )
                      },

                      new Outfit (Categories.LSPA, false, "PAP Marineuniform")
                      {
                          Prop01 = new Prop            ( 0, 44, 8 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 15, 1 ),
                          Comp04 = new Component       ( 4, 49, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 26, 1 ),
                          Comp07 = new Component       ( 7, 2, 1 ),
                          Comp08 = new Component       ( 8, 66, 1 ),
                          Comp09 = new Component       ( 9, 28, 2 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 32, 1 )
                      },

                      new Outfit (Categories.LSPA, true, "PAP Winteruniform")
                      {
                          Prop01 = new Prop            ( 0, 3, 2 ),
                          Prop02 = new Prop            ( 1, 1, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 39, 1 ),
                          Comp04 = new Component       ( 4, 36, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 58, 1 ),
                          Comp09 = new Component       ( 9, 27, 6 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 124, 2 )
                      },

                      new Outfit (Categories.LSPA, false, "PAP Winteruniform")
                      {
                          Prop01 = new Prop            ( 0, 6, 2 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 37, 1 ),
                          Comp04 = new Component       ( 4, 35, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 53, 1 ),
                          Comp07 = new Component       ( 7, 9, 1 ),
                          Comp08 = new Component       ( 8, 35, 1 ),
                          Comp09 = new Component       ( 9, 29, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 120, 2 )
                      },

                   #endregion LSPA

                      #region LSFD

                      new Outfit (Categories.LSFD, true, "LSFD Fehrwehruniform (Brand)")
                      {
                          Prop01 = new Prop            ( 0, 46, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 97, 1 ),
                          Comp04 = new Component       ( 4, 44, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 14, 1 ),
                          Comp07 = new Component       ( 7, 1, 1 ),
                          Comp08 = new Component       ( 8, 69, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 17, 1 ),
                          Comp11 = new Component       ( 11, 78, 1 )
                      },

                      new Outfit (Categories.LSFD, false, "LSFD Fehrwehruniform (Brand)")
                      {
                          Prop01 = new Prop            ( 0, 45, 1 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 112, 1 ),
                          Comp04 = new Component       ( 4, 19, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 29, 1 ),
                          Comp07 = new Component       ( 7, 1, 1 ),
                          Comp08 = new Component       ( 8, 49, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 16, 1 ),
                          Comp11 = new Component       ( 11, 65, 1 )
                      },

                      new Outfit (Categories.LSFD, true, "LSFD Feuerwehruniform")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 1, 1 ),
                          Comp04 = new Component       ( 4, 26, 4 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 1, 1 ),
                          Comp08 = new Component       ( 8, 89, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 48, 2 )
                      },

                      new Outfit (Categories.LSFD, false, "LSFD Feuerwehruniform")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 15, 1 ),
                          Comp04 = new Component       ( 4, 42, 4 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 53, 1 ),
                          Comp07 = new Component       ( 7, 1, 1 ),
                          Comp08 = new Component       ( 8, 8, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 21, 2 )
                      },

                      new Outfit (Categories.LSFD, true, "LSFD EMT Uniform")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 86, 1 ),
                          Comp04 = new Component       ( 4, 48, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 31, 4 ),
                          Comp08 = new Component       ( 8, 55, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 48, 1 )
                      },

                      new Outfit (Categories.LSFD, false, "LSFD EMT Uniform")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 110, 1 ),
                          Comp04 = new Component       ( 4, 50, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 53, 1 ),
                          Comp07 = new Component       ( 7, 15, 4 ),
                          Comp08 = new Component       ( 8, 7, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 21, 1 )
                      },

                      new Outfit (Categories.LSFD, true, "LSFD Jacke")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 1, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 5, 1 ),
                          Comp04 = new Component       ( 4, 26, 4 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 1, 1 ),
                          Comp08 = new Component       ( 8, 52, 8 ),
                          Comp09 = new Component       ( 9, 2, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 152, 3 )
                      },

                      new Outfit (Categories.LSFD, false, "LSFD Jacke")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 4, 1 ),
                          Comp04 = new Component       ( 4, 42, 4 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 53, 1 ),
                          Comp07 = new Component       ( 7, 1, 1 ),
                          Comp08 = new Component       ( 8, 46, 19 ),
                          Comp09 = new Component       ( 9, 2, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 149, 3 )
                      },

                   #endregion LSFD

                      #region LSCoFD

                      new Outfit (Categories.LSCoFD, true, "LSCoFD Fehrwehruniform (Brand)")
                      {
                          Prop01 = new Prop            ( 0, 46, 4 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 97, 1 ),
                          Comp04 = new Component       ( 4, 44, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 14, 1 ),
                          Comp07 = new Component       ( 7, 1, 1 ),
                          Comp08 = new Component       ( 8, 69, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 18, 1 ),
                          Comp11 = new Component       ( 11, 78, 2 )
                      },

                      new Outfit (Categories.LSCoFD, false, "LSCoFD Fehrwehruniform (Brand)")
                      {
                          Prop01 = new Prop            ( 0, 45, 4 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 112, 1 ),
                          Comp04 = new Component       ( 4, 19, 2 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 29, 1 ),
                          Comp07 = new Component       ( 7, 1, 1 ),
                          Comp08 = new Component       ( 8, 49, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 17, 1 ),
                          Comp11 = new Component       ( 11, 65, 2 )
                      },

                      new Outfit (Categories.LSCoFD, true, "LSCoFD Feuerwehruniform")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 1, 1 ),
                          Comp04 = new Component       ( 4, 26, 4 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 1, 1 ),
                          Comp08 = new Component       ( 8, 89, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 35, 2 )
                      },

                      new Outfit (Categories.LSCoFD, false, "LSCoFD Feuerwehruniform")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 15, 1 ),
                          Comp04 = new Component       ( 4, 42, 4 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 53, 1 ),
                          Comp07 = new Component       ( 7, 1, 1 ),
                          Comp08 = new Component       ( 8, 8, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 20, 2 )
                      },

                      new Outfit (Categories.LSCoFD, true, "LSCoFD EMT Uniform")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 1, 1 ),
                          Comp04 = new Component       ( 4, 48, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 31, 2 ),
                          Comp08 = new Component       ( 8, 55, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 35, 1 )
                      },

                      new Outfit (Categories.LSCoFD, false, "LSCoFD EMT Uniform")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 15, 1 ),
                          Comp04 = new Component       ( 4, 42, 4 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 53, 1 ),
                          Comp07 = new Component       ( 7, 15, 1 ),
                          Comp08 = new Component       ( 8, 7, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 20, 1 )
                      },

                      new Outfit (Categories.LSCoFD, true, "LSCoFD Jacke")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 1, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 5, 1 ),
                          Comp04 = new Component       ( 4, 26, 4 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 1, 1 ),
                          Comp08 = new Component       ( 8, 52, 8 ),
                          Comp09 = new Component       ( 9, 2, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 152, 4 )
                      },

                      new Outfit (Categories.LSCoFD, false, "LSCoFD Jacke")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 4, 1 ),
                          Comp04 = new Component       ( 4, 42, 4 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 53, 1 ),
                          Comp07 = new Component       ( 7, 1, 1 ),
                          Comp08 = new Component       ( 8, 46, 19 ),
                          Comp09 = new Component       ( 9, 2, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 149, 4 )
                      },

                   #endregion LSCoFD

                      #region BCFD

                      new Outfit (Categories.BCFD, true, "BCFD Fehrwehruniform (Brand)")
                      {
                          Prop01 = new Prop            ( 0, 46, 7 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 97, 1 ),
                          Comp04 = new Component       ( 4, 44, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 14, 1 ),
                          Comp07 = new Component       ( 7, 1, 1 ),
                          Comp08 = new Component       ( 8, 69, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 19, 1 ),
                          Comp11 = new Component       ( 11, 78, 1 )
                      },

                      new Outfit (Categories.BCFD, false, "BCFD Fehrwehruniform (Brand)")
                      {
                          Prop01 = new Prop            ( 0, 45, 7 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 112, 1 ),
                          Comp04 = new Component       ( 4, 19, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 29, 1 ),
                          Comp07 = new Component       ( 7, 1, 1 ),
                          Comp08 = new Component       ( 8, 49, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 18, 1 ),
                          Comp11 = new Component       ( 11, 65, 1 )
                      },

                      new Outfit (Categories.BCFD, true, "BCFD Feuerwehruniform")
                      {
                          Prop01 = new Prop            ( 0, 46, 7 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 1, 1 ),
                          Comp04 = new Component       ( 4, 20, 2 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 19, 1 ),
                          Comp08 = new Component       ( 8, 89, 1 ),
                          Comp09 = new Component       ( 9, 15, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 41, 2 )
                      },

                      new Outfit (Categories.BCFD, false, "BCFD Feuerwehruniform")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 15, 1 ),
                          Comp04 = new Component       ( 4, 42, 4 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 53, 1 ),
                          Comp07 = new Component       ( 7, 1, 1 ),
                          Comp08 = new Component       ( 8, 8, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 20, 4 )
                      },

                      new Outfit (Categories.BCFD, true, "BCFD EMT Uniform")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 86, 1 ),
                          Comp04 = new Component       ( 4, 26, 4 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 31, 3 ),
                          Comp08 = new Component       ( 8, 89, 1 ),
                          Comp09 = new Component       ( 9, 15, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 41, 1 )
                      },

                      new Outfit (Categories.BCFD, false, "BCFD EMT Uniform")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 15, 1 ),
                          Comp04 = new Component       ( 4, 42, 4 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 53, 1 ),
                          Comp07 = new Component       ( 7, 15, 3 ),
                          Comp08 = new Component       ( 8, 7, 1 ),
                          Comp09 = new Component       ( 9, 15, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 20, 3 )
                      },

                      new Outfit (Categories.BCFD, true, "BCFD Jacke")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 1, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 5, 1 ),
                          Comp04 = new Component       ( 4, 26, 4 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 1, 1 ),
                          Comp08 = new Component       ( 8, 52, 9 ),
                          Comp09 = new Component       ( 9, 15, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 152, 5 )
                      },

                      new Outfit (Categories.BCFD, false, "BCFD Jacke")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 4, 1 ),
                          Comp04 = new Component       ( 4, 42, 4 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 53, 1 ),
                          Comp07 = new Component       ( 7, 1, 1 ),
                          Comp08 = new Component       ( 8, 46, 20 ),
                          Comp09 = new Component       ( 9, 2, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 149, 5 )
                      },

                   #endregion BCFD

                      #region SAMS

                new Outfit (Categories.SAMS, true, "SAMS EMT Uniform")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 1, 1 ),
                          Comp04 = new Component       ( 4, 48, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 31, 1 ),
                          Comp08 = new Component       ( 8, 55, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 17, 1 )
                      },

                      new Outfit (Categories.SAMS, false, "SAMS EMT Uniform")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 110, 1 ),
                          Comp04 = new Component       ( 4, 50, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 53, 1 ),
                          Comp07 = new Component       ( 7, 15, 2 ),
                          Comp08 = new Component       ( 8, 7, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 33, 1 )
                      },

                      new Outfit (Categories.SAMS, true, "SAMS Jacke")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 1, 1 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 5, 1 ),
                          Comp04 = new Component       ( 4, 48, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 52, 1 ),
                          Comp07 = new Component       ( 7, 1, 1 ),
                          Comp08 = new Component       ( 8, 52, 9 ),
                          Comp09 = new Component       ( 9, 2, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 152, 6 )
                      },

                      new Outfit (Categories.SAMS, false, "SAMS Jacke")
                      {
                          Prop01 = new Prop            ( 0, 0, 0 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 1, 1 ),
                          Comp04 = new Component       ( 4, 50, 1 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 53, 1 ),
                          Comp07 = new Component       ( 7, 1, 1 ),
                          Comp08 = new Component       ( 8, 46, 20 ),
                          Comp09 = new Component       ( 9, 2, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 149, 6 )
                      },

                   #endregion SAMS

                      #region USCG

                new Outfit (Categories.USCG, true, "USCG Uniform")
                      {
                          Prop01 = new Prop            ( 0, 45, 4 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 1, 1 ),
                          Comp04 = new Component       ( 4, 53, 3 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 25, 1 ),
                          Comp07 = new Component       ( 7, 1, 1 ),
                          Comp08 = new Component       ( 8, 16, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 25, 1 ),
                          Comp11 = new Component       ( 11, 38, 3 )
                      },

                      new Outfit (Categories.USCG, false, "USCG Uniform")
                      {
                          Prop01 = new Prop            ( 0, 44, 4 ),
                          Prop02 = new Prop            ( 1, 0, 0 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 15, 1 ),
                          Comp04 = new Component       ( 4, 55, 3 ),
                          Comp05 = new Component       ( 5, 1, 1 ),
                          Comp06 = new Component       ( 6, 25, 1 ),
                          Comp07 = new Component       ( 7, 1, 1 ),
                          Comp08 = new Component       ( 8, 15, 1 ),
                          Comp09 = new Component       ( 9, 1, 1 ),
                          Comp10 = new Component       ( 10, 24, 1 ),
                          Comp11 = new Component       ( 11, 113, 3 )
                      },

                      new Outfit (Categories.USCG, true, "U.S Küste")
                      {
                          Prop01 = new Prop            ( 0, 88, 1 ),
                          Prop02 = new Prop            ( 1, 16, 10 ),
                          Prop03 = new Prop            ( 2, 0, 0 ),
                          Prop04 = new Prop            ( 3, 0, 0 ),

                          Comp01 = new Component       ( 1, 1, 1 ),
                          Comp03 = new Component       ( 3, 1, 1 ),
                          Comp04 = new Component       ( 4, 53, 3 ),
                          Comp05 = new Component       ( 5, 49, 1 ),
                          Comp06 = new Component       ( 6, 25, 1 ),
                          Comp07 = new Component       ( 7, 2, 1 ),
                          Comp08 = new Component       ( 8, 88, 1 ),
                          Comp09 = new Component       ( 9, 26, 1 ),
                          Comp10 = new Component       ( 10, 1, 1 ),
                          Comp11 = new Component       ( 11, 42, 1 )
                      },

                      new Outfit(Categories.USCG, true, "USCG Swat")
                  {
                      Prop01 = new Prop(0, 76, 3),
                          Prop02 = new Prop(1, 0, 0),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 97, 1),
                          Comp04 = new Component(4, 53, 3),
                          Comp05 = new Component(5, 49, 1),
                          Comp06 = new Component(6, 26, 1),
                          Comp07 = new Component(7, 2, 1),
                          Comp08 = new Component(8, 16, 1),
                          Comp09 = new Component(9, 17, 2),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 54, 4)
                      },

                      new Outfit(Categories.USCG, false, "USCG Swat")
                  {
                      Prop01 = new Prop(0, 75, 3),
                          Prop02 = new Prop(1, 26, 1),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 112, 1),
                          Comp04 = new Component(4, 55, 3),
                          Comp05 = new Component(5, 49, 1),
                          Comp06 = new Component(6, 26, 1),
                          Comp07 = new Component(7, 2, 1),
                          Comp08 = new Component(8, 16, 1),
                          Comp09 = new Component(9, 19, 2),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 47, 4)
                      },

                      new Outfit(Categories.USCG, false, "U.S Küstenwache")
                      {
                      Prop01 = new Prop(0, 87, 1),
                          Prop02 = new Prop(1, 0, 0),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 15, 1),
                          Comp04 = new Component(4, 55, 3),
                          Comp05 = new Component(5, 1, 1),
                          Comp06 = new Component(6, 25, 1),
                          Comp07 = new Component(7, 2, 1),
                          Comp08 = new Component(8, 66, 1),
                          Comp09 = new Component(9, 28, 1),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 93, 1)
                      },

                      new Outfit(Categories.USCG, true, "USCG Rettungsschwimmer 1")
                  {
                      Prop01 = new Prop(0, 88, 1),
                          Prop02 = new Prop(1, 26, 1),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 97, 1),
                          Comp04 = new Component(4, 45, 1),
                          Comp05 = new Component(5, 1, 1),
                          Comp06 = new Component(6, 25, 1),
                          Comp07 = new Component(7, 1, 1),
                          Comp08 = new Component(8, 16, 1),
                          Comp09 = new Component(9, 1, 1),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 156, 1)
                      },

                      new Outfit(Categories.USCG, false, "USCG Rettungsschwimmer 1")
                  {
                      Prop01 = new Prop(0, 87, 1),
                          Prop02 = new Prop(1, 28, 01),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 112, 1),
                          Comp04 = new Component(4, 73, 1),
                          Comp05 = new Component(5, 1, 1),
                          Comp06 = new Component(6, 25, 1),
                          Comp07 = new Component(7, 1, 1),
                          Comp08 = new Component(8, 15, 1),
                          Comp09 = new Component(9, 1, 1),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 153, 1)
                      },

                      new Outfit(Categories.USCG, true, "USCG Rettungsschwimmer 2")
                  {
                      Prop01 = new Prop(0, 91, 1),
                          Prop02 = new Prop(1, 26, 1),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 97, 1),
                          Comp04 = new Component(4, 71, 1),
                          Comp05 = new Component(5, 1, 1),
                          Comp06 = new Component(6, 34, 1),
                          Comp07 = new Component(7, 1, 1),
                          Comp08 = new Component(8, 16, 1),
                          Comp09 = new Component(9, 1, 1),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 156, 2)
                      },

                      new Outfit(Categories.USCG, false, "USCG Rettungsschwimmer 2")
                  {
                      Prop01 = new Prop(0, 90, 1),
                          Prop02 = new Prop(1, 28, 1),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 36, 1),
                          Comp03 = new Component(3, 112, 1),
                          Comp04 = new Component(4, 73, 1),
                          Comp05 = new Component(5, 1, 1),
                          Comp06 = new Component(6, 35, 1),
                          Comp07 = new Component(7, 1, 1),
                          Comp08 = new Component(8, 15, 1),
                          Comp09 = new Component(9, 1, 1),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 153, 2)
                      },

                      new Outfit(Categories.USCG, true, "USCG Overall")
                  {
                      Prop01 = new Prop(0, 79, 5),
                          Prop02 = new Prop(1, 0, 0),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 97, 1),
                          Comp04 = new Component(4, 39, 3),
                          Comp05 = new Component(5, 1, 1),
                          Comp06 = new Component(6, 25, 1),
                          Comp07 = new Component(7, 1, 1),
                          Comp08 = new Component(8, 16, 1),
                          Comp09 = new Component(9, 1, 1),
                          Comp10 = new Component(10, 16, 5),
                          Comp11 = new Component(11, 66, 3)
                      },

                      new Outfit(Categories.USCG, false, "USCG Overall")
                  {
                      Prop01 = new Prop(0, 78, 5),
                          Prop02 = new Prop(1, 0, 0),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 19, 1),
                          Comp04 = new Component(4, 39, 3),
                          Comp05 = new Component(5, 1, 1),
                          Comp06 = new Component(6, 25, 1),
                          Comp07 = new Component(7, 1, 1),
                          Comp08 = new Component(8, 15, 1),
                          Comp09 = new Component(9, 1, 1),
                          Comp10 = new Component(10, 15, 5),
                          Comp11 = new Component(11, 60, 3)
                      },

                      new Outfit(Categories.USCG, true, "USCG Pilot")
                  {
                      Prop01 = new Prop(0, 79, 5),
                          Prop02 = new Prop(1, 1, 1),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 97, 1),
                          Comp04 = new Component(4, 71, 2),
                          Comp05 = new Component(5, 49, 1),
                          Comp06 = new Component(6, 25, 1),
                          Comp07 = new Component(7, 120, 1),
                          Comp08 = new Component(8, 16, 1),
                          Comp09 = new Component(9, 1, 1),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 156, 3)
                      },

                      new Outfit(Categories.USCG, false, "USCG Pilot")
                  {
                      Prop01 = new Prop(0, 78, 5),
                          Prop02 = new Prop(1, 0, 0),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 19, 1),
                          Comp04 = new Component(4, 73, 2),
                          Comp05 = new Component(5, 1, 1),
                          Comp06 = new Component(6, 25, 1),
                          Comp07 = new Component(7, 1, 1),
                          Comp08 = new Component(8, 15, 1),
                          Comp09 = new Component(9, 1, 1),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 153, 3)
                      },

                   #endregion USCG

                      #region LG

                new Outfit(Categories.LG, true, "Rettungsschwimmer Badeanzug")
                  {
                      Prop01 = new Prop(0, 0, 0),
                          Prop02 = new Prop(1, 0, 0),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 16, 1),
                          Comp04 = new Component(4, 7, 4),
                          Comp05 = new Component(5, 1, 1),
                          Comp06 = new Component(6, 35, 1),
                          Comp07 = new Component(7, 16, 1),
                          Comp08 = new Component(8, 20, 1),
                          Comp09 = new Component(9, 1, 1),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 16, 1)
                      },

                      new Outfit(Categories.LG, false, "Rettungsschwimmer Badeanzug")
                  {
                      Prop01 = new Prop(0, 0, 0),
                          Prop02 = new Prop(1, 0, 0),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 12, 1),
                          Comp04 = new Component(4, 18, 5),
                          Comp05 = new Component(5, 1, 1),
                          Comp06 = new Component(6, 36, 1),
                          Comp07 = new Component(7, 20, 1),
                          Comp08 = new Component(8, 19, 1),
                          Comp09 = new Component(9, 1, 1),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 12, 4)
                      },

                      new Outfit(Categories.LG, true, "Rettungsschwimmerkleidung")
                  {
                      Prop01 = new Prop(0, 0, 0),
                          Prop02 = new Prop(1, 0, 0),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 1, 1),
                          Comp04 = new Component(4, 7, 4),
                          Comp05 = new Component(5, 1, 1),
                          Comp06 = new Component(6, 35, 1),
                          Comp07 = new Component(7, 16, 1),
                          Comp08 = new Component(8, 16, 1),
                          Comp09 = new Component(9, 1, 1),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 23, 1)
                      },

                      new Outfit(Categories.LG, false, "Rettungsschwimmerkleidung")
                  {
                      Prop01 = new Prop(0, 0, 0),
                          Prop02 = new Prop(1, 0, 0),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 15, 1),
                          Comp04 = new Component(4, 11, 4),
                          Comp05 = new Component(5, 1, 1),
                          Comp06 = new Component(6, 36, 1),
                          Comp07 = new Component(7, 20, 1),
                          Comp08 = new Component(8, 15, 1),
                          Comp09 = new Component(9, 1, 1),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 24, 1)
                      },

                      new Outfit(Categories.LG, true, "Rettungsschwimmer (Weste)")
                  {
                      Prop01 = new Prop(0, 0, 0),
                          Prop02 = new Prop(1, 0, 0),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 1, 1),
                          Comp04 = new Component(4, 7, 4),
                          Comp05 = new Component(5, 1, 1),
                          Comp06 = new Component(6, 35, 1),
                          Comp07 = new Component(7, 1, 1),
                          Comp08 = new Component(8, 16, 1),
                          Comp09 = new Component(9, 9, 1),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 23, 1)
                      },

                   #endregion LG

                      #region Army

                new Outfit(Categories.Army, true, "Nationalgarde Uniform")
                  {
                      Prop01 = new Prop(0, 29, 2),
                          Prop02 = new Prop(1, 0, 0),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 1, 1),
                          Comp04 = new Component(4, 60, 5),
                          Comp05 = new Component(5, 49, 1),
                          Comp06 = new Component(6, 36, 1),
                          Comp07 = new Component(7, 1, 1),
                          Comp08 = new Component(8, 16, 1),
                          Comp09 = new Component(9, 1, 1),
                          Comp10 = new Component(10, 23, 1),
                          Comp11 = new Component(11, 38, 2)
                      },

                      new Outfit(Categories.Army, false, "Nationalgarde Uniform")
                  {
                      Prop01 = new Prop(0, 29, 2),
                          Prop02 = new Prop(1, 0, 0),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 15, 1),
                          Comp04 = new Component(4, 62, 5),
                          Comp05 = new Component(5, 1, 1),
                          Comp06 = new Component(6, 37, 1),
                          Comp07 = new Component(7, 1, 1),
                          Comp08 = new Component(8, 15, 1),
                          Comp09 = new Component(9, 1, 1),
                          Comp10 = new Component(10, 22, 1),
                          Comp11 = new Component(11, 113, 2)
                      },

                      new Outfit(Categories.Army, true, "U.S Army Kampfuniform")
                  {
                      Prop01 = new Prop(0, 40, 4),
                          Prop02 = new Prop(1, 24, 1),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 16, 1),
                          Comp04 = new Component(4, 38, 3),
                          Comp05 = new Component(5, 49, 1),
                          Comp06 = new Component(6, 36, 1),
                          Comp07 = new Component(7, 43, 1),
                          Comp08 = new Component(8, 16, 1),
                          Comp09 = new Component(9, 28, 10),
                          Comp10 = new Component(10, 15, 1),
                          Comp11 = new Component(11, 54, 3)
                      },

                      new Outfit(Categories.Army, false, "U.S Army Kampfuniform")
                  {
                      Prop01 = new Prop(0, 39, 4),
                          Prop02 = new Prop(1, 26, 1),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 15, 1),
                          Comp04 = new Component(4, 37, 3),
                          Comp05 = new Component(5, 49, 1),
                          Comp06 = new Component(6, 37, 1),
                          Comp07 = new Component(7, 30, 1),
                          Comp08 = new Component(8, 15, 1),
                          Comp09 = new Component(9, 30, 10),
                          Comp10 = new Component(10, 14, 1),
                          Comp11 = new Component(11, 47, 3)
                      },

                   #endregion Army

                      #region MW

                new Outfit(Categories.MW, true, "Merryweather Sicherheit")
                  {
                      Prop01 = new Prop(0, 45, 5),
                          Prop02 = new Prop(1, 1, 1),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 1, 1),
                          Comp04 = new Component(4, 50, 2),
                          Comp05 = new Component(5, 49, 1),
                          Comp06 = new Component(6, 26, 1),
                          Comp07 = new Component(7, 9, 1),
                          Comp08 = new Component(8, 43, 1),
                          Comp09 = new Component(9, 15, 1),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 100, 1)
                      },

                      new Outfit(Categories.MW, false, "Merryweather Sicherheit")
                  {
                      Prop01 = new Prop(0, 44, 5),
                          Prop02 = new Prop(1, 0, 0),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 15, 1),
                          Comp04 = new Component(4, 52, 2),
                          Comp05 = new Component(5, 1, 1),
                          Comp06 = new Component(6, 56, 1),
                          Comp07 = new Component(7, 9, 1),
                          Comp08 = new Component(8, 9, 1),
                          Comp09 = new Component(9, 1, 1),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 46, 1)
                      },

                      new Outfit(Categories.MW, true, "Gepanzert Merryweather")
                  {
                      Prop01 = new Prop(0, 45, 5),
                          Prop02 = new Prop(1, 0, 0),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 1, 1),
                          Comp04 = new Component(4, 50, 2),
                          Comp05 = new Component(5, 49, 1),
                          Comp06 = new Component(6, 52, 1),
                          Comp07 = new Component(7, 120, 1),
                          Comp08 = new Component(8, 88, 1),
                          Comp09 = new Component(9, 7, 1),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 100, 1)
                      },

                      new Outfit(Categories.MW, true, "Merryweather Söldner")
                  {
                      Prop01 = new Prop(0, 7, 7),
                          Prop02 = new Prop(1, 1, 1),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 1, 1),
                          Comp04 = new Component(4, 53, 1),
                          Comp05 = new Component(5, 49, 1),
                          Comp06 = new Component(6, 36, 1),
                          Comp07 = new Component(7, 113, 1),
                          Comp08 = new Component(8, 17, 1),
                          Comp09 = new Component(9, 1, 1),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 100, 3)
                      },

                      new Outfit(Categories.MW, false, "Merryweather Söldner")
                  {
                      Prop01 = new Prop(0, 44, 7),
                          Prop02 = new Prop(1, 0, 0),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 15, 1),
                          Comp04 = new Component(4, 55, 1),
                          Comp05 = new Component(5, 1, 1),
                          Comp06 = new Component(6, 37, 1),
                          Comp07 = new Component(7, 84, 1),
                          Comp08 = new Component(8, 10, 1),
                          Comp09 = new Component(9, 1, 1),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 46, 3)
                      },

                      new Outfit(Categories.MW, true, "Gepanzert Söldner")
                  {
                      Prop01 = new Prop(0, 45, 6),
                          Prop02 = new Prop(1, 0, 0),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 1, 1),
                          Comp04 = new Component(4, 53, 1),
                          Comp05 = new Component(5, 49, 1),
                          Comp06 = new Component(6, 36, 1),
                          Comp07 = new Component(7, 2, 3),
                          Comp08 = new Component(8, 16, 1),
                          Comp09 = new Component(9, 7, 2),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 100, 3)
                      },

                      new Outfit(Categories.MW, true, "Black Ops Söldner")
                  {
                      Prop01 = new Prop(0, 7, 2),
                          Prop02 = new Prop(1, 24, 1),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 97, 1),
                          Comp04 = new Component(4, 32, 3),
                          Comp05 = new Component(5, 49, 1),
                          Comp06 = new Component(6, 26, 1),
                          Comp07 = new Component(7, 111, 1),
                          Comp08 = new Component(8, 16, 1),
                          Comp09 = new Component(9, 10, 4),
                          Comp10 = new Component(10, 20, 1),
                          Comp11 = new Component(11, 50, 3)
                      },

                      new Outfit(Categories.MW, true, "Black Ops UCP Söldner")
                  {
                      Prop01 = new Prop(0, 7, 8),
                          Prop02 = new Prop(1, 24, 1),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 18, 4),
                          Comp04 = new Component(4, 38, 4),
                          Comp05 = new Component(5, 49, 1),
                          Comp06 = new Component(6, 36, 1),
                          Comp07 = new Component(7, 43, 2),
                          Comp08 = new Component(8, 16, 1),
                          Comp09 = new Component(9, 10, 2),
                          Comp10 = new Component(10, 21, 1),
                          Comp11 = new Component(11, 50, 4)
                      },

                      new Outfit(Categories.MW, false, "Black Ops Söldner")
                  {
                      Prop01 = new Prop(0, 44, 5),
                          Prop02 = new Prop(1, 0, 0),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 19, 1),
                          Comp04 = new Component(4, 31, 3),
                          Comp05 = new Component(5, 1, 1),
                          Comp06 = new Component(6, 26, 1),
                          Comp07 = new Component(7, 82, 1),
                          Comp08 = new Component(8, 16, 1),
                          Comp09 = new Component(9, 7, 4),
                          Comp10 = new Component(10, 19, 1),
                          Comp11 = new Component(11, 43, 5)
                      },

                   #endregion MW

                      #region Sec

                new Outfit(Categories.Sec, true, "VIP Sicherheit")
                  {
                      Prop01 = new Prop(0, 0, 0),
                          Prop02 = new Prop(1, 0, 0),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 5, 1),
                          Comp04 = new Component(4, 11, 1),
                          Comp05 = new Component(5, 1, 1),
                          Comp06 = new Component(6, 11, 1),
                          Comp07 = new Component(7, 39, 1),
                          Comp08 = new Component(8, 11, 1),
                          Comp09 = new Component(9, 1, 1),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 11, 1)
                      },

                      new Outfit(Categories.Sec, true, "Türsteher")
                  {
                      Prop01 = new Prop(0, 0, 0),
                          Prop02 = new Prop(1, 0, 0),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 1, 1),
                          Comp04 = new Component(4, 23, 5),
                          Comp05 = new Component(5, 1, 1),
                          Comp06 = new Component(6, 11, 1),
                          Comp07 = new Component(7, 1, 1),
                          Comp08 = new Component(8, 89, 1),
                          Comp09 = new Component(9, 1, 1),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 100, 5)
                      },

                      new Outfit(Categories.Sec, true, "Sicherheitsuniform")
                  {
                      Prop01 = new Prop(0, 42, 1),
                          Prop02 = new Prop(1, 1, 1),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 1, 1),
                          Comp04 = new Component(4, 26, 6),
                          Comp05 = new Component(5, 49, 1),
                          Comp06 = new Component(6, 52, 1),
                          Comp07 = new Component(7, 9, 1),
                          Comp08 = new Component(8, 43, 1),
                          Comp09 = new Component(9, 1, 1),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 47, 1)
                      },

                      new Outfit(Categories.Sec, false, "Sicherheitsuniform")
                  {
                      Prop01 = new Prop(0, 41, 1),
                          Prop02 = new Prop(1, 0, 0),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 15, 1),
                          Comp04 = new Component(4, 4, 10),
                          Comp05 = new Component(5, 1, 1),
                          Comp06 = new Component(6, 56, 1),
                          Comp07 = new Component(7, 9, 1),
                          Comp08 = new Component(8, 9, 1),
                          Comp09 = new Component(9, 1, 1),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 127, 1)
                      },

                      new Outfit(Categories.Sec, true, "Sicherheitsuniform (Winter)")
                  {
                      Prop01 = new Prop(0, 3, 1),
                          Prop02 = new Prop(1, 1, 1),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 39, 1),
                          Comp04 = new Component(4, 26, 6),
                          Comp05 = new Component(5, 49, 1),
                          Comp06 = new Component(6, 52, 1),
                          Comp07 = new Component(7, 9, 1),
                          Comp08 = new Component(8, 43, 1),
                          Comp09 = new Component(9, 27, 10),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 124, 3)
                      },

                      new Outfit(Categories.Sec, false, "Sicherheitsuniform")
                  {
                      Prop01 = new Prop(0, 6, 1),
                          Prop02 = new Prop(1, 0, 0),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 37, 1),
                          Comp04 = new Component(4, 4, 10),
                          Comp05 = new Component(5, 49, 1),
                          Comp06 = new Component(6, 53, 1),
                          Comp07 = new Component(7, 9, 1),
                          Comp08 = new Component(8, 37, 1),
                          Comp09 = new Component(9, 29, 10),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 120, 3)
                      },

                      new Outfit(Categories.Sec, true, "Sicherheitsuniform (gepanzert)")
                  {
                      Prop01 = new Prop(0, 82, 4),
                          Prop02 = new Prop(1, 0, 0),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 5, 1),
                          Comp04 = new Component(4, 26, 7),
                          Comp05 = new Component(5, 49, 1),
                          Comp06 = new Component(6, 55, 1),
                          Comp07 = new Component(7, 9, 1),
                          Comp08 = new Component(8, 21, 4),
                          Comp09 = new Component(9, 1, 1),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 20, 2)
                      },

                   #endregion Sec

                      #region Wartung

                new Outfit(Categories.Wartung, true, "Hygiene-Overall")
                  {
                      Prop01 = new Prop(0, 0, 0),
                          Prop02 = new Prop(1, 1, 1),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 65, 1),
                          Comp04 = new Component(4, 54, 1),
                          Comp05 = new Component(5, 1, 1),
                          Comp06 = new Component(6, 28, 1),
                          Comp07 = new Component(7, 1, 1),
                          Comp08 = new Component(8, 16, 1),
                          Comp09 = new Component(9, 1, 1),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 72, 1)
                      },

                      new Outfit(Categories.Wartung, false, "Hygiene-Overall")
                  {
                      Prop01 = new Prop(0, 0, 0),
                          Prop02 = new Prop(1, 14, 1),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 76, 1),
                          Comp04 = new Component(4, 56, 1),
                          Comp05 = new Component(5, 1, 1),
                          Comp06 = new Component(6, 27, 1),
                          Comp07 = new Component(7, 1, 1),
                          Comp08 = new Component(8, 15, 1),
                          Comp09 = new Component(9, 1, 1),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 68, 1)
                      },

                      new Outfit(Categories.Wartung, true, "Müllabfuhr")
                  {
                      Prop01 = new Prop(0, 0, 0),
                          Prop02 = new Prop(1, 1, 1),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 67, 1),
                          Comp04 = new Component(4, 37, 1),
                          Comp05 = new Component(5, 1, 1),
                          Comp06 = new Component(6, 28, 1),
                          Comp07 = new Component(7, 1, 1),
                          Comp08 = new Component(8, 60, 1),
                          Comp09 = new Component(9, 1, 1),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 58, 1)
                      },

                      new Outfit(Categories.Wartung, false, "Müllabfuhr")
                  {
                      Prop01 = new Prop(0, 0, 0),
                          Prop02 = new Prop(1, 0, 0),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 80, 1),
                          Comp04 = new Component(4, 36, 1),
                          Comp05 = new Component(5, 1, 1),
                          Comp06 = new Component(6, 27, 1),
                          Comp07 = new Component(7, 1, 1),
                          Comp08 = new Component(8, 37, 1),
                          Comp09 = new Component(9, 1, 1),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 51, 1)
                      },

                      new Outfit(Categories.Wartung, true, "Hausmeister")
                  {
                      Prop01 = new Prop(0, 0, 0),
                          Prop02 = new Prop(1, 0, 0),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 5, 1),
                          Comp04 = new Component(4, 39, 2),
                          Comp05 = new Component(5, 1, 1),
                          Comp06 = new Component(6, 25, 1),
                          Comp07 = new Component(7, 1, 1),
                          Comp08 = new Component(8, 16, 1),
                          Comp09 = new Component(9, 1, 1),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 66, 2)
                      },

                      new Outfit(Categories.Wartung, false, "Hausmeister")
                  {
                      Prop01 = new Prop(0, 0, 0),
                          Prop02 = new Prop(1, 0, 0),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 4, 1),
                          Comp04 = new Component(4, 39, 2),
                          Comp05 = new Component(5, 1, 1),
                          Comp06 = new Component(6, 25, 1),
                          Comp07 = new Component(7, 95, 2),
                          Comp08 = new Component(8, 15, 1),
                          Comp09 = new Component(9, 1, 1),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 60, 2)
                      },

                   #endregion Wartung

                      #region Bau und Technik

                new Outfit(Categories.BauTechnik, true, "Mechaniker Overall")
                  {
                      Prop01 = new Prop(0, 0, 0),
                          Prop02 = new Prop(1, 0, 0),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 5, 1),
                          Comp04 = new Component(4, 40, 2),
                          Comp05 = new Component(5, 1, 1),
                          Comp06 = new Component(6, 25, 1),
                          Comp07 = new Component(7, 1, 1),
                          Comp08 = new Component(8, 16, 1),
                          Comp09 = new Component(9, 1, 1),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 67, 2)
                      },

                      new Outfit(Categories.BauTechnik, true, "Privater Auftragnehmer")
                  {
                      Prop01 = new Prop(0, 26, 1),
                          Prop02 = new Prop(1, 16, 10),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 64, 1),
                          Comp04 = new Component(4, 1, 11),
                          Comp05 = new Component(5, 49, 1),
                          Comp06 = new Component(6, 13, 1),
                          Comp07 = new Component(7, 1, 1),
                          Comp08 = new Component(8, 90, 1),
                          Comp09 = new Component(9, 4, 1),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 57, 1)
                      },

                      new Outfit(Categories.BauTechnik, false, "Privater Auftragnehmer")
                  {
                      Prop01 = new Prop(0, 54, 1),
                          Prop02 = new Prop(1, 0, 0),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 73, 1),
                          Comp04 = new Component(4, 5, 2),
                          Comp05 = new Component(5, 49, 1),
                          Comp06 = new Component(6, 27, 1),
                          Comp07 = new Component(7, 1, 1),
                          Comp08 = new Component(8, 57, 1),
                          Comp09 = new Component(9, 6, 1),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 50, 1)
                      },

                      new Outfit(Categories.BauTechnik, true, "Arbeiter")
                  {
                      Prop01 = new Prop(0, 61, 2),
                          Prop02 = new Prop(1, 16, 10),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 64, 1),
                          Comp04 = new Component(4, 50, 4),
                          Comp05 = new Component(5, 49, 1),
                          Comp06 = new Component(6, 52, 4),
                          Comp07 = new Component(7, 1, 1),
                          Comp08 = new Component(8, 91, 1),
                          Comp09 = new Component(9, 4, 3),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 3, 6)
                      },

                      new Outfit(Categories.BauTechnik, false, "Arbeiter")
                  {
                      Prop01 = new Prop(0, 61, 1),
                          Prop02 = new Prop(1, 0, 0),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 82, 1),
                          Comp04 = new Component(4, 5, 2),
                          Comp05 = new Component(5, 49, 1),
                          Comp06 = new Component(6, 27, 1),
                          Comp07 = new Component(7, 1, 1),
                          Comp08 = new Component(8, 55, 1),
                          Comp09 = new Component(9, 6, 3),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 118, 1)
                      },

                      new Outfit(Categories.BauTechnik, true, "Arbeiter Overall")
                  {
                      Prop01 = new Prop(0, 61, 1),
                          Prop02 = new Prop(1, 0, 0),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 67, 1),
                          Comp04 = new Component(4, 40, 3),
                          Comp05 = new Component(5, 1, 1),
                          Comp06 = new Component(6, 25, 1),
                          Comp07 = new Component(7, 1, 1),
                          Comp08 = new Component(8, 16, 1),
                          Comp09 = new Component(9, 11, 5),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 67, 3)
                      },

                      new Outfit(Categories.BauTechnik, false, "Arbeiter Overall")
                  {
                      Prop01 = new Prop(0, 61, 2),
                          Prop02 = new Prop(1, 0, 0),
                          Prop03 = new Prop(2, 0, 0),
                          Prop04 = new Prop(3, 0, 0),

                          Comp01 = new Component(1, 1, 1),
                          Comp03 = new Component(3, 76, 1),
                          Comp04 = new Component(4, 40, 3),
                          Comp05 = new Component(5, 49, 1),
                          Comp06 = new Component(6, 27, 1),
                          Comp07 = new Component(7, 1, 1),
                          Comp08 = new Component(8, 15, 1),
                          Comp09 = new Component(9, 1, 1),
                          Comp10 = new Component(10, 1, 1),
                          Comp11 = new Component(11, 61, 3)
                      },

                   #endregion Bau und Technik
            };
        }

        public static class Categories
        {
            public static Outfit_Category Alle;

            public static Outfit_Category Army;

            public static Outfit_Category BauTechnik;

            public static Outfit_Category BCFD;

            public static Outfit_Category BCSO;

            public static Outfit_Category Custom;

            public static Outfit_Category DEA;

            public static Outfit_Category FBI;

            public static Outfit_Category LG;

            public static Outfit_Category LSCoFD;

            public static Outfit_Category LSFD;

            public static Outfit_Category LSIA;

            public static Outfit_Category LSPA;

            public static Outfit_Category LSPD;

            public static Outfit_Category LSSD;

            public static Outfit_Category MW;

            public static Outfit_Category NOOSE;

            public static Outfit_Category SADCR;

            public static Outfit_Category SAHP;

            public static Outfit_Category SAMS;

            public static Outfit_Category SASP;

            public static Outfit_Category Sec;

            public static Outfit_Category USAF;

            public static Outfit_Category USCG;

            public static Outfit_Category Wartung;
        }
    }
}