﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextMenu.Abstractions;
using DesktopTrayIcon.Abstractions;
using Xamarin.Forms;

namespace XamTrayIcon.Core
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<IContextMenuItem> _items = new ObservableCollection<IContextMenuItem>();
        private ObservableCollection<IContextMenuItem> _subItems1 = new ObservableCollection<IContextMenuItem>();

        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ((App)App.Current).ContextMenu.ItemsSource = _items;
            _items.Add(new ContextMenuButton { Label = "Test" });
            _items.Add(new ContextMenuButton { Label = "Test2" });
            _items.Move(1, 0);
            _items.Add(new ContextMenuButton { Label = "Test3" });
            _items.RemoveAt(2);

            var menu = new ContextMenu.ContextMenu();
            menu.ItemsSource = _subItems1;
            _items.Add(menu);
            _subItems1.Add(new ContextMenuButton { Label = "SubItem 1" });
            _subItems1.Add(new ContextMenuButton { Label = "SubItem 2" });
            _subItems1.Add(new ContextMenuButton { Label = "SubItem 3" });
            _subItems1.Move(2, 1);

            _items.Add(new ContextMenuButton { Label = "Test4" });
        }

        public void ShowIconButton_Clicked(object sender, EventArgs e)
        {
            ((App)App.Current).TrayIcon.Show();
        }

        public void HideIconButton_Clicked(object sender, EventArgs e)
        {
            ((App)App.Current).TrayIcon.Hide();
        }

        public void ShowContextMenuButton_Clicked(object sender, EventArgs e)
        {
            ((App)App.Current).TrayIcon.ShowContextMenu();
            Device.StartTimer(TimeSpan.FromSeconds(3), () => {
                ((App)App.Current).TrayIcon.HideContextMenu();
                return false;
            });
        }

        public void HideContextMenuButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}
