using CitizenFX.Core;
using CitizenFX.Core.Native;
using DLEA_Lib;
using DLEA_Lib.Shared;
using DLEA_Lib.Shared.Application;
using DLEA_Lib.Shared.EventHandling;
using DLEA_Lib.Shared.Services;
using DLEA_Lib.Shared.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    public class TSyncService : TService
    {
        public override string Name => nameof(TSyncService);

        #region Events
        public Action<int, string> EventOnGetPlayerData { get; }
        public Action<int, int> EventOnChangeWeather { get; }
        #endregion

        public List<ExtendedUser> ListOfUsers { get; set; } = new List<ExtendedUser>();

        public TSyncService(TServerObject ServerObject) : base(ServerObject)
        {
            EventOnGetPlayerData = OnGetPlayerData;
            EventOnChangeWeather = OnChangeWeather;
        }

        private void OnChangeWeather(int PlayerId, int Weather)
        {
            foreach (Player Player in new PlayerList()) 
            {
                Player.TriggerEvent(ClientEvents.SyncService_ChangeWeather, Weather);
            }
        }

        public void OnGetPlayerData(int PlayerId, string UserRAW)
        {
            try 
            {
                ExtendedUser CurrentUser = ExtendedUser.GetData(UserRAW);
                CurrentUser.ServerID = PlayerId;
                CurrentUser.TimeStamp = DateTime.Now.Ticks;
                IEnumerable<ExtendedUser> UserList = ListOfUsers.Where(o => o.ServerID == CurrentUser.ServerID);
                if (UserList.Any() && new PlayerList()[PlayerId] != null)
                {
                    ListOfUsers[ListOfUsers.IndexOf(UserList.First())] = CurrentUser;
                }
                else if(new PlayerList()[PlayerId] != null)
                {
                    ListOfUsers.Add(CurrentUser);
                    CurrentUser.ServerID = PlayerId;
                }
                DateTime Now = DateTime.Now;
                ListOfUsers.RemoveAll(o => Now.Subtract(new DateTime(o.TimeStamp)).TotalSeconds > 10);
                new PlayerList()[PlayerId]?.TriggerEvent(ClientEvents.SyncService_SendPlayerList, Users.Serialize(ListOfUsers));
            }
            catch (Exception ex) { Tracing.Trace(ex); }
        }
    }
}
