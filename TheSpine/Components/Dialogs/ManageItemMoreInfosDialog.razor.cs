using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace TheSpine.AppLibrary.Components.Dialogs;

public partial class ManageItemMoreInfosDialog
{
    private CancellationTokenSource cancelationTokenSource = new CancellationTokenSource();
    [CascadingParameter] public MudDialogInstance MudDialog { get; set; }

    private void Close()
    {
        cancelationTokenSource.Cancel();
        MudDialog.Cancel();
    }
}