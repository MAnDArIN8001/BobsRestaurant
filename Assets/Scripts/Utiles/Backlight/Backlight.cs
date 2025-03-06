using UnityEngine;
using WorkSpace;
using WorkSpace.CutingTable;

namespace Utiles.Backlight
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