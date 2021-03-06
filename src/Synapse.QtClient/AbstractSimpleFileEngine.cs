//
// AbstractSimpleFileEngine.cs
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
using Qyoto;

namespace Synapse.QtClient
{
	public abstract class AbstractSimpleFileEngine : QAbstractFileEngine
	{
		public override bool Open (uint openMode)
		{
			if ((openMode & (uint)QIODevice.OpenModeFlag.WriteOnly) == (uint)QIODevice.OpenModeFlag.WriteOnly)
				return false;
			return true;
		}		

		public override long Write (string data, long len)
		{
			throw new InvalidOperationException();
		}

		public override bool SupportsExtension (QAbstractFileEngine.Extension extension)
		{
			return false;
		}

		public override bool SetSize (long size)
		{
			throw new InvalidOperationException();
		}

		public override bool SetPermissions (uint perms)
		{
			return false;
		}

		public override void SetFileName (string file)
		{
			throw new InvalidOperationException();
		}

		public override bool Rmdir (string dirName, bool recurseParentDirectories)
		{
			return false;
		}

		public override bool Rename (string newName)
		{
			return false;
		}

		public override bool Remove ()
		{
			return false;
		}

		public override uint OwnerId (QAbstractFileEngine.FileOwner arg1)
		{
			unchecked {
				return (uint)-2;
			}
		}

		public override string Owner (QAbstractFileEngine.FileOwner arg1)
		{
			return String.Empty;
		}

		public override bool Mkdir (string dirName, bool createParentDirectories)
		{
			return false;
		}

		public override bool Link (string newName)
		{
			return false;
		}

		public override bool IsSequential ()
		{
			return false;
		}

		public override bool IsRelativePath ()
		{
			return false;
		}

		public override bool Flush ()
		{
			return false;
		}

		public override QDateTime fileTime (QAbstractFileEngine.FileTime time)
		{
			return new QDateTime();
		}
		
		public override uint FileFlags ()
		{
			throw new InvalidOperationException();
		}
		
		public new bool AtEnd ()
		{
			return Pos() == Size();
		}

        const uint PermsMask  = 0x0000FFFF;
        const uint TypesMask  = 0x000F0000;
        const uint FlagsMask  = 0x0FF00000;
		
		public override uint FileFlags (uint type)
		{
			QAbstractFileEngine.FileFlag ret = 0;
			
			if ((type & PermsMask) == PermsMask) {
				ret |= (QAbstractFileEngine.FileFlag.ReadOwnerPerm | 
					QAbstractFileEngine.FileFlag.ReadUserPerm | 
					QAbstractFileEngine.FileFlag.ReadGroupPerm |
					QAbstractFileEngine.FileFlag.ReadOtherPerm);
			}
			
			if ((type & TypesMask) == TypesMask) {
				ret |= QAbstractFileEngine.FileFlag.FileType;
			}
			
			if ((type & FlagsMask) == FlagsMask) {
					ret |= Qyoto.QAbstractFileEngine.FileFlag.ExistsFlag;
			}

			return (uint) ret;
		}

		public override bool extension (QAbstractFileEngine.Extension extension)
		{
			throw new InvalidOperationException();
		}

		public override System.Collections.Generic.List<string> EntryList (uint filters, System.Collections.Generic.List<string> filterNames)
		{
			throw new InvalidOperationException();
		}

		public override QAbstractFileEngineIterator EndEntryList ()
		{
			throw new InvalidOperationException();
		}

		public override bool Copy (string newName)
		{
			throw new InvalidOperationException();
		}

		public override bool CaseSensitive ()
		{
			return true;
		}

		public override QAbstractFileEngineIterator BeginEntryList (uint filters, System.Collections.Generic.List<string> filterNames)
		{
			throw new InvalidOperationException();
		}	
	}
}
