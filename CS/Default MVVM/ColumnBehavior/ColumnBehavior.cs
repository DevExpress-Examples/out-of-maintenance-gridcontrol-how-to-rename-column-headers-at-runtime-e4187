using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Grid;

namespace Default_MVVM
{
    public static class ColumnBehavior
    {
        public static readonly DependencyProperty IsRenameEditorActivatedProperty = DependencyProperty.RegisterAttached("IsRenameEditorActivated",
                                                                                        typeof(bool),
                                                                                        typeof(ColumnBehavior),
                                                                                        new PropertyMetadata(false));

        public static void SetIsRenameEditorActivated(GridColumn element, bool value)
        {
            if (element.Header == null && value)
                element.Header = element.FieldName;
            element.SetValue(IsRenameEditorActivatedProperty, value);
        }
        public static bool GetIsRenameEditorActivated(GridColumn element)
        {
            return (bool)element.GetValue(IsRenameEditorActivatedProperty);
        }
    }
}