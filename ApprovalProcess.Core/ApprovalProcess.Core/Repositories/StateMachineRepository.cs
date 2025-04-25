using ApprovalProcess.Core.Actions;
using ApprovalProcess.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ApprovalProcess.Core.Repositories
{
	public class StateMachineRepository : IStateMachineRepository
	{
		private StateMachineEntity _approvalProcess = new StateMachineEntity()
		{
			Id = "1",
			InitialState = "Edit",
			CurrentState = "Edit",
			StateSettings = new List<StateSettingsEntity>()
			{
				new StateSettingsEntity()
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
					},
					ExecutableActions=new List<ExecutableActionEntity>()
					{
						new ExecutableActionEntity()
						{
							Id="1",
							Description="test",
							Name=SmActionNames.SetNextApprover,
							Type=ExecutableActionType.Exit,
						},
						new ExecutableActionEntity()
						{
							Id="2",
							Description="test",
							Name=SmActionNames.NotificationSend,
							Type=ExecutableActionType.Exit,
						},
						new ExecutableActionEntity()
						{
							Id="3",
							Description="test",
							Name=SmActionNames.TestEntryAction,
							Type=ExecutableActionType.Entry,
						}
					}
				},
				new StateSettingsEntity()
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
				new StateSettingsEntity()
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
					},
					ExecutableActions=new List<ExecutableActionEntity>()
					{
						new ExecutableActionEntity()
						{
							Id="1",
							Description="test",
							Name=SmActionNames.SetNextApprover,
							Type=ExecutableActionType.Exit,
						},
						new ExecutableActionEntity()
						{
							Id="2",
							Description="test",
							Name=SmActionNames.NotificationSend,
							Type=ExecutableActionType.Exit,
						},
						new ExecutableActionEntity()
						{
							Id="3",
							Description="test",
							Name=SmActionNames.TestEntryAction,
							Type=ExecutableActionType.Entry,
						}
					}
				},
				new StateSettingsEntity()
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

		public StateMachineRepository() { }

		public ValueTask<StateMachineEntity> GetStateMachine(string id)
		{
			return new ValueTask<StateMachineEntity>(_approvalProcess);
		}

		public ValueTask<StateSettingsEntity> GetStateSettings(string id)
		{
			var settings = _approvalProcess.StateSettings.Single(x => x.Id == id);
			return new ValueTask<StateSettingsEntity>(settings);
		}
	}
}
