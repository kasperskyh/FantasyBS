using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float followSpeed = 2f;  // Szybko��, z jak� kamera b�dzie pod��a� za obiektem.
    public float yOffset = 1f;      // Przesuni�cie kamery w osi Y, aby dostosowa� jej wysoko�� wzgl�dem obiektu.
    public Transform target;        // Obiekt, kt�ry kamera ma �ledzi� (np. gracz).

    // Update jest wywo�ywane raz na klatk� (frame)
    void Update()
    {
        // Tworzymy now� pozycj� kamery, bazuj�c na pozycji obiektu "target".
        // Zmieniamy tylko warto�� Y, aby kamera by�a zawsze "na wysoko�ci" obiektu, ale troch� wy�ej (przesuni�cie yOffset).
        Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, -10f);

        // Slerp (spherical interpolation) jest u�ywane do p�ynnego przej�cia kamery do nowej pozycji.
        // Dzi�ki temu ruch kamery nie jest nag�y, ale p�ynny.
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
    }
}