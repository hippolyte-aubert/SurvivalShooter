using UnityEngine;

public class ButtonHover : MonoBehaviour
{
    public void ScaleOnHover(float scale)
    {
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
