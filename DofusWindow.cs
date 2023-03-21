using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;

namespace Dtwo.API
{
    public class DofusWindow
    {
        public readonly Process Process;
        public readonly Process WindowProcess; // for retro
        
        public Character Character { get; private set; }
        public Action OnSetCharacter;

        #region Static
        public static List<DofusWindow> WindowsList = new List<DofusWindow>();
        public static Action<DofusWindow>? OnDofusWindowStarted;
        public static Action<DofusWindow>? OnDofusWindowStoped;

        public static DofusWindow Selected { get; private set; }
        public static Action<DofusWindow>? OndofusWindowSelected;

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

        public bool IsSelected => Selected == this;
        public void Select() => SelectDofusWindow(this);

        public static void SelectDofusWindow(DofusWindow dofusWindow)
        {
            Selected = dofusWindow;
            OndofusWindowSelected?.Invoke(dofusWindow);
        }
    }
}
