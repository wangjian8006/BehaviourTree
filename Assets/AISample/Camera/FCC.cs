
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace RoleController
{
    public class FCC : MonoBehaviour
    {
        protected Camera m_AttachCamera = null;//相机//

        protected GameObject m_AttachElement = null;

        protected float m_fCameraDistance;

        protected float m_fCameraTargetDistance = -1.0f;

        protected float m_fCameraDistanceSmoothLag = 0.3f;

        protected float m_fCameraDistanceVelocity = 0.0f;
        
        protected float m_fCameraHeight = 0.0f;

        protected float m_fCameraTargetHeight = -1.0f;

        protected float m_fCameraHeightSmoothLag = 0.3f;

        protected float m_fCameraHeightVelocity = 0.0f;

        protected float m_fCameraPositionSmooth = 0.02f;

        protected Vector3 m_vCameraTargetPosition;

        protected Vector3 m_vAttachGameObjectBoundExtent;

        protected Vector3 m_vCameraVelocity = Vector3.zero;

        protected float m_fCameraAngularSmoothLag = 0.2f;

        protected float m_fCameraAngularMaxSpeed = 720.0f;

        protected float m_fCameraAngleVelocity = 0.0f;

        protected float m_fCameraYawTarget = float.MaxValue;

        protected float m_fCameraPitchTarget = float.MaxValue;

        protected bool m_bFollowAttachElementRotate = true;

        public virtual void Awake()
        {
            m_AttachElement = null;
            AttachCamera(Camera.main);
            m_fCameraDistance = 3.0f;
            m_vCameraTargetPosition = new Vector3(0, 0, 0);
            m_vAttachGameObjectBoundExtent = new Vector3(0.5f, 0.5f, 0.5f);
        }

        public bool FollowAttachElementRotate
        {
            get { return m_bFollowAttachElementRotate; }
            set
            {
                m_bFollowAttachElementRotate = value;
                if (m_bFollowAttachElementRotate == true)
                {
                    m_fCameraYawTarget = float.MaxValue;
                }
            }
        }

        public void AttachCamera(Camera pCamera)
        {
            m_AttachCamera = pCamera;
            if (m_AttachCamera != null)
            {
                CalCameraTargetPosition();
                m_AttachCamera.transform.localPosition = m_vCameraTargetPosition;

            }
        }

        public virtual void AttachGameObject(GameObject gameObject, bool bFollowAttachElementRotate)
        {
            m_AttachElement = gameObject;
            if (m_AttachElement != null)
            {
                m_bFollowAttachElementRotate = bFollowAttachElementRotate;
                Collider cd = m_AttachElement.GetComponent<Collider>();
                if (cd != null)
                {
                    m_vAttachGameObjectBoundExtent = cd.bounds.extents;
                }
                else
                {
                    m_vAttachGameObjectBoundExtent = new Vector3(0.5f, 0.5f, 0.5f);
                }
                CalCameraTargetPosition();
                if (m_AttachCamera != null)
                {
                    m_AttachCamera.transform.localPosition = m_vCameraTargetPosition;
                }
            }

        }

        void CalCameraTargetPosition()
        {
            if (m_AttachElement == null || m_AttachCamera == null)
                return;

            if (m_fCameraTargetDistance > 0.0f)
            {
                m_fCameraDistance = Mathf.SmoothDamp(m_fCameraDistance, m_fCameraTargetDistance, ref m_fCameraDistanceVelocity, m_fCameraDistanceSmoothLag);
                if (Mathf.Abs(m_fCameraDistance - m_fCameraTargetDistance) <= 0.01f)
                {
                    m_fCameraTargetDistance = -1.0f;
                }

            }

            if (m_fCameraTargetHeight > 0.0f)
            {
                m_fCameraHeight = Mathf.SmoothDamp(m_fCameraHeight, m_fCameraTargetHeight, ref m_fCameraHeightVelocity, m_fCameraHeightSmoothLag);
                if (Mathf.Abs(m_fCameraHeight - m_fCameraTargetHeight) <= 0.01f)
                {
                    m_fCameraTargetHeight = -1.0f;
                }

            }


            Vector3 vCameraAt = m_AttachElement.transform.position;
            vCameraAt.y += m_fCameraHeight;// (m_vAttachGameObjectBoundExtent.y);


            Vector3 vCameraForward = m_AttachCamera.transform.forward;

            float fDistance = m_fCameraDistance;
            RaycastHit hit;
            bool bHit = Physics.SphereCast(vCameraAt, 0.05f, -vCameraForward, out hit, m_fCameraDistance, 0);
            if (bHit == true)
            {
                fDistance = hit.distance;
            }

            m_vCameraTargetPosition = vCameraAt - fDistance * vCameraForward;

        }

        public void SetCameraTargetPosition(Vector3 vPosition, bool bSetCamera)
        {
            m_vCameraTargetPosition = vPosition;
            if (bSetCamera == true && m_AttachCamera != null)
            {
                m_AttachCamera.transform.localPosition = m_vCameraTargetPosition;
            }
        }

        public void SetRotate(Vector3 vEuler)
        {
            if (vEuler.x > 180.0f)
                vEuler.x = Mathf.Max(vEuler.x, 300.0f);
            else
                vEuler.x = Mathf.Min(vEuler.x, 60.0f);
            m_AttachCamera.transform.eulerAngles = vEuler;
            m_fCameraYawTarget = float.MaxValue;
            m_fCameraPitchTarget = float.MaxValue;
        }

        public void SetCameraDistance(float fDistance, bool bSmooth)
        {
            if (bSmooth == false)
            {
                m_fCameraDistance = fDistance;
                CalCameraTargetPosition();
                if (m_AttachCamera != null)
                    m_AttachCamera.transform.localPosition = m_vCameraTargetPosition;
            }
            else
            {
                m_fCameraTargetDistance = fDistance;
            }
        }

        public void SetCameraHeight(float fHeight, bool bSmooth)
        {

            if (bSmooth == false)
            {
                m_fCameraHeight = fHeight;
                CalCameraTargetPosition();
                if (m_AttachCamera != null)
                    m_AttachCamera.transform.localPosition = m_vCameraTargetPosition;
            }
            else
            {
                m_fCameraTargetHeight = fHeight;
            }

        }

        void AdjustCameraRotator()
        {

            Vector3 vEuler = m_AttachCamera.transform.eulerAngles;
            float targetAngle = m_AttachElement.transform.eulerAngles.y;
            float currentAngle = AngleDistance(vEuler.y, targetAngle);
            vEuler.y = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref m_fCameraAngleVelocity, m_fCameraAngularSmoothLag, m_fCameraAngularMaxSpeed);
            m_AttachCamera.transform.eulerAngles = vEuler;

        }

        public void SetCameraYaw(float fTargetYaw, bool bSmooth)
        {
            Vector3 vEuler = m_AttachCamera.transform.eulerAngles;
            if (bSmooth == true)
            {
                m_fCameraAngleVelocity = 0.0f;
                m_fCameraYawTarget = AngleDistance(fTargetYaw, vEuler.y);
            }
            else
            {
                vEuler.y = fTargetYaw;
                m_AttachCamera.transform.eulerAngles = vEuler;
                CalCameraTargetPosition();
                m_AttachCamera.transform.position = m_vCameraTargetPosition;
            }
        }

        public virtual void Update()
        {
            if (m_AttachCamera == null || m_AttachElement == null)
                return;
            if (m_fCameraYawTarget != float.MaxValue || m_fCameraPitchTarget != float.MaxValue)
            {
                Vector3 vEuler = m_AttachCamera.transform.eulerAngles;
                if (m_fCameraPitchTarget != float.MaxValue)
                {
                    float currentAngle = AngleDistance(vEuler.x, m_fCameraPitchTarget);
                    vEuler.x = Mathf.SmoothDampAngle(currentAngle, m_fCameraPitchTarget, ref m_fCameraAngleVelocity, m_fCameraAngularSmoothLag, m_fCameraAngularMaxSpeed);
                    m_AttachCamera.transform.eulerAngles = vEuler;
                    float fSub = Mathf.Abs(vEuler.x - m_fCameraPitchTarget);
                    if (fSub <= 1.0f || fSub >= 359.0f)
                    {
                        m_fCameraPitchTarget = float.MaxValue;
                    }
                }
                else
                {
                    float currentAngle = AngleDistance(vEuler.y, m_fCameraYawTarget);
                    vEuler.y = Mathf.SmoothDampAngle(currentAngle, m_fCameraYawTarget, ref m_fCameraAngleVelocity, m_fCameraAngularSmoothLag, m_fCameraAngularMaxSpeed);
                    m_AttachCamera.transform.eulerAngles = vEuler;
                    float fSub = Mathf.Abs(vEuler.y - m_fCameraYawTarget);
                    if (fSub <= 1.0f || fSub >= 359.0f)
                    {
                        m_fCameraYawTarget = float.MaxValue;
                    }
                }
            }

            if (m_bFollowAttachElementRotate == true)
            {
                AdjustCameraRotator();
            }
            CalCameraTargetPosition();

            //  Vector3 vCurrentPos = m_AttachCamera.transform.localPosition;
            Vector3 vNewPos = m_vCameraTargetPosition;// Vector3.SmoothDamp(vCurrentPos, m_vCameraTargetPosition, ref m_vCameraVelocity, m_fCameraPositionSmooth);//,100.0f,Time.deltaTime*2.0f);
            m_AttachCamera.transform.position = vNewPos;
        }

        public void SetProjectMatrix(float fFov, float fNear, float fFar)
        {
            if (m_AttachCamera != null)
            {
                m_AttachCamera.fieldOfView = fFov;
                m_AttachCamera.farClipPlane = fFar;
                m_AttachCamera.nearClipPlane = fNear;

            }
        }

        public float GetCameraYaw()
        {
            return m_AttachCamera.transform.eulerAngles.y;
        }

        public static float AngleDistance(float fCurrent, float fOld)
        {
            float fSub = fCurrent - fOld;
            if (fSub < -180.0f)
            {
                return fCurrent + 360.0f;
            }
            else if (fSub > 180.0f)
            {
                return fCurrent - 360.0f;
            }
            return fCurrent;
        }
    }
}