using UnityEngine;

public class LightFlickerRandom : MonoBehaviour
{
    public Light lightToFlicker; // La lumière qui scintille
    public float minIntensity = 0.5f; // Intensité minimale
    public float maxIntensity = 2.0f; // Intensité maximale
    public float minFlickerInterval = 0.1f; // Intervalle minimum entre les scintillements
    public float maxFlickerInterval = 2.0f; // Intervalle maximum entre les scintillements
    public float transitionSpeed = 50.0f; // Vitesse de transition de l'intensité

    private float targetIntensity; // Intensité cible
    private float currentInterval; // Intervalle actuel entre les scintillements
    private float timer = 0.0f; // Compteur de temps

    void Start()
    {
        if (lightToFlicker == null)
        {
            lightToFlicker = GetComponent<Light>(); // Utilise la lumière attachée
        }

        // Définir le premier intervalle aléatoire et l'intensité cible
        currentInterval = Random.Range(minFlickerInterval, maxFlickerInterval);
        targetIntensity = Random.Range(minIntensity, maxIntensity);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= currentInterval) // Changer l'intensité lorsque le temps écoulé dépasse l'intervalle
        {
            targetIntensity = Random.Range(minIntensity, maxIntensity); // Nouvelle intensité cible aléatoire
            currentInterval = Random.Range(minFlickerInterval, maxFlickerInterval); // Nouveau temps d'attente aléatoire
            timer = 0.0f; // Réinitialiser le compteur de temps
        }

        // Transition rapide vers la nouvelle intensité
        lightToFlicker.intensity = Mathf.Lerp(lightToFlicker.intensity, targetIntensity, Time.deltaTime * transitionSpeed);
    }
}
