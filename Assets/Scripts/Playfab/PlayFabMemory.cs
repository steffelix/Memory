using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFabMemory : MonoBehaviour
{
   public string Email { get; set; }
#region Register
    public void Register()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Email = Email.Trim(),
            Username = "Stef",
            Password = "Felix",
            DisplayName = "StefFelix"
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnAPICallFailure);
    }

    private void OnAPICallFailure(PlayFabError error)
    {
        Debug.Log("Error: "+ error.ErrorMessage);
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("user registration succeeded for: " + result.Username);
    }

    #endregion

    #region Login
    private LoginResult _loginResult;

    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = Email.Trim(),
            Password = "Felix"
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnAPICallFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        _loginResult = result;
        Debug.Log("Login succesfull");

        GetAccountInfo();
        GetUserInventory();
    }
    #endregion
    private void GetAccountInfo()
    {
        var request = new GetAccountInfoRequest
        {
            AuthenticationContext = _loginResult.AuthenticationContext
        };
        PlayFabClientAPI.GetAccountInfo(request, OnGetAccountInfoSuccess, OnAPICallFailure);
    }

    private void OnGetAccountInfoSuccess(GetAccountInfoResult result)
    {
        Debug.Log($"Account info success:  Username = {result.AccountInfo.Username} / Displayname: {result.AccountInfo.TitleInfo.DisplayName}");
    }

    private void GetUserInventory()
    {
        var request = new GetUserInventoryRequest
        {
            AuthenticationContext = _loginResult.AuthenticationContext
        };
        PlayFabClientAPI.GetUserInventory(request, OnGetUserInventorySuccess, OnAPICallFailure);

    }

    private void OnGetUserInventorySuccess(GetUserInventoryResult result)
    {
        Debug.Log("inventory:" + result.VirtualCurrency["GO"] + " gold");
        Debug.Log("inventory items: ");
        foreach (var ii in result.Inventory)
        {
            Debug.Log(ii.DisplayName);
        }
    }

    public void IncreasePlayerFunds()
    {
        var request = new AddUserVirtualCurrencyRequest
        {
            AuthenticationContext = _loginResult.AuthenticationContext,
            Amount = 10,
            VirtualCurrency = "GO"
        };
        PlayFabClientAPI.AddUserVirtualCurrency(request, OnAddUserVirtualCurrencySuccess, OnAPICallFailure);
    }

    private void OnAddUserVirtualCurrencySuccess(ModifyUserVirtualCurrencyResult result)
    {
        Debug.Log("Currency added, current balance: " + result.Balance);
    }


    public void PurchaseExampleItem()
    {
        var request = new PurchaseItemRequest
        {
            AuthenticationContext = _loginResult.AuthenticationContext,
            CatalogVersion = "Memory",
            ItemId = "Memory01",
            Price = 10,
            VirtualCurrency = "GO"

        };

        PlayFabClientAPI.PurchaseItem(request, OnPurchaseItemSuccess, OnAPICallFailure);
    }

    private void OnPurchaseItemSuccess(PurchaseItemResult obj)
    {
        Debug.Log("Succesfully bought");
        GetUserInventory();
    }
}
