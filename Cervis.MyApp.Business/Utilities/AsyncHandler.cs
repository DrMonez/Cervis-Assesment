using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cervis.MyApp.Business.Utilities
{
	public abstract class AsyncHandler
	{
		protected readonly List<Func<Task>> handlers = new();

		public void AddHandler(Func<Task> handler)
		{
			lock (handlers)
				handlers.Add(handler);
		}

		public void RemoveHandler(Func<Task> handler)
		{
			lock (handlers)
				handlers.Remove(handler);
		}
	}

	public sealed class InvokableAsyncHandler : AsyncHandler
	{
		public Task InvokeAsync()
		{
			lock (handlers)
			{
				using (ExecutionContext.SuppressFlow())
					return Task.Run(() => Task.WhenAll(handlers.Select(h => h()).ToArray()));
			}
		}
	}

	public abstract class AsyncHandler<P>
	{
		protected readonly List<Func<P, Task>> handlers = new();

		public void AddHandler(Func<P, Task> handler)
		{
			lock (handlers)
				handlers.Add(handler);
		}

		public void RemoveHandler(Func<P, Task> handler)
		{
			lock (handlers)
				handlers.Remove(handler);
		}
	}

	public sealed class InvokableAsyncHandler<P> : AsyncHandler<P>
	{
		public Task InvokeAsync(P argument)
		{
			lock (handlers)
			{
				using (ExecutionContext.SuppressFlow())
					return Task.Run(() => Task.WhenAll(handlers.Select(h => h(argument)).ToArray()));
			}
		}
	}

	public abstract class AsyncHandler<P1, P2>
	{
		protected readonly List<Func<P1, P2, Task>> handlers = new();

		public void AddHandler(Func<P1, P2, Task> handler)
		{
			lock (handlers)
				handlers.Add(handler);
		}

		public void RemoveHandler(Func<P1, P2, Task> handler)
		{
			lock (handlers)
				handlers.Remove(handler);
		}
	}

	public sealed class InvokableAsyncHandler<P1, P2> : AsyncHandler<P1, P2>
	{
		public Task InvokeAsync(P1 argument1, P2 argument2)
		{
			lock (handlers)
			{
				using (ExecutionContext.SuppressFlow())
					return Task.Run(() => Task.WhenAll(handlers.Select(h => h(argument1, argument2)).ToArray()));
			}
		}
	}

	public abstract class AsyncHandler<P1, P2, P3>
	{
		protected readonly List<Func<P1, P2, P3, Task>> handlers = new();

		public void AddHandler(Func<P1, P2, P3, Task> handler)
		{
			lock (handlers)
				handlers.Add(handler);
		}

		public void RemoveHandler(Func<P1, P2, P3, Task> handler)
		{
			lock (handlers)
				handlers.Remove(handler);
		}
	}

	public sealed class InvokableAsyncHandler<P1, P2, P3> : AsyncHandler<P1, P2, P3>
	{
		public Task InvokeAsync(P1 argument1, P2 argument2, P3 argument3)
		{
			lock (handlers)
			{
				using (ExecutionContext.SuppressFlow())
					return Task.Run(() => Task.WhenAll(handlers.Select(h => h(argument1, argument2, argument3)).ToArray()));
			}
		}
	}

	public abstract class AsyncHandler<P1, P2, P3, P4>
	{
		protected readonly List<Func<P1, P2, P3, P4, Task>> handlers = new();

		public void AddHandler(Func<P1, P2, P3, P4, Task> handler)
		{
			lock (handlers)
				handlers.Add(handler);
		}

		public void RemoveHandler(Func<P1, P2, P3, P4, Task> handler)
		{
			lock (handlers)
				handlers.Remove(handler);
		}
	}

	public sealed class InvokableAsyncHandler<P1, P2, P3, P4> : AsyncHandler<P1, P2, P3, P4>
	{
		public Task InvokeAsync(P1 argument1, P2 argument2, P3 argument3, P4 argument4)
		{
			lock (handlers)
			{
				using (ExecutionContext.SuppressFlow())
					return Task.Run(() => Task.WhenAll(handlers.Select(h => h(argument1, argument2, argument3, argument4)).ToArray()));
			}
		}
	}

	public abstract class AsyncHandler<P1, P2, P3, P4, P5>
	{
		protected readonly List<Func<P1, P2, P3, P4, P5, Task>> handlers = new();

		public void AddHandler(Func<P1, P2, P3, P4, P5, Task> handler)
		{
			lock (handlers)
				handlers.Add(handler);
		}

		public void RemoveHandler(Func<P1, P2, P3, P4, P5, Task> handler)
		{
			lock (handlers)
				handlers.Remove(handler);
		}
	}

	public sealed class InvokableAsyncHandler<P1, P2, P3, P4, P5> : AsyncHandler<P1, P2, P3, P4, P5>
	{
		public Task InvokeAsync(P1 argument1, P2 argument2, P3 argument3, P4 argument4, P5 argument5)
		{
			lock (handlers)
			{
				using (ExecutionContext.SuppressFlow())
					return Task.Run(() => Task.WhenAll(handlers.Select(h => h(argument1, argument2, argument3, argument4, argument5)).ToArray()));
			}
		}
	}
}
