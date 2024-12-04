using TMPro;
using UnityEngine;

namespace ObserverPattern
{
    public class UiManager : Observer
    {
        [SerializeField] public TMP_Text scoreText;

        private int _score;

        public override void Notify(Subject subject)
        {
            _score += 10;
            
            scoreText.text = "Score: " + _score;
        }
    }
}
