using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LossCounterText : MonoBehaviour
{
    public Text lossCounterText;
    void Update()
    {   
        int lossCount = LossCounterManager.Instance.GetLossCount();
        if(lossCount == 0){
            lossCounterText.text = "Nie zostałeś złapany ani razu! Gratulacje!";
            return;
        }

        if(lossCount == 1){
            lossCounterText.text = "Zostałeś złapany tylko raz! Dobra robota!";
            return;
        }

        lossCounterText.text = "Zostałeś złapany " + LossCounterManager.Instance.GetLossCount() + " razy";
    }
}
