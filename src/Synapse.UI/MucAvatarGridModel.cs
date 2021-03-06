//
// MucAvatarGridModel.cs
// 
// Copyright (C) 2008 Eric Butler
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
using Synapse.Core;
using Synapse.Xmpp;
using jabber;
using jabber.connection;
using jabber.protocol.client;

namespace Synapse.UI
{	
	public class MucAvatarGridModel : IAvatarGridModel<RoomParticipant>
	{
		Account m_Account;
		Room    m_Room;
		
		public event ItemEventHandler<RoomParticipant> ItemAdded;
		public event ItemEventHandler<RoomParticipant> ItemRemoved;
		public event ItemEventHandler<RoomParticipant> ItemChanged;
		public event EventHandler Refreshed;
		public event EventHandler ItemsChanged;
		
		public MucAvatarGridModel(Account account, Room room)
		{
			if (account == null)
				throw new ArgumentNullException("account");

			if (room == null)
				throw new ArgumentNullException("room");
			
			m_Account = account;
			m_Account.ConnectionStateChanged +=HandleConnectionStateChanged; 
			
			m_Room = room;
			m_Room.OnParticipantJoin += HandleOnParticipantJoin;
			m_Room.OnParticipantLeave += HandleOnParticipantLeave;
			m_Room.OnParticipantPresenceChange += HandleOnParticipantPresenceChange;
			m_Room.OnPresenceChange += HandleOnPresenceChange;
			m_Room.OnJoin += HandleOnJoin;
			m_Room.OnLeave += HandleOnLeave;
		}
		
		public IEnumerable<RoomParticipant> Items {
			get {
				if (m_Room.IsParticipating) {
					foreach (RoomParticipant p in m_Room.Participants) {
						yield return p;
					}
				}
			}
		}

		public int NumItemsInGroup (string groupName)
		{
			throw new NotImplementedException();
		}

		public int NumOnlineItemsInGroup (string groupName)
		{
			throw new NotImplementedException();
		}
		
		public bool ModelUpdating {
			get {
				return false;
			}
		}

		public string TextFilter {
			get {
				return null;
			}
			set {
				throw new NotImplementedException();
			}
		}
		
		public int GetGroupOrder (string groupName)
		{
			if (groupName == "Moderators")
				return 1;
			else if (groupName == "Participants")
				return 2;
			else if (groupName == "Visitor")
				return 3;
			else
				return 4;
		}
			
		public string GetName (RoomParticipant participant)
		{
			return participant.Nick;
		}

		public JID GetJID (RoomParticipant participant)
		{
			return String.IsNullOrEmpty(participant.RealJID) ? participant.NickJID : participant.RealJID;
		}

		public object GetImage (RoomParticipant participant)
		{
			// FIXME: AvatarManager only deals with bare jids, so unless we know the participant's real jid, 
			// this wont work since nickJids look like room@server/nickname.
			if (!String.IsNullOrEmpty(participant.RealJID)) {
				return AvatarManager.GetAvatar(participant.RealJID);
			} else {
				return AvatarManager.GetAvatar("default");
			}
		}

		public string GetPresenceInfo (RoomParticipant participant)
		{
			return String.Empty;
		}

		public bool IsVisible (RoomParticipant participant)
		{
			return true;
		}
		
		public IEnumerable<string> GetItemGroups(RoomParticipant participant)
		{
			// FIXME: This is not i18n safe
			string role = participant.Role.ToString();
			role = role.Substring(0,1).ToUpper() + role.Substring(1) + "s";			
			return new string [] { role };
		}

		public IEnumerable<RoomParticipant> GetItemsInGroup (string groupName)
		{
			/*
			foreach (var participant in m_Room.Participants)
				if (participant.Affiliation.ToString() == groupName)
					yield return participant;
			*/
			throw new NotImplementedException();
		}
		
		void HandleConnectionStateChanged(Account account)
		{
			OnRefreshed();
		}
		
		
		void HandleOnParticipantJoin(Room room, RoomParticipant participant)
		{
			OnItemAdded(participant);
		}
		
		void HandleOnParticipantLeave(Room room, RoomParticipant participant)
		{
			OnItemRemoved(participant);
		}

		void HandleOnParticipantPresenceChange(Room room, RoomParticipant participant, Presence oldPresence)
		{
			OnItemChanged(participant);
		}
		
		void HandleOnPresenceChange(Room room, RoomParticipant participant, Presence oldPresence)
		{
			OnItemChanged(participant);
		}
		
		void HandleOnLeave(Room room, Presence pres)
		{
			OnRefreshed();
		}

		void HandleOnJoin(Room room)
		{
			OnRefreshed();
		}

		protected void OnItemAdded (RoomParticipant participant)
		{
			var evnt = ItemAdded;
			if (evnt != null)
				evnt(this, participant);
		}

		protected void OnItemRemoved (RoomParticipant participant)
		{
			var evnt = ItemRemoved;
			if (evnt != null)
				evnt(this, participant);
		}

		protected void OnItemChanged (RoomParticipant participant)
		{
			var evnt = ItemChanged;
			if (evnt != null)
				evnt(this, participant);
		}
		
		protected void OnItemsChanged ()
		{
			var evnt = ItemsChanged;
			if (evnt != null)
				ItemsChanged(this, EventArgs.Empty);
		}
		
		protected void OnRefreshed ()
		{
			var evnt = Refreshed;
			if (evnt != null)
				Refreshed(this, EventArgs.Empty);
		}
	}
}
