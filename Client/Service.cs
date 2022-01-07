using DLEA_Lib.Shared;
using DLEA_Lib.Shared.Services;
using DLEA_Lib.Shared.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public abstract class Service : ClientService
    {
        protected ClientObject ClientObject { get; }
        protected int ServerID { get => ClientObject.ServerID; }
        protected ExtendedUser CurrentUser { get => ClientObject.CurrentUser; set => ClientObject.CurrentUser = value; }

        public abstract string UserFriendlyName { get; }

        public Service(ClientObject ClientObject)
        {
            this.ClientObject = ClientObject;
            ClientObject.Trace($"Initialized {Name}");
        }

        public virtual void OnTick() { }

        public virtual void Start() { }

    }
}
