using System;
using Comunication.ComunicatableObjects;
using UnityEngine;
using WorkSpace.View;
using Zenject.SpaceFighter;

public class CutingTable : MonoBehaviour, ICommunicatable
{
    public event Action OnCommunicationStart;
    public event Action OnCommunicationEnd;

    [SerializeField] private CommunicationViewController _communicationViewController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Bob.Bob>(out var player))
        {
            _communicationViewController.ShowUI();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Bob.Bob>(out var player))
        {
            _communicationViewController.HideUI();
        }
    }

    public void StartCommunication()
    {
        OnCommunicationStart?.Invoke();
        
        _communicationViewController.HideUI();
    }

    public void StopCommunication()
    {
        OnCommunicationEnd?.Invoke();
        
        _communicationViewController.ShowUI();
    }
}
