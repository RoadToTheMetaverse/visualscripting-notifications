using System;
using UnityEngine;

namespace Notifications
{
    [CreateAssetMenu(fileName = "New Notification Type", menuName = "Notifications/Create New Notification", order = 1)]
    public class NotificationTypeScriptableObject : ScriptableObject
    {
        [Tooltip("A unique ID for this notification. By default a new GUID (Globally Unique Identifier) is generated for you. You can change it, just make sure it's unique.")]
        public string NotificationUniqueID = Guid.NewGuid().ToString();
    }
}