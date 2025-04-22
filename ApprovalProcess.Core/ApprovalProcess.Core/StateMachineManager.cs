using ApprovalProcess.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApprovalProcess.Core
{
    public class StateMachineManager : IStateMachineManager
    {

        private StateMachineEntity _approvalProcess = new StateMachineEntity()
        {
            Id = "1",
            InitialState = "Edit",
            State = "Edit",
            StateRepresentations = new List<StateRepresentationEntity>()
            {
                new StateRepresentationEntity()
                {
                    Id = "2",
                    State = "Edit",
                    Transitions = new List<TransitionEntity>()
                    {
                        new TransitionEntity()
                        {
                            Id = Guid.NewGuid().ToString("N"),
                            Trigger = "Submitted",
                            DtState = "FirstApprove"
                        }
                    }
                },
                new StateRepresentationEntity()
                {
                    Id = "3",
                    State = "Return",
                    Transitions = new List<TransitionEntity>()
                    {
                        new TransitionEntity()
                        {
                            Id = Guid.NewGuid().ToString("N"),
                            Trigger = "Rewrite",
                            DtState = "Edit"
                        }
                    }
                },
                new StateRepresentationEntity()
                {
                    Id = "4",
                    State = "FirstApprove",
                    Transitions = new List<TransitionEntity>()
                    {
                        new TransitionEntity()
                        {
                            Id = Guid.NewGuid().ToString("N"),
                            Trigger = "FirstApprovedPass",
                            DtState = "SecondApprove"
                        },
                        new TransitionEntity()
                        {
                            Id = Guid.NewGuid().ToString("N"),
                            Trigger = "Reject",
                            DtState = "Return"
                        }
                    }
                },
                new StateRepresentationEntity()
                {
                    Id = "5",
                    State = "SecondApprove",
                    Transitions = new List<TransitionEntity>()
                    {
                        new TransitionEntity()
                        {
                            Id = Guid.NewGuid().ToString("N"),
                            Trigger = "SecondApprovedPass",
                            DtState = "Completed"
                        },
                        new TransitionEntity(){
                            Id = Guid.NewGuid().ToString("N"),
                            Trigger = "Reject",
                            DtState = "Return"
                        }
                    }
                },
            }
        };


        public StateMachineManager()
        {

        }

        public ValueTask<StateMachineEntity> GetSm(string id)
        {

            return new ValueTask<StateMachineEntity>(_approvalProcess);
        }

        public ValueTask<StateRepresentationEntity> GetSr(string id)
        {
            var sr = _approvalProcess.StateRepresentations.Single(x => x.Id == id);
            return new ValueTask<StateRepresentationEntity>(sr);
        }
    }
}
