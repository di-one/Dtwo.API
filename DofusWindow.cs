using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;

namespace Dtwo.API
{
    public class DofusWindow
    {
        /// <summary>
        /// Sub process, It is preferable to use WindowProcess for actions on the window.
        /// </summary>
        public readonly Process Process;

        /// <summary>
        /// Window process (parent process)
        /// </summary>
        public readonly Process? WindowProcess;
        
        /// <summary>
        /// The character basic informations
        /// </summary>
        public Character? Character { get; private set; }

        /// <summary>
        /// Event when a character is set (after the character selection)
        /// </summary>
        public Action? OnSetCharacter;

        /// <summary>
        /// Get if is the currently selected Dofus window
        /// </summary>
        public bool IsSelected => Selected == this;

        /// <summary>
        /// Select the current Dofus window
        /// </summary>
        public void Select() => SelectDofusWindow(this);

        #region Static

        /// <summary>
        /// All Dofus windows
        /// </summary>
        public static List<DofusWindow> WindowsList = new List<DofusWindow>();

        /// <summary>
        /// Event when a Dofus window is started
        /// </summary>
        public static Action<DofusWindow>? OnDofusWindowStarted;

        /// <summary>
        /// Event when a Dofus window is stoped
        /// </summary>
        public static Action<DofusWindow>? OnDofusWindowStoped;

        /// <summary>
        /// Event when a Dofus window is selected
        /// </summary>
        public static Action<DofusWindow>? OndofusWindowSelected;

        /// <summary>
        /// The currently selected Dofus window
        /// </summary>
        public static DofusWindow? Selected { get; private set; }
        #endregion

        public DofusWindow(Process process, bool otherWindow)
        {
            Process = process;

            if (otherWindow)
            {
                WindowProcess = ParentProcessUtilities.GetParentProcess(process.Id);
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

        /// <summary>
        /// Select a Dofus window
        /// </summary>
        /// <param name="dofusWindow"></param>
        public static void SelectDofusWindow(DofusWindow dofusWindow)
        {
            Selected = dofusWindow;
            OndofusWindowSelected?.Invoke(dofusWindow);
        }
    }
}
