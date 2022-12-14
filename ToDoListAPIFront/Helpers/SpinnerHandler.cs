using ToDoListAPIFront.Services;

namespace ToDoListAPIFront.Helpers;

public class SpinnerHandler : DelegatingHandler
{
    private readonly SpinnerService _spinnerService;

    public SpinnerHandler(SpinnerService spinnerService)
    {
        _spinnerService = spinnerService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        _spinnerService.Show();
        var response = await base.SendAsync(request, cancellationToken);
        _spinnerService.Hide();
        return response;
    }
}