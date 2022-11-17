using Notifications;
using Unity.VisualScripting;

namespace Editor.Notifications
{
    
    [Descriptor(typeof(SendNotificationUnit))]
    public class SendNotificationDescriptor : UnitDescriptor<SendNotificationUnit>
    {
        public SendNotificationDescriptor(SendNotificationUnit target) : base(target)
        {
        }
        
              
        protected override void DefinedPort(IUnitPort port, UnitPortDescription description)
        {
            base.DefinedPort(port, description);
            
            switch (port.key)
            {
                case "notificationType":
                    description.summary = "Notification asset created in the Project tab via Create > Notifications > Create Notification.";
                    break;
                case "sendOnce":
                    description.summary = "If true, the notification will only trigger once.";
                    break;
            }
        }
    }
}