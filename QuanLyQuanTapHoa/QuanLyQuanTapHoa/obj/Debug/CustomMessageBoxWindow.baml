   M S B A M L   `   `   `  ����K  GQuanLyQuanTapHoa, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:QuanLyQuanTapHoaQuanLyQuanTapHoa  5   	   -    'QuanLyQuanTapHoa.CustomMessageBoxWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:QuanLyQuanTapHoa     5   	   _ [MaterialDesignThemes.Wpf, Version=4.2.1.2, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5        $��CustomMessage��5	   	     � Title$  CustomMessageBoxWindow��5
   	   $	��150��6(   $	��300��65    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	    � AllowsTransparency . ��66   Z� .��5      �� -   5         ��Name$ bdMessageBox��5       �� CornerRadius$ 20��6#   
����������#ff9800$��1q�6^   ._�5   
   �  � RowDefinitions 5      �� 5      6"   �� 5      6"   �� 5      6"   .+�5      � $��0��6    � ColumnDefinitions 5      �� 5      $
��auto �6'   66   �� 5      6)   �� 5      $
��auto �6'   66   .+�5      � $��0��6   .+�5      �� -   5         ��Name$	 img��5      $��30��6)   $��40��65   $��/./Images/smile.png��6@   5      � 5      $��1��6   .+�5      �� $��Thông báo��6    $��23(�61   $��Center=�6?   $��Center=�6Z   6y   5      � 5       $��2��6   .+�5!      '  !MaterialDesignThemes.Wpf.PackIcon  -   5        	   Kind$	 WindowClose=�5!   .   $��25��6A   $��25��6M   $
��10 5q�6X   
 � Cursor$

 Hand��6f   6t   5"      5#      � 5$      $��1��6   .+�5%      �� -   5         ��Name$ TextMessage��5%   *   $
��50 0q�6    �� TextAlignment$ Center=�6?   $��Message here!!!��6V   $
��Wrap=�6m   $��Center=�6�   $��14(�6�   6�   5&      � 5'      $��2��6    � 	Resources 5(       �� 
TargetType �� Property �� Value �� 
TargetType �� Padding �� Triggers P� Property P� Value �� 
TargetName��%z  '��       �� 5)      " ��6   .�5*      ��  � �� 5+      �� 5,   "   " ��62   .R�5-   &   �� $ ButtonBorder��6-   ���#ff9800$ 10��6X   $
 20 5q�6j   ._�5.   *   �� $

 Hand��6;   5/   '    50   &   P� 51   *    �@J�IsMouseOver . . �52   .   �� $ ButtonBorder��6[    � �� �#1273de53   +   55   #   57      58      59      .+�5:      �� -   5         ��Name$ OK��5:      $y�Hidden=�6%   $��OK��69   $��Center=�6F   
�������$��Center=�6v   $��14(�6�   6�   �� 5;      -   5        $	 Yes��5;      $y�Hidden=�6&   
�������$��Center=�6M   $��14(�6h   $��
30 0 175 0q�6v   .��5<      �� $	��Có��6    $��Center=�6*   6I   5=      �� 5>      -   5        $ No��5>      $y�Hidden=�6%   
�������$��Center=�6M   $��14(�6h   $��
175 0 30 0q�6v   .��5?      �� $��Không��6    $��Center=�6-   6L   5@      5A      5B      5C      5E      