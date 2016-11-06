using System;
using Gtk;
using ModbusTCP;
using System.Net;
using System.Net.Sockets;

public partial class MainWindow: Gtk.Window
{	
	string Text;
	private byte[]				data;
	Master MBmaster;

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void ButtonClick (object sender, EventArgs e)
	{
		if(MBmaster == null)
			MBmaster = new Master ();
		MBmaster.OnResponseData += new ModbusTCP.Master.ResponseData(MBmaster_OnResponseData);
		MBmaster.OnException	+= new ModbusTCP.Master.ExceptionData(MBmaster_OnException);
		MBmaster.connect ("192.168.100.3", 502);
		byte[] values = new byte[2];
		MBmaster.ReadHoldingRegister (3, 1, 900 , 1, ref values );
		ushort i = (ushort)BitConverter.ToInt16 (values, 0); 
		short a0 = (short)(values [0]);
		short a1 = (short)(values [1]);
		ShowAs(i, null);
	}
	// ------------------------------------------------------------------------
	// Event for response data
	// ------------------------------------------------------------------------
	private void MBmaster_OnResponseData(ushort ID, byte unit, byte function, byte[] values)
	{
		// ------------------------------------------------------------------
		// Seperate calling threads
/*		if (this.InvokeRequired)
		{
			this.BeginInvoke(new Master.ResponseData(MBmaster_OnResponseData), new object[] { ID, unit, function, values });
			return;
		}*/

		// ------------------------------------------------------------------------
		// Identify requested data
		switch(ID)
		{
			case 1:
			Text = "Read coils";
			data = values;
			ShowAs(null, null);
			break;
			case 2:
			Text = "Read discrete inputs";
			data = values;
			ShowAs(null, null);
			break;
		case 3:
			Text = "Read holding register";
			data = values;
			ushort i = (ushort)BitConverter.ToInt16 (values, 0); 
			short a0 = (short)(values [0]);
			short a1 = (short)(values [1]);
			ShowAs(i, null);
			break;
			case 4:
			Text = "Read input register";
			data = values;
			ShowAs(null, null);
			break;
			case 5:
			Text = "Write single coil";
			break;
			case 6:
			Text = "Write multiple coils";
			break;
			case 7:
			Text = "Write single register";
			break;
			case 8:
			Text = "Write multiple register";
			break;
		}	
	}

	// ------------------------------------------------------------------------
	// Modbus TCP slave exception
	// ------------------------------------------------------------------------
	private void MBmaster_OnException(ushort id, byte unit, byte function, byte exception)
	{
		string exc = "Modbus says error: ";
		switch(exception)
		{
			case Master.excIllegalFunction: exc += "Illegal function!"; break;
			case Master.excIllegalDataAdr: exc += "Illegal data adress!"; break;
			case Master.excIllegalDataVal: exc += "Illegal data value!"; break;
			case Master.excSlaveDeviceFailure: exc += "Slave device failure!"; break;
			case Master.excAck: exc += "Acknoledge!"; break;
			case Master.excGatePathUnavailable: exc += "Gateway path unavailbale!"; break;
			case Master.excExceptionTimeout: exc += "Slave timed out!"; break;
			case Master.excExceptionConnectionLost: exc += "Connection is lost!"; break;
			case Master.excExceptionNotConnected: exc += "Not connected!"; break;
		}

		//MessageBox.Show(exc, "Modbus slave exception");
	}
	private void ShowAs(object sender, System.EventArgs e)
	{
	}

}
