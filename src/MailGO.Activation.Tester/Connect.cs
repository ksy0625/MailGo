/* Copyright 2008 Data Design Vietnam. All rights reserved.
 * 
 * Created 2008.01.23 Tran Dinh Thoai
 * 
 */

using OL = Microsoft.Office.Interop.Outlook;

namespace DataDesign.MailGO.Activation.Tester
{
	using System;
	using Extensibility;
	using System.Runtime.InteropServices;

	#region Read me for Add-in installation and setup information.
	// When run, the Add-in wizard prepared the registry for the Add-in.
	// At a later time, if the Add-in becomes unavailable for reasons such as:
	//   1) You moved this project to a computer other than which is was originally created on.
	//   2) You chose 'Yes' when presented with a message asking if you wish to remove the Add-in.
	//   3) Registry corruption.
	// you will need to re-register the Add-in by building the MailGO.Activation.TesterSetup project, 
	// right click the project in the Solution Explorer, then choose install.
	#endregion
	
	/// <summary>
	///   The object for implementing an Add-in.
	/// </summary>
	/// <seealso class='IDTExtensibility2' />
	[GuidAttribute("A4FFD00B-F49F-463A-BD89-FD5B939C785C"), ProgId("DataDesign.MailGO.Activation.Tester.Connect")]
	public class Connect : Object, Extensibility.IDTExtensibility2
	{
        private Model.IMailGoPG m_mailgo;

		/// <summary>
		///		Implements the constructor for the Add-in object.
		///		Place your initialization code within this method.
		/// </summary>
		public Connect()
		{
		}

		/// <summary>
		///      Implements the OnConnection method of the IDTExtensibility2 interface.
		///      Receives notification that the Add-in is being loaded.
		/// </summary>
		/// <param term='application'>
		///      Root object of the host application.
		/// </param>
		/// <param term='connectMode'>
		///      Describes how the Add-in is being loaded.
		/// </param>
		/// <param term='addInInst'>
		///      Object representing this Add-in.
		/// </param>
		/// <seealso class='IDTExtensibility2' />
		public void OnConnection(object application, Extensibility.ext_ConnectMode connectMode, object addInInst, ref System.Array custom)
		{
			applicationObject = application;
			addInInstance = addInInst;
		}

		/// <summary>
		///     Implements the OnDisconnection method of the IDTExtensibility2 interface.
		///     Receives notification that the Add-in is being unloaded.
		/// </summary>
		/// <param term='disconnectMode'>
		///      Describes how the Add-in is being unloaded.
		/// </param>
		/// <param term='custom'>
		///      Array of parameters that are host application specific.
		/// </param>
		/// <seealso class='IDTExtensibility2' />
		public void OnDisconnection(Extensibility.ext_DisconnectMode disconnectMode, ref System.Array custom)
		{
		}

		/// <summary>
		///      Implements the OnAddInsUpdate method of the IDTExtensibility2 interface.
		///      Receives notification that the collection of Add-ins has changed.
		/// </summary>
		/// <param term='custom'>
		///      Array of parameters that are host application specific.
		/// </param>
		/// <seealso class='IDTExtensibility2' />
		public void OnAddInsUpdate(ref System.Array custom)
		{
		}

		/// <summary>
		///      Implements the OnStartupComplete method of the IDTExtensibility2 interface.
		///      Receives notification that the host application has completed loading.
		/// </summary>
		/// <param term='custom'>
		///      Array of parameters that are host application specific.
		/// </param>
		/// <seealso class='IDTExtensibility2' />
		public void OnStartupComplete(ref System.Array custom)
		{
            this.m_mailgo = new MailGoPG((OL.Application)applicationObject);
            this.m_mailgo.Startup();
		}

		/// <summary>
		///      Implements the OnBeginShutdown method of the IDTExtensibility2 interface.
		///      Receives notification that the host application is being unloaded.
		/// </summary>
		/// <param term='custom'>
		///      Array of parameters that are host application specific.
		/// </param>
		/// <seealso class='IDTExtensibility2' />
		public void OnBeginShutdown(ref System.Array custom)
		{
            this.m_mailgo.Shutdown();
		}
		
		private object applicationObject;
		private object addInInstance;
	}
}