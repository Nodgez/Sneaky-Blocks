using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyMobile;
public class SneakyBlocksStore : MonoBehaviour
{
	public void PurhcaseAdRemoval()
	{
		if (!InAppPurchasing.IsInitialized())
			return;

		var removeAdProduct = InAppPurchasing.GetIAPProductById("remove_ads");
		InAppPurchasing.Purchase(removeAdProduct);
	}

	private void OnEnable()
	{
		InAppPurchasing.PurchaseCompleted += InAppPurchasing_PurchaseCompleted;
		InAppPurchasing.PurchaseFailed += InAppPurchasing_PurchaseFailed;
		}

	private void InAppPurchasing_PurchaseFailed(IAPProduct product)
	{
		Debug.Log("purchased product: " + product.Name + " failed");
	}

	private void OnDisable()
	{
		InAppPurchasing.PurchaseCompleted -= InAppPurchasing_PurchaseCompleted;
		InAppPurchasing.PurchaseFailed -= InAppPurchasing_PurchaseFailed;
	}

	private void InAppPurchasing_PurchaseCompleted(IAPProduct product)
	{
		Debug.Log("purchased product: " + product.Name);
	}
}
