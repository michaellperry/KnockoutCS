/**********************************************************************
 * 
 * Update Controls .NET
 * Copyright 2010 Michael L Perry
 * MIT License
 * 
 * http://updatecontrols.net
 * http://updatecontrols.codeplex.com/
 * 
 **********************************************************************/

using System;
using System.Windows.Threading;

namespace KnockoutCS.Library.Impl
{
    public interface IObjectInstance
    {
        ClassInstance ClassInstance { get; }
        dynamic Model { get; }
        object ViewModel { get; }
        Dispatcher Dispatcher { get; }
        ObjectProperty LookupProperty(ClassProperty classProperty);
        void FirePropertyChanged(string name);
    }
}
