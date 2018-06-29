/* Copyright 2008 Data Design Vietnam. All rights reserved.
 * 
 * Created 2008.01.21 Tran Dinh Thoai
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace DataDesign.MailGO.License
{
    public class Share
    {
        private const string c_license = "aoekghoakwhgodkahgoekhgaowighke1234dfhe@343tdogh&68*964546dhgoqdkgha";

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

        public static Model.ILicensePG CreatePG(Model.IMailGoPG v_mailgo)
        {
            if (Ready)
            {
                return new LicensePG(v_mailgo);
            }
            else
            {
                return null;
            }
        }
    }
}
