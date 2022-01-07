using DLEA_Lib.Shared;
using DLEA_Lib.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public abstract class TService : BaseService
    {
        public TServerObject ServerObject { get; }

        public TService(TServerObject ServerObject)
        {
            this.ServerObject = ServerObject;
            InitializeSettings();
            TServerObject.Trace($"Initialized {Name}");
        }

        protected virtual void InitializeSettings() { }
        public virtual void OnTick() { }

    }
}
