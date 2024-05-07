using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using TMPro;

namespace DefaultNamespace
{
    public class ButtonPic : MonoBehaviour
    {
        [SerializeField] private List<GameCard> cards;
        [SerializeField] private List<Sprite> sprites;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private TextMeshProUGUI Ui;
        [SerializeField] private int MaxScoreRound;
        [SerializeField] private int ErrorScore;
        [SerializeField] private ScorePanel scoreView;


        private GameCard _selectedCard;

        private int _currentPairs;
        private int curentScore;
        private int curentAddScore;
        private bool pairOpened = false;

        private void Start()
        {
            if (cards.Count != sprites.Count * 2)
            {
                Debug.LogError("ʳ������ ������ � ��������� �� ������� ���� �����");
                return;
            }

            _currentPairs = cards.Count / 2;
            curentAddScore = MaxScoreRound;
            GeneratePairs();
        }

        private void GeneratePairs()
        {
            var currentCards = new List<GameCard>(cards);
            var currentSprites = new Queue<Sprite>(sprites);

            while (currentSprites.Count > 0)
            {
                var sprite = currentSprites.Dequeue();
                AssignSpriteToCard(sprite, currentCards);
                AssignSpriteToCard(sprite, currentCards);
            }
        }

        private void AssignSpriteToCard(Sprite sprite, List<GameCard> cardList)
        {
            int randomIndex = Random.Range(0, cardList.Count);
            var card = cardList[randomIndex];
            cardList.RemoveAt(randomIndex);
            card.InitializeCard(sprite, this); // ��� �������� this
        }

        public void OnCardClicked(GameCard card)
        {
            if (pairOpened)
            {
                return;
            }
            if (_selectedCard == null)
            {
                _selectedCard = card;
                card.RevealCard();
            }
            else
            {
                
                card.RevealCard();
                if (_selectedCard.currentImage.sprite == card.currentImage.sprite && _selectedCard != card)
                {
                    // �������� �����������, �������� ������ ���������
                    // curentScore += 25;
                    _currentPairs--;
                    _selectedCard = null;

                    if (_currentPairs == 0)
                    {
                        curentScore += curentAddScore;
                        SumScore.ScoreLevel2 += curentScore;
                        UpdateScore();
                        Start();
                    }
                }
                else
                {
                    // �� �������� ����������, ����������� ������ ����� ����� ��������
                    curentAddScore -= ErrorScore;
                    pairOpened = true;
                    StartCoroutine(HideCardsAfterDelay(_selectedCard, card));
                    _selectedCard = null;
                }

            }
        }

        private IEnumerator HideCardsAfterDelay(GameCard card1, GameCard card2)
        {
            yield return new WaitForSeconds(3);
            card1.HideCard();
            card2.HideCard();
            pairOpened = false;
        }
        
        private void UpdateScore()
        {
            Debug.Log("[eq");
            text.text = $"Score: {curentScore}";
            Ui.text = $"Score: {curentScore}";
            scoreView.AcitivatePanel(curentScore);
        }
    }
    [Serializable]
    public class GameCard
    {
        public Button button;
        public Image upImage;
        public Image currentImage; // �� ���������� �� ������� ������� ������
        public Guid id; // ���������� ������������� ������

        public void InitializeCard(Sprite image, ButtonPic buttonPicScript)
        {
            id = Guid.NewGuid();
            currentImage.sprite = image;
            button.onClick.AddListener(() => buttonPicScript.OnCardClicked(this));
        }

        public void RevealCard()
        {
            upImage.gameObject.SetActive(false);
            // ����� ��� ����������� ������
        }

        public void HideCard()
        {
            upImage.gameObject.SetActive(true);
            // ����� ��� ������������ ������
        }
    }

}