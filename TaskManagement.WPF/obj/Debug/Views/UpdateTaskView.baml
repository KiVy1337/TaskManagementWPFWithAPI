   M S B A M L   `   `   `  ����M  ITaskManagement.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:TaskManagement.WPF.ViewsTaskManagement.WPF.Views  5      -    'TaskManagement.WPF.Views.UpdateTaskView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:TaskManagement.WPF.Views     5      $��18(�5   6   .��5	      � ���	#FFBDBDBD$��0 0 40 0q�6"     � ColumnDefinitions  5
   
   �� 5      $
��Auto �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��2��6   $��0��6(   $��0��68   $��Center=�6E   $��40,20,38,20q�6b    !  UpdateTasksView_UpdateTheTask#���   6y   �� 5   
   $��1��6   $��2��6!   $��0��65   �� 5   
   $��20 20 20 20q�6   $��0��6+   $��2��6;     IssuesAndTasksView_Title#���  6H   $��Center=�5      �� 5   
   ���	#FFDEDCDC$��20 20 20 20q�6r   $��1��6�   $��2��6�   ��6   �� *TaskToUpdate.Title+ �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5   
   $��20 20 20 20q�6   $��0��6+   $��3��6;     TasksView_Description#���  6H   $��Center=�5      �� 5   
   $��10 10 10 10q�5      $��1��6$   $��3��64    �� TextWrapping$
 Wrap=�5       ���	#FFDEDCDC �� AcceptsReturn . $ �� HorizontalScrollBarVisibility$ Disabled=�5#      " �� VerticalScrollBarVisibility$
 Auto=�5$      $	��200��5%      ��5      �� *TaskToUpdate.Description+$ PropertyChanged=�5%      �� 5'   
   $��20 20 20 20q�6   $��0��6+   $��4��6;     TasksView_Progress#���  6H   $��Center=�5(      � 5)   
   $��1��6   $��4��6     5*      �� 5+      $��* �6#   �� 5,      $
��Auto �6#   .+�5.      �� -   5         ��Name$ ProgressSlider��5.       � Minimum$ 0Q�5/      	 � Maximum$		 100Q�6   $��20q�50      
 �� TickPlacement$
 BottomRight=�51       �� IsSnapToTickEnabled .  �� TickFrequency$ 10Q�53      $��0��54       � Value 5.   -   �� *TaskToUpdate.Progress+$ PropertyChanged=�54      �� 55      $��1��6   $��20(�6(   $��	5 20 5 20q�66   ��6I   ��  �� ElementName$ ProgressSlider�� �� Path$ Value� �� StringFormat$
 {0}%��56      � 57   
   $��0��6   $��2��6   $��5��63     58      �� 59      $��* �6#   �� 5:      $��* �6#   .+�5<      �� $��20q�6`   $��0��6l     Buttons_Update#���  6|   ��6   �� *UpdateTaskCommand+$ PropertyChanged=�6|   �� 5=      $��20q�6d   $��1��6p     Buttons_Cancel#���  6�   ��6   �� *CloseInnerViewCommand+$ PropertyChanged=�6�   5>      5?      5@      