using Comunication.ComunicatableObjects;
using UnityEngine;

namespace WorkSpace.CutingTable
{
    public class Knife : MonoBehaviour, ICommunicatable
    {
        [SerializeField] private Transform _handPosition;
        [SerializeField] private Transform _root;

        public Transform HandPosition => _handPosition;
        public Transform Root => _root;
    }
}