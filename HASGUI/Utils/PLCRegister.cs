using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HASGUI.Utils
{
  //Klasa reprezentująca pojedynczy rejestr sterownika PLC
  public class PLCRegister
  {
    protected Int32 _value;
    protected Int32 _regnumber;
    protected PLCRegisterType _type;
    protected bool _autoRefresh;

    public PLCRegister(PLCRegisterType type, int number)
    {
      _type = type;
      _regnumber = number;
    }

    public Int32 Value
    {
      get
      {
        return _value;
      }
      set
      {
        _value = value;
      }
    }

    public PLCRegisterType Type
    {
      get
      {
        return _type;
      }
    }

    public bool AutoRefresh
    {
      get
      {
        return _autoRefresh;
      }
      set
      {
        _autoRefresh = value;
      }
    }
  }
}
