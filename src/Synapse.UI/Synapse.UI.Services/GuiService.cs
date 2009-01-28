// 
// GuiService.cs
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
using System.Reflection;
using System.Collections.Generic;

using Synapse.ServiceStack;
using Synapse.UI.Chat;

using Mono.Addins;

namespace Synapse.UI.Services
{
	public class GuiService : IService, IRequiredService, IInitializeService
	{
		List<IMessageFormatter> m_MessageDisplayFormatters = new List<IMessageFormatter>();
		
		public void Initialize ()
		{
			foreach (TypeExtensionNode node in AddinManager.GetExtensionNodes("/Synapse/UI/Chat/MessageDisplayFormatters")) {
				m_MessageDisplayFormatters.Add((IMessageFormatter)node.CreateInstance());
			}
		}

		public IEnumerable<IMessageFormatter> MessageDisplayFormatters {
			get {
				return m_MessageDisplayFormatters.AsReadOnly();
			}
		}
			
		public string ServiceName {
			get {
				return "GuiService";
			}
		}
	}
}
