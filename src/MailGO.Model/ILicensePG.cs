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
    public interface ILicensePG
    {
        IMailGoPG MailGo { get; }

        IHardware ReadHardware();

        string GenerateLicenseID(bool v_evaluation, int v_activation_period);
        string GenerateActivationID(string v_license_id, IHardware v_hardware);
        void RequestActivationID(out string v_request, string v_license_id, IHardware v_hardware);
        void RequestActivationID(out string v_license_id, out IHardware v_hardware, string v_request);

        void CheckLicense(out bool v_installed, out bool v_evaluted, out bool v_activated, out bool v_expired, out DateTime? v_expired_date, out string v_license_id);
        void InstallLicense(string v_license_id);
        void ActivateLicense(string v_activation_id);

        void GenerateLicenseID();
        void GenerateActivationID();
        void ActivateLicense();
        void InstallLicense();

        bool CheckCDO();
    }
}
