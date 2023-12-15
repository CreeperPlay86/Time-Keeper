using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
	public static TimerManager instance;

	public InteractableItem[] items;
	int n = 0;

	private void Awake()
	{
		instance = this;
	}
	private void Start()
	{
		ActiveNext();
	}

	public void DisableAllItmes()
	{
		foreach (var item in items)
		{
			item.SetInteract(false);
		}
	}
	public void ActiveNext()
	{
		DisableAllItmes();
		if(n == items.Length) n = 0;
		items[n].SetInteract(true);
		n++;
	}
}
