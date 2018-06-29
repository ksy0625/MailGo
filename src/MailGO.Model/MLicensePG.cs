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
    public class MLicensePG : ILicensePG
    {
        #region Fields

        protected IMailGoPG m_mailgo;

        #endregion

        #region Constructors

        public MLicensePG(IMailGoPG v_mailgo)
        {
            this.m_mailgo = v_mailgo;
        }

        #endregion

        #region ILicensePG Members

        IMailGoPG ILicensePG.MailGo
        {
            get { return this.m_mailgo; }
        }

        IHardware ILicensePG.ReadHardware()
        {
            return this.OnReadHardware();
        }

        string ILicensePG.GenerateLicenseID(bool v_evaluation, int v_activation_period)
        {
            return this.OnGenerateLicenseID(v_evaluation, v_activation_period);
        }

        string ILicensePG.GenerateActivationID(string v_license_id, IHardware v_hardware)
        {
            return this.OnGenerateActivationID(v_license_id, v_hardware);
        }

        void ILicensePG.CheckLicense(out bool v_installed, out bool v_evaluted, out bool v_activated, out bool v_expired, out DateTime? v_expired_date, out string v_license_id)
        {
            this.OnCheckLicense(out v_installed, out v_evaluted, out v_activated, out v_expired, out v_expired_date, out v_license_id);
        }

        void ILicensePG.InstallLicense(string v_license_id)
        {
            this.OnInstallLicense(v_license_id);
        }

        void ILicensePG.ActivateLicense(string v_activation_id)
        {
            this.OnActivateLicense(v_activation_id);
        }

        void ILicensePG.GenerateLicenseID()
        {
            this.OnGenerateLicenseID();
        }

        void ILicensePG.GenerateActivationID()
        {
            this.OnGenerateActivationID();
        }

        void ILicensePG.RequestActivationID(out string v_request, string v_license_id, IHardware v_hardware)
        {
            this.OnRequestActivationID(out v_request, v_license_id, v_hardware);
        }

        void ILicensePG.RequestActivationID(out string v_license_id, out IHardware v_hardware, string v_request)
        {
            this.OnRequestActivationID(out v_license_id, out v_hardware, v_request);
        }

        void ILicensePG.ActivateLicense()
        {
            this.OnActivateLicense();
        }

        void ILicensePG.InstallLicense()
        {
            this.OnInstallLicense();
        }

        bool ILicensePG.CheckCDO()
        {
            return this.OnCheckCDO();
        }

        #endregion

        #region Overridable

        protected virtual IHardware OnReadHardware()
        {
            return null;
        }

        protected virtual string OnGenerateLicenseID(bool v_evaluation, int v_activation_period)
        {
            return null;
        }

        protected virtual string OnGenerateActivationID(string v_license_id, IHardware v_hardware)
        {
            return null;
        }

        protected virtual void OnCheckLicense(out bool v_installed, out bool v_evaluted, out bool v_activated, out bool v_expired, out DateTime? v_expired_date, out string v_license_id)
        {
            v_installed = false;
            v_evaluted = false;
            v_activated = false;
            v_expired = false;
            v_expired_date = null;
            v_license_id = null;
        }

        protected virtual void OnInstallLicense(string v_license_id) { }

        protected virtual void OnActivateLicense(string v_activation_id) { }

        protected virtual void OnGenerateLicenseID() { }

        protected virtual void OnGenerateActivationID() { }

        protected virtual void OnRequestActivationID(out string v_request, string v_license_id, IHardware v_hardware) 
        {
            v_request = null;
        }

        protected virtual void OnRequestActivationID(out string v_license_id, out IHardware v_hardware, string v_request) 
        {
            v_license_id = null;
            v_hardware = null;
        }

        protected virtual void OnActivateLicense() { }

        protected virtual void OnInstallLicense() { }

        protected virtual bool OnCheckCDO() { return false; }

        #endregion

    }
}
