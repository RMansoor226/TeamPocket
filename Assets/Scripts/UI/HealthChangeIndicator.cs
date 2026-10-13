using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    [SerializeField] Image IndicatorImage;
    [SerializeField] float FadeSpeed = 2f;

    public void DamageFlash()
    {
        IndicatorImage.color = new Color(1f, 0, 0, 0.4f);;
        IndicatorImage.enabled = true;
        this.enabled = true;
    }

    public void HealFlash()
    {
        IndicatorImage.color = new Color(0, 1f, 0, 0.4f);;
        IndicatorImage.enabled = true;
        this.enabled = true;
    }

    private void Update()
    {
        var c = IndicatorImage.color;
        c.a = Mathf.MoveTowards(c.a, 0f, FadeSpeed * Time.deltaTime);
        IndicatorImage.color = c;

        if (c.a <= 0f)
        {
            IndicatorImage.enabled = false;
            this.enabled = false;
        }
    }
}
