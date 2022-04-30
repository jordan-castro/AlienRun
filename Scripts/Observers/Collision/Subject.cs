using Godot;
using System;
using System.Collections.Generic;

namespace Observers
{
	namespace Collision
	{
		public class Subject : ISubject
		{
			private List<Observer> observers = new List<Observer>();

			public void NotifyObservers()
			{
				// List that might need to be cleared
				List<Observer> toRemove = new List<Observer>();
				foreach (Observer observer in observers)
				{
					if (observer.ShouldRemove)
					{
						toRemove.Add(observer);
					}
					else
					{
						observer.Update(null);
					}
				}

				// Remove all the observers that should be removed
				foreach (Observer observer in toRemove)
				{
					RemoveObserver(observer);
				}
			}

			public void RegisterObserver(IObserver observer)
			{
				observers.Add(observer as Collision.Observer);
			}

			public void RemoveObserver(IObserver observer)
			{
				observers.Remove(observer as Collision.Observer);
			}
		}
	}
}
