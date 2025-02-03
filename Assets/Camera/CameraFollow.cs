using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float followSpeed = 2f;  // Szybkoœæ, z jak¹ kamera bêdzie pod¹¿aæ za obiektem.
    public float yOffset = 1f;      // Przesuniêcie kamery w osi Y, aby dostosowaæ jej wysokoœæ wzglêdem obiektu.
    public Transform target;        // Obiekt, który kamera ma œledziæ (np. gracz).

    // Update jest wywo³ywane raz na klatkê (frame)
    void Update()
    {
        // Tworzymy now¹ pozycjê kamery, bazuj¹c na pozycji obiektu "target".
        // Zmieniamy tylko wartoœæ Y, aby kamera by³a zawsze "na wysokoœci" obiektu, ale trochê wy¿ej (przesuniêcie yOffset).
        Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, -10f);

        // Slerp (spherical interpolation) jest u¿ywane do p³ynnego przejœcia kamery do nowej pozycji.
        // Dziêki temu ruch kamery nie jest nag³y, ale p³ynny.
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
    }
}