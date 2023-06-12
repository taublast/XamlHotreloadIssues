namespace HotReloadCases
{
	[ContentProperty("CustomView")]
	public class CustomControl : ContentView
	{
		public CustomControl()
		{
			_grid = new Grid();
			//this changes in code behind also will not be applied
			_pseudoUi = new Label
			{
				Text = "OVERLAY",
				TextColor = Colors.Red,
				FontSize = 12,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center
			};
			Content = _grid;
			Update();
		}

		public virtual void Update()
		{
			MainThread.BeginInvokeOnMainThread(() =>
			{
				_grid.Children.Clear();

				if (CustomView is ContentView view)
				{
					if (view.Content is Label label) //case 1 not working
					{
						// if we are using our own renderer (here we use a silly label but we might draw with skia, whatever)
						// we will never get the xaml update, as we do not have the changed Element in the visual tree.
						// basically when we use our renderer we have no link with the possible hotreload changes.
						// absolutely same case goes with using a DataTemplate. we consume and we render it and we
						// will never get notified when template xaml changes.
						// the solution could be to create an interface like IHotReloadAware and call a method like
						// OnHotRead(changed element) for the parent element if it implements it.
						// This interface will likely be used by custom controls only, standart controls will act as usual.
						_grid.Children.Add(new Label()
						{
							Text = label.Text //we could consume a datatemplate here.
						});
					}
					else //case 2 working
					{
						// here hot reload finds the element for the standart label and updates it
						_grid.Children.Add(view.Content);
					}
				}

				_grid.Children.Add(_pseudoUi);
			});
		}

		public static readonly BindableProperty CustomViewProperty = BindableProperty.Create(
			nameof(CustomView),
			typeof(View),
			typeof(CustomControl),
			defaultValue: null, propertyChanged: OnViewChanged);

		private readonly Grid _grid;
		private readonly View _pseudoUi;

		private static void OnViewChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			if (bindable is CustomControl control)
			{
				control.Update();
			}
		}

		public View CustomView
		{
			get { return (View)GetValue(CustomViewProperty); }
			set { SetValue(CustomViewProperty, value); }
		}




	}
}
