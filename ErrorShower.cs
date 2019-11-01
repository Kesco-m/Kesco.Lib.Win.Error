using System;
using System.Windows.Forms;

namespace Kesco.Lib.Win.Error
{
	/// <summary>
	/// Класс для показа ошибок без блокировки окна.
	/// </summary>
	public class ErrorShower
	{
		public static bool ErrorShown;

		public static event EventHandler StartErrorShow;
		public static event EventHandler ErrorShowEnd;

		public static void OnShowError(Control askControl, string errorText, string errorTitle)
		{
			if(StartErrorShow != null)
				StartErrorShow(askControl, EventArgs.Empty);
			ErrorShown = true;
			if(askControl != null && askControl.InvokeRequired)
				askControl.Invoke(new NeedShowError(ShowError), new object[] { errorText, errorTitle });
			else
				ShowError(errorText, errorTitle);
			OnEndShowError(askControl);
		}

		private static void ShowError(string errorText, string errorTitle)
		{
			MessageBox.Show(errorText, errorTitle);
		}

		public static void OnEndShowError(Control askControl)
		{
			ErrorShown = false;
			if(ErrorShowEnd != null)
				ErrorShowEnd(askControl, System.EventArgs.Empty);
		}
	}

	internal delegate void NeedShowError(string errorText, string errorTitle);
}