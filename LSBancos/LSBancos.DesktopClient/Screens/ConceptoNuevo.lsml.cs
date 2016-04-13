using Microsoft.LightSwitch.Presentation.Extensions;
using Microsoft.LightSwitch.Presentation;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.IO;
using System.Linq;
using System;

namespace LightSwitchApplication
{
    public partial class ConceptoNuevo
    {
        partial void ConceptoNuevo_InitializeDataWorkspace(global::System.Collections.Generic.List<global::Microsoft.LightSwitch.IDataService> saveChangesTo)
        {
            // Write your code here.
            this.ConceptoProperty = new Concepto();
        }

        partial void ConceptoNuevo_Saved()
        {
            // Write your code here.
            this.Close(false);

            // Refresh all screens
            var screens = Application.ActiveScreens;
            foreach (var s in screens)
            {
                var screen = s.Screen;
                screen.Details.Dispatcher.BeginInvoke(() => {
                    if (screen.DisplayName == "Conceptos")
                        screen.Refresh();
                });
            }

            // Application.Current.ShowDefaultScreen(this.ConceptoProperty);
        }
    }
}