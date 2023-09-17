using Cinemachine;
using UnityEngine;
namespace Assets.Scripts.Runtime.Extentions
{
    //bu komponentin Editör modunda da çalışmasına izin verir
    [ExecuteInEditMode]
    //bu komponentin oyunu oynarken yapılan değişikliklerini kaydetmesine izin verir
    [SaveDuringPlay]
    //bu komponenti sürükle-bırak veya menüden eklemek yerine, kod içinde başka bir nesneye eklemek için kullanılır
    [AddComponentMenu("")]
    public class LookCinemachineAxis : CinemachineExtension
    {
        [Tooltip("Lock the camera's X position to this value")]
        public byte m_XPosition=0;

        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            //eğer işlenen aşama "Body" aşamasıysa, yani kameranın pozisyonunun hesaplandığı aşamadaysak, aşağıdaki kodları çalıştırır.
            if (stage == CinemachineCore.Stage.Body)
            {
                //mevcut kamera pozisyonunu pos adlı bir değişkene kopyalar
                var pos = state.RawPosition;
                //kameranın X pozisyonunu m_XPosition değeri ile değiştirir
                pos.x = m_XPosition;
                //kameranın pozisyonunu günceller ve sabitlenmiş X pozisyonunu atar. Bu sayede, kameranın X pozisyonu istenen değere sabitlenir
                state.RawPosition = pos;
            }
        }
    }
}