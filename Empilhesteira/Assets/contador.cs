using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class contador : MonoBehaviour
{
    public TextMeshProUGUI counterText;
    private int caixasEntregues = 0;
  
    public void AddContador(Component sender, object data){
        caixasEntregues++;
        UpdateContador();
    }

    private void UpdateContador(){
        counterText.text = caixasEntregues.ToString();
    }
}
