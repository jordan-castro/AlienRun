using Godot;
using System;
using System.Collections.Generic;

namespace Observers
{
    namespace OffScreen
    {
        public class Subject : ISubject
        {
            private List<Observer> observers = new List<Observer>();
            private int bottom;

            public Subject(int bottom)
            {
                this.bottom = bottom;
            }

            public void NotifyObservers()
            {
                List<Observer> toRemove = new List<Observer>();

                foreach (var observer in observers)
                {
                    if (observer.ShouldRemove)
                    {
                        toRemove.Add(observer);
                    }
                    else
                    {
                        observer.Update(bottom);
                    }
                }

                foreach (var observer in toRemove)
                {
                    RemoveObserver(observer);
                }
            }

            public void RegisterObserver(IObserver observer)
            {
                observers.Add(observer as Observer);
            }

            public void RemoveObserver(IObserver observer)
            {
                observers.Remove(observer as Observer);
            }
        }
    }
}