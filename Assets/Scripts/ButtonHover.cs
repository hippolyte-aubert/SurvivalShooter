using UnityEngine;

public class ButtonHover : MonoBehaviour
{
    public void ScaleOnHover(float scale)
    {
        AudioManager.Instance.Play("Hover");
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
