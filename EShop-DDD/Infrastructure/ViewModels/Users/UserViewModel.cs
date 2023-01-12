namespace Persistence.Users.ViewModels
{
	public class UserViewModel : UserRequestViewModel
	{
		public UserViewModel() : base()
		{
		}

		public System.Guid? Id { get; set; }

		public bool EmailAddressIsVerified { get; set; }

		public string EmailAddressVerificationKey { get; set; }
	}
}
