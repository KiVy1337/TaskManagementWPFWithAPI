   M S B A M L   `   `   `  ����M  ITaskManagement.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:TaskManagement.WPF.ViewsTaskManagement.WPF.Views  5      1    +TaskManagement.WPF.Views.ChangeLanguageView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:TaskManagement.WPF.Views     5      $��24(�5   7     � 	Resources  5	          BooleanToVisibilityConverter��%   &	        �� 5
   
   6'   5      .��5      ��  �� Padding$ 50q�6   $��Center=�6   $��Center=�5        
BaseBrush1#��[ 5      $��3q�5       �� CornerRadius$ 10��6!   ._�5   
   �  � IsSharedSizeScope .  � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $
��auto �6    .+�5      �� $��0��6   $��Center=�6%   $��48(�6B   $��Task Manager��6P   � 5      $��1��6   $
��0 20q�6     5      �� 5      $
��auto �6$   �� 5      $
��auto �6$   .+�5      � $��0��6   $
��0 10q�6$    � ColumnDefinitions 5      �� 5       z� SharedSizeGroup$ Label��6+   $
��auto �6C   �� 5      $	��300 �6+   .+�5!      �� $��0��6    $��Center=�60     ChangeLanguageView_Language#���  6K   �� 5"      -   5         ��Name$ LanguagesCB��5"      $��1��64   $��20 20 20 20q�6�   ��6D   �� *	Languages+ �� SelectedValue 6f   �� *
Language+	 �� UpdateSourceTrigger$	 PropertyChanged=�6�   5#      � 5$      $��1��6    5%      �� 5&      $��* �6+   �� 5'      $
��Auto �6+   .+�5)      �� $��0��6   $��Center=�6-   $
��0 20q�5*      
 �� 	IsDefault
 .   ChangeLanguageView_Accept#���  5,      ��5-      �� *ChangeLanguageCommand+�� 5.      $��1��6     ChangeLanguageView_Cancel#���  6-   $��0 0 60 0q�6c   ��5/      �� *ViewLoginCommand+y�50      �� *IsVisibilityNeeded+ �� 	Converter# [  51      52      54      55      56      