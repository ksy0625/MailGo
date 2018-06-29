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
    public class MActivationPG : IActivationPG
    {
        #region Fields

        protected IMailGoPG m_mailgo;
        protected IOption m_option;
        protected bool m_status;

        #endregion

        #region Constructors

        public MActivationPG(IMailGoPG v_mailgo)
        {
            this.m_mailgo = v_mailgo;
            this.m_option = new MOption();
        }

        #endregion

        #region IActivationPG Members
        public IMailGoPG MailGo
        {
            get { return this.m_mailgo; }
        }

        IMailGoPG IActivationPG.MailGo
        {
            get { return this.m_mailgo; }
        }

        IOption IActivationPG.Option
        {
            get { return this.m_option; }
        }

        bool IActivationPG.Status
        {
            get { return this.m_status; }
            set { this.m_status =value; }
        }

        void IActivationPG.ChangeOption()
        {
            this.OnChangeOption();
        }

        void IActivationPG.BeforeSendEmail(IEmail v_email, out bool v_cancel)
        {
            this.OnBeforeSendEmail(v_email, out v_cancel);
        }

        void IActivationPG.SaveOption()
        {
            this.OnSaveOption();
        }

        void IActivationPG.LoadOption()
        {
            this.OnLoadOption();
        }

        void IActivationPG.CleanUp()
        {
            this.OnCleanUp();
        }

        void IActivationPG.Activate()
        {
            this.OnActivate();
        }

        #endregion

        #region Overriable

        protected virtual void OnChangeOption() { }

        protected virtual void OnBeforeSendEmail(IEmail v_email, out bool v_cancel)
        {
            v_cancel = false;
        }

        protected virtual void OnLoadOption() { }

        protected virtual void OnSaveOption() { }

        protected virtual void OnCleanUp() { }

        protected virtual void OnActivate() { }

        #endregion

    }
}
