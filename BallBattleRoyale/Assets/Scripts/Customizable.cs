using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Customizable : MonoBehaviour
{

    public List<Material> customizations;
    private int currentCustomizationIndex;
    public GameObject nextButton;
    public GameObject prevButton;

    private new Renderer renderer;

    //public Transferer transferer;

    //public Customization currentCustomization { get; private set; }
    // Start is called before the first frame update
    void Start()
    {

        renderer = GameObject.Find("Sphere").GetComponent<Renderer>();
        renderer.material = customizations[currentCustomizationIndex];
        prevButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Transferer.transferMaterial = customizations[currentCustomizationIndex];
    }

    public void NextMaterial()
    {
        if (currentCustomizationIndex+1 == customizations.Count-1)
        {
            nextButton.SetActive(false);
        }
      
            currentCustomizationIndex++;
        
        if (prevButton.activeSelf == false)
        {
            prevButton.SetActive(true);
        }

        //Transferer.transferMaterial = customizations[currentCustomizationIndex];
        UpdateRenderer();
    }

    public void PreviousMaterial()
    {
        
        if (currentCustomizationIndex-1 == 0)
        {
            prevButton.SetActive(false);
        }
       
            currentCustomizationIndex--;
       
        if (nextButton.activeSelf == false)
        {
            nextButton.SetActive(true);
        }
        //Transferer.transferMaterial = customizations[currentCustomizationIndex];
        UpdateRenderer();
    }

    public void UpdateRenderer()
    {
        renderer.material = customizations[currentCustomizationIndex];
        Transferer.transferMaterial = customizations[currentCustomizationIndex];
    }
  
}

