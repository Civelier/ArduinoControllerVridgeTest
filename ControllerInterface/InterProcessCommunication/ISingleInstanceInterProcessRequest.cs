using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerInterface.InterProcessCommunication
{
    public interface ISingleInstanceInterProcessRequest
    {
        string ID { get; }
        bool RequireSuccess { get; }
        bool Execute();
        bool IsInstance(ISingleInstanceInterProcessRequest other);
    }
}
