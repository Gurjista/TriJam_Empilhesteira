using TMPro; // Importar TextMeshPro
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float initialTime = 300f; // Tempo inicial em segundos (5 minutos)
    private float currentTime; // Tempo atual do timer
    public TextMeshProUGUI timerText; // Referência para o objeto TextMeshPro

    private void Start()
    {
        currentTime = initialTime; // Inicializa o timer com o tempo inicial
        UpdateTimerDisplay(); // Atualiza a exibição do timer
    }

    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime; // Diminui o tempo atual com o tempo decorrido
            UpdateTimerDisplay(); // Atualiza a exibição do timer
        }
        else
        {
            currentTime = 0; // Garante que o tempo não fique negativo
            UpdateTimerDisplay(); // Atualiza a exibição do timer
        }
    }

    public void AddTime(float seconds)
    {
        currentTime += seconds; // Adiciona tempo ao timer atual
        UpdateTimerDisplay(); // Atualiza a exibição do timer
    }

    private void UpdateTimerDisplay()
    {
        // Converte o tempo atual em minutos e segundos
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        // Atualiza o texto do TextMeshPro com o formato MM:SS
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}