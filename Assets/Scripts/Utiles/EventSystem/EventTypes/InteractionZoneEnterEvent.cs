using WorkSpace;

namespace Utiles.EventSystem.EventTypes
{
    public struct InteractionZoneEnterEvent
    {
        public IWorkSpace WorkSpace { get; private set; }

        public InteractionZoneEnterEvent(IWorkSpace workSpace)
        {
            WorkSpace = workSpace;
        }
    }
}