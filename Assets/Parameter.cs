using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameter<T> : System.Object
{
	private T value;
	internal string description;
	private List<T> subscribers;


    // Start is called before the first frame update
    public Parameter()
    {
		this.description = "Nan";
		this.subscribers = new List<T>();
    }
	internal void SetValue(T newValue)
	{
		this.value = newValue;
		this.FireUpdate();
	}

	internal void SetDescription(string newDescription)
	{
		this.description = newDescription;
	}

	internal void Subscribe(T newSubscriber)
	{
		this.subscribers.Add(newSubscriber);
		newSubscriber = this.value;
	}
	private void FireUpdate()
	{
		for (int indSubscriber = 0 ; indSubscriber < this.subscribers.Count; indSubscriber++)
		{
			this.subscribers[indSubscriber] = this.value;
		}
	}
}
