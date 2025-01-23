using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAtSingle : MonoBehaviour
{

    // مرجع به هدفی که دوربین باید به آن نگاه کند (مثلاً یک بازیکن یا شیء)
    public Transform target;

    // مرجع به اسکریپت حرکت دوربین در شروع بازی
    private CameraIntroMoveSingle cameraIntro;

    // مرجع به اسکریپت توپ (برای چک کردن موقعیت توپ)
    public BallSinglePlayer ball;

    void Start()
    {
        // گرفتن مرجع به اسکریپت CameraIntroMoveSingle
        cameraIntro = GetComponent<CameraIntroMoveSingle>();

        // غیرفعال کردن اسکریپت حرکت دوربین در شروع بازی
        cameraIntro.enabled = false;
    }

    // آپدیت در هر فریم
    void Update()
    {
        // اگر موقعیت توپ در محور Z کمتر از 107 باشد (یعنی توپ در نزدیکی دوربین است)
        if (ball.transform.position.z < 107f)
        {

            // موقعیت دوربین به‌گونه‌ای تغییر می‌کند که از هدف فاصله بگیرد و کمی پایین‌تر و عقب‌تر قرار گیرد
            // این تغییر موقعیت باعث می‌شود دوربین به هدف نگاه کند
            transform.position = (target.position - new Vector3(0, -6, 25));
        }
    }
}
