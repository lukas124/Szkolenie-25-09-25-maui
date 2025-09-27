using Android.Gms.Tasks;
using MauiStart.Models.Services.Interfaces;
using Xamarin.Google.Android.Play.Core.Integrity;

namespace MauiStart.Platforms.Services;

public class GooglePlayIntegrityService : Java.Lang.Object, IGooglePlayIntegrityService
{
    public Task<string> GetIntegrityTokenAsync()
    {
        var tcs = new TaskCompletionSource<string>();

        var integrityManager = IntegrityManagerFactory.Create(Android.App.Application.Context);
        var nonce = Guid.NewGuid().ToString();

        var request = IntegrityTokenRequest.InvokeBuilder()
            .SetNonce(nonce)
            .Build();

        integrityManager
            .RequestIntegrityToken(request)
            .AddOnSuccessListener(new IntegritySuccessListener(tcs))
            .AddOnFailureListener(new IntegrityFailureListener(tcs));
        
        return tcs.Task;
    }

    private class IntegritySuccessListener : Java.Lang.Object, IOnSuccessListener
    {
        private readonly TaskCompletionSource<string> _tcs;

        public IntegritySuccessListener(TaskCompletionSource<string> tcs)
        {
            _tcs = tcs;
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            if (result is IntegrityTokenResponse response)
            {
                _tcs.TrySetResult(response.Token());
            }
            else
            {
                _tcs.TrySetException(new InvalidCastException("Unexpected IntegrityTokenResponse"));
            }
        }
    }

    private class IntegrityFailureListener : Java.Lang.Object, IOnFailureListener
    {
        private readonly TaskCompletionSource<string> _tcs;

        public IntegrityFailureListener(TaskCompletionSource<string> tcs)
        {
            _tcs = tcs;
        }
        
        public void OnFailure(Java.Lang.Exception e)
        {
            _tcs.TrySetException(new Exception("Faild to get integrity token", e));
        }
    }
}