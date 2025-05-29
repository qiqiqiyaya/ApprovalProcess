using Ap.Share.Models;
using System;

namespace Ap.Share.Services
{
	public interface IChangeCurrentUser
	{
		void Default(User user);

		IDisposable Change(User user);
	}
}
