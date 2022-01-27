using System;
using System.Runtime.InteropServices;

namespace Medical_Studio
{
    public static class NativeFunctions
    {
        public unsafe delegate void CopyImageHandler(byte* srcPtr, int srcStride, byte* destPtr, int destStride, int lines, bool downToUp);

        [DllImport("NativeFunctions.dll")]
        public extern static unsafe void CopyImageRGB32toRGB32(byte* srcPtr, int srcStride, byte* destPtr, int destStride, int lines, bool downToUp);

        [DllImport("NativeFunctions.dll")]
        public extern static unsafe void CopyImageRGB24toRGB32(byte* srcPtr, int srcStride, byte* destPtr, int destStride, int lines, bool downToUp);

        [DllImport("NativeFunctions.dll")]
        public extern static unsafe void CopyImageGray8toRGB32(byte* srcPtr, int srcStride, byte* destPtr, int destStride, int lines, bool downToUp);

        [DllImport("NativeFunctions.dll")]
        public extern static unsafe void CopyImageGray16toRGB32(byte* srcPtr, int srcStride, byte* destPtr, int destStride, int lines, bool downToUp);
    }
}
