using DLEA_Lib.Shared.Services;
using DLEA_Lib.Shared.User;

namespace Client
{
    public abstract class Service : ClientService
    {
        public Service(ClientObject ClientObject)
        {
            this.ClientObject = ClientObject;
            ClientObject.Trace($"Initialized {Name}");
        }

        public abstract string UserFriendlyName { get; }
        protected ClientObject ClientObject { get; }
        protected ExtendedUser CurrentUser { get => ClientObject.CurrentUser; set => ClientObject.CurrentUser = value; }
        protected int ServerID { get => ClientObject.ServerID; }

        public virtual void OnTick()
        { }

        public virtual void Start()
        { }
    }
}