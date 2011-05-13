//  
//  RoleBuilder.cs
//  
//  Author:
//       Giacomo Tesio <giacomo@tesio.it>
// 
//  Copyright (c) 2010-2011 Giacomo Tesio
// 
//  This file is part of Epic.NET.
// 
//  Epic.NET is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Affero General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  Epic.NET is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Affero General Public License for more details.
// 
//  You should have received a copy of the GNU Affero General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//  
using System;
using System.Security.Principal;

namespace Epic.Enterprise
{
	/// <summary>
	/// Role builder base class. Enforce <typeparamref name="TRole"/> to be an interface 
	/// and the concrete roles to derive <see cref="RoleBase"/>.
	/// </summary>
	/// <typeparam name="TRole">Type (interface) of role that the builder can build.</typeparam>
	/// <exception cref='InvalidOperationException'>
	/// Is thrown on type initialization if <typeparamref name="TRole"/> is not an interface.
	/// </exception>
	/// <exception cref='InvalidCastException'>
	/// Is thrown when the role created by <see cref="RoleBuilder{TRole}.BuildRole(IPrincipal)"/> 
	/// do not extend <typeparamref name="TRole"/>.
	/// </exception>
	[Serializable]
	public abstract class RoleBuilderBase<TRole> where TRole : class
	{
		/// <summary>
		/// Initializes a role builder class type.
		/// </summary>
		/// <exception cref='InvalidOperationException'>
		/// <typeparamref name="TRole"/> is not an interface.
		/// </exception>
		static RoleBuilderBase()
		{
			if(!typeof(TRole).IsInterface)
			{
				string message = string.Format("Can not initialize a RoleBuilder<{0}> since {0} is not an interface.", typeof(TRole).FullName);
				throw new InvalidOperationException(message);
			}
		}
		
		/// <summary>
		/// Build the <typeparamref name="TRole"/> for <paramref name="player"/>. Template method.
		/// </summary>
		/// <param name='player'>
		/// Role player.
		/// </param>
		/// <exception cref='InvalidCastException'>
		/// Is thrown when the role created by <see cref="RoleBuilder{TRole}.BuildRole(IPrincipal)"/> 
		/// do not extend <typeparamref name="TRole"/>.
		/// </exception>
		/// <seealso cref="RoleBuilder{TRole}.BuildRole(IPrincipal)"/>
		public TRole Build(IPrincipal player)
		{
			RoleBase baseRole = BuildRole(player);
			TRole newRole = baseRole as TRole;
			if(null == newRole)
			{
				string message = string.Format("The {0} role do not implement {1}.", baseRole.GetType().FullName, typeof(TRole).FullName);
				throw new InvalidCastException(message);
			}
			return newRole;
		}
		
		/// <summary>
		/// Build the <typeparamref name="TRole"/> for <paramref name="player"/>.
		/// </summary>
		/// <returns>
		/// The role. Must implement <typeparamref name="TRole"/>.
		/// </returns>
		/// <param name='player'>
		/// Role player.
		/// </param>
		protected abstract RoleBase BuildRole(IPrincipal player);
	}
}
