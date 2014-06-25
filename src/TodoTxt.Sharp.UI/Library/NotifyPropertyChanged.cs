using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace TodoTxt.Sharp.UI.Library
{
    public abstract class NotifyPropertyChanged : INotifyPropertyChanged, INotifyPropertyChanging
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

        public void RaisePropertyChangedEvent([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RaisePropertyChangingEvent<T>(Expression<Func<T>> property)
        {
            RaisePropertyChangingEvent(PropertyNameHelper.GetPropertyName(property));
        }

        public void RaisePropertyChangingEvent([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanging;
            if (handler != null)
                handler(this, new PropertyChangingEventArgs(propertyName));
        }

        public void RaisePropertyChangedEvent<T>(Expression<Func<T>> property)
        {
            RaisePropertyChangedEvent(PropertyNameHelper.GetPropertyName(property));
        }

        public void RaiseAndSetIfChanged<T>(ref T backingField, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
                throw new ArgumentNullException("propertyName");

            if (EqualityComparer<T>.Default.Equals(backingField, newValue))
                return;

            RaisePropertyChangingEvent(propertyName);
            backingField = newValue;
            RaisePropertyChangedEvent(propertyName);

        }
    }

    /// <summary>
    /// Source: http://keith-woods.com/Blog/post/Strongly-Typed-INotifyPropertyChanged-Event-Raisers.aspx
    /// </summary>
    internal class PropertyNameHelper
    {
        public static string GetPropertyName<T>(Expression<Func<T>> expression)
        {
            return GetPropertyNameFromLambda(expression);
        }

        public static string GetPropertyName<T>(Expression<Func<T, Object>> expression)
        {
            return GetPropertyNameFromLambda(expression);
        }

        private static string GetPropertyNameFromLambda(LambdaExpression lambda)
        {
            MemberExpression memberExpression = null;
            var unaryExpression = lambda.Body as UnaryExpression;

            if (unaryExpression != null)
                memberExpression = unaryExpression.Operand as MemberExpression;
            else
                memberExpression = lambda.Body as MemberExpression;

            if (memberExpression == null)
                throw new ArgumentException(String.Format("Property expression '{0}' did not provide a property name.", lambda));

            return memberExpression.Member.Name;
        }
    }
}
