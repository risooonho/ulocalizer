﻿using System;
using System.Linq;
using ULocalizer.Binding;
using ULocalizer.Classes;

namespace ULocalizer.Windows
{
    /// <summary>
    ///     Interaction logic for NewProjectWindow.xaml
    /// </summary>
    public partial class NewProjectWindow
    {
        public NewProjectWindow()
        {
            InitializeComponent();
            Projects.NewProject = new CProject();

        }

        private async void ProjectPropertiesControl_Executed(object sender, EventArgs e)
        {
            Close();
            await CBuilder.Build(false);
        }
    }
}