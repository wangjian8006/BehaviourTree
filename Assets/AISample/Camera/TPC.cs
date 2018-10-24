using UnityEngine;
using System.Collections;
using System;

namespace RoleController
{

    public class TPCFollowData
    {
        public float fDistFactorMin = 0.0f;

        public float fDistFactorMax = 0.0f;

        public float fFollowPitchMin = 0.0f;

        public float fFollowPitchMax = 0.0f;

        public float fFollowOffsetMinY = 0.0f;

        public float fFollowOffsetMaxY = 0.0f;

        public float fFollowEulerY = 0.0f;

        public float fFollowFactor = 0.0f;

        public void SetData(float fDistFactorMin,
                            float fDistFactorMax,
                            float fFollowPitchMin,
                            float fFollowPitchMax,
                            float fFollowOffsetMinY,
                            float fFollowOffsetMaxY,
                            float fFollowEulerY,
                            float fFollowFactor)
        {
            this.fDistFactorMin = fDistFactorMin;
            this.fDistFactorMax = fDistFactorMax;
            this.fFollowPitchMin = fFollowPitchMin;
            this.fFollowPitchMax = fFollowPitchMax;
            this.fFollowOffsetMinY = fFollowOffsetMinY;
            this.fFollowOffsetMaxY = fFollowOffsetMaxY;
            this.fFollowEulerY = fFollowEulerY;
            this.fFollowFactor = fFollowFactor;
        }

        public void SetData(TPCFollowData data)
        {
            this.fDistFactorMin = data.fDistFactorMin;
            this.fDistFactorMax = data.fDistFactorMax;
            this.fFollowPitchMin = data.fFollowPitchMin;
            this.fFollowPitchMax = data.fFollowPitchMax;
            this.fFollowOffsetMinY = data.fFollowOffsetMinY;
            this.fFollowOffsetMaxY = data.fFollowOffsetMaxY;
            this.fFollowEulerY = data.fFollowEulerY;
            this.fFollowFactor = data.fFollowFactor;
        }
    }

    public class TPC : FCC
    {
        private static TPC s_instance;
        public static TPC Instance { get { return s_instance; } }

        protected Vector3 m_vCameraSmoothVelocity = Vector3.zero;

//      public float m_fCameraDistance;

        protected float m_fCameraYOffset;

        protected float m_fCameraScroollVelocity = 0.0f;

        [Range(1.0f, 10.0f)]
        public float m_fDistFactorMin = 6.0f;
        [Range(1.0f, 100.0f)]
        public float m_fDistFactorMax = 20.0f;
        // 跟随高度
        [Range(0.0f, 30.0f)]
        public float m_fFollowPitchMin = 30.0f;
        [Range(0.0f, 80.0f)]
        public float m_fFollowPitchMax = 40f;
        [Range(0.0f, 1.0f)]
        public float m_fFollowFactor = 1.0f;

        public float m_fFollowoffestMiniY = -0.23f;

        public float m_fFollowoffestMaxY = -0.3f;

        public float m_fFollowEulerY = 100f;

        public string m_easeType = string.Empty;

        public override void Awake()
        {
            s_instance = this;
            RecordParam();
            base.Awake();
        }

        public void Start()
        {
            m_fCameraYOffset = 2.0f;
            m_fCameraDistance = (m_fDistFactorMin + m_fDistFactorMax) * 0.5f;
            if (m_AttachCamera != null)
            {
                UpdateFollowData();

                UpdateAttachCamera();
            }
        }

        public void UpdateCamerePos(bool bSmooth)
        {
            if (m_AttachElement == null || m_AttachCamera == null)
                return;

            Vector3 vCameraAt = m_AttachElement.transform.position;
            vCameraAt.y += m_fCameraYOffset - Mathf.Lerp(m_fFollowoffestMiniY, m_fFollowoffestMaxY, m_fFollowFactor);
            Vector3 vCameraForward = m_AttachCamera.transform.forward;
            m_vCameraTargetPosition = vCameraAt - m_fCameraDistance * vCameraForward;
            if (bSmooth == true)
            {
                Vector3 vLastPos = m_AttachCamera.transform.position;
                Vector3 vSmoothPos = Vector3.SmoothDamp(vLastPos, m_vCameraTargetPosition, ref m_vCameraSmoothVelocity, 0.05f);
                m_AttachCamera.transform.position = vSmoothPos;
            }
            else
            {
                m_AttachCamera.transform.position = m_vCameraTargetPosition;
            }
        }
        public void RotateCamera(float fRotateDistance)
        {
            Vector3 vEuler = m_AttachCamera.transform.eulerAngles;
            vEuler.y += fRotateDistance * 720.0f / Screen.width;
            m_AttachCamera.transform.eulerAngles = vEuler;
            UpdateCamerePos(false);
        }

        public void SetYAngle(float fAngle)
        {
            Vector3 vEuler = m_AttachCamera.transform.eulerAngles;
            vEuler.y = fAngle;
            m_AttachCamera.transform.eulerAngles = vEuler;
            UpdateCamerePos(false);
        }

        void UpdateFollowData()
        {
            m_fCameraDistance = Mathf.Lerp(m_fDistFactorMin, m_fDistFactorMax, m_fFollowFactor);
            Vector3 vEuler = m_AttachCamera.transform.eulerAngles;
            vEuler.x = Mathf.Lerp(m_fFollowPitchMin, m_fFollowPitchMax, m_fFollowFactor);
            vEuler.y = m_fFollowEulerY;
            m_AttachCamera.transform.eulerAngles = vEuler;
        }
        
        void OnValidate()
        {
            UpdateFollowData();

        }

        void UpdateAttachCamera()
        {

        }

        public override void Update()
        {
            UpdateData();
            if (m_AttachCamera == null)
            {
                m_AttachCamera = Camera.main.GetComponent<Camera>();
                UpdateFollowData();
                UpdateCamerePos(false);
                UpdateAttachCamera();
                //SetYAngle(0.0f);
            }
            m_fCameraScroollVelocity = m_fCameraScroollVelocity * 0.9f + Input.mouseScrollDelta.y *1.5f;// Input.mouseScrollDelta.y * 1.5f;
            if (m_fCameraScroollVelocity != 0.0f)
            {
                UpdateFollowData();
                m_fFollowFactor = Mathf.Clamp01(m_fFollowFactor - m_fCameraScroollVelocity * Time.deltaTime);
                //UpdateCamerePos(false);
            }
            UpdateCamerePos(false);
        }

        

        private TPCFollowData dataPre = new TPCFollowData();

        private TPCFollowData dataStart = new TPCFollowData();

        private TPCFollowData dataEnd = new TPCFollowData();

        private float m_fStartTime = float.MinValue;

        private float m_fTotalTime = 2.0f;

        public void SetData(TPCFollowData data)
        {
            dataStart.SetData(m_fDistFactorMin,
                            m_fDistFactorMax,
                            m_fFollowPitchMin,
                            m_fFollowPitchMax,
                            m_fFollowoffestMiniY,
                            m_fFollowoffestMaxY,
                            m_fFollowEulerY,
                            m_fFollowFactor);

            dataEnd.SetData(data);
            LerpData(1.0f);
        }

        public void SetData(float TotalTime,
                            float fDistFactorMin,
                            float fDistFactorMax,
                            float fFollowPitchMin,
                            float fFollowPitchMax,
                            float fFollowOffsetMinY,
                            float fFollowOffsetMaxY,
                            float fFollowEulerY,
                            float fFollowFactor,
                            string easeType)
        {
            dataStart.SetData(m_fDistFactorMin,
                            m_fDistFactorMax,
                            m_fFollowPitchMin,
                            m_fFollowPitchMax,
                            m_fFollowoffestMiniY,
                            m_fFollowoffestMaxY,
                            m_fFollowEulerY,
                            m_fFollowFactor);

            dataEnd.SetData(fDistFactorMin,
                            fDistFactorMax,
                            fFollowPitchMin,
                            fFollowPitchMax,
                            fFollowOffsetMinY,
                            fFollowOffsetMaxY,
                            fFollowEulerY,
                            fFollowFactor);

            m_fStartTime = Time.time;
            m_fTotalTime = TotalTime;
            m_easeType = easeType;
        }

        public void ResetParam()
        {
            dataStart.SetData(m_fDistFactorMin,
                            m_fDistFactorMax,
                            m_fFollowPitchMin,
                            m_fFollowPitchMax,
                            m_fFollowoffestMiniY,
                            m_fFollowoffestMaxY,
                            m_fFollowEulerY,
                            m_fFollowFactor);

            dataEnd.SetData(dataPre);

            m_fStartTime = Time.time;
        }

        private void RecordParam()
        {
            dataPre.SetData(m_fDistFactorMin,
                            m_fDistFactorMax,
                            m_fFollowPitchMin,
                            m_fFollowPitchMax,
                            m_fFollowoffestMiniY,
                            m_fFollowoffestMaxY,
                            m_fFollowEulerY,
                            m_fFollowFactor);

            dataStart.SetData(dataPre);
            dataEnd.SetData(dataPre);
        }

        private void LerpData(float fRate)
        {
            /*EEaseType et = EEaseType.linear;
            if (!string.IsNullOrEmpty(m_easeType))
            {
                et = (EEaseType)Enum.Parse(typeof(EEaseType), m_easeType);
            }
            fRate = EaseUtils.Instance.GetValue(0, 1, fRate, et);*/
            m_fDistFactorMin = Mathf.Lerp(dataStart.fDistFactorMin, dataEnd.fDistFactorMin, fRate);
            m_fDistFactorMax = Mathf.Lerp(dataStart.fDistFactorMax, dataEnd.fDistFactorMax, fRate);
            m_fFollowPitchMin = Mathf.Lerp(dataStart.fFollowPitchMin, dataEnd.fFollowPitchMin, fRate);
            m_fFollowPitchMax = Mathf.Lerp(dataStart.fFollowPitchMax, dataEnd.fFollowPitchMax, fRate);

            m_fFollowoffestMiniY = Mathf.Lerp(dataStart.fFollowOffsetMinY, dataEnd.fFollowOffsetMinY, fRate);
            m_fFollowoffestMaxY = Mathf.Lerp(dataStart.fFollowOffsetMaxY, dataEnd.fFollowOffsetMaxY, fRate);
            m_fFollowFactor = Mathf.Lerp(dataStart.fFollowFactor, dataEnd.fFollowFactor, fRate);
            m_fFollowEulerY = Mathf.Lerp(dataStart.fFollowEulerY, dataEnd.fFollowEulerY, fRate);
            UpdateFollowData();
        }

        private void UpdateData()
        {
            if (Time.time - m_fStartTime <= m_fTotalTime)
            {
                float fRate = (Time.time - m_fStartTime) / m_fTotalTime;
                LerpData(fRate);
            }
        }
    }
}