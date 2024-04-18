using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text;
using System.ComponentModel;
using Unity.VisualScripting.Antlr3.Runtime;

public class PlayerProfilePage : MonoBehaviour
{
    private const string apiUrl = "http://20.15.114.131:8080/api/user/profile/view";
    private const string updateUrl = "http://20.15.114.131:8080/api/user/profile/update";


    public TMP_InputField firstName;
    public TMP_InputField lastName;
    public TMP_InputField username;
    public TMP_InputField NIC;
    public TMP_InputField phoneNumber;
    public TMP_InputField email;

    private UserData userdata;
    private NewUserData ChangedUserData;
    public Text InvalidFirstName;
    public Text InvalidLastName;
    public Text InvalidUserName;
    public Text InvalidNIC;
    public Text InvalidMobile;
    public Text InvalidEmail;

    public Toggle checkbox;
    public Text Prompt;

    void Start()
    {
        Debug.Log("Fletching data from the api...");
        StartCoroutine(FetchPlayerProfile());
        GameObject.Find("SaveButton").GetComponent<Button>().onClick.AddListener(SaveChanges);

    }

    IEnumerator FetchPlayerProfile()
    {
        if (string.IsNullOrEmpty(staticdata.JWTtoken))
        {
            Debug.LogError("Token not found.");
            yield break; // Exit the coroutine if token is not found
        }

        using (UnityWebRequest request = UnityWebRequest.Get(apiUrl))
        {
            // Set authorization header with JWT token
            request.SetRequestHeader("Authorization", "Bearer " + staticdata.JWTtoken);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                UserDataResponse userDataResponse = JsonUtility.FromJson<UserDataResponse>(jsonResponse);
                if (userDataResponse != null && userDataResponse.user != null)
                {
                    userdata = userDataResponse.user;
                    UpdateProfileFields();
                    Debug.Log("Player data received");
                    if (!string.IsNullOrEmpty(userdata.lastname) && !string.IsNullOrEmpty(userdata.email))
                    {
                        checkbox.isOn = true;
                    }
                    
                }
                else
                {
                    Debug.LogError("Error: User data is null or invalid JSON response structure.");
                }
                
            }
            else
            {
                Debug.LogError("Failed to fetch player profile: " + request.error);
                
            }
        }
    }

    IEnumerator UpdateUserData()
    {
        string jsonUserData = JsonUtility.ToJson(ChangedUserData);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonUserData);

        UnityWebRequest request1 = UnityWebRequest.Put(updateUrl, bodyRaw);
        Debug.Log(staticdata.JWTtoken);
        request1.SetRequestHeader("Authorization", "Bearer " + staticdata.JWTtoken);
        request1.SetRequestHeader("Content-Type", "application/json");
        yield return request1.SendWebRequest();

        if (request1.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("User data updated successfully!");
            //  StartCoroutine(SetProfileEdited());
            Prompt.text = "profile successfully updated..! ";
            yield return new WaitForSeconds(3f);
            Prompt.text = "";
        }
        else
        {
            Debug.LogError("Error updating user data: " + request1.error);
            Prompt.text = "Invalid Input..! ";
            yield return new WaitForSeconds(3f);
            Prompt.text = "";
        }
    }
    void UpdateProfileFields()
    {
        firstName.text = userdata.firstname;
        lastName.text = userdata.lastname;
        username.text = userdata.username;
        NIC.text = userdata.nic;
        phoneNumber.text = userdata.phoneNumber;
        email.text = userdata.email;
       
    }

    public void SaveChanges()
    {
        ChangedUserData = new NewUserData();
        ChangedUserData.firstname = firstName.text;
        ChangedUserData.lastname = lastName.text;
        ChangedUserData.nic = NIC.text;
        ChangedUserData.phoneNumber = phoneNumber.text;
        ChangedUserData.email = email.text;
        staticdata.Email = email.text;

        InvalidFirstName.text = "";
        InvalidLastName.text = "";
        InvalidNIC.text = "";
        InvalidMobile.text = "";
        InvalidEmail.text = "";


        if (string.IsNullOrEmpty(ChangedUserData.firstname))
        {
            InvalidFirstName.text = "Empty Input";
        }
        if (string.IsNullOrEmpty(ChangedUserData.lastname))
        {
            InvalidLastName.text = "Empty Input";
        }
        if (string.IsNullOrEmpty(ChangedUserData.nic))
        {
            InvalidNIC.text = "Empty Input";
        }
        if (string.IsNullOrEmpty(ChangedUserData.phoneNumber))
        {
            InvalidMobile.text = "Empty Input";
        }
        if (string.IsNullOrEmpty(ChangedUserData.email))
        {
            InvalidEmail.text = "Empty Input";
        }
        else
        {
           
            StartCoroutine(UpdateUserData());
        }
       

    }

    public void OnResetButton()
    {
        Debug.Log("Resetinging data...");
        StartCoroutine(FetchPlayerProfile());
        Debug.Log("Finished reseting data...");
    }





    [System.Serializable]
    private class UserDataResponse
    {
        public UserData user;
    }

    [System.Serializable]
    private class UserData
    {
        public string firstname;
        public string lastname;
        public string username;
        public string nic;
        public string phoneNumber;
        public string email;

    }

    private class NewUserData
    {
        public string firstname;
        public string lastname;
        public string nic;
        public string phoneNumber;
        public string email;

    }
}
