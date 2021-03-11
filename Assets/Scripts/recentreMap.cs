using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recentreMap : MonoBehaviour
{
    public GameObject map;

    public void reset() {
        map.transform.position = new Vector2(0, 0);
    }
}
