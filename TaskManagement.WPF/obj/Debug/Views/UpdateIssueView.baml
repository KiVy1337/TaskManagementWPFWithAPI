   M S B A M L   `   `   `  ����M  ITaskManagement.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:TaskManagement.WPF.ViewsTaskManagement.WPF.Views  5      .    (TaskManagement.WPF.Views.UpdateIssueView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      :d2http://schemas.microsoft.com/expression/blend/2008  5      2local&clr-namespace:TaskManagement.WPF.Views  5      $��18(�5	   7   .��5
      � ���	#FFBDBDBD$��0 0 40 0q�6"     � ColumnDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��2��6   $��0��6(   $��0��68   $��Center=�6E   $��40 20 40 20q�6b    #  UpdateIssuesView_UpdateTheIssue#���   6y   �� 5   
   $��1��6   $��2��6!   $��0��65   �� 5   
   $��20 20 20 20q�6   $��0��6+   $��2��6;     IssuesAndTasksView_Title#���  6H   $��Center=�5      �� 5   
   ���	#FFDEDCDC$��20 20 20 20q�6s   $��1��6�   $��2��6�   ��6   �� *IssueToUpdate.Title+ �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5   
   $��20 20 20 20q�6   $��0��6+   $��3��6;     IssuesView_StartDate#���  6H   $��Center=�5      (  "System.Windows.Controls.DatePicker  5   
   $��20 20 20 20q�6k   $��1��6�   $��3��6�      SelectedDateFormat$ Short=�6�      IsTodayHighlighted .    SelectedDate 6   �� *IssueToUpdate.StartDate+$ PropertyChanged=�6�   �� 5   
   $��20 20 20 20q�6   $��0��6+   $��4��6;     IssuesView_Status#���  6H   $��Center=�5       �� 5!   
   $��20 20 20 20q�6�   $��1��6�   $��4��6�   ��6   �� *
StatusList+ �� SelectedValue 66   �� *IssueToUpdate.Status+$ PropertyChanged=�6�   � 5"   
   $��0��6   $��2��6   $��5��63     5#      �� 5$      $��* �6#   �� 5%      $��* �6#   .+�5'      �� $��20q�6a   $��0��6m     Buttons_Update#���  6}   ��6   �� *UpdateIssueCommand+$ PropertyChanged=�6}   �� 5(      $��20q�6d   $��1��6p     Buttons_Cancel#���  6�   ��6   �� *CloseInnerViewCommand+$ PropertyChanged=�6�   5)      5*      5+      