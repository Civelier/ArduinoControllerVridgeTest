using System;
using System.Collections.Generic;
using System.Linq;
using Ara3D;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace ControllerInterface.Kinect
{
    public class KinectNewSkeletonFrameReadyEventArgs
    {
        public Vector3 RightHand
        {
            get;
        }
        public Vector3 LeftHand
        {
            get;
        }
        public Vector3 Head
        {
            get;
        }

        public KinectNewSkeletonFrameReadyEventArgs(Vector3 rightHand, Vector3 leftHand, Vector3 head)
        {
            RightHand = rightHand;
            LeftHand = leftHand;
            Head = head;
        }
    }

    public class KinectConnectEventArgs
    {
        public KinectConnectEventArgs()
        {

        }
    }

    public delegate void KinectConnectEventHandler(KinectDevice sender, KinectConnectEventArgs args);

    public delegate void KinectNewSkeletonFrameReadyEventHandler(KinectDevice sender, KinectNewSkeletonFrameReadyEventArgs args);

    public class KinectDevice : IDisposable
    {
        KinectSensor _kinect;
        private bool _isInitialized;

        public bool IsConnected => _kinect?.Status == KinectStatus.Connected;

        public bool IsInitialized => _isInitialized && IsConnected;

        private bool _setCenterRequested;
        private bool _setForwardRequested;
        private bool _newFrame;

        private Matrix4x4 _worldMatrix = Matrix4x4.CreateRotationZ(0);

        public float Rotation
        {
            get => _rotation;
            set
            {
                _rotation = value;
                _worldMatrix = Matrix4x4.CreateRotationY((float)VRE.Vridge.API.Client.Helpers.MathHelpers.DegToRad(value));
            }
        }

        public Vector3 RightHand
        {
            get; private set;
        }
        public Vector3 LeftHand
        {
            get; private set;
        }
        public Vector3 Head
        {
            get; private set;
        }
        public Joint LeftShoulder
        {
            get; private set;
        }
        public Joint RightShoulder
        {
            get; private set;
        }

        public event KinectNewSkeletonFrameReadyEventHandler NewSkeletonFrameReady;
        public event KinectConnectEventHandler Connected;

        private Thread _kinectProcess;
        private float _rotation;

        public int Angle
        {
            get => _setAngle;
            set
            {
                _setAngle = value;
            }
        }
        private int _setAngle;
        public int MinAngle
        {
            get; private set;
        } 

        public int MaxAngle
        {
            get; private set;
        }

        public KinectDevice()
        {
            _kinectProcess = new Thread(Process);
        }

        private void Process()
        {
            while (_kinectProcess.ThreadState != ThreadState.AbortRequested)
            {
                if (_kinect?.Status != KinectStatus.Connected) _isInitialized = false;
                if (!TryConnect()) Thread.Sleep(500);
                else
                {
                    Initialize();
                    if (_setAngle != _kinect.ElevationAngle)
                    {
                        try
                        {
                            _kinect.ElevationAngle = _setAngle;
                        }
                        catch (InvalidOperationException)
                        {
                        }
                    }
                    //if (_setForwardRequested)
                    //{
                    //    _setForwardRequested = false;
                    //    int i = 0;
                    //    Vector3 rightAvg = new Vector3();
                    //    Vector3 leftAvg = new Vector3();
                    //    Thread.Sleep(3000);
                    //    while (i < 15)
                    //    {
                    //        while (!_newFrame) Thread.Sleep(1);
                    //        _newFrame = false;
                    //        if (RightShoulder.TrackingState == JointTrackingState.Tracked && LeftShoulder.TrackingState == JointTrackingState.Tracked)
                    //        {
                    //            i++;
                    //            rightAvg += new Vector3(RightShoulder.Position.X, RightShoulder.Position.Y, RightShoulder.Position.Z);
                    //            leftAvg += new Vector3(LeftShoulder.Position.X, LeftShoulder.Position.Y, LeftShoulder.Position.Z);
                    //        }
                    //        if (RightShoulder.TrackingState == JointTrackingState.NotTracked || LeftShoulder.TrackingState == JointTrackingState.NotTracked)
                    //        {
                    //            i = 0;
                    //            rightAvg = new Vector3();
                    //            leftAvg = new Vector3();
                    //        }
                    //    }
                    //    rightAvg /= 15;
                    //    leftAvg /= 15;
                    //    var line = new Line(rightAvg, leftAvg);
                    //    var r1 = rightAvg;
                    //    var l1 = leftAvg;
                    //    var r2 = rightAvg;
                    //    r1.SetY(0);
                    //    l1.SetY(0);
                    //    r2.SetY(1);

                    //    var p = Plane.CreateFromVertices(r1, r2, l1);
                    //    _worldMatrix = Matrix4x4.CreateWorld(line.MidPoint, p.Normal, Vector3.UnitY);
                    //}
                }
            }
        }

        private bool TryConnect()
        {
            if (IsConnected) return true;
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

        private void Initialize()
        {
            if (!IsInitialized)
            {
                _isInitialized = true;
                if (_kinect.IsRunning) _kinect.Stop();
                _kinect.SkeletonStream.EnableTrackingInNearRange = true;
                _kinect.SkeletonStream.TrackingMode = SkeletonTrackingMode.Default;
                _kinect.SkeletonStream.Enable(new TransformSmoothParameters()
                {
                    Correction = 0.1f,
                    JitterRadius = 0.3f,
                    MaxDeviationRadius = 0.1f,
                    Prediction = 0.5f,
                    Smoothing = 0.2f,
                });
                _kinect.SkeletonFrameReady += _kinect_SkeletonFrameReady;
                _kinect.Start();
                MinAngle = _kinect.MinElevationAngle;
                MaxAngle = _kinect.MaxElevationAngle;
                _setAngle = _kinect.ElevationAngle;
                Connected?.Invoke(this, new KinectConnectEventArgs());
            }

        }

        public bool QueueKinectSetForward()
        {
            if (_kinect == null || _kinect.Status != KinectStatus.Connected) return false;
            _setForwardRequested = true;
            return true;
        }

        public bool QueueKinectSetCenter()
        {
            if (_kinect == null || _kinect.Status != KinectStatus.Connected) return false;
            _setCenterRequested = true;
            return true;
        }

        public void StartKinectProcess()
        {
            if (!_kinectProcess.IsAlive) _kinectProcess.Start();
        }

        public void StopKinectProcess()
        {
            if (_kinectProcess.IsAlive) _kinectProcess.Abort();
        }

        private Vector3 ToVector(SkeletonPoint point)
        {
            return new Vector3(point.X, point.Y, point.Z);
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

                RightHand = ToVector(skeleton?.Joints[JointType.HandRight].Position ?? new SkeletonPoint()).Transform(_worldMatrix);
                LeftHand = ToVector(skeleton?.Joints[JointType.HandLeft].Position ?? new SkeletonPoint()).Transform(_worldMatrix);
                Head = ToVector(skeleton?.Joints[JointType.ShoulderCenter].Position ?? new SkeletonPoint()).Transform(_worldMatrix);
                RightShoulder = skeleton?.Joints[JointType.ShoulderRight] ?? new Joint();
                LeftShoulder = skeleton?.Joints[JointType.ShoulderLeft] ?? new Joint();
                _newFrame = true;
                NewSkeletonFrameReady?.Invoke(this, new KinectNewSkeletonFrameReadyEventArgs(RightHand, LeftHand, Head));
            }
        }

        private void Stop()
        {
            _isInitialized = false;
            _kinect?.Stop();
        }

        public void Dispose()
        {
            _kinect?.SkeletonStream.Disable();
            StopKinectProcess();
            if (_kinect?.IsRunning ?? false) Stop();
            if (_kinect != null) _kinect.SkeletonFrameReady -= _kinect_SkeletonFrameReady;
            _kinect?.Dispose();
        }
    }
}
