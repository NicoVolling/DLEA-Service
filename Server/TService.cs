using DLEA_Lib.Shared.Services;

namespace Server
{
    public abstract class TService : BaseService
    {
        public TService(TServerObject ServerObject)
        {
            this.ServerObject = ServerObject;
            InitializeSettings();
            TServerObject.Trace($"Initialized {Name}");
        }

        public TServerObject ServerObject { get; }

        public virtual void OnTick()
        { }

        protected virtual void InitializeSettings()
        { }
    }
}