using System;
using System.Collections;
using System.Data;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
	static public UI_Manager instance;

	[SerializeField] private UI_Element[] allUIElements = new UI_Element[0];

	private void Awake()
	{
		instance = this;

		StartCoroutine(TimeoutCoroutine());
	}

	void callInitOnUIElements()
	{
		foreach (var element in allUIElements)
		{
			try
			{
				element.init();
			}
			catch (Exception error)
			{
				 Debug.Log(error);
			}
		}
	}

	public void openPage(UI_Element page)
	{
		page.gameObject.SetActive(true);
		page.onOpen();
	}

	public void closePage(UI_Element page)
	{
		page.gameObject.SetActive(false);
		page.onClose();
	}

	public void closeAllPage()
	{
		foreach (var element in allUIElements)
		{
			if (element.gameObject.activeSelf) closePage(element);
		}
	}

	public void OpenPageByType<T>(Type pageType) where T : UI_Element
	{
		foreach (var element in allUIElements)
		{
			if (pageType.IsInstanceOfType(element))
			{
				openPage(element);
				return;
			}
		}

		// Log error if the page was not found  
		Console.Error.WriteLine("Page of type " + pageType + " not found!");
	}

	public T GetPageByType<T>(Type pageType) where T : UI_Element
	{
		foreach (var element in allUIElements)
		{
			if (pageType.IsInstanceOfType(element))
			{
				return (T)element;
			}
		}

		// Log error if the page was not found  
		Console.Error.WriteLine("Page of type " + pageType + " not found!");
		return null; // Note: In C#, returning null works for reference types  
	}

	public void ClosePageByType<T>(Type pageType) where T : UI_Element
	{
		foreach (var element in allUIElements)
		{
			if (pageType.IsInstanceOfType(element))
			{
				closePage(element);
				return; // Exit after closing the first instance  
			}
		}

		// Log error if the page was not found  
		Console.Error.WriteLine("Page of type " + pageType + " not found!");
	}

	private IEnumerator TimeoutCoroutine()
	{
		yield return new WaitForSeconds(0.01f);
		callInitOnUIElements();
	}

}