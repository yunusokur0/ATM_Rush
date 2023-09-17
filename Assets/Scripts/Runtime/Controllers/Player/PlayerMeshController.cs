using TMPro;
using UnityEngine;

namespace Assets.Scripts.Runtime.Controllers.Player
{
    public class PlayerMeshController : MonoBehaviour
    {
        [SerializeField] private TextMeshPro scoreText;

        internal void SetTotalScore(int value)
        {
            scoreText.text = value.ToString();
        }
    }
}