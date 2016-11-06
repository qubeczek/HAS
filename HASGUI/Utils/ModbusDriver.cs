using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HASGUI.Utils
{
  public class ModbusDriver : ModbusTCPMaster
  {
    public void ReadHoldingRegister(ushort startAddress, ushort numInputs, ref Int16[] values)
    {
      byte[] buf= null;
      ReadHoldingRegister(0, 0, startAddress, numInputs, ref buf);
      //przestawienei bitów
      int i = 0;
      while (i < buf.Length-1)
      {
        byte a = buf[i];
        buf[i] = buf[i + 1];
        buf[i + 1] = a;
        i = i + 2;
      }
      Buffer.BlockCopy(buf, 0, values, 0, buf.Length);

    }
  }
}
