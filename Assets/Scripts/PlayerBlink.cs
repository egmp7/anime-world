using UnityEngine;

public class PlayerBlink : MonoBehaviour
{
    [Header("Target SkinnedMeshRenderer")]
    [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;

    [Header("Blink Properties")]
    [Tooltip("Length of a blink in seconds")]
    [SerializeField] float BlinkLength;

    [Tooltip("Length between blinks in seconds")]
    [SerializeField] float BlinkInterval;

    [Tooltip("Random variable for blinking")]
    [SerializeField][Range(0, 6)] float BlinkingRandomness = 3;

    private float timeToNextBlink;
    private int blinkBlendShapeIndex;


    void Start()
    {
        timeToNextBlink = BlinkInterval;
        blinkBlendShapeIndex = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex("blink");
    }


    void Update()
    {
        if (Time.time > timeToNextBlink)
        {
            if (Time.time < timeToNextBlink + BlinkLength)
            {
                skinnedMeshRenderer.SetBlendShapeWeight(blinkBlendShapeIndex, 100);
            }
            else
            {
                skinnedMeshRenderer.SetBlendShapeWeight(blinkBlendShapeIndex, 0);
                timeToNextBlink = Time.time + BlinkInterval + Random.Range(0, BlinkingRandomness);
            }

        }
    }
}
