   M S B A M L   `   `   `  ����K  GQuanLyQuanTapHoa, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:QuanLyQuanTapHoa.UserControlsQuanLyQuanTapHoa.UserControls  5      2    ,QuanLyQuanTapHoa.UserControls.SettingControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      =local+clr-namespace:QuanLyQuanTapHoa.UserControls     5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      _ [MaterialDesignThemes.Wpf, Version=4.2.1.2, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      h dMicrosoft.Expression.Interactions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bei9http://schemas.microsoft.com/expression/2010/interactions  5      -   5          >�Name$  	SettingCT��5	         	SettingVM � DataContext# [  5      .  (System.Windows.Interactivity.Interaction   Triggers 5      D  >Microsoft.Expression.Interactivity.Core.PropertyChangedTrigger  5   
      Binding 6$   �� *
Visibility+ �� ElementName$ 	SettingCT��.  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *LoadCommand+   CommandParameter 6D   �� $ 	SettingCT��5      .��5      � .+�5   
   �� 
������� �� CornerRadius$ 	20 0 0 20��6#   ._�5      � 	 � RowDefinitions	 5      �� 5      $��60 �6$   62   �� 5      $
��auto �6$   64   �� 5      6&   .+�5      � $��0��6   $
��Left=�6$   $��45 0 0 0q�6?   .+�5      �� 
 �� Orientation$
 
Horizontal=�6!   $
��auto��6:   $
��auto��6G   $��Center=�6U   $��Center=�6p   .+�5      �� $��24(�6$   $��Light"�62   $��Danh mục ��6F   6Y   �� 5      $��24(�6$   $
��Bold"�62   $��Cài đặt��6D   6V   5      5       � 5!      $��1��6    � ColumnDefinitions 5"      �� 5#      6-   �� 5$      6-   .+�5&      � $��0��6   $
��Left=�6+    � 	Resources 5'       �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� Padding � Cursor �� Triggers P� Property P� Value �� 
TargetName��%�  '��       �� 5(      " ��6$   .�5)   "   ��  � �� 5*   &   �� 5+   *   " ��6:   .R�5,   .   �� $ ButtonBorder��65   ���#6F89E9$ 10��6`   $��45 0 0 0q�6r   $	 4 2q�6�   ._�5-   2   �� $��5q�6C   $
 Hand��6N   5.   /    5/   .   P� 50   2    �@J�IsMouseOver . . �51   6   �� $ ButtonBorder��6c    � �� �#ff980052   3   54   +   56   #   57      58      .+�59      �� $��Center=�6!   $��Center=�6>   $!��Thêm sản phẩm từ kho��5:   !   
���������59   Y   �� *OpenAddProduct+��6|   �� $ productList��5:   U   5;      � 5<      $��1��6   $��Center=�6+   .+�5=      �� 
�������$ 15��64   $��30 15q�6F   ._�5>      �� $
 
Horizontal=�6)   $��Center=�6B   $��Center=�6]   .+�5?   "   '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Magnify=�6:   $��25��6I   $��30��6U   ���#747373
������ 6u   �� 5@   "   -   5        $��	SearchBox��5@   *   
������ $��25��6T   $	��200��6`   $��0q�6l   $��0 0 10 0q�6�   $
��Left=�5A   *   $��Center=�6L   $��Right=�5B   *   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Tìm kiếm��6F   $��13(�6p   .��6~    5C   &   /  )System.Windows.Interactivity.EventTrigger  5D   *      	EventName$ TextChanged��69   . 5E   .      6D   �� *SearchCommand+ 6f   �� $ 	SettingCT��5F   +   .��5G   >   5H   #   5I      5J      5K      5L      � 5M      $��2��6   .+�5N      �� $
��20 5q�6!   .+�5O      �  5P      �� 5Q   "   $
��0.5* �63   6B   �� 5R   "   $
��2.5* �63   6B   �� 5S   "   65   �� 5T   "   65   �� 5U   "   65   �� 5V   "   $
��1.5* �63   6B   �� 5W   "   65    5Y      ��%Q  '��       '�� �     �� 5Z   "   " ��6(   .�5[   &   ��  � �� �#6F89E96U   �� 5\   &    � ��$ 1q�6H   6T   �� 5]   &    � ��
 �����6T   5^   #   �� 5_   "   " ��6(   .�5`   &   ��  � ��$	 0 2q�6?   6M   �� 5a   &    � ��$ Center=�6L   6]   �� 5b   &    � ��
 �����6S   5c   #   5d      .+�5e      � $��0��6#   .+�5f   "   �� $ 10 0 0 0��6)   ._�5g   &   �� $��Mã số��60   6?   5h   #   5i      � 5j      $��1��6#   .+�5k   "   �� ._�5l   &   �� $��Tên sản phẩm��60   6F   5m   #   5n      � 5o      $��2��6#   .+�5p   "   �� ._�5q   &   �� $��Số lượng bày bán��60   6J   5r   #   5s      � 5t      $��3��6#   .+�5u   "   �� ._�5v   &   �� $��
Đơn vị��60   6@   5w   #   5x      � 5y      $��4��6#   .+�5z   "   �� ._�5{   &   �� $��Loại��60   6>   5|   #   5}      � 5~      $��5��6#   .+�5   "   �� ._�5�   &   �� $��	Giá bán��60   6A   5�   #   5�      � 5�      $��6��6#   .+�5�   "   �� $ 0 10 0 0��6)   6C   5�      5�      � 5�      .+�5�      �� $	��450��6+   
�������$
��Auto=�6K   .��5�   "   �� -   5         ��Name$ productList��5�   /   $y�Hidden=�6D   ��5�   &   �� 5�   *   .R�5�   .   �� 5�   +   5�   #   5�      � 5�      -   5        $��progressBar��5�   !   $��80��64   $��80��6?    % !MaterialDesignCircularProgressBar#��[ 5�   !    � Value$ 0Q�5�   !    � IsIndeterminate . 6+   5�      5�      5�      5�      5�      5�      5�      