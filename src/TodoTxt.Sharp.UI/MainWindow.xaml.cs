﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Microsoft.Win32;

namespace TodoTxt.Sharp.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TaskFile _file;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenButton_OnClick(object sender, RoutedEventArgs e)
        {
            var fileChooser = new OpenFileDialog();

            if (fileChooser.ShowDialog() == true) {
                _file = new TaskFile(fileChooser.FileName);
                TaskListBox.ItemsSource = _file.Tasks;
            }
        }

        private void InputTextBox_OnKeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key != Key.Return)
                return;

            _file.AddTask(new Task(InputTextBox.Text));
            InputTextBox.Clear();

        }
    }
}
