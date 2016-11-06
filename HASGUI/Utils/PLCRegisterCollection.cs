using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HASGUI.Utils
{
  public enum PLCRegisterType{R, D};
  public class PLCRegisterCollection
  {
    protected PLCRegisterType _type;
    protected Dictionary<int, PLCRegister> _registers;
    protected ModbusDriver _modbus;

    /// <summary>
    /// Tablica rejestrów
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public Int32 this[Int32 number]
    {
      get
      {
        if (_registers.ContainsKey(number))
          return _registers[number].Value;
         return 0;
      }
      set
      {
        if (!_registers.ContainsKey(number))
        {
          _registers.Add(number, new PLCRegister(_type, number));
        }
        _registers[number].Value = value;
      }
    }

    public PLCRegisterCollection(PLCRegisterType type, ModbusDriver driver)
    {
      _registers = new Dictionary<int, PLCRegister>();
      _type = type;

    }

    public PLCRegister Register(int number)
    {
        if (!_registers.ContainsKey(number))
          _registers.Add(number, new PLCRegister(_type, number));
        return _registers[number];
    }
  }
}
