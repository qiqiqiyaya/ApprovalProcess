using Ap.Core.Share.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ap.Core.Share.Entities;

namespace Ap.Repository.FreeSql
{
    public class ApRepository : IApRepository
    {
        public ValueTask<StateMachineEntity> GetStateMachine(string id)
        {
            throw new NotImplementedException();
        }

        public ValueTask<StateSettingsEntity> GetStateSettings(string id)
        {
            throw new NotImplementedException();
        }
    }
}
