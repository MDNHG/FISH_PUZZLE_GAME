using UnityEngine;

public class Rotator_VS : MonoBehaviour
{
    Rotator rotator;
    void Start()
    {
        rotator = gameObject.GetComponentInParent<Rotator>();
        UpdateSpriteRotation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void UpdateSpriteRotation() {
        gameObject.transform.localScale = rotator.GetTurnDirection();
        gameObject.transform.eulerAngles = rotator.GetInputDirection();
}
}