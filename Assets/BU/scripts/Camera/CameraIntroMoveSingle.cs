using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraIntroMoveSingle : MonoBehaviour
{

    // نقطه شروع حرکت دوربین
    public Transform startMarker;

    // نقطه پایان حرکت دوربین
    public Transform endMarker;

    // شیء که نمایش نیروی توپ را نشان می‌دهد
    public GameObject ballForceShow;

    // سرعت حرکت دوربین
    public float speed = 0.3F;

    // زمان شروع حرکت دوربین
    private float startTime;

    // طول مسیر حرکت دوربین
    private float journeyLength;

    // مرجع به اسکریپت دوربین که به هدف نگاه می‌کند
    private CameraLookAtSingle cameraLook;

    // مرجع به توپ (برای استفاده در برخی شرایط)
    public Transform ball;

    // نور محیطی که قرار است فعال یا غیرفعال شود
    public GameObject light;



    // استفاده شده برای آغاز حرکت دوربین
    void Start()
    {
        // گرفتن مرجع به اسکریپت CameraLookAtSingle
        cameraLook = GetComponent<CameraLookAtSingle>();

        // ذخیره زمان شروع بازی
        startTime = Time.time;

        // غیرفعال کردن نمایش نیروی توپ در ابتدا
        ballForceShow.SetActive(false);

        // محاسبه طول مسیر حرکت دوربین
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    }

    // آپدیت در هر فریم
    void Update()
    {

        // محاسبه مسافت طی شده توسط دوربین
        float distCovered = (Time.time - startTime) * speed;

        // محاسبه درصد مسیر طی شده
        float fracJourney = distCovered / journeyLength;

        // حرکت دوربین از نقطه شروع به نقطه پایان
        transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);

        // زمانی که دوربین از نقطه شروع به موقعیت Z خاصی رسید
        if (transform.position.z > -119f)
        {

            // غیرفعال کردن نور عمومی (light)
            light.SetActive(false);

          

            // فعال کردن نمایش نیروی توپ
            ballForceShow.SetActive(true);

            // فعال کردن اسکریپت CameraLookAtSingle برای دنبال کردن توپ
            cameraLook.enabled = true;
        }
    }
}
