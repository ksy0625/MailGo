/* Copyright 2008 Data Design Vietnam. All rights reserved.
 * 
 * Created 2008.01.18 Tran Dinh Thoai
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace DataDesign.MailGO.Address
{
    public class Share
    {
        private const string c_license = "awoekghaoskdghoawieghdkaoskdghaksdkghaslkdoekqyghdkgha";

        private static string sm_license = null;

        public static string License
        {
            set
            {
                if (value != null && sm_license == null)
                {
                    sm_license = value;
                }
            }
        }

        public static bool Ready
        {
            get
            {
                return (sm_license == c_license);
            }
        }

        public static Model.IAddressPG CreatePG(Model.IMailGoPG v_mailgo)
        {
            if (Ready)
            {
                return new AddressPG(v_mailgo);
            }
            else
            {
                return null;
            }
        }
    }
}
