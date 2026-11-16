using UnityEngine;

public class IKprueba : MonoBehaviour
{
    public Transform body;
    public float stepDistance;
    private float footSpacing;
    [SerializeField] LayerMask terrainLayer = default;
    Vector3 currentPos, newPosition, oldPos;
    float lerp;
    public float speed;
    public float stepHeight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPos = newPosition= oldPos= transform.position;
        footSpacing = transform.localPosition.x;
        lerp = 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = currentPos;
        Ray ray = new Ray(body.position + (body.right * footSpacing), Vector3.down);
        Debug.DrawRay(body.position + (body.right * footSpacing), Vector3.down);
        if(Physics.Raycast(ray, out RaycastHit info, 10, terrainLayer))
        {
            if(Vector3.Distance(newPosition, info.point) > stepDistance)
            {
                newPosition = info.point;
                lerp = 0;
            }
        }
        if (lerp < 1)
        {
            Vector3 footPos = Vector3.Lerp(oldPos, newPosition, lerp);
            footPos.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;
            currentPos = footPos;
            lerp += Time.deltaTime * speed;
        }
        else
        {
            oldPos = newPosition;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(newPosition, .5f);
    }
}
