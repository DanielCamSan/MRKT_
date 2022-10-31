using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;

public class RestManager : MonoBehaviour
{

    public static RestManager instance;
    public Text materialText;

    void Awake()
    {
        //Creamos un singleton (solo puede existir un RestManager)
        if (RestManager.instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        //Carga el resumen de la pantalla inicial
        //Al ser una funci�n que tardar� en base a la conexi�n y respuesta del servidor
        //es una rutina de trabajo por lo que es necesario ponerle StarCoroutine.
        StartCoroutine(GetAll());
    }
    public IEnumerator GetAll()
    {

        //Hace petici�n de descarga
        UnityWebRequest www = UnityWebRequest.Get("https://pokeapi.co/api/v2/pokemon/");
        yield return www.SendWebRequest();


        //En caso de error
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            materialText.text = "Error : " + www.error;
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            //Convierte a JSONNode la informaci�n descargada
            JSONNode data = JSON.Parse(www.downloadHandler.text);

            //Muestra los datos
            materialText.text = "Product: " + data["name"] + "\n";
            materialText.text += "Id:  " + data["id"] + "\n";
            materialText.text += "Description " + data["weight"] + "\n";

        }
    }
    public IEnumerator GetMaterial(string material = "bulbasaur")
    {

        //La URL de la petici�n depende del par�metro country, por defecto es espa�a pero desde country selector es modificado para cada pa�s seleccionable.
        UnityWebRequest www = UnityWebRequest.Get("https://pokeapi.co/api/v2/pokemon/" + material);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {

            JSONNode data = JSON.Parse(www.downloadHandler.text);
            materialText.text = "Product: " + data["name"] + "\n";
            materialText.text += "Id:  " + data["id"] + "\n";
            materialText.text += "Description " + data["weight"] + "\n";
        }
    }
 
}
