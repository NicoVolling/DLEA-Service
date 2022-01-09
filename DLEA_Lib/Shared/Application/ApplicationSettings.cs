using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLEA_Lib.Shared.Application
{
    public static class ApplicationSettings
    {
        public static readonly string Name = "DLEA-Service";
        public static readonly string Name_Long = "Digital Law Enforcement Assistant";

        public static readonly string Version = "3.3.34";
        public static readonly string Creator = "Nico Volling & Florian Mömmerzheim";

        public static bool ServerSide { get; set; } 
        public static bool ClientSide { get => !ServerSide; set => ServerSide = !value; }
    }
}