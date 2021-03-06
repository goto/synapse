//
// AvatarManager.cs
// 
// Copyright (C) 2008-2009 Eric Butler
//
// Authors:
//   Eric Butler <eric@extremeboredom.net>
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Drawing;
using System.Reflection;
using Synapse.Core;
using Synapse.ServiceStack;
using jabber;
using jabber.protocol;
using jabber.protocol.client;
using jabber.protocol.iq;

namespace Synapse.Xmpp
{
	public delegate void AvatarEventHandler (string jid, string avatarHash);
	
	public class AvatarManager
	{
		static string s_AvatarPath;
		
		// Bare Jid => Hash
		static Dictionary<string, string> s_HashCache = new Dictionary<string, string>();

		// Hash => Native Image Object
		static Dictionary<string, object> s_AvatarCache = new Dictionary<string, object>();

		static object s_DefaultAvatarImage;
		
		Account m_Account;

		public event AvatarEventHandler AvatarUpdated;
		
		static AvatarManager ()
		{
			s_AvatarPath = Path.Combine(Paths.ApplicationData, "avatars");
			if (!Directory.Exists(s_AvatarPath))
				Directory.CreateDirectory(s_AvatarPath);

			s_DefaultAvatarImage = Application.CreateImage("resource:/default-avatar.png");
			if (s_DefaultAvatarImage == null)
				throw new Exception("Unable to load default avatar!");

			s_AvatarCache.Add("octy", Application.CreateImage("resource:/octy-32.png"));
		}
		
		public AvatarManager(Account account)
		{
			account.Client.OnPresence += HandleOnPresence;
			account.Client.OnBeforePresenceOut += HandleOnBeforePresenceOut;
			account.MyVCardUpdated += HandleMyVCardUpdated;
			m_Account = account;	
		}

		void HandleOnBeforePresenceOut(object sender, Presence pres)
		{
			Element vcardUpdateElem = new Element("x", "vcard-temp:x:update", m_Account.Client.Document);
			Element photoElem = new Element("photo", m_Account.Client.Document);
			vcardUpdateElem.AppendChild(photoElem);
			
			if (GetAvatarHash(m_Account.Jid) != null) {
				photoElem.InnerText = GetAvatarHash(m_Account.Jid);
			}
				
			pres.AppendChild(vcardUpdateElem);
		}

		public static bool HasAvatarHash (JID jid)
		{
			if (jid != null) {
				lock (s_HashCache) {
					return s_HashCache.ContainsKey(jid.Bare);
				}
			} else {
				return false;
			}
		}
		
		public static string GetAvatarHash (JID jid)
		{
			if (jid != null) {
				lock (s_HashCache) {
					string bare = jid.Bare;
					if (s_HashCache.ContainsKey(bare)) {
						return s_HashCache[bare];
					}
				}
			}
			return "default";
		}

		public static object GetAvatar (JID jid)
		{
			string hash = GetAvatarHash(jid);
			return GetAvatar(hash);
		}

		public static object GetAvatar (string hash)
		{
			lock (s_AvatarCache) {
				if (hash != null) {
					if (s_AvatarCache.ContainsKey(hash)) {
						return s_AvatarCache[hash];
					} else {
						if (AvatarExists(hash)) {
							object obj = Application.CreateImage(AvatarFileName(hash));
							s_AvatarCache[hash] = obj;
							return obj;
						}
					}
				}
			}
			return s_DefaultAvatarImage;
		}
		
		public static string GetAvatarFileName (JID jid)
		{
			return AvatarFileName(GetAvatarHash(jid));
		}
		
		void HandleOnPresence(object sender, Presence pres)
		{			
			// Make sure this isn't related to a MUC at all. 
			// Is there really no better way?
			foreach (XmlNode element in pres.ChildNodes) {
				if (element.NamespaceURI.StartsWith("http://jabber.org/protocol/muc")) {
					return;
				}
			}
			
			var nsmgr = new XmlNamespaceManager(m_Account.Client.Document.NameTable);
			nsmgr.AddNamespace("v", "vcard-temp:x:update");
			var x = pres.SelectSingleNode("v:x", nsmgr);
			if (x != null) {
				var photo = x["photo"];
				if (photo != null) {
					string bareJid = pres.From.Bare;
					string hash = photo.InnerText;						
					s_HashCache[bareJid] = hash;						
					if (!AvatarExists(hash))
						UpdateAvatar(bareJid, hash);
				}
			}
		}

		static bool AvatarExists(string hash)
		{
			if (String.IsNullOrEmpty(hash))
				return false;
			else
				return File.Exists(AvatarFileName(hash));
		}

		static string AvatarFileName(string hash)
		{
			return Path.Combine(s_AvatarPath, String.Format("{0}.png", hash));
		}
		
		void UpdateAvatar(string jid, string expectedHash)
		{
			m_Account.RequestVCard(jid, HandleReceivedAvatar, new [] { jid, expectedHash, });
		}

		void HandleMyVCardUpdated(object sender, EventArgs e)
		{
			Account account = (Account)sender;
			string hash = null;
			if (account.VCard != null && account.VCard.Photo != null && account.VCard.Photo.BinVal != null) {
				byte[] imageData = account.VCard.Photo.BinVal;
				hash = Util.SHA1(imageData);
				s_HashCache[account.Jid.Bare] = hash;
				if (!AvatarExists(hash))
					UpdateAvatar(account.Jid.Bare, hash);
			} else {
				if (s_HashCache.ContainsKey(account.Jid.Bare))
					s_HashCache.Remove(account.Jid.Bare);
			}

			account.SetProperty("AvatarHash", hash);
			
			// Re-send presence
			if (m_Account.Status != null)
				m_Account.Status = m_Account.Status;
			
			if (AvatarUpdated != null) {
				AvatarUpdated(account.Jid.Bare, hash);
			}
		}

		void HandleReceivedAvatar (object o, IQ i, object data)
		{
			string jid          = ((string[])data)[0];
			string expectedHash = ((string[])data)[1];
			string hash         = null;

			if (i != null && i["vCard"] != null && i["vCard"] is VCard) {
				VCard vcard = (VCard)i["vCard"];
				if (vcard != null && vcard.Photo != null) {
					if (vcard.Photo.Image != null) {
						byte[] imageData = vcard.Photo.BinVal;
						hash = Util.SHA1(imageData);
						if (expectedHash == null || hash == expectedHash) {
							vcard.Photo.Image.Save(AvatarFileName(hash), System.Drawing.Imaging.ImageFormat.Png);
						}
					} else {
						Console.WriteLine(String.Format("Error with avatar for: {0}. Expected hash: {1}", jid, expectedHash));
					}
				}
			}
			
			if (AvatarUpdated != null) {
				AvatarUpdated(jid, hash);
			}
		}
	}
}
