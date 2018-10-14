﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.Net;
using System.IO;

namespace NSites_V.Global
{
    public class GmailHandler
    {
        private string username;
        private string password;
        private string gmailAtomUrl;

        public string GmailAtomUrl
        {
            get { return gmailAtomUrl; }
            set { gmailAtomUrl = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public GmailHandler(string _Username, string _Password, string _GmailAtomUrl)
        {
            Username = _Username;
            Password = _Password;
            GmailAtomUrl = _GmailAtomUrl;
        }

        public GmailHandler(string _Username, string _Password)
        {
            Username = _Username;
            Password = _Password;
            GmailAtomUrl = "https://mail.google.com/mail/feed/atom";
        }
        public XmlDocument GetGmailAtom()
        {
            byte[] buffer = new byte[8192];
            int byteCount = 0;
            XmlDocument _feedXml = null;
            try
            {
                System.Text.StringBuilder sBuilder = new System.Text.StringBuilder();
                WebRequest webRequest = WebRequest.Create(GmailAtomUrl);

                webRequest.PreAuthenticate = true;

                System.Net.NetworkCredential credentials = new NetworkCredential(this.Username, this.Password);
                webRequest.Credentials = credentials;

                WebResponse webResponse = webRequest.GetResponse();
                Stream stream = webResponse.GetResponseStream();

                while ((byteCount = stream.Read(buffer, 0, buffer.Length)) > 0)
                    sBuilder.Append(System.Text.Encoding.ASCII.GetString(buffer, 0, byteCount));


                _feedXml = new XmlDocument();
                _feedXml.LoadXml(sBuilder.ToString());


            }
            catch (Exception ex)
            {
                //add error handling
                throw ex;
            }
            return _feedXml;
        }
    }
}
