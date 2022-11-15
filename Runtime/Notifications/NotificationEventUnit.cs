using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Notifications
{
    
    [TypeIcon(typeof(CustomEvent))]
    [UnitCategory("Notifications")]
    [UnitSurtitle("Notifications")]
    [UnitTitle("Handle Notification")]

    public class NotificationEventUnit: GlobalEventUnit<CustomEventArgs>
    {
        
        public const string EventBusHook = "Notifications.EventBusHook";
        protected override string hookName => NotificationEventUnit.EventBusHook;

        [SerializeAs(nameof(argumentCount))]
        private int _argumentCount;

        [DoNotSerialize]
        [Inspectable, UnitHeaderInspectable("Arguments")]
        public int argumentCount
        {
            get => _argumentCount;
            set => _argumentCount = Mathf.Clamp(value, 0, 10);
        }

        /// <summary>
        /// The name of the event.
        /// </summary>
        [DoNotSerialize]
        [PortLabelHidden]
        public ValueInput notificationType { get; private set; }

        [DoNotSerialize]
        public List<ValueOutput> argumentPorts { get; } = new List<ValueOutput>();

        protected override void Definition()
        {
            base.Definition();

            notificationType = ValueInput<NotificationTypeScriptableObject>(nameof(notificationType), null);

            argumentPorts.Clear();

            for (var i = 0; i < argumentCount; i++)
            {
                argumentPorts.Add(ValueOutput<object>("argument_" + i));
            }
        }

        protected override bool ShouldTrigger(Unity.VisualScripting.Flow flow, CustomEventArgs args)
        {
            var n = flow.GetValue<NotificationTypeScriptableObject>(notificationType);
            
            if (string.IsNullOrEmpty(args.name) || n == null)
                return false;
            else
                return n.NotificationUniqueID.Equals(args.name);
            
        }

        protected override void AssignArguments(Unity.VisualScripting.Flow flow, CustomEventArgs args)
        {
            for (var i = 0; i < argumentCount; i++)
            {
                flow.SetValue(argumentPorts[i], args.arguments[i]);
            }
        }
        
    }
}