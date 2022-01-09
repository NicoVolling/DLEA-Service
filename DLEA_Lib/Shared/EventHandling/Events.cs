using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLEA_Lib.Shared.EventHandling
{
    public static class ServerEvents
    {
        public const string SyncService_SendData                = "S:01";
        public const string SyncService_ChangeWeather           = "S:02";
        public const string DataService_SendPlayerData          = "S:03";
        public const string DataService_RequestPlayerData       = "S:04";
        public const string DataService_ChangePlayerData        = "S:05";
        public const string DataService_DeletePlayerData        = "S:06";
        public const string DataService_Login                   = "S:07";
        public const string DataService_GetLogin                = "S:08";
        public const string DataService_SetPermissions          = "S:09";

        public const string OutfitService_RequestOutfit         = "S:10";
        public const string OutfitService_GetOutfit             = "S:11";

        public const string MessageService_GetPing              = "S:12";
        public const string LocationService_GetLocations        = "S:13";
    }

    public static class ClientEvents
    {
        public const string SyncService_SendPlayerList          = "C:01";
        public const string SyncService_ChangeWeather           = "C:02";
        public const string DataService_SendPlayerData          = "C:03";
        public const string DataService_AutoLogin               = "C:04";
        public const string DataService_PermissiosChanged       = "C:05";

        public const string OutfitService_GetOutfit             = "C:06";

        public const string MessageService_SendMessages         = "C:07";
        public const string LocationService_SendLocations       = "C:08";
    }
}