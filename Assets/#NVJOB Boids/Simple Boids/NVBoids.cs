// Copyright (c) 2016 Unity Technologies.

// Librairie des oiseaux et voilieres : MIT license - license_unity.txt
// #NVJOB Simple Boids. MIT license - license_nvjob.txt
// #NVJOB Nicholas Veselov - https://nvjob.github.io
// #NVJOB Simple Boids v1.1.1 - https://nvjob.github.io/unity/nvjob-boids


using System.Collections;
using UnityEngine;

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


public class NVBoids : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    [Header("Préférences voilières")]
    [Range(1, 150)] public int nombreVolieres = 2;
    [Range(0, 5000)] public int fragmentationVoiliere = 30;
    [Range(0, 1.0f)] public float frequenceChangementPosition = 0.5f;

    [Header("Préférences Oiseaux")]
    public GameObject preferencesOiseau;
    [Range(1, 9999)] public int nombreOiseaux = 10;
    [Range(0, 150)] public float vitesseOiseau = 1;
    [Range(0, 100)] public int fragmentationOiseaux = 10;
    public Vector2 echelleRandom = new Vector2(1.0f, 1.5f);

    [Header("Préférences générales")]
    public Vector2 changementComportement = new Vector2(2.0f, 6.0f);
    public bool debug;
    //-------------- 

    Transform thisTransform;
    Transform[] transformeeOiseaux, transformeeVoliere;
    Vector3[] positionCible, positionVoliere, vitesseVoliere;
    float[] vitesseOiseaux, vitesseCouranteOiseaux, velociteSP;
    int[] voliereCourante;


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void Awake()
    {
        //--------------

        thisTransform = transform;
        creerVoliere();
        creerOiseaux();
        StartCoroutine(changerComportement());
        //StartCoroutine(Danger());

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void LateUpdate()
    {
        //--------------  

        deplacerVoilieres();
        deplacerOiseaux();

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void deplacerVoilieres()
    {
        // float lissageChangementFrequence = 0.5f;        

        for (int f = 0; f < nombreVolieres; f++)
        {
        //transformeeVoliere[f].localPosition = Vector3.SmoothDamp(transformeeVoliere[f].localPosition, positionVoliere[f], ref vitesseVoliere[f], lissageChangementFrequence);
        }
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void deplacerOiseaux()
    {
        for (int b = 0; b < nombreOiseaux; b++)
        {
            //vitesseCouranteOiseaux[b] = Mathf.SmoothDamp(vitesseCouranteOiseaux[b], vitesseOiseaux[b], ref velociteSP[b], 0.5f);
            //transformeeOiseaux[b].Translate(translateCur * vitesseOiseaux[b]);
            //else transformeeOiseaux[b].localRotation = clamperRotationOiseaux(rotationCur, rotationClamp);
        }

        //--------------
    }


    IEnumerator changerComportement()
    {
        //--------------

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(changementComportement.x, changementComportement.y));

            //---- Voilieres

            for (int f = 0; f < nombreVolieres; f++)
            {
                if (Random.value < frequenceChangementPosition)
                {
                    //positionVoliere[f] = new Vector3(rdvf.x, Mathf.Abs(rdvf.y), rdvf.z);
                }
            }

            //---- Oiseaux

            for (int b = 0; b < nombreOiseaux; b++)
            {
                //vitesseOiseaux[b] = Random.Range(3.0f, 7.0f);
                //positionCible[b] = new Vector3(lpv.x, lpv.y, lpv.z);
            } 
        }

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void creerVoliere()
    {
        //--------------

        transformeeVoliere = new Transform[nombreVolieres];
        positionVoliere = new Vector3[nombreVolieres];
        vitesseVoliere = new Vector3[nombreVolieres];
        voliereCourante = new int[nombreOiseaux];

        for (int positionVoliere = 0; positionVoliere < nombreVolieres; positionVoliere++)
        {
            GameObject nobj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            nobj.SetActive(debug);
            transformeeVoliere[positionVoliere] = nobj.transform;
            Vector3 rdvf = Random.onUnitSphere * fragmentationVoiliere;
            transformeeVoliere[positionVoliere].position = thisTransform.position;
            this.positionVoliere[positionVoliere] = new Vector3(rdvf.x, Mathf.Abs(rdvf.y), rdvf.z);
            transformeeVoliere[positionVoliere].parent = thisTransform;
        }

    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void creerOiseaux()
    {
        //--------------

        transformeeOiseaux = new Transform[nombreOiseaux];
        vitesseOiseaux = new float[nombreOiseaux];
        vitesseCouranteOiseaux = new float[nombreOiseaux];
        positionCible = new Vector3[nombreOiseaux];
        velociteSP = new float[nombreOiseaux];

        for (int b = 0; b < nombreOiseaux; b++)
        {
            transformeeOiseaux[b] = Instantiate(preferencesOiseau, thisTransform).transform;
            Vector3 lpv = Random.insideUnitSphere * fragmentationOiseaux;
            transformeeOiseaux[b].localPosition = positionCible[b] = new Vector3(lpv.x, lpv.y, lpv.z);
            transformeeOiseaux[b].localScale = Vector3.one * Random.Range(echelleRandom.x, echelleRandom.y);
            transformeeOiseaux[b].localRotation = Quaternion.Euler(0, Random.value * 360, 0);
            voliereCourante[b] = Random.Range(0, nombreVolieres);
            vitesseOiseaux[b] = Random.Range(3.0f, 7.0f);
        }

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    static Quaternion clamperRotationOiseaux(Quaternion rotationActuelle, float clampNePasDepasser)
    {
        //--------------

        Vector3 angleClamp = rotationActuelle.eulerAngles;
        rotationActuelle.eulerAngles = new Vector3(Mathf.Clamp((angleClamp.x > 180) ? angleClamp.x - 360 : angleClamp.x, -clampNePasDepasser, clampNePasDepasser), angleClamp.y, 0);
        return rotationActuelle;

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
