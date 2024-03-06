using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using TheSpine.Application.Features.Commands.SegmentItems;
using TheSpine.Application.Features.Commands.SegmentItems.Manage;
using TheSpine.Application.Features.Commands.Segments;
using TheSpine.Application.Features.Queries.GetSegments;
using TheSpine.Core.Enums;

namespace TheSpine.AppLibrary.Components.Dialogs
{
    public partial class ManageSegmentItemDialog
    {
        [CascadingParameter] 
        public MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public EventCallback<int> OnChangedEventCallback { get; set; }

        [Parameter]
        public SegmentItemViewModel SegmentItemViewModel { get; set; }

        [Parameter]
        public bool IsEditing { get; set; }

        [Inject]
        public IMediator Mediator { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        private IEnumerable<EaseOfUse?> easeOfUseGrades = Enum.GetValues(typeof(EaseOfUse)).Cast<EaseOfUse?>();
        private IEnumerable<SegmentViewModel> segments= new List<SegmentViewModel>(10);
        private Func<EaseOfUse?, string> easeOfUseConverter = x => x.ToString();
        private Func<SegmentViewModel, string> segmentConverter = x => x?.Title;

        private MudForm form;
        private SegmentItemViewModelValidator validator = new SegmentItemViewModelValidator();

        private SegmentItemViewModel initViewModel;

        protected override async Task OnInitializedAsync()
        {
            segments = await Mediator.Send(new GetSegmentsQuery());
            initViewModel = new(SegmentItemViewModel);
        }

        private async Task Submit()
        {
            await form.Validate();

            if (form.IsValid)
            {
                var response = await Mediator.Send(new ManageSegmentItemCommand(SegmentItemViewModel, IsEditing));

                if (response != null)
				{
					Snackbar.Add(response.Message, response.IsSuccess ? Severity.Success : Severity.Error);

					if (response.IsSuccess)
                    {
                        await OnChangedEventCallback.InvokeAsync(1);

                        MudDialog.Close(DialogResult.Ok(true));
                    }
                }
            }
        }

        private async Task Cancel()
        {
            await OnChangedEventCallback.InvokeAsync(1);

            MudDialog.Cancel();
        }
    }
}
