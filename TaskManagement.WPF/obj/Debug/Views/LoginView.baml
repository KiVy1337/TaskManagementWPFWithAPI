   M S B A M L   `   `   `  ����M  ITaskManagement.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:TaskManagement.WPF.ViewsTaskManagement.WPF.Views  5      (    "TaskManagement.WPF.Views.LoginView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:TaskManagement.WPF.Views     5      $��24(�5	        � 	Resources  5
          BooleanToVisibilityConverter��%   &	        �� 5   
   6'   5      .��5      ��  �� Padding$ 50q�6   $��Center=�6   $��Center=�5        
BaseBrush1#��[ 5      $��3q�5       �� CornerRadius$ 10��6!   ._�5   
   �  � IsSharedSizeScope .  � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    .+�5      �� $��0��6   $��Center=�6%   $��48(�6B   $��Task Manager��6P   � 5      $��1��6   $
��0 20q�6     5      �� 5      $
��auto �6$   �� 5      $
��auto �6$   �� 5      $
��auto �6$   �� 5      $
��auto �6$   .+�5       � $��0��6   $
��0 10q�6$    � ColumnDefinitions 5!      �� 5"       z� SharedSizeGroup$ Label��6+   $
��auto �6C   �� 5#      $	��300 �6+   .+�5%      �� $��0��6    $��Center=�60     LoginRegisterView_Username#���  6K   �� 5&      $��1��6   $��Center=�6.   $��20 0 0 0q�6I   ��6[   �� *
Username+ �� UpdateSourceTrigger$ PropertyChanged=�5'      � 5+      $��1��6   $
��0 10q�6$    5,      �� 5-      $ Label��6+   $
��auto �6C   �� 5.      $	��300 �6+   .+�50      �� $��0��6      LoginRegisterView_Password#���  60   $��Center=�6d   G� 51      -   5         G�Name$ 
pbPassword��51   "   $��Center=�67   $��20 0 0 0q�6R   $��1��6d   52   "   53      �� 55      -   5        $��2��55      $��Center=�6&   $
��0 20q�56      	 �� 	IsDefault	 .   LoginView_Login#���  58      {�59      �� *IsLoginEnabled+$ PropertyChanged=�5:      �� 5<      $��3��6   $��Center=�6&   $��0 10 0 0q�5=      
������   LoginView_To_RegisterView#���  5?      
����� ���5A      �� *ViewRegisterCommand+5C      �� 5E      $��2��6   $��0 10 0 0q�6%   $��Center=�5F      
���  ��
 � DataContext
 5G      �� *ErrorMessageViewModel+��5H      �� *	Message+y�5J      �� *
HasMessage+ �� 	Converter# [  5K      5L      5M      