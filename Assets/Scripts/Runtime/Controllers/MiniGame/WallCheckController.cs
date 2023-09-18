using Assets.Scripts.Runtime.Managers;
using DG.Tweening;
using UnityEngine;

public class WallCheckController : MonoBehaviour
{
    [SerializeField] private MiniGameManager manager;
    private float _changesColor;
    private float _multiplier;
    private readonly string _wall = "Wall";
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(_wall)) return;
        _multiplier += 0.1f;
        manager.SetMultiplier(_multiplier);
        ChangeColor(other);
    }

    private void ChangeColor(Collider other)
    {
        _changesColor = (0.036f + _changesColor) % 1;
        var otherGameObject = other.gameObject;
        otherGameObject.GetComponent<Renderer>().material.DOColor(Color.HSVToRGB(_changesColor, 1, 1), 0.1f);
        otherGameObject.transform.DOLocalMoveZ(-3, 0.1f);
    }

    internal void OnReset()
    {
        _changesColor = 0;
        _multiplier = 0.90f;
    }
}
