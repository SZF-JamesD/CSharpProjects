# MvvmUtilities

MvvmUtilities is a MVVM suppoer library for WPF (.Net Framework 4.7.2+). It simplifies common tasks such as view model wiring, command binding, dialog abstraction,
event aggregation, and safe async task handling.



---


Features

ViewModelBase				|	Implements 'INotifyPropertyChanged' with 'SetProperty' helper
(Async)RelayCommand<T>		|	Command patterns with 'CanExecute', parameter support, async exception handling
DialogService				|	Interface-based UI messaging for testable MVVM
EventAggregator				|	Lightweight pub-sub for decoupled ViewModel communication
TaskHelper					|	Helper for safe async execution with error handling



---


Installation

Clone or reference the 'MvvmUtilities' project in your solution.


---


Usage Examples

1. ViewModelBase

public class UserViewModel : ViewModelBase
{
	private string _username;
	public string Username
	{
		get =>_username;
		set => SetProperty(ref _username, value)
	}
}



2. RelayCommand / AsyncRelayCommand

public ICommand SubmitCommand { get; }

public MyViewModel()
{
	SubmiteCommand = new RelayCommand(OnSubmit);
}

async 

SubmitCommand = new AsyncRelayCommand<object>(
	async (obj) => await SubmitAsync(obj),
	dialogService: new DialogService()
);


3. DialogService(IDialogService)

IDialogService dialog = new DialogService();

dialog.ShowMessage("Welcome message!", "Welcome Window");
dialog.ShowError("Error message!", "Error Window");
boo confirmed = dialog.AskUserConfirmation("Are you sure?", "Caption");


4. EventAggregator

var aggregator = new EventAggregator();

aggregator.Subscribe<string>(msg => Console.WriteLine($"Got: {msg}"));
aggregator.Publish("Hello World!");


5. TaskHelper

await TaskHelper.RunSafeAsync(async () =>
	{
		await LoadData();
	}, ex =>
	{
		_dialogService.ShowError(ex.Message);
	});

