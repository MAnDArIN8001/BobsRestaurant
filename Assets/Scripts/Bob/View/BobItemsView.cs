using Bob.Comunication.Items;
using Bob.View.Rigging;
using Communication.ManipulatableObjects;
using UnityEngine;

namespace Bob.View
{
    public class BobItemsView : MonoBehaviour
    {
        [Header("Right")]
        [SerializeField] private Transform _rightRoot;
        [SerializeField] private Transform _rightItemRoot;

        private bool _isRightRootEnabled;

        [Header("Left")] 
        [SerializeField] private Transform _leftRoot;
        [SerializeField] private Transform _leftItemRoot;

        private bool _isLeftRootEnabled;
        
        [Header("Items controls")]
        [SerializeField] private ItemsPickerSystem _itemsPickerSystem;
        [SerializeField] private ItemsDropingSystem _itemsDropingSystem;
        
        [Space, SerializeField] private BobRiggingController _riggingController;

        private void OnEnable()
        {
            if (_itemsPickerSystem is not null)
            {
                _itemsPickerSystem.OnPickItem += HandlePickItem;
            }

            if (_itemsDropingSystem is not null)
            {
                _itemsDropingSystem.OnDropItem += HandleDropItem;
            }
        }

        private void OnDisable()
        {
            if (_itemsPickerSystem is not null)
            {
                _itemsPickerSystem.OnPickItem -= HandlePickItem;
            }
        }
        
        private void HandlePickItem(IBaseItem obj)
        {
            if (_isRightRootEnabled)
            {
                return;
            }

            _isRightRootEnabled = true;
            
            _riggingController.SetRightHandRoot(_rightRoot);
        }

        private void HandleDropItem()
        {
            _isRightRootEnabled = false;
            
            _riggingController.DisableRightHandIK();
        }
    }
}