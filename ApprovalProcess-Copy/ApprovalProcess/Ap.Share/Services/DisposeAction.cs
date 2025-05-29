using System;

namespace Ap.Share.Services
{
	public class DisposeAction(Action action) : IDisposable
	{
		private readonly Action _action = action ?? throw new ArgumentNullException(nameof(action));

		public void Dispose()
		{
			_action();
		}
	}
}
