using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("Referências de UI")]
    [SerializeField] private GameObject pausePanel; // Arrastar o Pause_Panel aqui

    private bool isPausado = false;

    private void Update()
    {
        // Deteta se o jogador pressionou a tecla Escape (ESC) ou P
        var keyboard = Keyboard.current;
        if (keyboard != null && (keyboard.escapeKey.wasPressedThisFrame || keyboard.pKey.wasPressedThisFrame))
        {
            if (isPausado)
            {
                ContinuarJogo();
            }
            else
            {
                PausarJogo();
            }
        }
    }

    public void PausarJogo()
    {
        isPausado = true;
        pausePanel.SetActive(true); // Mostra a tela de pause
        Time.timeScale = 0f;        // Congela o tempo do jogo (física, colisões, etc.)
    }

    public void ContinuarJogo()
    {
        isPausado = false;
        pausePanel.SetActive(false); // Esconde a tela de pause
        Time.timeScale = 1f;         // Normaliza o tempo do jogo
    }

    public void ReiniciarNivel()
    {
        // Garante que o tempo volta ao normal antes de recarregar a cena!
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void VoltarAoMenuPrincipal()
    {
        Time.timeScale = 1f;
        // Substitui pelo index ou nome da tua cena de menu
        SceneManager.LoadScene("MenuPrincipal");
    }
}