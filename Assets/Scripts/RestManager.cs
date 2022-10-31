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
        //Al ser una función que tardará en base a la conexión y respuesta del servidor
        //es una rutina de trabajo por lo que es necesario ponerle StarCoroutine.
        StartCoroutine(GetAll());
    }
    public IEnumerator GetAll()
    {

        //Hace petición de descarga
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

            //Convierte a JSONNode la información descargada
            JSONNode data = JSON.Parse(www.downloadHandler.text);

            //Muestra los datos
            materialText.text = "Product: " + data["name"] + "\n";
            materialText.text += "Id:  " + data["id"] + "\n";
            materialText.text += "Description " + data["weight"] + "\n";

        }
    }
    public IEnumerator GetMaterial(string material = "bulbasaur")
    {

        //La URL de la petición depende del parámetro country, por defecto es españa pero desde country selector es modificado para cada país seleccionable.
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
