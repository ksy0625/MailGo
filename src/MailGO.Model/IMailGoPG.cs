/* Copyright 2008 Data Design Vietnam. All rights reserved.
 * 
 * Created 2008.01.18 Tran Dinh Thoai
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace DataDesign.MailGO.Model
{
    public interface IMailGoPG
    {
        ITrack Track { get; }
        IAddressPG Address { get; }
        ILicensePG License { get; }
        IActivationPG Activation { get; }

        void Startup();
        void Shutdown();
    }
}
