﻿using Microsoft.Maui.Controls;

namespace Spectre.Net
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}