using UnityEngine;
using WorkSpace.CutingTable;

namespace WorkSpace
{
    public class Backlight : MonoBehaviour, IBacklighable
    {
        [SerializeField] private BacklightController _backlightController;
        
        public void EnableBacklight()
        {
            _backlightController.EnableBacklight();
        }

        public void DisableBacklight()
        {
            _backlightController.DisableBacklight();
        }
    }
}