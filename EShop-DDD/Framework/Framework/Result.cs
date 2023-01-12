// **************************************************
// ***** Step (1) ***********************************
// **************************************************
//namespace Dtx
//{
//	public class Result<T> : object
//	{
//		public Result() : base()
//		{
//			Errors =
//				new System.Collections.Generic.List<string>();

//			Successes =
//				new System.Collections.Generic.List<string>();
//		}

//		public T Value { get; set; }

//		public bool IsFailed { get; set; }

//		public bool IsSuccess { get; set; }

//		public System.Collections.Generic.IList<string> Errors { get; set; }

//		public System.Collections.Generic.IList<string> Successes { get; set; }
//	}

//	public static class ResultExtensions
//	{
//		static ResultExtensions()
//		{
//		}

//		public static Result<T> ConvertToDtxResult<T>(this FluentResults.Result<T> result)
//		{
//			Dtx.Result<T> dtxResult = new Result<T>();

//			dtxResult.IsFailed = result.IsFailed;
//			dtxResult.IsSuccess = result.IsSuccess;

//			if (result.IsSuccess)
//			{
//				dtxResult.Value = result.Value;
//			}

//			if (result.Errors != null)
//			{
//				foreach (var item in result.Errors)
//				{
//					dtxResult.Errors.Add(item.Message);
//				}
//			}

//			if (result.Successes != null)
//			{
//				foreach (var item in result.Successes)
//				{
//					dtxResult.Successes.Add(item.Message);
//				}
//			}

//			return dtxResult;
//		}
//	}
//}
// **************************************************
// ***** /Step (1) **********************************
// **************************************************

// **************************************************
// ***** Step (2) ***********************************
// **************************************************
//namespace Dtx
//{
//	public class Result<T> : object
//	{
//		public Result() : base()
//		{
//			_errors =
//				new System.Collections.Generic.List<string>();

//			_successes =
//				new System.Collections.Generic.List<string>();
//		}

//		public T Value { get; set; }

//		public bool IsFailed { get; set; }

//		public bool IsSuccess { get; set; }

//		[System.Text.Json.Serialization.JsonIgnore]
//		private readonly System.Collections.Generic.List<string> _errors;

//		public System.Collections.Generic.IReadOnlyList<string> Errors
//		{
//			get
//			{
//				return _errors;
//			}
//		}

//		[System.Text.Json.Serialization.JsonIgnore]
//		private readonly System.Collections.Generic.List<string> _successes;

//		public System.Collections.Generic.IReadOnlyList<string> Successes
//		{
//			get
//			{
//				return _successes;
//			}
//		}

//		public void AddErrorMessage(string message)
//		{
//			message =
//				message.Fix();

//			if (message == null)
//			{
//				return;
//			}

//			if (_errors.Contains(message))
//			{
//				return;
//			}

//			_errors.Add(message);
//		}

//		public void AddSuccessMessage(string message)
//		{
//			message =
//				message.Fix();

//			if (message == null)
//			{
//				return;
//			}

//			if (_successes.Contains(message))
//			{
//				return;
//			}

//			_successes.Add(message);
//		}
//	}

//	public static class ResultExtensions
//	{
//		static ResultExtensions()
//		{
//		}

//		public static Result<T> ConvertToDtxResult<T>(this FluentResults.Result<T> result)
//		{
//			Result<T> dtxResult = new()
//			{
//				IsFailed = result.IsFailed,
//				IsSuccess = result.IsSuccess
//			};

//			if (result.IsSuccess)
//			{
//				dtxResult.Value = result.Value;
//			}

//			if (result.Errors != null)
//			{
//				foreach (var item in result.Errors)
//				{
//					dtxResult.AddErrorMessage(message: item.Message);
//				}
//			}

//			if (result.Successes != null)
//			{
//				foreach (var item in result.Successes)
//				{
//					dtxResult.AddSuccessMessage(message: item.Message);
//				}
//			}

//			return dtxResult;
//		}
//	}
//}
// **************************************************
// ***** /Step (2) **********************************
// **************************************************

// **************************************************
// ***** Step (3) ***********************************
// **************************************************
namespace Framework
{
	public class Result : object
	{
		public Result() : base()
		{
			_errors =
				new System.Collections.Generic.List<string>();

			_successes =
				new System.Collections.Generic.List<string>();
		}

		public bool IsFailed { get; set; }

		public bool IsSuccess { get; set; }

		[System.Text.Json.Serialization.JsonIgnore]
		private readonly System.Collections.Generic.List<string> _errors;

		public System.Collections.Generic.IReadOnlyList<string> Errors
		{
			get
			{
				return _errors;
			}
		}

		[System.Text.Json.Serialization.JsonIgnore]
		private readonly System.Collections.Generic.List<string> _successes;

		public System.Collections.Generic.IReadOnlyList<string> Successes
		{
			get
			{
				return _successes;
			}
		}

		public void AddErrorMessage(string message)
		{
			message =
				message.Fix();

			if (message == null)
			{
				return;
			}

			if (_errors.Contains(message))
			{
				return;
			}

			_errors.Add(message);
		}

		public void AddSuccessMessage(string message)
		{
			message =
				message.Fix();

			if (message == null)
			{
				return;
			}

			if (_successes.Contains(message))
			{
				return;
			}

			_successes.Add(message);
		}
	}

	public class Result<T> : Result
	{
		public Result() : base()
		{
		}

		public T Value { get; set; }
	}

	public static class ResultExtensions
	{
		static ResultExtensions()
		{
		}

		public static Result ConvertToDtxResult(this FluentResults.Result result)
		{
			Result dtxResult = new()
			{
				IsFailed = result.IsFailed,
				IsSuccess = result.IsSuccess
			};

			if (result.Errors != null)
			{
				foreach (var item in result.Errors)
				{
					dtxResult.AddErrorMessage(message: item.Message);
				}
			}

			if (result.Successes != null)
			{
				foreach (var item in result.Successes)
				{
					dtxResult.AddSuccessMessage(message: item.Message);
				}
			}

			return dtxResult;
		}

		public static Result<T> ConvertToDtxResult<T>(this FluentResults.Result<T> result)
		{
			Result<T> dtxResult = new()
			{
				IsFailed = result.IsFailed,
				IsSuccess = result.IsSuccess
			};

			if (result.IsSuccess)
			{
				dtxResult.Value = result.Value;
			}

			if (result.Errors != null)
			{
				foreach (var item in result.Errors)
				{
					dtxResult.AddErrorMessage(message: item.Message);
				}
			}

			if (result.Successes != null)
			{
				foreach (var item in result.Successes)
				{
					dtxResult.AddSuccessMessage(message: item.Message);
				}
			}

			return dtxResult;
		}
	}
}
// **************************************************
// ***** /Step (3) **********************************
// **************************************************
