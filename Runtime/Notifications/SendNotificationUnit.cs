using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Notifications
{
    [TypeIcon(typeof(CustomEvent))]
    [UnitCategory("Notifications")]
    [UnitSurtitle("Notifications")]
    [UnitTitle("Send Notification")]
    
    public class SendNotificationUnit: Unit
    {
        [SerializeAs(nameof(argumentCount))]
        private int _argumentCount;

        [DoNotSerialize]
        public List<ValueInput> arguments { get; private set; }

        [DoNotSerialize]
        [Inspectable, UnitHeaderInspectable("Arguments")]
        public int argumentCount
        {
            get => _argumentCount;
            set => _argumentCount = Mathf.Clamp(value, 0, 10);
        }

        /// <summary>
        /// The entry point to trigger the event.
        /// </summary>
        [DoNotSerialize]
        [PortLabelHidden]
        public ControlInput enter { get; private set; }

        /// <summary>
        /// The name of the event.
        /// </summary>
        [DoNotSerialize]
        [PortLabelHidden]
        public ValueInput notificationType { get; private set; }
        
        /// <summary>
        /// The action to do after the event has been triggered.
        /// </summary>
        [DoNotSerialize]
        [PortLabelHidden]
        public ControlOutput exit { get; private set; }

        protected override void Definition()
        {
            enter = ControlInput(nameof(enter), Trigger);

            exit = ControlOutput(nameof(exit));

            notificationType = ValueInput<NotificationTypeScriptableObject>(nameof(notificationType), null);
            
            arguments = new List<ValueInput>();

            for (var i = 0; i < argumentCount; i++)
            {
                var argument = ValueInput<object>("argument_" + i);
                arguments.Add(argument);
                Requirement(argument, enter);
            }

            Requirement(notificationType, enter);
            Succession(enter, exit);
        }

        private ControlOutput Trigger(Unity.VisualScripting.Flow flow)
        {
            var n = flow.GetValue<NotificationTypeScriptableObject>(this.notificationType);

            if (n != null)
            {
                var name = n.NotificationUniqueID;
                var arguments = this.arguments.Select(flow.GetConvertedValue).ToArray();
            
                EventBus.Trigger( NotificationEventUnit.EventBusHook, new CustomEventArgs(name, arguments));
            }
            
            return exit;
        }
    }
}