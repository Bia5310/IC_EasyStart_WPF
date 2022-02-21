using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_Studio.Capture
{
    public interface ISnapFrame
    {
        void SnapImage(string filename, int timeout_ms);
    }
}
