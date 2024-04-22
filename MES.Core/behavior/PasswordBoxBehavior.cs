using MES.Core.password;
using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;

namespace MES.Core.behavior
{
    public class PasswordBoxBehavior : Behavior<PasswordBox>
    {
        private static void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var box = sender as PasswordBox;
            if (box == null)
            {
                return;
            }

            var password = PasswordBoxHelper.GetPassword(box);

            if (box.Password != password)
            {
                PasswordBoxHelper.SetPassword(box, box.Password);
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PasswordChanged += OnPasswordChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PasswordChanged -= OnPasswordChanged;
        }
    }
}
