using System;

namespace Cervis.MyApp.Business.Operations
{
	public class OperationResult<CodeT> where CodeT : Enum
	{
		public CodeT Code { get; }

		public OperationResult(CodeT code)
		{
			Code = code;
		}
	}

	public class OperationResult<CodeT, DataT> : OperationResult<CodeT>
		where CodeT : Enum
	{
		public DataT Data { get; }

		public OperationResult(CodeT code, DataT data) : base(code)
		{
			Data = data;
		}
	}
}
