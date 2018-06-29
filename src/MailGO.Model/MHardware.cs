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
    public class MHardware : IHardware
    {
        #region Fields

        protected List<string> m_mac = new List<string>();
        protected List<string> m_cpu = new List<string>();

        #endregion

        #region IHardware Members

        List<string> IHardware.MAC
        {
            get { return this.m_mac; }
        }

        List<string> IHardware.CPU
        {
            get { return this.m_cpu; }
        }

        #endregion
    }
}
