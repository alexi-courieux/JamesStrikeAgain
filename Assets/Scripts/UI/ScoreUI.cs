using ashlight.james_strike_again.Entities;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace ashlight.james_strike_again.UI
{
    public class ScoreUI : MonoBehaviour
    {
        private TextMeshProUGUI _textMesh;
        private int _score = 0;
        [SerializeField] private int deathBetweenMajorUpgrade = 3;

        private void Awake()
        {
            _textMesh = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            Player.Instance.OnDeath += Score;
            UpdateUI();
        }

        private void Score()
        {
            _score++;
            UpdateUI();
            if (_score % deathBetweenMajorUpgrade == 0)
            {
                // Major upgrade
                Player.Instance.MajorUpgrade();
            }
            else
            {
                // Minor upgrade
                Player.Instance.MinorUpgrade();
            }
        }

        private void UpdateUI()
        {
            _textMesh.text = _score.ToString();
        }
    }
}
