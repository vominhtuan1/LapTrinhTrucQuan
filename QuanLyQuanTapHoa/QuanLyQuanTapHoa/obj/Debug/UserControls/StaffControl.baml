   M S B A M L   `   `   `  ����K  GQuanLyQuanTapHoa, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:QuanLyQuanTapHoa.UserControlsQuanLyQuanTapHoa.UserControls  5      G(clr-namespace:QuanLyQuanTapHoa.ViewModelQuanLyQuanTapHoa.ViewModel  5      0    *QuanLyQuanTapHoa.UserControls.StaffControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      =local+clr-namespace:QuanLyQuanTapHoa.UserControls     5      _ [MaterialDesignThemes.Wpf, Version=4.2.1.2, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5	      h dMicrosoft.Expression.Interactions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bei9http://schemas.microsoft.com/expression/2010/interactions  5
      8	viewmodel(clr-namespace:QuanLyQuanTapHoa.ViewModel  5      -   5          >�Name$  StaffCT��5         StaffVM � DataContext# [  5        MaterialDesignBody#���  5      $��Regular"�5      $��13(�5      &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5         TextRenderingMode$
 Auto=�5        MaterialDesignPaper#���  5        MaterialDesignFont#���  5      .  (System.Windows.Interactivity.Interaction   Triggers 5      D  >Microsoft.Expression.Interactivity.Core.PropertyChangedTrigger  5   
      Binding 6$   �� *
Visibility+ �� ElementName$ StaffCT��.  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *LoadCommand+	   CommandParameter	 6D   �� $ StaffCT��5      .��5      � 
�������.+�5   
   �� 
�������
 �� CornerRadius$
 	20 0 0 20��6#   ._�5       �  � RowDefinitions 5!      �� 5"      $
��auto �6$   64   �� 5#      $
��auto �6$   64   �� 5$      6'   .+�5&      � $��0��6   $��60��6$    � ColumnDefinitions 5'      �� 5(      6-   �� 5)      6-   .+�5+      � $��0��6   $
��Left=�6+   $��45 0 0 0q�6F   .+�5,      ��  �� Orientation$ 
Horizontal=�6%   $
��auto��6>   $
��auto��6K   $��Center=�6Y   $��Center=�6t   .+�5-      �� $��24(�6(   $��Light"�66   $��Danh mục ��6J   6]   �� 5.      $��24(�6(   $
��Bold"�66   $��Nhân viên��6H   6\   5/      50      � 51      $��1��6   53      54      � 55      $��1��6    � 	Resources 56       �� 
TargetType �� Property �� Value �� 
TargetType � Cursor��%v  '��       �� 57      " ��6    .�58      ��  � ��$
 auto��66   �� 59       � ��$ 20(�69   �� 5:       � �� 5;   "   �� 5<   &   " ��66   .R�5=   *   �� ���#8EA0E0$
 10��6F   $	��0 5q�6X   ._�5>   .   � .+�5?   2   �� $��5 5 5 5q�6C   $
 Hand��6T   $��Thêm nhân viên��6b   5@   /   5A   +   5B   '   5D      5E      5F       5G      �� 5H      6-   �� 5I      6-   .+�5K      � $��0��6   $
��Left=�6+    5L      ��%  '��       �� 5M      " ��6$   .�5N   "   ��  � �� 5O   &   �� 5P   *   " ��6:   .R�5Q   .   �� ���#6F89E9$
 10��6J   $��
45 10 0 10q�6\   ._�5R   2   � .+�5S   6   �� $��5q�6G   $
 Hand��6R   5T   3   5U   /   5V   +   5X   #   5Y      5Z      .+�5[      �� $��Thêm nhân viên��5\   !   
�������$
��Auto��6M   ��5[   !   �� *OpenAddStaff+��6B   �� $ 	staffList��5\   \   5]      � 5^      $��1��6   $
��Left=�6+   .+�5_      �� 
�������$
 15��64   $��40 10 100 10q�6F   ._�5`      �� $ 
Horizontal=�6)   $��Center=�6B   $��Center=�6]   .+�5a   "   '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Magnify=�6:   $��25��6I   $��30��6U   ���#747373
������ 6u   �� 5b   "   -   5         ��Name$ 	txbSearch��5b   *   
������ $��25��6V   $	��150��6b   $��0q�6n   $��0 0 10 0q�6�   $
��Left=�5c   *   $��Center=�6L   $��Right=�5d   *   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Tìm kiếm��6F   $��14(�6p   .��6~    5e   &   /  )System.Windows.Interactivity.EventTrigger  5f   *      	EventName$ TextChanged��69   . 5g   .      6D   �� *SearchCommand+	 6f   �� $ StaffCT��5h   +   .��5i   >   5j   #   5k      5l      5m      5n      � 5p      $��2��6   .+�5q      �� $
��20 5q�6!   .+�5r      �  5s      �� 5t   "   $
��0.5* �63   6B   �� 5u   "   $
��1.5* �63   6B   �� 5v   "   $
��0.5* �63   6B   �� 5w   "   $��0.75* �63   6C   �� 5x   "   65   �� 5y   "   $��1* �63   6@   �� 5z   "   $��1* �63   6@   �� 5{   "   $��1* �63   6@   �� 5|   "   65    5~      ��%Q  '��       '�� �     �� 5   "   " ��6(   .�5�   &   ��  � �� �#6F89E96U   �� 5�   &    � ��$ 1q�6H   6T   �� 5�   &    � ��
 �����6T   5�   #   �� 5�   "   " ��6(   .�5�   &   ��  � ��$	 0 2q�6?   6M   �� 5�   &    � ��$ Center=�6L   6]   �� 5�   &    � ��
 �����6S   5�   #   5�      .+�5�      � $��0��6#   .+�5�   "   �� $
 10 0 0 0��6)   ._�5�   &   �� $��Mã số��60   6?   5�   #   5�      � 5�      $��1��6#   .+�5�   "   �� ._�5�   &   �� $��Tên nhân viên��60   6G   5�   #   5�      � 5�      $��2��6#   .+�5�   "   �� ._�5�   &   �� $��Giới tính��60   6C   5�   #   5�      � 5�      $��3��6#   .+�5�   "   �� ._�5�   &   �� $��	Năm sinh��60   6B   5�   #   5�      � 5�      $��4��6#   .+�5�   "   �� ._�5�   &   �� $��Số điện thoại��60   6G   5�   #   5�      � 5�      $��5��6#   .+�5�   "   �� ._�5�   &   �� $��Tên tài khoản��60   6G   5�   #   5�      � 5�      $��6��6#   .+�5�   "   �� ._�5�   &   �� $��Chức vụ��60   6A   5�   #   5�      � 5�      $��7��6#   .+�5�   "   �� ._�5�   &   �� $��Lương��60   6?   5�   #   5�      � 5�      $��8��6#   .+�5�   "   �� $
 0 10 0 0��6)   6C   5�      5�      � 5�       5�      �� 5�   "   $
��163* �63   �� 5�   "   $
��277* �63   .+�5�      �� $	��450��6+   
�������$��Hidden=�6K   $��2��6p   .��5�   "   �� -   5         ��Name$ 	staffList��5�   /   $y�Hidden=�6B    5�   &     5�   *   $ Loaded��69   . 5�   .      6D   �� *LoadCommand+	 6d   �� $ StaffCT��5�   +   ��5�   &   �� 5�   *   .R�5�   .   �� 5�   +   5�   #   5�      � 5�      -   5        $��progressBar��5�   !   $��80��64   $��80��6?    % !MaterialDesignCircularProgressBar#��[ 5�   !    � Value$ 20Q�5�   !    � IsIndeterminate . $��1��6C   $��74.394,185,400,185q�6S   5�      5�      5�      5�      5�      5�      5�      