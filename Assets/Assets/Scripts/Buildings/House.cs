using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Buildings
{
    public class House : Building, ICollideable
    {
        public int capacity;
        private readonly ObservableCollection<Entity> _occupants = new();

        private void Start()
        {
            _occupants.CollectionChanged += OnNewRoom;
        }

        public async void OnCollision(Entity entity)
        {
            await entity.Stop(1000);
            entity.RefillSleep();
        }

        private void OnNewRoom(object sender, NotifyCollectionChangedEventArgs e)
        {
            NewRoom?.Invoke(this, gameObject);
        }

        public int GetOccupantsCount()
        {
            return _occupants.Count;
        }

        public static event EventHandler<GameObject> NewRoom;
    }
}