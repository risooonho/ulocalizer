﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ULocalizer.Binding;
using ULocalizer.Classes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ULocalizer.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string _Title = "ULocalizer";
        public string WTitle
        {
            get 
            {
                if (string.IsNullOrWhiteSpace(Projects.CurrentProject.Name))
                {
                    return _Title;
                }
                else
                {
                    return _Title + " - "+Projects.CurrentProject.Name + ".ulp";
                }
            }
        }


        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;    
    
        }


        private void NewProjectWnd_Closed(object sender, EventArgs e)
        {
            this.GetBindingExpression(Window.TitleProperty).UpdateTarget();
        }
        
        private void LocDocs_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://docs.unrealengine.com/latest/INT/Gameplay/Localization/index.html");
        }

        private async void BuildBtn_Click(object sender, RoutedEventArgs e)
        {
            await CBuilder.Build();
        }


        private void DefaultLocConfigBtn_Click(object sender, RoutedEventArgs e)
        {
            if (System.IO.File.Exists(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"data\Localization.ini")))
            {
                System.Diagnostics.Process.Start(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"data\Localization.ini"));
            }
            else
            {
                Common.WriteToConsole("[ERROR] Cannot locate default localization config file.");
            }
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if ((e.NewValue != null) && (e.NewValue.GetType().ToString() == "ULocalizer.Classes.CTranslationNode"))
            {
                    Common.SelectedNode = (CTranslationNode)e.NewValue;
            }
        }

        public ItemsControl GetSelectedTreeViewItemParent(TreeView item)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(item);
            while (!(parent is TreeViewItem || parent is TreeView))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as ItemsControl;
        }

        private void LanguagesBtn_Click(object sender, RoutedEventArgs e)
        {
            PropertiesWindow PropertiesWnd = new PropertiesWindow();
            PropertiesWnd.Owner = this;
            PropertiesWnd.ShowDialog();
        }


        private void GettingStartedBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/aoki-sora/ulocalizer#getting-started");
        }

        private void NewProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            NewProjectWindow NewProjectWnd = new NewProjectWindow();
            NewProjectWnd.Owner = this;
            NewProjectWnd.Closed += NewProjectWnd_Closed;
            NewProjectWnd.ShowDialog();
        }

        private async void OpenProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            await Projects.Open();
            this.GetBindingExpression(Window.TitleProperty).UpdateTarget();
        }

        private void LanguagesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((e.AddedItems.Count>0) && (e.AddedItems[0].GetType().ToString() == "ULocalizer.Classes.CTranslation"))
            {
                Common.SelectedLang = (CTranslation)e.AddedItems[0];
                if (Common.SelectedLang.Nodes.Count > 0)
                {
                    NodeSelectionBtn.SelectedIndex = 0;
                }
            }
        }

        private async void SaveAllBtn_Click(object sender, RoutedEventArgs e)
        {
            await Projects.CurrentProject.SaveTranslations(true);
        }


        private void GitHubBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/aoki-sora/ulocalizer");
        }

        private void PropertiesBtn_Click(object sender, RoutedEventArgs e)
        {
            PropertiesWindow PropertiesWnd = new PropertiesWindow();
            PropertiesWnd.Owner = this;
            PropertiesWnd.ShowDialog();
        }

        private void ConsoleBtn_Click(object sender, RoutedEventArgs e)
        {
            ConsoleWindow ConsoleWnd = new ConsoleWindow();
            ConsoleWnd.Owner = this;
            ConsoleWnd.Show();
        }



    }
}