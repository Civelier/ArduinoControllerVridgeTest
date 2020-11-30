using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace ControllerInterface.Kinect
{
    public class KinectNewSkeletonFrameReadyEventArgs
    {
        public SkeletonPoint RightHand
        {
            get;
        }
        public SkeletonPoint LeftHand
        {
            get;
        }
        public SkeletonPoint Head
        {
            get;
        }

        public KinectNewSkeletonFrameReadyEventArgs(SkeletonPoint rightHand, SkeletonPoint leftHand, SkeletonPoint head)
        {
            RightHand = rightHand;
            LeftHand = leftHand;
            Head = head;
        }
    }

    public delegate void KinectNewSkeletonFrameReadyEventHandler(KinectDevice sender, KinectNewSkeletonFrameReadyEventArgs args);

    public class KinectDevice
    {
        KinectSensor _kinect;
        private bool _isInitialized;

        public bool IsConnected => _kinect?.Status == KinectStatus.Connected;

        public bool IsInitialized => _isInitialized && IsConnected;


        public SkeletonPoint RightHand
        {
            get; private set;
        }
        public SkeletonPoint LeftHand
        {
            get; private set;
        }
        public SkeletonPoint Head
        {
            get; private set;
        }

        public event KinectNewSkeletonFrameReadyEventHandler NewSkeletonFrameReady;

        public KinectDevice()
        {
            if (TryConnect()) Initialize();
        }

        public bool TryConnect()
        {
            if (_kinect != null && _kinect.Status == KinectStatus.Connected) return true;
            foreach (var kinect in KinectSensor.KinectSensors)
            {
                if (kinect.Status == KinectStatus.Connected)
                {
                    _kinect = kinect;
                    return true;
                }
            }
            _isInitialized = false;
            _kinect = null;
            return false;
        }

        public void Initialize()
        {
            if (IsConnected)
            {
                _isInitialized = true;
                _kinect.SkeletonStream.Enable();
                _kinect.SkeletonFrameReady += _kinect_SkeletonFrameReady;
                _kinect.Start();
            }
        }

        private void _kinect_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (var skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame == null) return;
                Skeleton[] skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                skeletonFrame.CopySkeletonDataTo(skeletons);

                Skeleton skeleton = null;
                foreach (var s in skeletons)
                {
                    if (s.Position != new SkeletonPoint()) skeleton = s;
                }

                var right = RightHand = skeleton?.Joints[JointType.HandRight].Position ?? new SkeletonPoint();
                var left = LeftHand = skeleton?.Joints[JointType.HandLeft].Position ?? new SkeletonPoint();
                var head = Head = skeleton?.Joints[JointType.Head].Position ?? new SkeletonPoint();
                NewSkeletonFrameReady?.Invoke(this, new KinectNewSkeletonFrameReadyEventArgs(right, left, head));
            }
        }

        public void Stop()
        {
            _isInitialized = false;
            _kinect.Stop();
        }
    }
}
