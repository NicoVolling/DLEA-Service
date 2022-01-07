using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLEA_Lib.Shared.EventHandling
{
    public static class ServerEvents
    {
        public const string Logging_SendMessage                 = "SERVER::Logging::SendMessage";
        public const string SyncService_SendData                = "SERVER::SyncService::SendData";
        public const string SyncService_ChangeWeather           = "SERVER::SyncService::ChangeWeather";
        public const string DataService_SendPlayerData          = "SERVER::DataService::SendPlayerData";
        public const string DataService_RequestPlayerData       = "SERVER::DataService::RequestPlayerData";
        public const string DataService_ChangePlayerData        = "SERVER::DataService::ChangePlayerData";
        public const string DataService_DeletePlayerData        = "SERVER::DataService::DeletePlayerData";
        public const string DataService_Login                   = "SERVER::DataService::Login";
        public const string DataService_GetLogin                = "SERVER::DataService::GetLogin";
        public const string DataService_SetPermissions          = "SERVER::DataService::SetPermissions";

        public const string OutfitService_RequestOutfit         = "SERVER::OutfitService::RequestOutfit";
        public const string OutfitService_GetOutfit             = "SERVER::OutfitService::GetOutfit";

        public const string MessageService_GetPing              = "SERVER::MessageService::GetPing";
    }

    public static class ClientEvents
    {
        public const string SyncService_SendPlayerList          = "CLIENT::SyncService::SendPlayerList";
        public const string SyncService_ChangeWeather           = "CLIENT::SyncService::ChangeWeather";
        public const string DataService_SendPlayerData          = "CLIENT::DataService::SendPlayerData";
        public const string DataService_AutoLogin               = "CLIENT::DataService::AutoLogin";
        public const string DataService_PermissiosChanged       = "CLIENT::DataService::PermissionsChanged";

        public const string OutfitService_GetOutfit             = "CLIENT::OutfitService::GetOutfit";

        public const string MessageService_SendMessage          = "CLIENT::MessageService::SendMessage";
    }
}