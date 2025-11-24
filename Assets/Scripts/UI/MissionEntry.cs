using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MissionEntry : MonoBehaviour
{
    public TextMeshProUGUI descText;
    public TextMeshProUGUI rewardText;
    public Button claimButton;
    public TextMeshProUGUI progressText;
    [SerializeField] private Image progressFill;

    public void FillWithMission(MissionBase m, MissionUI owner)
    {
        descText.text = m.GetMissionDesc();
        rewardText.text = "x"+  m.reward.ToString();
        progressFill.fillAmount = m.progress / m.max;
        if (m.isComplete)
        {
            claimButton.interactable = true;
            progressText.gameObject.SetActive(false);
			claimButton.onClick.AddListener(delegate { owner.Claim(m); } );
        }
        else
        {
            claimButton.interactable = false;
            progressText.gameObject.SetActive(true);
			progressText.text = ((int)m.progress) + " / " + ((int)m.max);
        }
    }

}
