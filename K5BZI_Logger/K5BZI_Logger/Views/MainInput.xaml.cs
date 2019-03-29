﻿using System.Windows.Controls;
using System.Windows.Input;

namespace K5BZI_Logger.Views
{
    /// <summary>
    /// Interaction logic for MainInput.xaml
    /// </summary>
    public partial class MainInput : UserControl
    {
        public MainInput()
        {
            InitializeComponent();

            Keyboard.Focus(lblCall);
        }
    }
}
