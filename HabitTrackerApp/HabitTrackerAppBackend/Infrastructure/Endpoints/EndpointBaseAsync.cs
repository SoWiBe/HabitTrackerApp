using Microsoft.AspNetCore.Mvc;

namespace HabitTrackerAppBackend.Infrastructure.Endpoints;

public static class EndpointBaseAsync
{
    public static class WithRequest<TRequest>
    {
        public abstract class WithResult<TResponse> : EndpointBase
        {
            public abstract Task<TResponse> HandleAsync(TRequest request,
                CancellationToken cancellationToken = default);
        }

        public abstract class WithoutResult : EndpointBase
        {
            public abstract Task HandleAsync(TRequest request, CancellationToken cancellationToken = default);
        }

        public abstract class WithActionResult<TResponse> : EndpointBase
        {
            public abstract Task<ActionResult<TResponse>> HandleAsync(TRequest request,
                CancellationToken cancellationToken = default);
        }

        public abstract class WithActionResult : EndpointBase
        {
            public abstract Task<ActionResult> HandleAsync(TRequest request,
                CancellationToken cancellationToken = default);
        }
        
        public abstract class WithViewActionResult : ViewEndpointBase
        {
            public abstract Task<IActionResult> IndexAsync(TRequest request,
                CancellationToken cancellationToken = default);
        }
    }

    public static class WithoutRequest
    {
        public abstract class WithResult<TResponse> : EndpointBase
        {
            public abstract Task<TResponse> HandleAsync(CancellationToken cancellationToken = default);
        }

        public abstract class WithoutResult : EndpointBase
        {
            public abstract Task HandleAsync(CancellationToken cancellationToken = default);
        }

        public abstract class WithActionResult<TResponse> : EndpointBase
        {
            public abstract Task<ActionResult<TResponse>> HandleAsync(CancellationToken cancellationToken = default);
        }

        public abstract class WithActionResult : EndpointBase
        {
            public abstract Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default);
        }
        
        public abstract class WithViewActionResult : ViewEndpointBase
        {
            public abstract Task<IActionResult> IndexAsync(CancellationToken cancellationToken = default);
        }
    }
}