using System;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;

namespace KMMobile
{
	class Program
	{
		static void Main (string[] args)
		{
			NSApplication.Init ();
			NSApplication.SharedApplication.Delegate = new App ();
			NSApplication.Main (args);
		}
	}
}

