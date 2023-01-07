using System.Diagnostics;
using System.Management;

namespace BankAccount.Common
{
    public static class PseudoIdGenerator
    {
        private static byte[] _deconflict = null;
        private static int _counter;
        private static byte[] GetDeconflictBytes()
        {
            var random = new Random();
            _counter = random.Next();

            var bytes = new byte[5];
            random.NextBytes(bytes.AsSpan()[0..3]);

            var pid = BitConverter.GetBytes(Environment.ProcessId);
            pid.AsSpan()[0..2].CopyTo(bytes.AsSpan()[3..]);
            return bytes;
        }
        public static string NewId()
        {
           byte[] idBytes = new byte[12];
           var epoch = GetBytes((int)DateTimeOffset.UtcNow.ToUnixTimeSeconds());
           epoch.CopyTo(idBytes, 0);
           _deconflict = GetDeconflictBytes();
           _deconflict.CopyTo(idBytes, 4);
           var ctr = GetBytes(_counter++);
           ctr.AsSpan()[1..4].CopyTo(idBytes.AsSpan()[9..]);
           return BitConverter.ToString(idBytes).ToLower().Replace("-", "");
        }
        private static byte[] GetBytes(int value)
        {
            if (BitConverter.IsLittleEndian)
                value = System.Buffers.Binary.BinaryPrimitives.ReverseEndianness(value);
            return BitConverter.GetBytes(value);
        }
        
    }
}