using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HASGUI.Utils
{
  public static class PLCUnit
  {
    static private PLCRegisterCollection _r;
    static private PLCRegisterCollection _d;
    static private ModbusDriver _modbus;

    static public PLCRegisterCollection R{get{ return _r;}}
    static public PLCRegisterCollection D{get{ return _d;}}

    static PLCUnit()
    {
       _modbus = new ModbusDriver();
       _r = new PLCRegisterCollection(PLCRegisterType.R, _modbus);
       _d = new PLCRegisterCollection(PLCRegisterType.D, _modbus);
    }

    public static void Start()
    {
      _modbus.connect("192.168.100.3", 502);
      short[] buf = null;
      _modbus.ReadHoldingRegister(1, 125, ref buf);
      int test = R[504];
    }
        

  }
}
