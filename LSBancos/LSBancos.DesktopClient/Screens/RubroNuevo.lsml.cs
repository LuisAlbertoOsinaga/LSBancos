﻿using Microsoft.LightSwitch.Presentation.Extensions;
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
    public partial class RubroNuevo
    {
        partial void RubroNuevo_InitializeDataWorkspace(global::System.Collections.Generic.List<global::Microsoft.LightSwitch.IDataService> saveChangesTo)
        {
            // Write your code here.
            this.RubroProperty = new Rubro();
        }

        partial void RubroNuevo_Saved()
        {
            // Write your code here.
            this.Close(false);

            // Refresh all screens
            var screens = Application.ActiveScreens;
            foreach (var s in screens)
            {
                var screen = s.Screen;
                screen.Details.Dispatcher.BeginInvoke(() => {
                    if (screen.DisplayName == "Rubros")
                        screen.Refresh();
                });
            }

            //Application.Current.ShowDefaultScreen(this.RubroProperty);
        }
    }
}