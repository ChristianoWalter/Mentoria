using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class KnockbackController : MonoBehaviour
{
    public float knockbackTime;
    public float hitDirectionForce;
    public float constForce;
    public float inputForce;

    public bool IsBeingKnockedBack {  get; private set; }

    public IEnumerator KnockBackAction(Vector2 hitDirection, Vector2 constantForceDirection, float inputDirection)
    {
        IsBeingKnockedBack = true;

        Vector2 _hitForce;
        Vector2 _constantForce;
        Vector2 _knockbackForce;
        Vector2 _combinedForce;

        _hitForce = hitDirection * hitDirectionForce;
        _constantForce = constantForceDirection * constForce;

        float _elapsedTime = 0f;
        while(_elapsedTime < knockbackTime)
        {
            _elapsedTime += Time.fixedDeltaTime;

            _knockbackForce = _hitForce * _constantForce;

            if (inputDirection != 0)
            {
                _combinedForce = _knockbackForce + new Vector2(inputForce, 0f);
            }
            else
            {
                _combinedForce = _knockbackForce;
            }

            yield return new WaitForFixedUpdate();
        }

        IsBeingKnockedBack = false;
    }
}
