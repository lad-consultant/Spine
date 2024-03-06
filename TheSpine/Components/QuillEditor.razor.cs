﻿using Microsoft.AspNetCore.Components;

namespace TheSpine.AppLibrary.Components
{
	public partial class QuillEditor
	{
		[Parameter] public RenderFragment EditorContent { get; set; }
		[Parameter] public RenderFragment ToolbarContent { get; set; }
		[Parameter] public bool ReadOnly { get; set; } = false;
		[Parameter] public string Placeholder { get; set; } = "Insert text here...";
		[Parameter] public string Theme { get; set; } = "snow";
		[Parameter] public string DebugLevel { get; set; } = "info";

		private ElementReference QuillElement;
		private ElementReference ToolBar;

		protected override async Task
			OnAfterRenderAsync(bool firstRender)
		{
			if (firstRender)
			{
				await QuillInterop.CreateQuill(
					JSRuntime,
					QuillElement,
					ToolBar,
					ReadOnly,
					Placeholder,
					Theme,
					DebugLevel);
			}
		}

		public async Task<string> GetText()
		{
			return await QuillInterop.GetText(
				JSRuntime, QuillElement);
		}

		public async Task<string> GetHTML()
		{
			return await QuillInterop.GetHTML(
				JSRuntime, QuillElement);
		}

		public async Task<string> GetContent()
		{
			return await QuillInterop.GetContent(
				JSRuntime, QuillElement);
		}

		public async Task LoadContent(string Content)
		{
			var QuillDelta =
				await QuillInterop.LoadQuillContent(
					JSRuntime, QuillElement, Content);
		}

		public async Task EnableEditor(bool mode)
		{
			var QuillDelta =
				await QuillInterop.EnableQuillEditor(
					JSRuntime, QuillElement, mode);
		}

		public async Task InsertImage(string ImageURL)
		{
			var QuillDelta =
				await QuillInterop.InsertQuillImage(
					JSRuntime, QuillElement, ImageURL);
		}
	}
}
