using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Reporting.WebForms;

namespace UMGI.GRCS.UI.Utilities
{
   [Serializable]
  public class CustomReportCredentials : IReportServerCredentials
  {
      private string _UserName;
      private string _PassWord;
      private string _DomainName;

      public CustomReportCredentials(string UserName, string PassWord, string DomainName)
      {
          _UserName = UserName;
          _PassWord = PassWord;
          _DomainName = DomainName;
      }

      public System.Security.Principal.WindowsIdentity ImpersonationUser
      {
          get { return null; }
      }

      public System.Net.ICredentials NetworkCredentials
      {
          get { return new System.Net.NetworkCredential(_UserName, _PassWord, _DomainName); }
      }

      public bool GetFormsCredentials(out System.Net.Cookie authCookie, out string user,

      out string password, out string authority)
      {
          authCookie = null;
          user = password = authority = null;
          return false;
      }
  }
   
}


