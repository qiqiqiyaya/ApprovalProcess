using Ap.Share.Services;

namespace TestProject1
{
	public class UserTest : BaseTest
	{
		[Fact]
		public void CurrentUserTest()
		{
			var currentUser = GetRequiredService<ICurrentUser>();
			Assert.Throws<NullReferenceException>(() =>
			{
				var user = currentUser.User;
			});
		}

		[Fact]
		public void ChangeCurrentUserTest()
		{
			var change = GetRequiredService<IChangeCurrentUser>();
			var currentUser = GetRequiredService<ICurrentUser>();

			using (change.Change(new Ap.Share.Models.User { Id = "1", Name = "test" }))
			{
				var user1 = currentUser.User;
				Assert.Equal("1", user1.Id);

				using (change.Change(new Ap.Share.Models.User { Id = "2", Name = "test" }))
				{
					var user2 = currentUser.User;
					Assert.Equal("2", user2.Id);
				}

				Assert.Equal("1", user1.Id);
			}

			Assert.Throws<NullReferenceException>(() =>
			{
				var user = currentUser.User;
			});
		}
	}
}
