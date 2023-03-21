using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;

namespace Dtwo.API
{
    public class DofusWindow
    {
		/// <summary>
		/// The process of the dofus window
		/// </summary>
		public readonly Process Process;

		/// <summary>
		/// In the case of an application split into a sub-application (this is the case for retro), you must be able to retrieve the handle of the window
		/// </summary>
		public readonly Process WindowProcess; // for retro
        
        /// <summary>
        /// Character informations of the dofus window
        /// </summary>
        public Character Character { get; private set; }

		/// <summary>
		/// Return true if this Dofus window is selected
		/// </summary>
		public bool IsSelected => Selected == this;

		/// <summary>
		/// Select this Dofus window
		/// </summary>
		public void Select() => SelectDofusWindow(this);

		/// <summary>
		/// Called when the character informations is set (after character selection in most cases)
		/// </summary>
		public Action OnSetCharacter;

		#region Static
		/// <summary>
		/// The select Dofus Window
		/// </summary>
		public static DofusWindow Selected { get; private set; }


		
		/// <summary>
		/// The list of all DofusWindow registered
		/// </summary>
		public static List<DofusWindow> WindowsList = new List<DofusWindow>();

		/// <summary>
		/// Called when a new DofusWindow is started
		/// </summary>
		public static Action<DofusWindow>? OnDofusWindowStarted;

		/// <summary>
		/// Called when a DofusWindow is stoped
		/// </summary>
		public static Action<DofusWindow>? OnDofusWindowStoped;


		/// <summary>
		/// Called when a DofusWindow is selected
		/// </summary>
		public static Action<DofusWindow>? OndofusWindowSelected;

		#endregion

		/// <param name="process">the process of dofus window</param>
		/// <param name="otherWindow">In the case of an application split into a sub-application (this is the case for retro)</param>
		public DofusWindow(Process process, bool otherWindow)
        {
            Process = process;

            if (otherWindow)
            {
                WindowProcess = ParentProcessUtilities.GetParentProcess(process.Id);

                Console.WriteLine("window hndle : " + WindowProcess.MainWindowHandle);
            }
            else
            {
                WindowProcess = process;
            }

            if (WindowsList.Count == 0)
            {
                Select();
            }
        }

        public void OnCharacterSelection(Character character)
        {
            Character = character;

            OnSetCharacter?.Invoke();
        }

        public static void SelectDofusWindow(DofusWindow dofusWindow)
        {
            Selected = dofusWindow;
            OndofusWindowSelected?.Invoke(dofusWindow);
        }
    }
}
