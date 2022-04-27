using Godot;
using System;
using System.Collections.Generic;

namespace Observers
{
	namespace Collision
	{
		public class Subject : ISubject
		{
			private List<Collision.Observer> observers = new List<Collision.Observer>();

			public void NotifyObservers()
			{
				foreach (Observer observer in observers)
				{
					// Should we remove this observer?
					if (observer.ShouldRemove())
					{
						GD.Print("Removing observer");
						RemoveObserver(observer);
					}
					else
					{
						observer.Update(null);
					}
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
