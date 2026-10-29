using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class HitboxTester : MonoBehaviour
{
    [SerializeField] private Hitbox hitbox;
    [SerializeField] private Health target;
    [SerializeField] private float activeTime = 0.2f;

    private int _hitCount;

    private void OnEnable()  => target.OnDamaged.AddListener(CountHit);
    private void OnDisable() => target.OnDamaged.RemoveListener(CountHit);
    private void CountHit(float _) => _hitCount++;

    private void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
            StartCoroutine(Swing());
    }

    [ContextMenu("Run 10 Swings")]
    private void RunTen() => StartCoroutine(RepeatSwings(10));

    private IEnumerator RepeatSwings(int count)
    {
        _hitCount = 0;
        for (int i = 0; i < count; i++)
            yield return Swing();
        Debug.Log($"{count} swings -> {_hitCount} hits (expected {count})");
    }

    private IEnumerator Swing()
    {
        hitbox.Enable();
        yield return new WaitForSeconds(activeTime);
        hitbox.Disable();
        yield return new WaitForFixedUpdate(); // let physics settle between swings
    }
}