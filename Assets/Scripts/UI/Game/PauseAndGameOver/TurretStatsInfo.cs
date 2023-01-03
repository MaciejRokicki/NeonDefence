using System.Text;
using UnityEngine;
using TMPro;

public class TurretStatsInfo : MonoBehaviour
{
    public void SetBuildedTurretCount(int count)
    {
        StringBuilder sb = new StringBuilder("Builded: ");
        sb.Append(count);

        gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = sb.ToString();
    }

    public void SetKilledBlocks(int count)
    {
        StringBuilder sb = new StringBuilder("Killed: ");
        sb.Append(count);

        gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = sb.ToString();
    }
}