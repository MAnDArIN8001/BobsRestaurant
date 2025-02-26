using WorkSpace;

namespace Utiles.EventSystem.EventTypes
{
    public struct InteractionZoneExitEvent
    {
        public IWorkSpace WorkSpace { get; private set; }

        public InteractionZoneExitEvent(IWorkSpace workSpace)
        {
            WorkSpace = workSpace;
        }
    }
}