using UnityEngine;

namespace Communication.ManipulatableObjects.Variants.Ingredients
{
    public class CuttingSystem : MonoBehaviour
    {
        [SerializeField] private GameObject _root;
         
        [Header("Effects")]
        [SerializeField] private GameObject _cuttedPeaces;
        [Space, SerializeField] private GameObject _cuttedEffect;
        
        public void Cut()
        {
            //Instantiate(_cuttedEffect, transform.position, Quaternion.identity);
            Destroy(_root);
            //Instantiate(_cuttedPeaces, transform.position, Quaternion.identity);
        }
    }
}