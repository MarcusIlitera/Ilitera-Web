using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.IO;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;




/// <summary>
/// Summary description for saml
/// </summary>

//namespace OneLogin 
//{

namespace Ilitera.Data
{

    public class AccountSettings
    {
        //public string certificate = "-----BEGIN CERTIFICATE-----\nMIIBrTCCAaGgAwIBAgIBATADBgEAMGcxCzAJBgNVBAYTAlVTMRMwEQYDVQQIDApD\nYWxpZm9ybmlhMRUwEwYDVQQHDAxTYW50YSBNb25pY2ExETAPBgNVBAoMCE9uZUxv\nZ2luMRkwFwYDVQQDDBBhcHAub25lbG9naW4uY29tMB4XDTEwMDMwOTA5NTgzNFoX\nDTE1MDMwOTA5NTgzNFowZzELMAkGA1UEBhMCVVMxEzARBgNVBAgMCkNhbGlmb3Ju\naWExFTATBgNVBAcMDFNhbnRhIE1vbmljYTERMA8GA1UECgwIT25lTG9naW4xGTAX\nBgNVBAMMEGFwcC5vbmVsb2dpbi5jb20wgZ8wDQYJKoZIhvcNAQEBBQADgY0AMIGJ\nAoGBANtmwriqGBbZy5Dwy2CmJEtHEENVPoATCZP3UDESRDQmXy9Q0Kq1lBt+KyV4\nkJNHYAAQ9egLGWQ8/1atkPBye5s9fxROtf8VO3uk/x/X5VSRODIrhFISGmKUnVXa\nUhLFIXkGSCAIVfoR5S2ggdfpINKUWGsWS/lEzLNYMBkURXuVAgMBAAEwAwYBAAMB\nAA==\n-----END CERTIFICATE-----";

        //public string certificate = "-----BEGIN CERTIFICATE-----MIIC8DCCAdigAwIBAgIQOHCDLZDt4b5GyB/8aKQsXjANBgkqhkiG9w0BAQsFADA0MTIwMAYDVQQDEylNaWNyb3NvZnQgQXp1cmUgRmVkZXJhdGVkIFNTTyBDZXJ0aWZpY2F0ZTAeFw0yMTA1MTQxMzQxNDVaFw0yNDA1MTQxMzQxNDZaMDQxMjAwBgNVBAMTKU1pY3Jvc29mdCBBenVyZSBGZWRlcmF0ZWQgU1NPIENlcnRpZmljYXRlMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAs9qXWdar/0CJ8x1QumDDHc2GvqrkhwahQKO7D0yCln1XSleX2RC2M496HUjPBllYhIE9b+7iV22qUTv1ULw18hdspMt+sXZTgBjlH//zT5XYXMxAYDz93cJ3WU+A9Q99ROr55yoBXL3T7Jq5vbB6tOhgdKajID29WnMfmlqaGmz9QvoC3sYVVNKlEWOlhvxYRFCKk/WYEyYyYqY4Ow5ZVvxsBuE+X4yURFNX25j9heAJ7GpFSxsSMaPj/N4A8FrqtOu3ttbMmHrfao7F3ijuOCVCrkyt6qm0J2KgAaJJ/wtfnqNm05aplf5h/HLQS7nRgDe9sYZ4NiItysDI4HOQUQIDAQABMA0GCSqGSIb3DQEBCwUAA4IBAQCDGYjH1SARA+18A/NI8Qi7PJa7rzuZ8halS7FIiu6eZ8+hf3Jc0q8C3d/7OuYQfX6u14qFvjGFQ+cs76AAU0FYLX9nHqJUKFFJa9M/OJiyAGZdKGzmVgKU/Gy/aSplC4yNVg1ASXcQHuXLhyBtPMu9IgOaAwOWK6XghBia0UEDld7WkMSny93NAEQE4KRXBk70MqvWZmDIDGqK8Nu7TGtElh8rNISDxDDfYRR2KljnuLA3NH+JHzd2hODL4GbKqs3jiqwPMm1l/kNyY+Rlss8HE/0jMTnIieka97grVCJAHUkekWuzC9MWfCE2f+1/LJkHQWQJ4e+EazXKYkh8W8MF-----END CERTIFICATE-----";

        public string certificate = "-----BEGIN CERTIFICATE-----MIIC8DCCAdigAwIBAgIQSvpQDyML2LVPRvNlOW1XszANBgkqhkiG9w0BAQsFADA0MTIwMAYDVQQDEylNaWNyb3NvZnQgQXp1cmUgRmVkZXJhdGVkIFNTTyBDZXJ0aWZpY2F0ZTAeFw0yNDA1MDcwNTU1MzFaFw0yNzA1MDcwNTU1MjhaMDQxMjAwBgNVBAMTKU1pY3Jvc29mdCBBenVyZSBGZWRlcmF0ZWQgU1NPIENlcnRpZmljYXRlMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAlA+M3V7ego6DtjKCDA5Kechuy4ZzCeVNF+1FTjX8a+8duZgdJ/Xn2DqypD4Y/m8ZOHkXBuEc31vV/ODCutzxjIaYHOu1n+V8RAkfgIa6YqdwXaez4tr9H9Ez0dZfihsq26FdEXnRhnTGn9xRnENm8hV46Q8O/l4C5xziCa+3lONppzO5B/RIrpxc2Rv/1nILzhH3nilVhyIAYwoN/hwRXmyXwMVIYIr3I2AXyHltV77VdPGvIJvlueIefyQ5twBt/ZeirNXFejUBtoMRI2uNxp1GIwCJTM+sYRGoG2vjFuMIbfM8yQz3cDg3ydVx6feyKlcI80mOaAeVxAokSkkD4QIDAQABMA0GCSqGSIb3DQEBCwUAA4IBAQBQgEqTWIW74fXCeoW+pysEyZWGJPC70rkB2YRUdEVvAECRUuS1OUv2mpOE4pvKe2AQR7Ko+7ax8I9RATugAZWAr21uldGVCWzwfgZOY7qRbOu6dFoHqKR+FSxrDcs3KHh4K9CT34dmf5i142OXkV5bJNYhw91BSj8GlR6246k2iU9ar0UAM9XhNQm20xyHUUYHDQ6/2eHqDf709ZeX1HkZtitzYsdJ4GeAlpantLFb9+R/T5Fkz92y/Zm791A7mII3We36xHhg4rMtxNcuyG1vlaFt6gmmEAFBndUiuq2/Esa4aoRs1SvLsAZsiJluxNk46YhmZoHGTRby8Z+wgbZ9-----END CERTIFICATE-----";

        public string idp_sso_target_url = "https://login.microsoftonline.com/9b9edfb8-4408-46c9-a75d-84dd9e39ae2f/saml2";// "https://app.onelogin.com/saml/signon/20219";

        public AccountSettings()
        {

        }

    }



    //namespace Saml
    //{

    public class Certificate
    {
        public X509Certificate2 cert;

        public void LoadCertificate(string certificate)
        {
            cert = new X509Certificate2();
            cert.Import(StringToByteArray(certificate));
        }

        public void LoadCertificate(byte[] certificate)
        {
            cert = new X509Certificate2();
            cert.Import(certificate);
        }

        private byte[] StringToByteArray(string st)
        {
            byte[] bytes = new byte[st.Length];
            for (int i = 0; i < st.Length; i++)
            {
                bytes[i] = (byte)st[i];
            }
            return bytes;
        }
    }

    public class Response
    {
        private XmlDocument xmlDoc;
        private AccountSettings accountSettings;
        private Certificate certificate;

        public Response(AccountSettings accountSettings)
        {
            this.accountSettings = accountSettings;
            certificate = new Certificate();
            certificate.LoadCertificate(accountSettings.certificate);
        }

        public void LoadXml(string xml)
        {
            xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.XmlResolver = null;
            xmlDoc.LoadXml(xml);

            xmlDoc.Save("I:\\temp\\SAML_" + System.DateTime.Now.Second.ToString().Trim() + System.DateTime.Now.Minute.ToString().Trim() + System.DateTime.Now.Millisecond.ToString().Trim() + System.DateTime.Now.Day.ToString().Trim() + System.DateTime.Now.Month.ToString().Trim() + ".xml");
        }

        public void LoadXmlFromBase64(string response)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            LoadXml(enc.GetString(Convert.FromBase64String(response)));
        }

        public bool IsValid()
        {
            bool status = true;



            XmlNamespaceManager manager = new XmlNamespaceManager(xmlDoc.NameTable);
            manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
            manager.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
            manager.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");
            XmlNodeList nodeList = xmlDoc.SelectNodes("//ds:Signature", manager);

            SignedXml signedXml = new SignedXml(xmlDoc);
            signedXml.LoadXml((XmlElement)nodeList[0]);

            status &= signedXml.CheckSignature(certificate.cert, true);

            var notBefore = NotBefore();
            status &= !notBefore.HasValue || (notBefore <= DateTime.Now);

            var notOnOrAfter = NotOnOrAfter();
            status &= !notOnOrAfter.HasValue || (notOnOrAfter > DateTime.Now);

            return status;
        }

        public DateTime? NotBefore()
        {
            XmlNamespaceManager manager = new XmlNamespaceManager(xmlDoc.NameTable);
            manager.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
            manager.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");

            var nodes = xmlDoc.SelectNodes("/samlp:Response/saml:Assertion/saml:Conditions", manager);
            string value = null;
            if (nodes != null && nodes.Count > 0 && nodes[0] != null && nodes[0].Attributes != null && nodes[0].Attributes["NotBefore"] != null)
            {
                value = nodes[0].Attributes["NotBefore"].Value;
            }
            return value != null ? DateTime.Parse(value) : (DateTime?)null;
        }

        public DateTime? NotOnOrAfter()
        {
            XmlNamespaceManager manager = new XmlNamespaceManager(xmlDoc.NameTable);
            manager.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
            manager.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");

            var nodes = xmlDoc.SelectNodes("/samlp:Response/saml:Assertion/saml:Conditions", manager);
            string value = null;
            if (nodes != null && nodes.Count > 0 && nodes[0] != null && nodes[0].Attributes != null && nodes[0].Attributes["NotOnOrAfter"] != null)
            {
                value = nodes[0].Attributes["NotOnOrAfter"].Value;
            }
            return value != null ? DateTime.Parse(value) : (DateTime?)null;
        }

        public string GetNameID()
        {
            XmlNamespaceManager manager = new XmlNamespaceManager(xmlDoc.NameTable);
            manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
            manager.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
            manager.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");

            XmlNode node = xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:Subject/saml:NameID", manager);
            return node.InnerText;
        }
    }

    public class AuthRequest
    {
        public string id;
        private string issue_instant;
        //private AppSettings appSettings;
        private string appSettings_Issuer;
        private string appSettings_assertionConsumerServiceUrl;
        private AccountSettings accountSettings;

        public enum AuthRequestFormat
        {
            Base64 = 1
        }

        //public AuthRequest(AppSettings appSettings, AccountSettings accountSettings)
        public AuthRequest(string appSettings_Issuer, string appSettings_assertionConsumerServiceUrl, AccountSettings accountSettings)
        {
            this.appSettings_Issuer = appSettings_Issuer;
            this.accountSettings = accountSettings;
            this.appSettings_assertionConsumerServiceUrl = appSettings_assertionConsumerServiceUrl;

            id = "_" + System.Guid.NewGuid().ToString();
            issue_instant = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
        }

        public string GetRequest(AuthRequestFormat format)
        {
            using (StringWriter sw = new StringWriter())
            {
                XmlWriterSettings xws = new XmlWriterSettings();
                xws.OmitXmlDeclaration = true;

                using (XmlWriter xw = XmlWriter.Create(sw, xws))
                {
                    xw.WriteStartElement("samlp", "AuthnRequest", "urn:oasis:names:tc:SAML:2.0:protocol");
                    xw.WriteAttributeString("ID", id);
                    xw.WriteAttributeString("Version", "2.0");
                    xw.WriteAttributeString("IssueInstant", issue_instant);
                    xw.WriteAttributeString("ProtocolBinding", "urn:oasis:names:tc:SAML:2.0:bindings:HTTP-POST");
                    xw.WriteAttributeString("AssertionConsumerServiceURL", appSettings_assertionConsumerServiceUrl);

                    xw.WriteStartElement("saml", "Issuer", "urn:oasis:names:tc:SAML:2.0:assertion");
                    xw.WriteString(appSettings_Issuer);
                    xw.WriteEndElement();

                    xw.WriteStartElement("samlp", "NameIDPolicy", "urn:oasis:names:tc:SAML:2.0:protocol");
                    xw.WriteAttributeString("Format", "urn:oasis:names:tc:SAML:2.0:nameid-format:unspecified");
                    xw.WriteAttributeString("AllowCreate", "true");
                    xw.WriteEndElement();

                    xw.WriteStartElement("samlp", "RequestedAuthnContext", "urn:oasis:names:tc:SAML:2.0:protocol");
                    xw.WriteAttributeString("Comparison", "exact");

                    xw.WriteStartElement("saml", "AuthnContextClassRef", "urn:oasis:names:tc:SAML:2.0:assertion");
                    xw.WriteString("urn:oasis:names:tc:SAML:2.0:ac:classes:PasswordProtectedTransport");
                    xw.WriteEndElement();

                    xw.WriteEndElement(); // RequestedAuthnContext

                    xw.WriteEndElement();
                }

                string zId = "";

                zId = "id" + System.Guid.NewGuid().ToString().ToLower();

                if (zId.Length > 34)
                    zId = zId.Substring(0, 34);


                //"ID='id6c1c178c166d486687be4aaf5e482730' " +

                string xTeste = "<samlp:AuthnRequest xmlns='urn:oasis:names:tc:SAML:2.0:assertion' " +
                                "ID='"+ zId + "' " +
                                "Version='2.0' IssueInstant = '" + issue_instant + "' " +
                                "xmlns:samlp='urn:oasis:names:tc:SAML:2.0:protocol'> " +
                                "<Issuer xmlns='urn:oasis:names:tc:SAML:2.0:assertion'>https://www.ilitera.net.br/johnson/consume.aspx</Issuer> " +
                                "</samlp:AuthnRequest> ";

                xTeste = xTeste.Replace("'", "\"");

                xTeste = xTeste.Replace("\r\n", "");

                if (format == AuthRequestFormat.Base64)
                {
                    //byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(sw.ToString());

                    //byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.UTF8.GetBytes(xTeste);
                    //return System.Convert.ToBase64String(toEncodeAsBytes);


                    MemoryStream memoryStream = new MemoryStream();
                    StreamWriter writer = new StreamWriter(new System.IO.Compression.DeflateStream(memoryStream, System.IO.Compression.CompressionMode.Compress, true), new System.Text.UTF8Encoding(false));
                    writer.Write(xTeste);
                    writer.Close();
                    string result = Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length, Base64FormattingOptions.None);
                    return result;
                }

                return null;
            }
        }
    }
    //}
}
