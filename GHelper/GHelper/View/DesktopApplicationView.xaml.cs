﻿using System;
using GHelper.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

namespace GHelper.View
{
    public partial class DesktopApplicationView : UserControl, IApplicationView
    {
        
        public static readonly DependencyProperty ApplicationProperty = DependencyProperty.Register(
             nameof (Application),
             typeof (ApplicationViewModel),
             typeof (DesktopApplicationView),
             new PropertyMetadata(null));
        
        public ApplicationViewModel Application
        {
            get { return (ApplicationViewModel) GetValue(ApplicationProperty); }
            set
            {
                SetValue(ApplicationProperty, value);
                ResetAppearance();
                DetermineNameViewStyle();
            }
        }
        
        public GHubRecordViewModel GHubRecord
        {
            get { return Application; }
        }

        public DesktopApplicationView(Action saveFunction, Action<GHubRecordViewModel> deleteFunction)
        {
            this.InitializeComponent();
            RegisterForSaveNotification(saveFunction);
            RegisterForDeleteNotification(deleteFunction);
        }
        
        public void RegisterForSaveNotification(Action saveFunction)
        {
            RecordViewControls.RegisterForSaveNotification(saveFunction);
        }
        
        public void RegisterForDeleteNotification(Action<GHubRecordViewModel> deleteFunction)
        {
            Action delete = () =>
            {
                deleteFunction(this.GHubRecord);
            };
		    
            RecordViewControls.RegisterForDeleteNotification(delete);
        }
        
        void RecordView.SendRecordChangedNotification()
        {
            RecordViewControls.NotifyOfUserChange();
        }
        
        protected void HandleNameChange(object sender, RoutedEventArgs routedEventInfo)
        {
            RecordView.ChangeName(this, sender);
        }

        protected void HandleNameChange(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs eventInfo)
        {
            RecordView.ChangeName(this, eventInfo);
        }

        public void ResetAppearance()
        {
            RecordViewControls.ResetAppearance();
        }

        public void DetermineNameViewStyle()
        {
            IApplicationView.DetermineNameViewStyle(Application, NameTextBox);
        }

    }
}
