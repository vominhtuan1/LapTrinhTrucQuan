   M S B A M L   `   `   `  ����K  GQuanLyQuanTapHoa, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:QuanLyQuanTapHoa.UserControlsQuanLyQuanTapHoa.UserControls  5      3    -QuanLyQuanTapHoa.UserControls.DiscountControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      =local+clr-namespace:QuanLyQuanTapHoa.UserControls     5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      _ [MaterialDesignThemes.Wpf, Version=4.2.1.2, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	      h dMicrosoft.Expression.Interactions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bei9http://schemas.microsoft.com/expression/2010/interactions  5      -   5          >�Name$  
DiscountCT��5
         
DiscountVM � DataContext# [  5      .  (System.Windows.Interactivity.Interaction   Triggers 5      D  >Microsoft.Expression.Interactivity.Core.PropertyChangedTrigger  5   
      Binding 6$   �� *
Visibility+ �� ElementName$ 
DiscountCT��.  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *LoadCommand+   CommandParameter 6D   �� $ 
DiscountCT��5      .��5      � .+�5   
   �� 
������� �� CornerRadius$ 	20 0 0 20��6#   ._�5      � 	 � RowDefinitions	 5      �� 5      $��60 �6$   62   �� 5      $
��auto �6$   64   �� 5      6&   .+�5      � $��0��6   $
��Left=�6$   $��45 0 0 0q�6?   .+�5      �� 
 �� Orientation$
 
Horizontal=�6!   $
��auto��6:   $
��auto��6G   $��Center=�6U   $��Center=�6p   .+�5      �� $��24(�6$   $��Light"�62   $��Danh mục ��6F   6Y   �� 5      $��26(�6$   $
��Bold"�62   $��Mã giảm giá��6D   6Z   5       5!      � 5"      $��1��6   $
��Left=�6$    � 	Resources 5#       �� 
TargetType �� Property �� Value �� 
TargetType �� Padding � Cursor��%2  '��       �� 5$      " ��6    .�5%      ��  � �� 5&   "   �� 5'   &   " ��66   .R�5(   *   �� ���#6F89E9$ 10��6F   $��
45 10 0 10q�6X   $��Center=�6l   $
 10 2q�6�   ._�5)   .   � .+�5*   2   �� $��5q�6C   $
 Hand��6N   5+   /   5,   +   5-   '   5/      50      51      .+�52      �� $��Center=�6   $��Thêm mã giảm giá��53   !   
�������$	��190��6P   $��55,-10,-40,10q�6\   ��52   9   �� *OpenAddDiscount+��6]   �� $ discountList��53   u   54      � 55      $��2��6   .+�56      �� $
��20 5q�6!   .+�57      �  � ColumnDefinitions 58      �� 59   "   $
��0.5* �63   6B   �� 5:   "   $
��1.5* �63   6B   �� 5;   "   $��2* �63   6@   �� 5<   "   $��2* �63   6@   �� 5=   "   $
��1.5* �63   6B   �� 5>   "   $
��1.5* �63   6B   �� 5?   "   66    5A      ��%Q  '��       '�� �     �� 5B   "   " ��6(   .�5C   &   ��  � �� �#6F89E96U   �� 5D   &    � ��$ 1q�6H   6T   �� 5E   &    � ��
 �����6T   5F   #   �� 5G   "   " ��6(   .�5H   &   ��  � ��$	 0 2q�6?   6M   �� 5I   &    � ��$ Center=�6L   6]   �� 5J   &    � ��
 �����6S   5K   #   5L      .+�5N      � $��0��6#   .+�5O   "   �� $ 10 0 0 0��6)   ._�5P   &   �� $	��STT��60   6=   5Q   #   5R      � 5S      $��1��6#   .+�5T   "   �� ._�5U   &   �� $��Mã giảm giá��60   6E   5V   #   5W      � 5X      $��2��6#   .+�5Y   "   �� ._�5Z   &   �� $��Ngày bắt đầu��60   6F   5[   #   5\      � 5]      $��3��6#   .+�5^   "   �� ._�5_   &   �� $��Ngày kết thúc��61   6H   5`   #   5a      � 5b      $��4��6#   .+�5c   "   �� ._�5d   &   �� $��Số tiền giảm��60   6F   5e   #   5f      � 5g      $��5��6#   .+�5h   "   �� ._�5i   &   �� $��Đơn hàng từ��61   6F   5j   #   5k      � 5m      $��6��6#   .+�5n   "   �� $ 0 10 0 0��6)   6C   5o      5p      � 5q      .+�5r      �� $	��450��6+   
�������$��Hidden=�6K   .��5s   "   �� -   5         ��Name$ discountList��5s   /   $y�Hidden=�6E    5t   &   /  )System.Windows.Interactivity.EventTrigger  5u   *      	EventName$ Loaded��69   . 5v   .      6D   �� *LoadCommand+ 6d   �� $ 
DiscountCT��5w   +   ��5y   &   �� 5z   *   .R�5{   .   �� 5|   +   5~   #   5      � 5�      -   5        $��progressBar��5�   !   $��80��64   $��80��6?    % !MaterialDesignCircularProgressBar#��[ 5�   !    � Value$ 20Q�5�   !    � IsIndeterminate . 6,   5�      5�      5�      5�      5�      5�      5�      