using UnityEngine;

public class LightFlickerRandom : MonoBehaviour
{
    public Light lightToFlicker; // La lumi�re qui scintille
    public float minIntensity = 0.5f; // Intensit� minimale
    public float maxIntensity = 2.0f; // Intensit� maximale
    public float minFlickerInterval = 0.1f; // Intervalle minimum entre les scintillements
    public float maxFlickerInterval = 2.0f; // Intervalle maximum entre les scintillements
    public float transitionSpeed = 50.0f; // Vitesse de transition de l'intensit�

    private float targetIntensity; // Intensit� cible
    private float currentInterval; // Intervalle actuel entre les scintillements
    private float timer = 0.0f; // Compteur de temps

    void Start()
    {
        if (lightToFlicker == null)
        {
            lightToFlicker = GetComponent<Light>(); // Utilise la lumi�re attach�e
        }

        // D�finir le premier intervalle al�atoire et l'intensit� cible
        currentInterval = Random.Range(minFlickerInterval, maxFlickerInterval);
        targetIntensity = Random.Range(minIntensity, maxIntensity);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= currentInterval) // Changer l'intensit� lorsque le temps �coul� d�passe l'intervalle
        {
            targetIntensity = Random.Range(minIntensity, maxIntensity); // Nouvelle intensit� cible al�atoire
            currentInterval = Random.Range(minFlickerInterval, maxFlickerInterval); // Nouveau temps d'attente al�atoire
            timer = 0.0f; // R�initialiser le compteur de temps
        }

        // Transition rapide vers la nouvelle intensit�
        lightToFlicker.intensity = Mathf.Lerp(lightToFlicker.intensity, targetIntensity, Time.deltaTime * transitionSpeed);
    }
}
