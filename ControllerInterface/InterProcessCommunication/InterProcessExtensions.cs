using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ControllerInterface.InterProcessCommunication
{
    public static class InterProcessExtensions
    {
        public static void WaitForConnectionEx(this NamedPipeServerStream stream)
        {
            var evt = new AutoResetEvent(false);
            Exception e = null;
            stream.BeginWaitForConnection(ar =>
            {
                try
                {
                    stream.EndWaitForConnection(ar);
                }
                catch (Exception er)
                {
                    e = er;
                }
                evt.Set();
            }, null);
            evt.WaitOne();
            if (e != null)
                throw e; // rethrow exception
        }

        public static void WaitForConnectionEx(this NamedPipeServerStream stream, int timeout)
        {
            var evt = new AutoResetEvent(false);
            Exception e = null;
            stream.BeginWaitForConnection(ar => {
                try
                {
                    stream.EndWaitForConnection(ar);
                }
                catch (Exception er)
                {
                    e = er;
                }
                evt.Set();
            }, null);
            evt.WaitOne(timeout);
            if (e != null)
                throw e; // rethrow exception
        }
    }
}
