   M S B A M L   `   `   `  ����M  ITaskManagement.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:TaskManagement.WPF.ControlsTaskManagement.WPF.Controls  5      *    $TaskManagement.WPF.Controls.TaskCard   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:TaskManagement.WPF.Controls     5      -   5          >�Name$  myTask��5      .��5	      �� 
�������$��1q�6%    �� CornerRadius$ 10��69   ._�5
   
   � $��10q�6    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    .+�5      �� $��0��6   $��Center=�6%   $��20(�6C   ��6Q   �� *Title+ �� ElementName$ myTask��6~   �� 5      $��1��6   � 5      $��2��6    5      �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $
��Auto �6$    � ColumnDefinitions 5      �� 5      $��* �6'   �� 5      $��* �6'   .+�5      �� $��18(�6   $��0��6*   $��0��67   $��5q�6G      TasksView_Description#���   6R   �� 5      $��14(�6   $��0��6(   $��1��65   $��5q�6E    �� TextWrapping$
 Wrap=�5        �� AcceptsReturn . $ �� HorizontalScrollBarVisibility$ Disabled=�5"      " �� VerticalScrollBarVisibility$
 Auto=�5#      	 �� 
IsReadOnly	 . $	��100��5%      $	��200��5&      ��5'      �� *Description+$ myTask���� 5)      $��18(�6   $��1��6*   $��0��67   $��5q�6G     TasksView_Progress#���  6R   $��Center=�6~   �� 5*      $��1��6   $��1��6)   $��20(�69   $��5q�6G   ��6R   �� *
Progress+
 �� StringFormat$

 {0}%��$ myTask��5+      5,      5-      5.      