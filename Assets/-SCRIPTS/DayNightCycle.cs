using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Time")]
    [SerializeField] private float sunsetDurationSeconds = 120f;
    [SerializeField] private bool playing = true;

    [Range(0f, 1f)] public float timeOfDay = 0f;

    private const float SunStartAngle = 60f;
    private const float SunEndAngle = -20f;

    private Light sun;
    private readonly Gradient sunColor = new();
    private readonly Gradient skyColor = new();
    private readonly Gradient groundColor = new();

    void OnEnable()
    {
        sun = GetComponent<Light>();

        sunColor.SetKeys( //Light coming from sun
            new GradientColorKey[]
            {
                new(new Color(1.0f, 0.9f, 0.7f), 0.0f),
                new(new Color(1.0f, 0.8f, 0.6f), 0.5f),
                new(new Color(0.75f, 0.1f, 0.05f), 1.0f),
            },
            new GradientAlphaKey[] { new(1f, 0f), new(1f, 1f) }
        );

        skyColor.SetKeys( //Light coming from above
            new GradientColorKey[]
            {
                new(new Color(0.4f, 0.6f, 0.9f), 0.0f),
                new(new Color(0.4f, 0.6f, 0.9f), 0.5f),
                new(new Color(0.45f, 0.4f, 0.2f), 0.65f),
                new(new Color(0.5f, 0.35f, 0.15f), 0.75f),
                new(new Color(0.45f, 0.3f, 0.1f), 0.8f),
                new(new Color(0.00f, 0.00f, 0.01f), 1.0f),
            },
            new GradientAlphaKey[] { new(1f, 0f), new(1f, 1f) }
        );

        groundColor.SetKeys( //Light coming from below
            new GradientColorKey[]
            {
                new(new Color(0.2f, 0.2f, 0.2f), 0.0f),
                new(new Color(0.0f, 0.0f, 0.0f), 1.0f),
            },
            new GradientAlphaKey[] { new(1f, 0f), new(1f, 1f) }
        );
    }

    void Update()
    {
        if (!Application.isPlaying || !playing) return;

        timeOfDay = Mathf.Min(timeOfDay + Time.deltaTime / sunsetDurationSeconds, 1f);
        if (timeOfDay >= 1f) playing = false;

        float pitch = Mathf.Lerp(SunStartAngle, SunEndAngle, timeOfDay);
        float t = Mathf.InverseLerp(SunStartAngle, SunEndAngle, pitch);

        transform.rotation = Quaternion.Euler(pitch, 170f, 0f);
        sun.color = sunColor.Evaluate(t);
        sun.intensity = Mathf.Lerp(1.2f, 0f, t);

        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
        RenderSettings.ambientSkyColor = skyColor.Evaluate(t);
        RenderSettings.ambientEquatorColor = skyColor.Evaluate(t) * 0.6f;
        RenderSettings.ambientGroundColor = groundColor.Evaluate(t);
        if (RenderSettings.skybox.HasProperty("_Exposure"))
            RenderSettings.skybox.SetFloat("_Exposure", Mathf.Lerp(1.2f, 0.1f, t));
    }
}