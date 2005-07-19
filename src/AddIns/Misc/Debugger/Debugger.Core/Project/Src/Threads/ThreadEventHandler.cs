// <file>
//     <owner name="David Srbeck�" email="dsrbecky@post.cz"/>
// </file>

using System;

namespace DebuggerLibrary 
{	
	public delegate void ThreadEventHandler (object sender, ThreadEventArgs e);
	
	[Serializable]
	public class ThreadEventArgs : DebuggerEventArgs
	{
		Thread thread;
		
		public Thread Thread {
			get {
				return thread;
			}
		}
		
		public ThreadEventArgs(NDebugger debugger, Thread thread): base(debugger)
		{
			this.thread = thread;
		}
	}
}
