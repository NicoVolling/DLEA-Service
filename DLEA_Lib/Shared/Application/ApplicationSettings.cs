namespace DLEA_Lib.Shared.Application
{
    public static class ApplicationSettings
    {
        public static readonly string Creator = "Nico Volling (CEO of Mömmerzheim und Volling GbR)";
        public static readonly string Name = "DLEA-Service";
        public static readonly string Name_Long = "Digital Law Enforcement Assistant";

        public static readonly string Version = "3.5.1";
        public static bool ClientSide { get => !ServerSide; set => ServerSide = !value; }
        public static bool ServerSide { get; set; }
    }
}