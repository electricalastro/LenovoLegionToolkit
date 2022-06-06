﻿using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using LenovoLegionToolkit.WPF.Utils;

namespace LenovoLegionToolkit.WPF.Windows
{
    public class BaseWindow : Window
    {
        private readonly ThemeManager _themeManager = Container.Resolve<ThemeManager>();

        public BaseWindow()
        {
            IsVisibleChanged += MicaWindow_IsVisibleChanged;
        }

        private void MicaWindow_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!IsVisible)
                return;

            var result = ApplyBackgroundEffect();
            if (!result)
                return;

            _themeManager.ThemeApplied += ThemeManager_ThemeApplied;

            Closed += BaseWindow_Closed;
            IsVisibleChanged -= MicaWindow_IsVisibleChanged;
        }

        private void BaseWindow_Closed(object? sender, EventArgs e) => _themeManager.ThemeApplied -= ThemeManager_ThemeApplied;

        private void ThemeManager_ThemeApplied(object? sender, EventArgs e) => ApplyBackgroundEffect();

        private bool ApplyBackgroundEffect()
        {
            var ptr = new WindowInteropHelper(this).Handle;
            if (ptr == IntPtr.Zero)
                return false;

            if (!WPFUI.Appearance.Background.IsSupported(WPFUI.Appearance.BackgroundType.Mica))
                return false;

            var result = WPFUI.Appearance.Background.Apply(ptr, WPFUI.Appearance.BackgroundType.Mica);
            if (!result)
                return false;

            Background = Brushes.Transparent;
            return true;
        }
    }
}