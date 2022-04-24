using Godot;
using System;
using System.Collections.Generic;

namespace Observer
{
    namespace Collision
    {
        public class Subject : ISubject
        {
            private List<IObserver> observers = new List<IObserver>();
            
            public void NotifyObservers()
            {
                foreach (IObserver observer in observers)
                {
                    observer.Update(null);
                }
            }

            public void RegisterObserver(IObserver observer)
            {
                observers.Add(observer);
            }

            public void RemoveObserver(IObserver observer)
            {
                observers.Remove(observer);
            }
        }
    }
}