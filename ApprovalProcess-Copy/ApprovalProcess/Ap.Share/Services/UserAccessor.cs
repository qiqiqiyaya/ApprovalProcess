using Ap.Share.Models;
using System;
using System.Threading;

namespace Ap.Share.Services
{
	public class UserAccessor : ICurrentUser, IChangeCurrentUser
	{
		private readonly AsyncLocal<User> _current = new AsyncLocal<User>();

		public User User => _current.Value ?? throw new NullReferenceException($"Please use {nameof(IChangeCurrentUser)}.{nameof(IChangeCurrentUser.Change)} for setup first.");

		public void Default(User user)
		{
			_current.Value ??= user;
		}

		public IDisposable Change(User user)
		{
			var parent = _current.Value;
			_current.Value = user;
			return new DisposeAction(() =>
			{
				_current.Value = parent;
			});
		}
	}
}
