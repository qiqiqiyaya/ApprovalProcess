using ApprovalProcess.Core.Converts.ToTransitions;
using ApprovalProcess.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace ApprovalProcess.Core.Converts.ToStateRepresentations
{
    public class EntityToStateRep : IConvertTo<StateRepresentationEntity, string, string>
    {
        private TransitionConvertContainer _container;

        public EntityToStateRep(TransitionConvertContainer container)
        {
            _container = container;
        }

        public Transition<string, string> To(StateRepresentationEntity parameter)
        {
            var converter = _container.Get<TransitionEntity, string, string>();

            var tsCollection = parameter.Transitions ?? throw new ArgumentNullException("");

            var transitions = tsCollection.Select(s => converter.To(s)).ToList();

            var sr = new StateRepresentation<string, string>(parameter.State);
        }
    }
}
